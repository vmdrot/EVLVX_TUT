#ifndef __EVOLVEX_COMMON_LIB_EVENT_LOG_WRITER_H_
#define __EVOLVEX_COMMON_LIB_EVENT_LOG_WRITER_H_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <Windows.h>
#include <string>

class CEventLogWriter
{
private:
    HANDLE m_hEvtSrc;
    std::string m_sEvtSrc;
    std::string m_sEventMessagesFile;
    CEventLogWriter(void);
    CEventLogWriter(const std::string& evtSrc, const std::string& evtMsgsFile);
    bool m_bRegistryChecked;
    void PatchTheRegistry();
public:
    ~CEventLogWriter(void);
    static CEventLogWriter* GetInstance();
    static CEventLogWriter* GetInstance(const std::string& evtSrc, const std::string& evtMsgsFile);
    void ReportEvent(WORD wType, WORD wCategory, DWORD dwEventID,
    PSID       lpUserSid,
    WORD       wNumStrings,
    DWORD      dwDataSize,
    LPCSTR   *lpStrings,
    LPVOID lpRawData);
    std::string& get_EventSource();
    std::string& get_EventMessagesFile();
    void set_EventSource(const std::string& val);
    void set_EventMessagesFile(const std::string& val);
    void LogSuccess(const std::string& msg);
    void LogError(const std::string& msg);
};
#endif //__EVOLVEX_COMMON_LIB_EVENT_LOG_WRITER_H_