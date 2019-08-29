#include "EventLogWriter.h"
#include "evolvex_common_lib.h"

CEventLogWriter* pEvtLgWrtrInstnc;

CEventLogWriter::CEventLogWriter(void): m_hEvtSrc(NULL), m_sEventMessagesFile(""), m_sEvtSrc(""), m_bRegistryChecked(false)
{
}

CEventLogWriter::~CEventLogWriter(void)
{
    if(m_hEvtSrc != NULL)
    {
        DeregisterEventSource(m_hEvtSrc);
        m_hEvtSrc = NULL;
    }
}

CEventLogWriter::CEventLogWriter(const std::string& evtSrc, const std::string& evtMsgsFile): m_hEvtSrc(NULL), m_sEventMessagesFile(evtMsgsFile), m_sEvtSrc(evtSrc), m_bRegistryChecked(false)
{
}

CEventLogWriter* CEventLogWriter::GetInstance()
{
    if(pEvtLgWrtrInstnc == NULL)
    {
        pEvtLgWrtrInstnc = new CEventLogWriter();
    }
    return pEvtLgWrtrInstnc;
}
CEventLogWriter* CEventLogWriter::GetInstance(const std::string& evtSrc, const std::string& evtMsgsFile)
{
    if(pEvtLgWrtrInstnc == NULL)
    {
        pEvtLgWrtrInstnc = new CEventLogWriter(evtSrc, evtMsgsFile);
    }
    return pEvtLgWrtrInstnc;
}

std::string& CEventLogWriter::get_EventSource()
{
    return m_sEvtSrc;
}

std::string& CEventLogWriter::get_EventMessagesFile()
{
    return m_sEventMessagesFile;
}

void CEventLogWriter::set_EventSource(const std::string& val)
{
    m_sEvtSrc = val;
}

void CEventLogWriter::set_EventMessagesFile(const std::string& val)
{
    m_sEventMessagesFile = val;
}

void CEventLogWriter::ReportEvent(WORD wType, WORD wCategory, DWORD dwEventID,PSID lpUserSid,WORD wNumStrings,DWORD dwDataSize,LPCSTR *lpStrings,LPVOID lpRawData)
{
    PatchTheRegistry();

    if(m_hEvtSrc == NULL)
    {
        m_hEvtSrc = RegisterEventSourceA(NULL,  // use local computer 
                m_sEvtSrc.c_str());           // event source name 
    }
    if (m_hEvtSrc == NULL) 
    {
        return;
    }
    if (!ReportEventA(m_hEvtSrc,           // event log handle 
                        wType, wCategory, dwEventID,lpUserSid,wNumStrings,dwDataSize,lpStrings,lpRawData))                // no data 
    {
        LOG_ERROR("%s(%d): Couldn't report the event.", __FUNCTION__, __LINE__);
    }
}


void CEventLogWriter::PatchTheRegistry()
{
    if(m_bRegistryChecked)
        return;
    m_bRegistryChecked = true;
    //patch the registry if needed
    const std::string sParentKeyPath = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Eventlog\\Application";
    std::string sFullKeyPath = sParentKeyPath + '\\' + m_sEvtSrc;
    if(RSLT_SUCCESS != helper::reg_key_exists(sFullKeyPath))
    {
        helper::reg_key_create(sParentKeyPath, m_sEvtSrc);
        helper::reg_write(sFullKeyPath, "EventMessageFile", m_sEventMessagesFile);
    }
}
void CEventLogWriter::LogSuccess(const std::string& msg)
{
    LPCSTR aLines[] = {msg.c_str()};
    this->ReportEvent(            EVENTLOG_SUCCESS,  // event type 
            0x0,            // event category  
            0,            // event identifier 
            NULL,                 // no user security identifier 
            sizeof(aLines)/sizeof(aLines[0]),             // number of substitution strings 
            0,                    // no data 
            aLines,                // pointer to strings 
            NULL);
}

void CEventLogWriter::LogError(const std::string& msg)
{
    LPCSTR aLines[] = {msg.c_str()};
    this->ReportEvent(            EVENTLOG_ERROR_TYPE,  // event type 
            0x0,            // event category  
            0,            // event identifier 
            NULL,                 // no user security identifier 
            sizeof(aLines)/sizeof(aLines[0]),             // number of substitution strings 
            0,                    // no data 
            aLines,                // pointer to strings 
            NULL);
}
