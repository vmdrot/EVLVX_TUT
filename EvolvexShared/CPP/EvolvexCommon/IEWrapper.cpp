#include "evolvex_common_lib.h"
#include "IEWrapper.h"
#include "SystemInfoGatherer.h"
#include "ProcessFilterer.h"

CIEWrapper::CIEWrapper(void): m_sIEFullPath(""),IE_EXE_NAME("iexplore.exe"), 
                              COMPLETE_STATE("complete"), 
                              m_bHiddenOperation(false), m_bInnerHTMLValid(false), 
                              m_bInnerHTMLLcValid(false),
                              m_bLastRefreshTakenInited(false)
{
    m_dwProcessId =0L;
    m_hwndIE = (HWND)NULL;
    m_pBrowserItfc = NULL;
    m_pDoc = NULL;
    m_hwndIESrv = NULL;
}

CIEWrapper::~CIEWrapper(void)
{
}

bool CIEWrapper::IsRequestComplete()
{
    LOG_STARTED();
    const int MAX_TIMEOUT = 5000L;
    const int SLEEP_LENGTH = 50L;
    int curr_timeout = 0;
    bool rslt = false;
    CComBSTR ready_state;
    
    while(curr_timeout < MAX_TIMEOUT)
    {
        /*
         * I've tried to measure how much time the get_readyState invocation consumes;
         * it turned out to be less than an ms, so I the diff. is neglectable and 
         * I've dispensed therewith, since it bloated the log.
         * If somebody (later) starts to believe he/she needs this logging back,
         * you know what to do.
         */
        //LOG_DEBUG("%s::before get_readyState", __FUNCTION__);
        Sleep(SLEEP_LENGTH);
        curr_timeout += SLEEP_LENGTH;
        HRESULT hr = m_pDoc->get_readyState(&ready_state);
        //LOG_DEBUG("%s::after get_readyState", __FUNCTION__);
        if(FAILED(hr))
        {
            helper::LogExplainError(hr,__FUNCTION__);
        }
        rslt = (SUCCEEDED(hr) && ready_state == COMPLETE_STATE);
        if(rslt)
            break;
    }
    if(!rslt)
    {
        Sleep(SLEEP_LENGTH);
        HRESULT hr = m_pDoc->get_readyState(&ready_state);
        if(FAILED(hr))
        {
            helper::LogExplainError(hr,__FUNCTION__);
        }
        rslt = (SUCCEEDED(hr) && ready_state == COMPLETE_STATE);
    }
    LOG_DEBUG("%s::(ready_state == COMPLETE_STATE) = '%s'", __FUNCTION__, (rslt ? "true" : "false"));
    LOG_FINISHED();
    if(rslt)
        resetInnerHtmlValidity();
    return rslt;
}

DWORD CIEWrapper::LaunchIE(const std::string& url)
{
    PROCESS_INFORMATION ps_info;
    STARTUPINFO si;
    ZeroMemory(&si, sizeof(si));
    ZeroMemory(&ps_info, sizeof(ps_info));
    std::string sIEFullPath;
    GetIEFullPath(sIEFullPath);
	OSVERSIONINFOA oi;
	oi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOA);
	if(!CSystemInfoGatherer::GetOsVersion(&oi))
		return NULL;
	if(oi.dwMajorVersion <6)
	{
		if(CreateProcess((LPCWSTR)(wchar_t*)_bstr_t(sIEFullPath.c_str()),(LPWSTR)(wchar_t*)_bstr_t(url.c_str()),NULL,NULL,TRUE,DETACHED_PROCESS,NULL,NULL,&si,&ps_info))
			return ps_info.dwProcessId;
	}
	else
	{
		HANDLE hImpToken;
		HANDLE hProcessToken;
		if(!OpenProcessToken(GetCurrentProcess(), MAXIMUM_ALLOWED, &hProcessToken))
		{
			helper::LogExplainError(GetLastError(), __FUNCTION__);
			return NULL;
		}
		SECURITY_ATTRIBUTES sa;
	    sa.bInheritHandle  = FALSE;
		sa.nLength = sizeof(sa);
	    sa.lpSecurityDescriptor = NULL;
		//CAcl acl;

		//acl.Allow(_T("EveryOne"));
		//acl.Deny(_T("stan"));

		//// Now create a SECURITY ATTRIBUTES object and

		//// set the DACL to the ACL we just created.

		//CAccessAttributes aa(&acl);

		if(!DuplicateTokenEx(hProcessToken, GENERIC_ALL, &sa, SECURITY_IMPERSONATION_LEVEL::SecurityImpersonation, TOKEN_TYPE::TokenPrimary, &hImpToken))
		{
			helper::LogExplainError(GetLastError(), __FUNCTION__);
			CloseHandle(hProcessToken);
			return NULL;
		}
		CloseHandle(hProcessToken);
		bool bIsUACOn;
		if(!CSystemInfoGatherer::IsUACOn(&bIsUACOn))
			bIsUACOn = true;
		if(!bIsUACOn)
		{
			if(CreateProcessAsUser(hImpToken,  
								   (LPCWSTR)(wchar_t*)_bstr_t(sIEFullPath.c_str()),
								   (LPWSTR)(wchar_t*)_bstr_t(url.c_str()),
									&sa,
									&sa,
									TRUE,
									DETACHED_PROCESS,
									NULL,
									NULL,
									&si,
									&ps_info))
			{
				CloseHandle(hImpToken);
				return ps_info.dwProcessId;
			}
		}
		
		CProcessFilterer fltrPrior("iexplore.exe");
		if(!CreateProcessAsUser(hImpToken,  
							   (LPCWSTR)(wchar_t*)_bstr_t(sIEFullPath.c_str()),
							   (LPWSTR)(wchar_t*)_bstr_t(url.c_str()),
								&sa,
								&sa,
								TRUE,
								DETACHED_PROCESS,
								NULL,
								NULL,
								&si,
								&ps_info))
		{
			CloseHandle(hImpToken);
			return -1;
		}
		DWORD rslt = 0L;
		DWORD timeSlept = 0L;
		const DWORD PS_SLEEP_LENGTH = 300L;
		while(true)
		{
			CProcessFilterer fltNew("iexplore.exe");
			rslt = fltNew.DetectNew(fltrPrior.get_filtered_process_ids());
			if(rslt != 0L && rslt != ps_info.dwProcessId)
				break;
			Sleep(PS_SLEEP_LENGTH);
			timeSlept += PS_SLEEP_LENGTH;
		}
		CloseHandle(hImpToken);
		return rslt;
	}
    return NULL;
}

bool CIEWrapper::GetIEFullPath(std::string& target)
{
    if(m_sIEFullPath.length() > 0)
    {
        target = m_sIEFullPath;
        return true;
    }
    char bfr[MAX_PATH] = { 0 };
    //todo - remove hard-coded default ie path
    sprintf(bfr,"%s%s", "C:\\Program Files\\Internet Explorer\\", IE_EXE_NAME.c_str());

    m_sIEFullPath =  bfr;
    target = m_sIEFullPath;
    return true;

}

DWORD CIEWrapper::GetProcessIDByHwnd(HWND wnd)
{
    LOG_STARTED();
    CWindow cwnd;
    cwnd.Attach(wnd);
    DWORD rslt = cwnd.GetWindowProcessID();
    LOG_FINISHED();
    return rslt;
}

HWND CIEWrapper::MapWindow2ProcessID(DWORD process_id, const std::string& wnd_class, const std::string& wnd_caption)
{
    LOG_STARTED();
    HWND rslt = NULL;
    while(true)
    {
        rslt = FindWindowExA(NULL,rslt,wnd_class.length() > 0 ? (LPCSTR)wnd_class.c_str() : NULL, wnd_caption.length() > 0 ? (LPCSTR)wnd_caption.c_str() : NULL);
        if(rslt == NULL)
            return rslt;
        if(process_id == GetProcessIDByHwnd(rslt))
            return rslt;
    }
    LOG_FINISHED();
    return rslt;
}

HWND CIEWrapper::GetIEServerWndByFrameWnd(HWND iefrm)
{
    LOG_STARTED();
	const std::string SHELL_DOCOBJ_VIEW_CLASS  = "Shell DocObject View";
	HWND shdocvw = FindWindowExA(iefrm, NULL, SHELL_DOCOBJ_VIEW_CLASS.c_str(), NULL);
    if(NULL == shdocvw)
	{
		//probably, IE7 case
		HWND tabWnd = FindWindowExA(iefrm, NULL, "TabWindowClass", NULL);
		if(tabWnd == NULL)
			return NULL;
		shdocvw = FindWindowExA(tabWnd, NULL, SHELL_DOCOBJ_VIEW_CLASS.c_str(), NULL);
		if(shdocvw == NULL)
			return NULL;
	}
    HWND rslt = FindWindowExA(shdocvw, NULL, "Internet Explorer_Server", NULL);
    LOG_FINISHED();
    return rslt;
}

int CIEWrapper::GetIEFromHWND(HWND ie, IWebBrowser2** p_browser, IHTMLDocument2** p_doc)
{
    LOG_STARTED();
    int ret = 0;
    HRESULT hr;
    LRESULT lRes;
    int iMsg = RegisterWindowMessageA("WM_HTML_GETOBJECT");
    LOG_DEBUG("%s::iMsg = %d", __FUNCTION__,iMsg);
    SendMessageTimeoutA(ie,iMsg,0,0,SMTO_ABORTIFHUNG, 1000,(PDWORD_PTR)&lRes);
    LOG_DEBUG("%s::lRes = %d", __FUNCTION__,lRes);
    hr = ObjectFromLresult(lRes,IID_IHTMLDocument2, 0, (void**)p_doc);
    LOG_DEBUG("%s::hr = %d", __FUNCTION__,hr);
    if(SUCCEEDED(hr))
    {
        ret++;
        CComPtr<IHTMLWindow2> p_html_wnd(NULL);
        hr = (*p_doc)->get_parentWindow(&(p_html_wnd.p));
        LOG_DEBUG("%s::(*p_doc)->get_parentWindow = %d", __FUNCTION__,hr);
        if(FAILED(hr))
            return ret;
            //helper::LogExplainError(hr, __FUNCTION__);
        if(SUCCEEDED(hr))
        {
            CComPtr<IServiceProvider> p_svc_pvd(NULL);
            hr = (p_html_wnd.p)->QueryInterface(IID_IServiceProvider,(void**)&(p_svc_pvd.p));
            LOG_DEBUG("%s::(p_html_wnd.p)->QueryInterface(IID_IServiceProvider = %d", __FUNCTION__,hr);
            if(FAILED(hr))
                helper::LogExplainError(hr, __FUNCTION__);
            if(SUCCEEDED(hr))
                hr = p_svc_pvd.p->QueryService(IID_IWebBrowserApp,IID_IWebBrowser2, (void**)p_browser);
            if(FAILED(hr))
                helper::LogExplainError(hr, __FUNCTION__);
            LOG_DEBUG("%s::p_svc_pvd.p->QueryService(IID_IWebBrowserApp,IID_IWebBrowser2 = %d", __FUNCTION__,hr);
            if(SUCCEEDED(hr)) ret++;
        }
    }
    LOG_FINISHED();
    return ret;
}

bool CIEWrapper::Navigate(const std::string& url)
{
    LOG_STARTED();
    resetInnerHtmlValidity();
    bool bInited = IsInitialized();
    if(!bInited )
    {
        bInited = DoInitialize(url);
        if(!bInited)
            return false;
        //std::string href;
        //GetLocation(href);
        //if(href == url)
        //    return true;
    }
    _variant_t vUrl, flags, target, post_data, headers;
    vUrl.SetString(url.c_str());
    LOG_DEBUG("%s - before p_browser->Navigate2", __FUNCTION__);
    HRESULT hr = m_pBrowserItfc->Navigate2(&vUrl, &flags, &target,&post_data, &headers);
    //HRESULT hr = m_pBrowserItfc->Navigate(&vUrl, &flags, &target,&post_data, &headers);
    if(FAILED(hr))
        return false;
    LOG_FINISHED();
    return IsRequestComplete();
    
}
//bool CIEWrapper::SetEditText(const std::string& editId, const std::string& text)
//{
//    char bfr[1024] = { 0 };
//    sprintf(bfr,"javascript: document.getElementById('%s').value = '%s';",editId.c_str(), text.c_str());
//    return Navigate(std::string(bfr));
//}

bool CIEWrapper::SetEditText(const std::string& editId, const std::string& text, const std::string& frmId)
{
    LOG_STARTED();
    if(editId.length() == 0 || frmId.length() ==0)
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    CComPtr<IHTMLFormElement> pForm;
    if(!GetForm(&pForm,frmId))
        return false;
    CComPtr<IHTMLInputTextElement> pEdit;
    if(!GetFormElement<IHTMLInputTextElement>(pForm,&pEdit,editId,IID_IHTMLInputTextElement))
        return false;
    //_variant_t vtText;
    //vtText.SetString(text.c_str());
    HRESULT hr = pEdit->put_value(_bstr_t(text.c_str()).GetBSTR());
    if(FAILED(hr))
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    LOG_FINISHED();
    return true;
}

#if USE_OLD_PUSHBUTTON
bool CIEWrapper::PushButton(const std::string& buttonId)
{
    char bfr[1024] = { 0 };
    sprintf(bfr,"javascript: document.getElementById('%s').click();",buttonId.c_str());
    return Navigate(std::string(bfr));
}
#else
//bool CIEWrapper::PushButton(const std::string& buttonId)
//{
//    //m_pDoc->get
//    return false;
//}
#endif
bool CIEWrapper::IsInitialized()
{
    return (m_dwProcessId != 0 && m_pBrowserItfc != NULL && m_pDoc != NULL);
}

bool CIEWrapper::DoInitialize(const std::string& initUrl)
{
    LOG_STARTED();
    resetInnerHtmlValidity();
    const int MAX_TIMEOUT = 5000L;
    int SLEEP_LENGTH = 1000L;
    int curr_timeout = 0;

    m_dwProcessId = LaunchIE(initUrl);
    if(m_dwProcessId == 0L)
        return false;
    
    while(curr_timeout < MAX_TIMEOUT)
    {
        Sleep(SLEEP_LENGTH);
        curr_timeout += SLEEP_LENGTH;
        m_hwndIE = GetIEWindowByProcessID(m_dwProcessId);
        LOG_DEBUG("%s(%d)::m_hwndIE = %x", __FUNCTION__, __LINE__, m_hwndIE);
        if(m_hwndIE != NULL)
            break;
    }
    if(m_hwndIE == NULL)
        return false;
    this->ApplyVisibilityOperationMode();
    curr_timeout = 0;
    SLEEP_LENGTH = 50L;
    HWND m_hwndIESrv = (HWND)NULL;
    while(curr_timeout < MAX_TIMEOUT)
    {
        m_hwndIESrv = GetIEServerWndByFrameWnd(m_hwndIE);
        if(m_hwndIESrv != NULL)
            break;
        Sleep(SLEEP_LENGTH);
        curr_timeout += SLEEP_LENGTH;
    }
    if(m_hwndIESrv == NULL)
        return false;

    int succeeded_iftcs = GetIEFromHWND(m_hwndIESrv,&m_pBrowserItfc,&m_pDoc);
    if(succeeded_iftcs == 0)
        return false;
    if(m_pDoc == NULL)
        return false;
    LOG_FINISHED();
    return IsRequestComplete();
}

HWND CIEWrapper::GetIEWindowByProcessID(DWORD process_id)
{
    return MapWindow2ProcessID(process_id, IE_WND_CLASS);
}

bool CIEWrapper::Refresh(bool bWaitUntilComplete)
{
    LOG_STARTED();
    DWORD tmStart= GetTickCount();
    resetInnerHtmlValidity();
    _variant_t vtLevel;
    vtLevel.intVal = (int)RefreshConstants::REFRESH_NORMAL;
    vtLevel.vt = VT_I4;
    HRESULT hr = m_pBrowserItfc->Refresh2(&vtLevel);
    if(FAILED(hr))
    {
        LOG_FINISHED();
        return false;
    }
    if(!bWaitUntilComplete)
    {
        LOG_FINISHED();
        return true;
    }
    bool rslt = IsRequestComplete();
    if(rslt)
        DoRecordLastRefreshStats(tmStart);
    LOG_FINISHED();
    return rslt;
}

//void CIEWrapper::EvalJS(const std::string& sJs)
//{
//    m_pDoc->get_e
//    //IHTMLElement2* pBody;
//    //pBody->
//    //HRESULT hr = m_pDoc->get_body(&(pBody.p));
//    //if(FAILED(hr))
//    //    return;
//    //pBody->
//}


bool CIEWrapper::SubmitForm(const std::string& formId, const std::string& submitBtnId, bool bSimulate)
{
    LOG_STARTED();
    resetInnerHtmlValidity();
    CComPtr<IHTMLFormElement> pForm;
    HRESULT hr;
    if(!GetForm(&pForm,formId))
        return false;
    if(submitBtnId.length() == 0)
    {
        hr = pForm->submit();
        if(FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        LOG_FINISHED();
        return true;
    }
    #pragma region todo - replace with GetFormElement() invocation
    CComPtr<IHTMLInputButtonElement> pBtn;
    if(!GetFormElement<IHTMLInputButtonElement>(pForm,&pBtn,submitBtnId,IID_IHTMLInputButtonElement))
        return false;
    //hr = pBtn->put_disabled(_variant_t(true));
    CComPtr<IHTMLElement> pBtnElem;
    if(!GetFormElement<IHTMLElement>(pForm,&pBtnElem,submitBtnId,IID_IHTMLElement))
        return false;
    if(bSimulate)
    {
        hr = pBtn->put_disabled(_variant_t(true));
        LOG_INFO("%s(%d)::running in simulate mode, just disabling the form's submit button.", __FUNCTION__, __LINE__);
    }
    else
        hr = pBtnElem->click();
    if(FAILED(hr))
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    LOG_FINISHED();
    return true;
}

bool CIEWrapper::GetForm(IHTMLFormElement** pForm, const std::string& formId)
{
    LOG_STARTED();
    CComPtr<IHTMLElementCollection> pForms;
    HRESULT hr = m_pDoc->get_forms(&pForms);
    if(FAILED(hr))
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    long lFormsCnt;
    hr = pForms->get_length(&lFormsCnt);
    if(FAILED(hr))
        LOG_DEBUG("%s::failed to obtain pForms->get_length", __FUNCTION__);
    else
        LOG_DEBUG("%s::lFormsCnt = %d", __FUNCTION__, lFormsCnt);
    CComPtr<IDispatch> pDispForm;
    //CComPtr<IHTMLFormElement> pForm;
    _variant_t id;
    id.SetString(formId.c_str());
    _variant_t idx;
    hr = pForms->item(id,idx,&pDispForm);
    if(FAILED(hr))
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    hr = pDispForm->QueryInterface(IID_IHTMLFormElement,(void**)pForm);

    if(FAILED(hr))
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    LOG_FINISHED();
    return true;
}

bool CIEWrapper::GetBodyInnerHTML(std::string& target)
{
    LOG_STARTED();
    if(m_pDoc == NULL)
        return false;
    CComPtr<IHTMLElement> spBody;
    HRESULT hr = m_pDoc->get_body(&spBody);
    if(FAILED(hr))
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    CComBSTR strHTML;
    
    hr = spBody->get_innerHTML(&strHTML);
    if(FAILED(hr))
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    _bstr_t strWrp(strHTML.m_str);
    target = (char*)strWrp;
    LOG_FINISHED();
    return true;
}

bool CIEWrapper::GetLocation(std::string& href)
{
    LOG_STARTED();
    CComPtr<IHTMLLocation> pLocation;
    HRESULT hr = m_pDoc->get_location(&pLocation);
    if(FAILED(hr))
    {
        helper::LogExplainError(hr,__FUNCTION__);
        return false;
    }
    CComBSTR bstrLoc;
    pLocation->toString(&bstrLoc);
    href = (char*)_bstr_t(bstrLoc.m_str);
    LOG_FINISHED();
    return true;
}
void CIEWrapper::CloseBrowser()
{
    helper::terminate_process(m_dwProcessId);
}

void CIEWrapper::set_HiddenOperationMode(bool bHidden)
{
    bool bChanged = (m_bHiddenOperation != bHidden);
    m_bHiddenOperation = bHidden;
    if(bChanged)
        ApplyVisibilityOperationMode();
}

void CIEWrapper::ApplyVisibilityOperationMode()
{
    if(this->m_hwndIE == NULL)
        return;
    ShowWindow(this->m_hwndIE, this->m_bHiddenOperation ? SW_HIDE : SW_SHOW);
}

const std::string& CIEWrapper::get_InnerHTMLLower()
{
    if(!m_bInnerHTMLLcValid)
    {
        str_lowercase<std::string>(get_InnerHTML(),m_sInnerHTMLLc);
        m_bInnerHTMLLcValid = true;
    }
    return m_sInnerHTMLLc;
}

const std::string& CIEWrapper::get_InnerHTML()
{
    if(!m_bInnerHTMLValid)
    {
        if(!GetBodyInnerHTML(m_sInnerHTML))
            m_sInnerHTML = "";
        else
            m_bInnerHTMLValid = true;
        
    }
    return m_sInnerHTML;
}

void CIEWrapper::set_InnerHTML(const std::string& val)
{
    m_sInnerHTML = val;
    m_bInnerHTMLValid = true;
    m_bInnerHTMLLcValid = false;
}


void CIEWrapper::resetInnerHtmlValidity()
{
    m_bInnerHTMLValid = false;
    m_bInnerHTMLLcValid = false;
}

void CIEWrapper::DoRecordLastRefreshStats(DWORD dwStartTicks)
{
    DWORD dwEnd = GetTickCount();
    if(dwEnd > dwStartTicks)
        m_dwLastRefreshTakenMs = dwEnd - dwStartTicks;
    if(!m_bLastRefreshTakenInited)
        m_bLastRefreshTakenInited = true;
}

DWORD CIEWrapper::get_LastRefreshTakenMs()
{
    if(m_bLastRefreshTakenInited)
        return m_dwLastRefreshTakenMs;
    else
        return 0;
}