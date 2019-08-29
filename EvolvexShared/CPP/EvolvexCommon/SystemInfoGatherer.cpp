#include "SystemInfoGatherer.h"
#include <Iphlpapi.h>
#include <iptypes.h>
#include "evolvex_common_lib.h"
CSystemInfoGatherer::CSystemInfoGatherer(void)
{
}

CSystemInfoGatherer::~CSystemInfoGatherer(void)
{
}

//bool CSystemInfoGatherer::GetHostName(std::string& hostName, COMPUTER_NAME_FORMAT nameType)
//{
//    DWORD cch = 0;
//    GetComputerNameExA(nameType, NULL, &cch);
//    cch++;
//    char* bfr = (char*)calloc(cch,sizeof(char));
//    if(bfr == NULL)
//        return false;
//    if(!GetComputerNameExA(nameType, bfr, &cch))
//    {
//        free((void*)bfr);
//        return false;
//    }
//    hostName = bfr;
//    free((void*)bfr);
//    return true;
//}

FIXED_INFO* CSystemInfoGatherer::GetNetworkParameters()
{
    FIXED_INFO * FixedInfo;
    try
    {
        ULONG    bufflen;
        DWORD    retval;
        FixedInfo = (FIXED_INFO *) GlobalAlloc( GPTR, sizeof( FIXED_INFO ) );
        bufflen = sizeof( FIXED_INFO );

        if( ERROR_BUFFER_OVERFLOW == GetNetworkParams( FixedInfo, &bufflen ) ) 
        {
            GlobalFree( FixedInfo );
            FixedInfo = (FIXED_INFO*)GlobalAlloc( GPTR, bufflen );
        }

        retval = GetNetworkParams( FixedInfo, &bufflen );

        if(retval!=ERROR_SUCCESS)
        {
            LOG_ERROR("%s::%s failed", __FUNCTION__,  "GetNetworkParams");
            GlobalFree(FixedInfo);
            return NULL;
        }
        //#pragma region just debug logging
        //{
        //    std::ostringstream out;
        //    out << *FixedInfo;
        //    LOG_DEBUG("%s(%d)::*FixedInfo = %s", __FUNCTION__, __LINE__, out.str().c_str());
        //}
        //#pragma endregion just debug logging
        return FixedInfo;
    }
    catch(...)
    {
        helper::LogExplainError(GetLastError(), __FUNCTION__);
        return NULL;
    }

}


bool CSystemInfoGatherer::GetHostName(std::string& hostName, bool domain_name_only )
{
    bool rslt = false;
    FIXED_INFO* fxinfo = NULL;
    try
    {

        fxinfo = CSystemInfoGatherer::GetNetworkParameters();
        //isdk_scope_guard_ptr_t sg(isdk_scope_guard_globalfree(fxinfo));
        if(fxinfo == NULL)
            return false;
        hostName = "";
        if (!domain_name_only)
        {
            hostName += fxinfo->HostName;
        hostName += ".";
        }
        hostName += fxinfo->DomainName;
        GlobalFree(fxinfo);
        fxinfo = NULL;
        rslt = true;
    }
    catch(...)
    {
        if(fxinfo != NULL)
        {
            GlobalFree(fxinfo);
            fxinfo = NULL;
        }
        helper::LogExplainError(GetLastError(), __FUNCTION__);
        rslt = false;
    }
    return rslt;
}

//bool CSystemInfoGatherer::GetHostIP(std::string& ip)
//{
//    //todo
//    return false;
//}

bool CSystemInfoGatherer::GetOsVersion(OSVERSIONINFOA* osVersion)
{
    //LPOSVER
    osVersion->dwOSVersionInfoSize = sizeof(*osVersion);
    if(GetVersionExA(osVersion))
        return true;
    else
        return false;
}
bool CSystemInfoGatherer::GetOsVersion(OSVERSIONINFOEXA* osVersion)
{
    osVersion->dwOSVersionInfoSize = sizeof(*osVersion);
    if(GetVersionExA((OSVERSIONINFOA*)osVersion))
        return true;
    else
        return false;
}
std::ostream& operator<<(std::ostream& out, const OSVERSIONINFOEXA& osVersionInfo)
{
    out << '{' << "dwOSVersionInfoSize:" << osVersionInfo.dwOSVersionInfoSize << ", ";  
    out << "dwMajorVersion:" << osVersionInfo.dwMajorVersion << ", ";
    out << "dwMinorVersion:" << osVersionInfo.dwMinorVersion << ", ";
    out << "dwBuildNumber:" << osVersionInfo.dwBuildNumber << ", ";
    out << "dwPlatformId:" << osVersionInfo.dwPlatformId << ", ";
    out << "szCSDVersion:'" << osVersionInfo.szCSDVersion << "', ";
    out << "wServicePackMajor:" << osVersionInfo.wServicePackMajor << ", ";
    out << "wServicePackMinor:" << osVersionInfo.wServicePackMinor << ", ";
    out << "wSuiteMask:" << osVersionInfo.wSuiteMask << ", ";
    out << "wProductType:" << osVersionInfo.wProductType << ", ";
    out << "wReserved:" << osVersionInfo.wReserved << '}';
    return out;
}

std::ostream& operator<<(std::ostream& out, const FIXED_INFO& fi)
{
    out << '{';
    out << "HostName:" << fi.HostName << ", ";
    out << "DomainName:" <<  fi.DomainName << ", ";
    out << "CurrentDnsServer:" <<  fi.CurrentDnsServer << ", ";
    out << "DnsServerList:" <<  fi.DnsServerList << ", ";
    out << "NodeType:" <<  fi.NodeType << ", ";
    out << "ScopeId:" <<  fi.ScopeId << ", ";
    out << "EnableRouting:" <<  fi.EnableRouting << ", ";
    out << "EnableProxy:" <<  fi.EnableProxy << ", ";
    out << "EnableDns:" <<  fi.EnableDns ;
    out << '}';
    return out;
}

std::ostream& operator<<(std::ostream& out, const IP_ADDR_STRING& ipadrs)
{
    out << '{';
    out << "Context:"<< ipadrs.Context<< ", ";
    out << "IpAddress:"<< ipadrs.IpAddress << ", ";
    out << "IpMask:"<< ipadrs.IpMask;
    out << '}';
    return out;
}

std::ostream& operator<<(std::ostream& out, const PIP_ADDR_STRING pipadrs)
{
    out << '[';
    int idx = 0;
    
    for( PIP_ADDR_STRING pNxt = pipadrs; pNxt != NULL;pNxt = pNxt->Next)
    {
        if(idx > 0)
            out << ", ";
        out << idx << ':' << *pNxt;
        idx++;

    }
    out << ']';
    return out;
}

std::ostream& operator<<(std::ostream& out, const IP_ADDRESS_STRING& ip)
{
    out << ip.String;
    return out;

}

//std::ostream& operator<<(std::ostream& out, const IP_MASK_STRING& ipmask)
//{
//    out << ipmask.String;
//    return out;
//}

std::ostream& operator<<(std::ostream& out, const OSVERSIONINFOA& osVersionInfo)
{
    out << '{' << "dwOSVersionInfoSize:" << osVersionInfo.dwOSVersionInfoSize << ", ";  
    out << "dwMajorVersion:" << osVersionInfo.dwMajorVersion << ", ";
    out << "dwMinorVersion:" << osVersionInfo.dwMinorVersion << ", ";
    out << "dwBuildNumber:" << osVersionInfo.dwBuildNumber << ", ";
    out << "dwPlatformId:" << osVersionInfo.dwPlatformId << ", ";
    out << "szCSDVersion:'" << osVersionInfo.szCSDVersion << '}';
    return out;
}
bool CSystemInfoGatherer::IsUACOn(bool* bEnabled)
{
    DWORD dwSzDw = sizeof(DWORD);
	DWORD dwRegVal;
	
    if(RSLT_SUCCESS!= helper::reg_read_raw("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "EnableLUA", NULL, (BYTE*)&dwRegVal, &dwSzDw))
    {
        LOG_ERROR_LOCATION;
        return false;
    }
	*bEnabled = (dwRegVal == 1L);
	return true;
}