#pragma once
#include <map>
#include <string>
#include "evolvex_common_lib.h"

typedef std::map<std::string,std::string> keys_map_t;
typedef std::map<std::string,keys_map_t*> sect_map_t;
typedef std::pair<std::string,std::string> key_val_pair_t;
typedef std::pair<std::string,keys_map_t*> sect_pair_t;


class IniFileWrapper
{
private:
    sect_map_t m_Sections;
    std::string m_sIniPath;
    bool ReadString(const std::string& sect, const std::string& key, std::string& target);
    bool DoesSectionExist(const std::string& sect);
    bool DoesKeyExist(const std::string& sect, const std::string& key);
    void CacheTheWholeFile();
public:
    IniFileWrapper();
    IniFileWrapper(const std::string& sIniPath);
    ~IniFileWrapper();
    bool GetString(const std::string& sect, const std::string& key, std::string& target);
    bool SetString(const std::string& sect, const std::string& key, const std::string& src);
    keys_map_t* GetSection(const std::string& sect);
    bool RemoveString(const std::string& sect, const std::string& key);
};
