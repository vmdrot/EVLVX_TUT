#pragma once
#include "evolvex_common_lib.h"

typedef enum RGTZIOutpuFormat
{
    SingleLine,
    Indented,
    Tab
} RGTZIOutpuFormat;

const RGTZIOutpuFormat REG_TIME_ZONE_INFORMATION_OUTPUT_FMT = Indented;

typedef struct reg_tzi_t
{
    std::string Display;
    std::string Dlt;
    DWORD Index;
    std::string MapID;
    std::string Std;
    TIME_ZONE_INFORMATION TZI;
} reg_tzi_t;

typedef struct TIME_ZONE_INFO
{
    ULONG Bias;
    ULONG StandardBias;
    ULONG DaylightBias;
    SYSTEMTIME StandardDate;
    SYSTEMTIME DaylightDate;
} TIME_ZONE_INFO;

class CTimeZonesConverter
{
private:
    CTimeZonesConverter(void);
    std::map<std::string,reg_tzi_t> m_zoneInfos;
    std::map<std::string,std::string> m_zoneAbbr2FullNames;
    static void TimeZoneInfoToTimeZoneInformation( TIME_ZONE_INFORMATION& TimeZoneInfo1, const TIME_ZONE_INFO& TimeZoneInfo2);
    static void TZFullName2Abbr(const std::string& tzfn, std::string& abbr);
    bool TZAbbr2TZFullName(const std::string& tza, std::string& tzfn);
public:
    ~CTimeZonesConverter(void);
    static CTimeZonesConverter* GetConverter();
    static rslt_code_t EnumTimeZones(std::map<std::string,reg_tzi_t>& target);
    bool TZ2Local(const ATL::CTime& srcTime, const std::string& srcTZ, ATL::CTime& localTime);
    static void CTime2SysTm(const ATL::CTime& src, SYSTEMTIME& dest);
    static void SysTm2CTime(const SYSTEMTIME& src, ATL::CTime& dest);
};
std::ostream& operator<<(std::ostream& out, const reg_tzi_t& rgtzi);
std::ostream& operator<<(std::ostream& out, const TIME_ZONE_INFORMATION& tzi);
std::ostream& operator<<(std::ostream& out, const ATL::CTime& tm);