#include "evolvex_common_lib.h"
#include "IniFileWrapper.h"

IniFileWrapper::IniFileWrapper(): m_sIniPath("")
{
}

IniFileWrapper::~IniFileWrapper()
{
}

IniFileWrapper::IniFileWrapper(const std::string& sIniPath):m_sIniPath(sIniPath)
{
    CacheTheWholeFile();
}

bool IniFileWrapper::ReadString(const std::string& sect, const std::string& key, std::string& target)
{
    return helper::ini_file_get_string(m_sIniPath, sect, key,target);
}


bool IniFileWrapper::GetString(const std::string& sect, const std::string& key, std::string& target)
{
    if(DoesSectionExist(sect) && DoesKeyExist(sect,key))
    {
        keys_map_t* pSection = m_Sections[sect];
        target = (*pSection)[key];
        return true;
    }
    return false;
    //todo...
//    if(DoesSectionExist(sect))
}

bool IniFileWrapper::DoesSectionExist(const std::string& sect)
{
    return (m_Sections.find(sect) != m_Sections.end());
}

bool IniFileWrapper::DoesKeyExist(const std::string& sect, const std::string& key)
{
    if(!DoesSectionExist(sect))
        return false;
    keys_map_t* pSection = m_Sections[sect];
    return (pSection->find(key) != pSection->end());
}

void IniFileWrapper::CacheTheWholeFile()
{
    string_array_t sectNames;
    helper::ini_file_get_sections(m_sIniPath, sectNames);
    
    for(string_array_t::iterator it = sectNames.begin();it!= sectNames.end();it++)
    {
        keys_map_t* pSection = new keys_map_t(); 
        m_Sections.insert(sect_pair_t(*it,pSection));
        string_array_t sectKeys;
        helper::ini_file_get_section_key_names(m_sIniPath,*it,sectKeys);
        for(string_array_t::iterator j = sectKeys.begin();j != sectKeys.end();j++)
        {
            std::string currVal;
            helper::ini_file_get_string(m_sIniPath,*it,*j, currVal);

            //std::string* pCurrVal = new std::string()
            pSection->insert(key_val_pair_t(*j,currVal));
        }
    }
}

bool IniFileWrapper::SetString(const std::string& sect, const std::string& key, const std::string& src)
{
    return helper::ini_file_set_string(m_sIniPath, sect,key,src);
}

keys_map_t* IniFileWrapper::GetSection(const std::string& sect)
{
    sect_map_t::iterator it = m_Sections.find(sect);
    if(it == m_Sections.end())
        return NULL;
    return it->second;
}

bool IniFileWrapper::RemoveString(const std::string& sect, const std::string& key)
{
    return false; //todo
}