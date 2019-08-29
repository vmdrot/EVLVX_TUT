#pragma once
#include "evolvex_common_lib.h"
#include <string>
#include <Oleacc.h>
#include <mshtml.h>
#include <exdisp.h>
#include "atlbase.h"
#include "atlwin.h"
static const std::string IE_WND_CLASS = "IEFrame";
class CIEWrapper
{
private:
    CComBSTR COMPLETE_STATE;
    DWORD m_dwProcessId;
    HWND m_hwndIE;
    HWND m_hwndIESrv;
    IWebBrowser2* m_pBrowserItfc;
    IHTMLDocument2* m_pDoc;
    std::string m_sIEFullPath;
    bool m_bHiddenOperation;// = false;
    bool GetIEFullPath(std::string& target);
    std::string IE_EXE_NAME;
    DWORD LaunchIE(const std::string& url = "");
    static HWND GetIEServerWndByFrameWnd(HWND iefrm);
    static int GetIEFromHWND(HWND ie, IWebBrowser2** p_browser, IHTMLDocument2** p_doc);
    bool IsInitialized();
    bool DoInitialize(const std::string& initUrl = "");
    bool GetForm(IHTMLFormElement** pForm, const std::string& formId);
    template<typename T>
    bool GetFormElement(IHTMLFormElement* pForm,T** pElem, const std::string& elemId,const IID iid)
    {
        CComPtr<IDispatch> pElemDisp;
        _variant_t vtElemId;
        _variant_t idx;
        vtElemId.SetString(elemId.c_str());
        HRESULT hr = pForm->item(vtElemId,idx,&pElemDisp);
        if(FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        hr = pElemDisp->QueryInterface(iid, (void**)pElem);
        if(FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        return true;
    }
    void ApplyVisibilityOperationMode();

    #pragma region perf opt
    std::string m_sInnerHTML;
    std::string m_sInnerHTMLLc;
    bool m_bInnerHTMLValid;
    bool m_bInnerHTMLLcValid;
    void resetInnerHtmlValidity();
    #pragma endregion
    
    #pragma region statistics
    DWORD m_dwLastRefreshTakenMs;
    bool m_bLastRefreshTakenInited;
    void DoRecordLastRefreshStats(DWORD dwStartTicks);
    #pragma endregion

public:
    CIEWrapper(void);
    ~CIEWrapper(void);
    bool IsRequestComplete();
    bool Navigate(const std::string& url);
    //void EvalJS(const std::string& sJs);
    //bool EvalString(const std::string& sJs, std::string& target);
    bool SubmitForm(const std::string& formId, const std::string& submitBtnId = "", bool bSimulate = false);
    //bool PushButton(const std::string& buttonId);
    //bool SetEditText(const std::string& editId, const std::string& text);
    bool SetEditText(const std::string& editId, const std::string& text, const std::string& frmId);
    /*bool SetPasswordText(const std::string& pwdId, const std::string& text, const std::string& frmId);*/
    static bool LaunchBrowser(DWORD* process_id, HWND* parent_ie_wnd);
    static DWORD GetProcessIDByHwnd(HWND wnd);
    static HWND MapWindow2ProcessID(DWORD process_id, const std::string& wnd_class = "", const std::string& wnd_caption = "");
    static HWND GetIEWindowByProcessID(DWORD process_id);
    bool GetBodyInnerHTML(std::string& target);
    bool Refresh(bool bWaitUntilComplete = true);
    bool GetLocation(std::string& href);
    void CloseBrowser();
    void set_HiddenOperationMode(bool bHidden);
    const std::string& get_InnerHTML();
    void set_InnerHTML(const std::string& val);
    const std::string& get_InnerHTMLLower(); 
    DWORD get_LastRefreshTakenMs();
};
