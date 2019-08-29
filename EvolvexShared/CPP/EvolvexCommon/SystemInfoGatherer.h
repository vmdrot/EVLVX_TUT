#ifndef __EVOLVEX_COMMON_LIB_SYSTEM_INFO_GATHERER_H_
#define __EVOLVEX_COMMON_LIB_SYSTEM_INFO_GATHERER_H_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include <Windows.h>
#include <string>
#include <iostream> 
#include <Iphlpapi.h>


class CSystemInfoGatherer
{
public:
    CSystemInfoGatherer(void);
    ~CSystemInfoGatherer(void);
    //static bool GetHostName(std::string& hostName, COMPUTER_NAME_FORMAT nameType = COMPUTER_NAME_FORMAT.ComputerNameDnsHostname);
    static bool GetHostName(std::string& hostName, bool domain_name_only = false);
    static FIXED_INFO* GetNetworkParameters();
    //static bool GetHostIP(std::string& ip);
    static bool GetOsVersion(OSVERSIONINFOA* osVersion);
    static bool GetOsVersion(OSVERSIONINFOEXA* osVersion);
	static bool IsUACOn(bool* bEnabled);
};

std::ostream& operator<<(std::ostream& out, const OSVERSIONINFOEXA& osVersionInfo);
std::ostream& operator<<(std::ostream& out, const OSVERSIONINFOA& osVersionInfo);
std::ostream& operator<<(std::ostream& out, const FIXED_INFO& fi);
std::ostream& operator<<(std::ostream& out, const PIP_ADDR_STRING pipadrs);
std::ostream& operator<<(std::ostream& out, const IP_ADDR_STRING& ipadrs);
std::ostream& operator<<(std::ostream& out, const IP_ADDRESS_STRING& ip);
//std::ostream& operator<<(std::ostream& out, const IP_MASK_STRING& ipmask);

#endif //__EVOLVEX_COMMON_LIB_SYSTEM_INFO_GATHERER_H_