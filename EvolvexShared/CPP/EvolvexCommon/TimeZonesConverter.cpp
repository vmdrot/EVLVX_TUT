//#include "StdAfx.h"
#include "evolvex_common_lib.h"
#include "TimeZonesConverter.h"

static CTimeZonesConverter* gpTZC = NULL;

CTimeZonesConverter::CTimeZonesConverter(void)
{
    EnumTimeZones(m_zoneInfos);
    IniFileWrapper* pIni = helper::get_ini_file();
    keys_map_t* pSection =  pIni->GetSection("TimeZoneAbbreviations");
    if(pSection)
    {
        for(keys_map_t::iterator si = pSection->begin();si != pSection->end();si++)
        {
            std::pair<std::string,std::string> currPair(si->first, si->second);
            m_zoneAbbr2FullNames.insert(currPair);
        }
    }
}

CTimeZonesConverter::~CTimeZonesConverter(void)
{
}


rslt_code_t CTimeZonesConverter::EnumTimeZones(std::map<std::string,reg_tzi_t>& target)
{
    const std::string time_zones_root("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones");
    string_array_t tzNames;
    rslt_code_t res = helper::reg_enum_sub_keys(time_zones_root,tzNames); // todo
    if(RSLT_SUCCESS!= res)
        return res;
    char bfr[MAX_REGISTRY_STRING_SIZE] = { 0 };
    int tzidx = 0;
    for(string_array_t::iterator tzn = tzNames.begin();tzn< tzNames.end();tzn++)
    {
        std::string currKeyPath(time_zones_root + "\\" + *tzn);
        LOG_DEBUG("%s(%d)::tzn[%d] = '%s'",__FUNCTION__, __LINE__, tzidx, tzn->c_str());
        HKEY hkRoot;
        std::string relPath;
        res = helper::reg_split_path(currKeyPath,hkRoot, relPath);
        CRegKey currTZKey;
        res = ((ERROR_SUCCESS == currTZKey.Open(hkRoot, (LPCTSTR)(wchar_t*)_bstr_t(relPath.c_str()),KEY_READ)) ? RSLT_SUCCESS : RSLT_NOT_SUCCESS );
        if(res != RSLT_SUCCESS)
            return res;
        reg_tzi_t currRTZI;
        std::string strIndex;
        res = helper::reg_read(currKeyPath, "Display", currRTZI.Display, &(currTZKey.m_hKey));
        if(RSLT_SUCCESS!= res)
        {
            LOG_ERROR("%s(%d)::tzn[%d] = '%s'",__FUNCTION__, __LINE__, tzidx, tzn->c_str());
            return res;
        }

        res = helper::reg_read(currKeyPath, "Dlt", currRTZI.Dlt, &(currTZKey.m_hKey));
        if(RSLT_SUCCESS!= res)
        {
            LOG_ERROR("%s(%d)::tzn[%d] = '%s'",__FUNCTION__, __LINE__, tzidx, tzn->c_str());
            return res;
        }

        //DWORD dwSzDw = sizeof(DWORD);
        //res = helper::reg_read_raw(currKeyPath, "Index", NULL, (BYTE*)&currRTZI.Index, &dwSzDw, &(currTZKey.m_hKey));
        //if(RSLT_SUCCESS!= res)
        //{
        //    LOG_ERROR("%s(%d)::tzn[%d] = '%s'",__FUNCTION__, __LINE__, tzidx, tzn->c_str());
        //    return res;
        //}

        //res = helper::reg_read(currKeyPath, "MapID", currRTZI.MapID, &(currTZKey.m_hKey));
        //if(RSLT_SUCCESS!= res)
        //{
        //    LOG_DEBUG("%s(%d)::absent MapID for tzn[%d] = '%s'",__FUNCTION__, __LINE__, tzidx, tzn->c_str());
        //}

        res = helper::reg_read(currKeyPath, "Std", currRTZI.Std, &(currTZKey.m_hKey));
        TIME_ZONE_INFO tzi;
        if(RSLT_SUCCESS!= res)
        {
            LOG_ERROR("%s(%d)::tzn[%d] = '%s'",__FUNCTION__, __LINE__, tzidx, tzn->c_str());
            return res;
        }

        DWORD dwTziDSz = sizeof(TIME_ZONE_INFO);
        res = helper::reg_read_raw(currKeyPath,"TZI",NULL,(BYTE*)&tzi,&dwTziDSz, &(currTZKey.m_hKey));
        if(RSLT_SUCCESS!= res)
        {
            LOG_ERROR("%s(%d)::tzn[%d] = '%s'",__FUNCTION__, __LINE__, tzidx, tzn->c_str());
            return res;
        }

        TIME_ZONE_INFORMATION tzi2;
        TimeZoneInfoToTimeZoneInformation(tzi2,tzi);
        currRTZI.TZI = tzi2;
        std::pair<std::string,reg_tzi_t> currPair(*tzn,currRTZI);
        target.insert(currPair);
        tzidx++;
        currTZKey.Close();
    }
    return RSLT_SUCCESS;
}

void CTimeZonesConverter::TimeZoneInfoToTimeZoneInformation( TIME_ZONE_INFORMATION& 
     TimeZoneInfo1 , const TIME_ZONE_INFO& TimeZoneInfo2 )
{
    TimeZoneInfo1.Bias = TimeZoneInfo2.Bias ;
    TimeZoneInfo1.DaylightBias = TimeZoneInfo2.DaylightBias ;
    TimeZoneInfo1.DaylightDate = TimeZoneInfo2.DaylightDate ;
    TimeZoneInfo1.StandardBias = TimeZoneInfo2.StandardBias ;
    TimeZoneInfo1.StandardDate = TimeZoneInfo2.StandardDate ;
}

std::ostream& operator<<(std::ostream& out, const reg_tzi_t& rgtzi)
{
    out << '{' << "Display:" << rgtzi.Display << ", Dlt:" << rgtzi.Dlt << ", Index:" << rgtzi.Index << ", MapID:" << rgtzi.MapID << ", Std:" << rgtzi.Std << ", TZI:" << rgtzi.TZI << '}';
    return out;
}

std::ostream& operator<<(std::ostream& out, const TIME_ZONE_INFORMATION& tzi)
{
    char delim[4] = { 0 };
    switch(REG_TIME_ZONE_INFORMATION_OUTPUT_FMT)
    {
        case Indented:
            strcpy(delim,"\r\n ");
            break;
        case Tab:
            strcpy(delim,"\t");
            break;
    }
    
    out << '{' << delim << "Bias:" << tzi.Bias << ',' <<delim << "DaylightBias:" <<tzi.DaylightBias << ',' <<delim << "DaylightDate:"  << tzi.DaylightDate << ',' <<delim << "StandardBias:"  << tzi.StandardBias << ',' <<delim << "StandardDate:"  << tzi.StandardDate << delim << '}';
    return out;
}



void CTimeZonesConverter::TZFullName2Abbr(const std::string& tzfn, std::string& abbr)
{
    
}

bool CTimeZonesConverter::TZAbbr2TZFullName(const std::string& tza, std::string& tzfn)
{
    LOG_STARTED();
    std::map<std::string, std::string>::iterator elem = m_zoneAbbr2FullNames.find(tza);
    if(elem == m_zoneAbbr2FullNames.end())
    {
        LOG_ERROR_LOCATION;
        LOG_DEBUG("%s(%d)::tza = '%s'", __FUNCTION__, __LINE__, tza.c_str());
        return false;
    }
    tzfn = elem->second;
    if(m_zoneInfos.find(elem->second) == m_zoneInfos.end())
    {
        LOG_ERROR_LOCATION;
        LOG_DEBUG("%s(%d)::tza = '%s'", __FUNCTION__, __LINE__, tza.c_str());
        LOG_DEBUG("%s(%d)::tzfn = '%s'", __FUNCTION__, __LINE__, tzfn.c_str());
        return false;
    }
    LOG_FINISHED();
    return true;
}

bool CTimeZonesConverter::TZ2Local(const CTime& srcTime, const std::string& srcTZ, CTime& localTime)
{
    std::string tzfn;
    if(!TZAbbr2TZFullName(srcTZ,tzfn))
        return false;
    TIME_ZONE_INFORMATION tzi = m_zoneInfos[tzfn].TZI;
    SYSTEMTIME tzLocalTime;
    CTime2SysTm(srcTime,tzLocalTime);
    SYSTEMTIME utc;
    if(!TzSpecificLocalTimeToSystemTime(&tzi,&tzLocalTime,&utc))
    {
        helper::LogExplainError( GetLastError(), __FUNCTION__);
        return false;
    }
    SYSTEMTIME ourTime;
    if(!SystemTimeToTzSpecificLocalTime(NULL,&utc,&ourTime))
    {
        helper::LogExplainError( GetLastError(), __FUNCTION__);
        return false;
    }
    SysTm2CTime(ourTime, localTime);
    return true;
}


void CTimeZonesConverter::CTime2SysTm(const CTime& src, SYSTEMTIME& dest)
{
    //dest.wYear = src.GetYear();
    //dest.wMonth = src.GetMonth();
    //dest.wDayOfWeek = src.GetDayOfWeek();
    //dest.wDay = src.GetDay();
    //dest.wHour = src.GetHour();
    //dest.wMinute = src.GetMinute();
    //dest.wSecond = src.GetSecond();
    //dest.wMilliseconds
    src.GetAsSystemTime(dest);    
}

void CTimeZonesConverter::SysTm2CTime(const SYSTEMTIME& src, CTime& dest)
{
    CTime cpy(src);
    dest = cpy;
}

CTimeZonesConverter* CTimeZonesConverter::GetConverter()
{
    if(gpTZC == NULL)
    {
        gpTZC = new CTimeZonesConverter();
    }
    return gpTZC;
}

std::ostream& operator<<(std::ostream& out, const CTime& tm)
{
    std::string tmp;
    helper::time_to_str(tm, tmp);
    out << tmp;
    return out;
}