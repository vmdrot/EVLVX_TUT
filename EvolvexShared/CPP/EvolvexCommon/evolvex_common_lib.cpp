// stdafx.cpp : source file that includes just the standard includes
// Sniper.pch will be the pre-compiled header
// stdafx.obj will contain the pre-compiled type information

#include "evolvex_common_lib.h"
#include <string>
#include <atltime.h>
//#include "RyeolHttpClient.h"
#include "isdk_file_reader.h"
#include <shellapi.h>
#include <excpt.h>
#include <algorithm>
#include <atlsecurity.h>
//#define DEBUG_REGISTRY_FUNCS //uncomment this define if you need to re-visit registry functions
Logger* helper::get_logger()
{
    if (! plogger)
        plogger = new Logger();
    return plogger;

}


void helper::destroy_logger()
{
    try 
    {
        if (plogger) 
        {
            delete plogger;
            plogger = NULL;
        }
    }
    catch(...) 
    {
    }
}

bool helper::get_log_file_path(HINSTANCE dllHandle, std::string& target, const std::string& ini_file_name)
{
    //std::string ini_file_path;
    //helper::get_current_dir(ini_patsh);
    if(ini_file_path.length() == 0)
    {
        helper::get_dll_path(dllHandle, ini_file_path);
        if(helper::ini_file_path.c_str()[ini_file_path.length()-1] != '\\')
            helper::ini_file_path.append("\\");
        ini_file_path.append(ini_file_name);
    }
    bool rslt = ini_file_get_string(ini_file_path,"Common","LogFilePath",target);
    return rslt;
}

bool helper::ini_file_get_string(const std::string& path_to_ini_file, const std::string& section_name, const std::string& key_name, std::string& target)
{
    char* bfr;
    const isdk_scope_guard_ptr_t sg(isdk_scope_guard_free((void**)&bfr));
    const int min_bfr_len = 1024;
    int max_entry_size = 1024;

    DWORD bfr_size = max_entry_size > min_bfr_len ? max_entry_size : min_bfr_len;
    bfr = (char*)calloc(bfr_size, sizeof(char));
    if (NULL == bfr) 
    {
        //LOG_ERROR_LOCATION;
        return false;
    }
    GetPrivateProfileStringA((LPCSTR)section_name.c_str(),(LPCSTR)key_name.c_str(),NULL, (LPSTR)bfr, bfr_size, (LPCSTR)path_to_ini_file.c_str());
    target = bfr;
    return true;

}

void helper::get_current_dir(std::string& target)
{
    char dirbuff[MAX_PATH];
    ::GetCurrentDirectoryA(MAX_PATH,dirbuff);
    target = dirbuff;
}

void helper::get_dll_path(HINSTANCE dllHandle, std::string& path)
{
    char bfr[MAX_PATH] = { 0 };
    GetModuleFileNameA(dllHandle,(LPCH)bfr,MAX_PATH);
    path_splitter spltr(bfr);
    path = spltr.get_drive();
    path += spltr.get_dir();
}

void helper::LogExplainError(DWORD err, const char* tszFunction)
{
    std::string errExplanation;
    ExplainError(err, errExplanation);
    LOG_ERROR("%s failed with %s", tszFunction, errExplanation.c_str());
}


void helper::LogExplainError(HRESULT hr, const char* tszFunction)
{
    std::string errExplanation;
    ExplainError(HRESULT_CODE(hr), errExplanation);
    LOG_ERROR("%s failed with %s", tszFunction, errExplanation.c_str());
}

void helper::ExplainError(DWORD err, std::string &dest)
{
    dest = "";
    //TCHAR szBuf[1024]; 
    try
    {
        LPVOID lpMsgBuf;
        //DWORD dw = GetLastError(); 
        std::ostringstream tmpstream("");
        FormatMessage(
            FORMAT_MESSAGE_ALLOCATE_BUFFER | 
            FORMAT_MESSAGE_FROM_SYSTEM,
            NULL,
            err,
            MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
            (LPTSTR) &lpMsgBuf,
            0, NULL );
        if (lpMsgBuf != NULL)
        {
            tmpstream << "error " << err << ":" << (sizeof(TCHAR) == sizeof(char) ? (char*)lpMsgBuf : (char*)_bstr_t((wchar_t*)lpMsgBuf));
            LocalFree(lpMsgBuf);
        }
        else
            tmpstream << "error " << err << ":" << "(failed to retrieve error description)";
        
        dest = tmpstream.str() ;

    }
    catch(...)
    {
        LOG_ERROR("%s failed to resolve the HRESULT code %d", __FUNCTION__, err);
    }
}

bool helper::ini_file_get_sections(const std::string& path_to_ini_file, string_array_t& target)
{
    
    char* bfr;
    const isdk_scope_guard_ptr_t sg(isdk_scope_guard_free((void**)&bfr));
    const int min_bfr_len = 300;
    int32_t ini_file_size;
    if (true != helper::get_file_size(path_to_ini_file, ini_file_size)) 
    {
        //LOG_ERROR_LOCATION;
        return false; /*return RSLT_OBJECT_NOT_FOUND;*/
    }

    DWORD bfr_size = (int)ini_file_size /10 > min_bfr_len ? (int)ini_file_size/10 : min_bfr_len;
    bfr = (char*)calloc(bfr_size, sizeof(char));
    if (NULL == bfr) 
    {
        //LOG_ERROR_LOCATION;
        return false;
    }
    GetPrivateProfileSectionNamesA((LPSTR)bfr,bfr_size, (LPCSTR)path_to_ini_file.c_str());
    double_sz_to_string_array(bfr,target);
    return true;
}

bool helper::ini_file_get_section(const std::string& path_to_ini_file, const std::string& section_name, string_array_t& target)
{

    char* bfr;
    const isdk_scope_guard_ptr_t sg(isdk_scope_guard_free((void**)&bfr));
    const int min_bfr_len = 1024;
    int32_t ini_file_size;
    if (true != helper::get_file_size(path_to_ini_file, ini_file_size)) 
    {
        //LOG_ERROR_LOCATION;
        return false; /*return RSLT_OBJECT_NOT_FOUND;*/
    }

    DWORD bfr_size = (int)ini_file_size /10 > min_bfr_len ? (int)ini_file_size/10 : min_bfr_len;
    bfr = (char*)calloc(bfr_size, sizeof(char));
    if (NULL == bfr) 
    {
        //LOG_ERROR_LOCATION;
        return false;
    }
    GetPrivateProfileSectionA((LPCSTR)section_name.c_str(),(LPSTR)bfr, bfr_size, (LPCSTR)path_to_ini_file.c_str());
    double_sz_to_string_array(bfr,target);
    return true;
}
bool helper::ini_file_get_section_key_names(const std::string& path_to_ini_file, const std::string& section_name, string_array_t& target)
{
    string_array_t section_entries;
    if (true != helper::ini_file_get_section(path_to_ini_file, section_name, section_entries)) 
    {
        //LOG_ERROR_LOCATION;
        return false;
    }
    for(string_array_t::iterator entry = section_entries.begin(); entry != section_entries.end();++entry)
    {
        std::string curr_key = (*entry).substr(0,(*entry).find("="));
        target.push_back(curr_key);
    }
    return true;
}

void
helper::double_sz_to_string_array(LPCSTR doublesz_str, string_array_t& target)
{
    try
    {
        int curr_offset = 0;
        while (strlen(&doublesz_str[curr_offset]))
        {
            target.push_back(std::string(&doublesz_str[curr_offset]));
            curr_offset += strlen(&doublesz_str[curr_offset]) + 1;
        }
    }
    catch(...)
    {
        LOG_ERROR("%s(%d) - exception", __FUNCTION__, __LINE__);
    }
}

//#define DEBUG_GET_FSZ //uncomment to re-visit this function
bool helper::get_file_size(const std::string& path, int32_t &file_size)
{
    FILE *f = fopen(path.c_str(), "rb");
    if (f == NULL)
    {
        LOG_ERROR("%s: failed to get size of file '%s' (file open failed)", __FUNCTION__, path.c_str());
        return false;
    }
    fseek(f, 0, SEEK_END);
    file_size = ftell(f);
    fclose(f);
    if (-1 == file_size)
    {
        LOG_ERROR("%s: failed to get size of file '%s'", __FUNCTION__, path.c_str());
        return false;
    }
#if DEBUG_GET_FSZ
    LOG_DEBUG("%s::file_size = %d", __FUNCTION__, file_size);
#endif
    return true;
}

IniFileWrapper* helper::get_ini_file()
{
    if (! pIniFile)
        pIniFile = new IniFileWrapper(ini_file_path);
    return pIniFile;
}


void helper::destroy_ini_file()
{
    try 
    {
        if (pIniFile) 
        {
            delete pIniFile;
            pIniFile = NULL;
        }
    }
    catch(...) 
    {
    }
}

bool helper::parse_us_time_str(const std::string& strTime, struct tm* tmDttm)
{
    //input sample - 4/15/2009 12:23:06 PM PT
    CTime t_now(_time64(NULL));
    time_t st = t_now.GetTime();
    tmDttm = localtime(&st);

    std::string sTmp(strTime);
    trim(sTmp);
    std::string::size_type pos0 = 0;
    std::string::size_type pos1 = 0;
    char aDelimiters[] = {'/','/', ' ', ':', ':', ' ', ' '};
    int delimCnt = sizeof(aDelimiters) / sizeof(aDelimiters[0]);
    for(int i=0;i<delimCnt;i++)
    {
        //if(i<delimCnt -1)
        //    pos1 = sTmp.find(aDelimiters[i],pos0);
        //else
        //    pos1 = sTmp.length();
        pos1 = sTmp.find(aDelimiters[i],pos0);

        std::string sM = sTmp.substr(pos0,pos1-pos0);
        switch(i)
        {
        case 0:
           tmDttm->tm_mon = atoi(sM.c_str());
           break;
        case 1:
           tmDttm->tm_mday = atoi(sM.c_str());
           break;
        case 2:
           tmDttm->tm_year = atoi(sM.c_str());
           break;
        case 3:
           tmDttm->tm_hour = atoi(sM.c_str());
           break;
        case 4:
           tmDttm->tm_min = atoi(sM.c_str());
           break;
        case 5:
           tmDttm->tm_sec = atoi(sM.c_str());
           break;
        case 6:
            tmDttm->tm_hour += GetHoursCorrectionAMPM(sM,tmDttm->tm_hour);
            break;
        default:
            break;
        }
        pos0 = pos1+1;
    }
    CTime t_rslt(tmDttm->tm_year,tmDttm->tm_mon, tmDttm->tm_mday,tmDttm->tm_hour, tmDttm->tm_min, tmDttm->tm_sec, tmDttm->tm_isdst);
    time_t stRslt = t_rslt.GetTime();
    tmDttm = localtime(&stRslt);
    return true;
}

void helper::ltrim(std::string &args, const std::string& white_chars) 
{
    std::size_t length = args.length();
    std::size_t index = 0;
    std::size_t nerase = 0;
    if (!length) return;
    while(index < length) {
        if (white_chars.find(args.at(index++)) != white_chars.npos)
            nerase++;
        //if (args.at(index++) == ' ') nerase++;
        else break;
    }
    if (nerase) args.erase(0, nerase);
}

void helper::rtrim(std::string &args, const std::string& white_chars) 
{
    std::size_t length = args.length();
    std::size_t index = length - 1;
    std::size_t nerase = 0;
    if (!length) return;
    while(index >= 0) {
        if (white_chars.find(args.at(index--)) != white_chars.npos)
            nerase++;
        else break;
    }
    if (nerase) args.erase(length - nerase, nerase);
}

void helper::trim(std::string &s, const std::string& white_chars) 
{
    ltrim(s, white_chars);
    rtrim(s, white_chars);
}

void helper::trim(string_array_t &array, const std::string& white_chars)
{
    for(string_array_t::iterator i = array.begin(); i != array.end(); ++i)
        trim(*i, white_chars);
}
// TODO: reference any additional headers you need in STDAFX.H
// and not in this file

int helper::GetHoursCorrectionAMPM(std::string sAMPM, int hrs)
{
    const std::string AM = "AM";
    const std::string PM = "PM";
    /* between 12:00 AM and 12:59 AM, subtract 12 hours:
    12:59 AM --> 0059 */
            if (sAMPM == AM && hrs == 12)
            {
                return -12;
            }
    /* between 1:00 AM and 12:59 PM, a straight conversion:
    10:00 AM --> 1000*/
            if ((sAMPM == AM && hrs >= 1) || (hrs == 12 && sAMPM == PM))
            {
                return 0;
            }

    /* between 1:00 PM and 11:59 PM, add 12 hours:
    10:59 PM --> 2259 */
            if (sAMPM == PM && hrs >= 1 && hrs <= 11)
            {
                return 12;
            }
        return 0;
}

bool helper::parse_us_time_str(const std::string& strTime, CTime& dttm, std::string* TZ)
{
    //input sample - 4/15/2009 12:23:06 PM PT
    int itm_mon, itm_mday, itm_year, itm_hour, itm_min, itm_sec;

    std::string sTmp(strTime);
    trim(sTmp);
    std::string::size_type pos0 = 0;
    std::string::size_type pos1 = 0;
    char aDelimiters[] = {'/','/', ' ', ':', ':', ' ', ' '};
    int delimCnt = sizeof(aDelimiters) / sizeof(aDelimiters[0]);
    for(int i=0;i<delimCnt;i++)
    {
        pos1 = sTmp.find(aDelimiters[i],pos0);

        std::string sM = sTmp.substr(pos0,pos1-pos0);
        switch(i)
        {
        case 0:
           itm_mon = atoi(sM.c_str());
           break;
        case 1:
           itm_mday = atoi(sM.c_str());
           break;
        case 2:
           itm_year = atoi(sM.c_str());
           break;
        case 3:
           itm_hour = atoi(sM.c_str());
           break;
        case 4:
           itm_min = atoi(sM.c_str());
           break;
        case 5:
           itm_sec = atoi(sM.c_str());
           break;
        case 6:
            itm_hour += GetHoursCorrectionAMPM(sM,itm_hour);
            break;
        default:
            break;
        }
        pos0 = pos1+1;
    }
    if(TZ != NULL)
    {
        *TZ = strTime.substr(pos0);
    }
    CTime t_rslt(itm_year,itm_mon, itm_mday,itm_hour, itm_min, itm_sec);
    dttm = t_rslt;
    return true;
}

void helper::time_to_str(const CTime& tm, std::string& target)
{
    CString strTime = tm.Format((LPCTSTR)L"%Y%m%d %H:%M:%S %Z");
    target = (char*)_bstr_t((wchar_t*)strTime.GetString());
}

bool helper::ini_file_set_string(const std::string& path_to_ini_file, const std::string& section_name, const std::string& key_name, const std::string& src)
{
    if(!WritePrivateProfileStringA((LPCSTR)section_name.c_str(),
        (LPCSTR) key_name.c_str(),
        (LPCSTR) src.c_str(),
        (LPCSTR)path_to_ini_file.c_str()))
        return false;
    return true;
}

bool helper::terminate_process(DWORD process_id)
{
    HANDLE hProcess = OpenProcess(PROCESS_ALL_ACCESS ,TRUE,process_id);
    if(hProcess == NULL)
        return false;
    if(!TerminateProcess(hProcess, 0))
        return false;
    return true;
}


rslt_code_t helper::reg_split_path(const std::string &regpath, HKEY &regkey, std::string &path)
{
    try 
    {
        regkey = HKEY_CLASSES_ROOT;       
        std::string full_path(regpath);
        std::size_t index = full_path.find_first_of('\\');       
        if(index == full_path.npos)
        {
            path = full_path;
            return RSLT_SUCCESS;
        }
        
        path = full_path.substr(index+1);
        std::string key(full_path.substr(0, index));        
        const char * rKey = key.c_str();
        if (0 == strcmp(rKey, "HKEY_CLASSES_ROOT")) 
        {
            regkey = HKEY_CLASSES_ROOT;
        } 
        else if (0 == strcmp(rKey, "HKEY_CURRENT_USER"))
        {
            regkey = HKEY_CURRENT_USER;
        }
        else if (0 == strcmp(rKey , "HKEY_LOCAL_MACHINE"))
        {
            regkey = HKEY_LOCAL_MACHINE;
        }
        else if (0 == strcmp(rKey, "HKEY_USERS") == 0)
        {
            regkey = HKEY_USERS;
        }
        else if (0 == strcmp(rKey, "HKEY_CURRENT_CONFIG"))
        {
            regkey = HKEY_CURRENT_CONFIG;
        }        
        return RSLT_SUCCESS;
    }
    catch(...) 
    {
        LOG_ERROR("exception in %s", __FUNCTION__ );
        return RSLT_UNKNOWN_OS_ERROR;
    }
}

rslt_code_t 
helper::reg_read_raw(const std::string &keypath, const std::string &key, DWORD* key_type, BYTE* pValueBuf, DWORD* pValueBufSize, HKEY* phOpenKey)
{
    HKEY rk_key(NULL), rk_root(NULL);
    std::string rel_path;
    rslt_code_t res;
    try
    {
        if(phOpenKey == NULL)
        {
            res = helper::reg_split_path(keypath, rk_root, rel_path);
            if (RSLT_SUCCESS != res)
                return res;
            
            if(ERROR_SUCCESS != RegOpenKeyExA(rk_root, rel_path.c_str(), 0, KEY_READ, &rk_key))
                return RSLT_NOT_SUCCESS;
        }
        else
        {
            rk_key = *phOpenKey;            
        }

        res = (ERROR_SUCCESS == RegQueryValueExA(rk_key, key.c_str(), 0, key_type, pValueBuf, pValueBufSize) ? RSLT_SUCCESS : RSLT_NOT_SUCCESS);
        
        // obligatory RegCloseKey
        if(phOpenKey == NULL)
        {
            if (ERROR_SUCCESS != RegCloseKey(rk_key))
            {
#if DEBUG_REGISTRY_FUNCS
            LOG_DEBUG("ignoring RegCloseKeyEx failure (keypath = '%s', key = '%s')", keypath.c_str(), key.c_str());
#endif
            }
        }
        // checking the result value of RegQueryValueExA 
#if DEBUG_REGISTRY_FUNCS
        LOG_DEBUG("Registry entry '%s' fetching was %s", key.c_str(), (res != RSLT_SUCCESS ? "unsuccessful" : "successful"));
#endif
        return res;
    }
    catch (...)
    {
        LOG_ERROR("exception in %s", __FUNCTION__ );
        return RSLT_UNKNOWN_OS_ERROR;
    }
}

rslt_code_t helper::reg_read(const std::string &keypath, const std::string &key, std::string &value, HKEY* phOpenKey)
{
    char buffer[MAX_REGISTRY_STRING_SIZE] = {0};
    DWORD value_type(0), buf_size(MAX_REGISTRY_STRING_SIZE);

    rslt_code_t res = reg_read_raw(keypath, key, &value_type, (BYTE*)buffer, &buf_size, phOpenKey);
    
    // checking the result value of read
    if (RSLT_SUCCESS != res)
        return res;

    switch(value_type)
    {
    case REG_EXPAND_SZ:
    case REG_SZ:
        buffer[buf_size] = 0;
        value = buffer;
        res = RSLT_SUCCESS;
        break;
    default:
        res = RSLT_FUNCTION_NOT_IMPLEMENTED;
    }
#if DEBUG_REGISTRY_FUNCS
    LOG_DEBUG("Registry entry '%s' fetching was %s", key.c_str(), (res != RSLT_SUCCESS ? "unsuccessful" : "successful"));
#endif
    return res;
}

rslt_code_t helper::reg_read(const std::string &keypath, const std::string &key, string_array_t& multisz_values, HKEY* phOpenKey)
{
    char buffer[MAX_REGISTRY_STRING_SIZE] = {0};
    DWORD value_type(0), buf_size(MAX_REGISTRY_STRING_SIZE);

    rslt_code_t res = reg_read_raw(keypath, key, &value_type, (BYTE*)buffer, &buf_size, phOpenKey);

    // checking the result value of read
    if (RSLT_SUCCESS != res)
        return res;

    if (REG_MULTI_SZ == value_type)
    {
        DWORD pos = 0;
        for (DWORD pos = 0; pos < buf_size;)
        {
            if (!buffer[pos])
                break;
            std::string str(buffer+pos);
            pos += (DWORD)str.size() + 1;
            multisz_values.push_back(str);
        }
    }
    else
    {
        res = RSLT_FUNCTION_NOT_IMPLEMENTED;
    }
    LOG_DEBUG("Registry entry '%s' fetching was %s", key.c_str(), (res != RSLT_SUCCESS ? "unsuccessful" : "successful"));
    return res;
}



rslt_code_t helper::reg_delete(const std::string &keypath, const std::string &key)
{
    HKEY rk_key(NULL), rk_root(NULL);
    std::string rel_path;
    try 
    {
        rslt_code_t res = helper::reg_split_path(keypath, rk_root, rel_path);
        if (RSLT_SUCCESS != res)
            return res;

        if(ERROR_SUCCESS != RegOpenKeyExA(rk_root, rel_path.c_str(), 0, KEY_WRITE, &rk_key))
            return RSLT_NOT_SUCCESS;

        res = (ERROR_SUCCESS == RegDeleteValueA(rk_key, key.c_str()) ? RSLT_SUCCESS : RSLT_NOT_SUCCESS);

        // obligatory RegCloseKey
        if (ERROR_SUCCESS != RegCloseKey(rk_key))
            LOG_DEBUG("ignoring RegCloseKeyEx failure (keypath = '%s', key = '%s')", keypath.c_str(), key.c_str());

        LOG_DEBUG("Registry entry '%s' deletion was %s", key.c_str(), (res != RSLT_SUCCESS ? "unsuccessful" : "successful"));
        return res;
    }
    catch(...) 
    {
        LOG_ERROR("exception in %s", __FUNCTION__ );
        return RSLT_UNKNOWN_OS_ERROR;
    }
}

static rslt_code_t 
reg_write_raw(const std::string &keypath, const std::string &key, const UINT key_type, const BYTE* pValue, DWORD value_buf_size)
{
    HKEY rk_key(NULL), rk_root(NULL);
    std::string rel_path;
    try
    {
        rslt_code_t res = helper::reg_split_path(keypath, rk_root, rel_path);
        if (RSLT_SUCCESS != res)
            return res;

        if(ERROR_SUCCESS != RegOpenKeyExA(rk_root, rel_path.c_str(), 0, KEY_WRITE, &rk_key))
            return RSLT_NOT_SUCCESS;

        res = (ERROR_SUCCESS == RegSetValueExA(rk_key, key.c_str(), 0, key_type, pValue, value_buf_size)) ? RSLT_SUCCESS : RSLT_NOT_SUCCESS;

        // obligatory RegCloseKey
        if (ERROR_SUCCESS != RegCloseKey(rk_key))
            LOG_DEBUG("ignoring RegCloseKeyEx failure (keypath = '%s', key = '%s')", keypath.c_str(), key.c_str());

        LOG_DEBUG("Registry entry '%s' modification was %s", key.c_str(), (res != RSLT_SUCCESS ? "unsuccessful" : "successful"));
        return res;
    }
    catch(...)
    {
        LOG_ERROR("exception in %s", __FUNCTION__ );
        return RSLT_UNKNOWN_OS_ERROR;
    }
}

rslt_code_t helper::reg_write(const std::string& keypath, const std::string& value_name, const std::string &value)
{
    return reg_write_raw(keypath, value_name, REG_SZ, (LPBYTE)value.c_str(), (DWORD)value.size() + 1);
}

rslt_code_t helper::reg_write(const std::string& keypath, const std::string& value_name, const string_array_t& multisz_values)
{
    char buf[MAX_REGISTRY_STRING_SIZE] = {0};

    //check size
    DWORD buf_size = 0;
    for (DWORD i = 0; i < multisz_values.size(); ++i)
    {
        buf_size += (DWORD)multisz_values[i].size() + 1;
    }
    buf_size++;
    if (buf_size > MAX_REGISTRY_STRING_SIZE)
        return RSLT_INVALID_PARAMETER;

    //fill buffer
    DWORD pos = 0;
    for (DWORD i = 0; i < multisz_values.size(); ++i)
    {
        strcpy(buf + pos, multisz_values[i].c_str());
        pos += (DWORD)multisz_values[i].size() + 1;
    }

    return reg_write_raw(keypath, value_name, REG_MULTI_SZ, (LPBYTE)buf, buf_size);
}


rslt_code_t
helper::reg_key_exists(const std::string &keypath)
{
    HKEY rk_key(NULL), rk_root(NULL);
    std::string rel_path;
    try
    {
        rslt_code_t res = helper::reg_split_path(keypath, rk_root, rel_path);
        if (RSLT_SUCCESS != res)
        {
            //std::cout << "reg_split_path returned" << get_result_code_str(res) << std::endl;
            return res;
        }
        //std::cout << "rk_root = " << rk_root << std::endl;
        //std::cout << "rel_path = " << rel_path << std::endl;
        CRegKey reg;
        if (ERROR_SUCCESS != reg.Open(rk_root, (LPCTSTR)(wchar_t*)_bstr_t(rel_path.c_str()), KEY_READ))
        {
            res = RSLT_NOT_SUCCESS;
            return res;
        }
        reg.Close();
        return res;
    }
    catch (...) 
    {
        LOG_ERROR("exception in %s", __FUNCTION__ );
        return RSLT_UNKNOWN_OS_ERROR;

    }
}

rslt_code_t helper::reg_enum_sub_keys(const std::string& keypath, string_array_t& target)
{
    HKEY rk_key(NULL), rk_root(NULL);
    std::string rel_path;
    rslt_code_t res = reg_split_path(keypath, rk_root, rel_path);
    if (RSLT_SUCCESS != res)
    {
        return res;
    }
    CRegKey reg;
    if (ERROR_SUCCESS != reg.Open(rk_root, (LPCTSTR)(wchar_t*)_bstr_t(rel_path.c_str()), KEY_READ))
    {
        res = RSLT_NOT_SUCCESS;
        return res;
    }
    const int KEY_NAME_SZ = 256;
#if UNICODE
    wchar_t szKeyName[KEY_NAME_SZ] = { 0 };
#else
    char szKeyName[KEY_NAME_SZ] = { 0 };
#endif
    DWORD dwIdx = 0;
    DWORD pNameLen;
    LONG lRes;
    do
    {
        pNameLen = KEY_NAME_SZ;
        lRes = reg.EnumKey(dwIdx, (LPTSTR)szKeyName,&pNameLen);
        if(ERROR_SUCCESS == lRes)
        {
#if UNICODE
            target.push_back((char*)_bstr_t(szKeyName));
#else
            target.push_back(szKeyName);
#endif
        }
        dwIdx++;
    } while(lRes != ERROR_NO_MORE_ITEMS);
    if (ERROR_NO_MORE_ITEMS == lRes)
        res = RSLT_SUCCESS;
    return res;
}

std::ostream& operator<<(std::ostream& out, const SYSTEMTIME& st)
{
    out << st.wYear << '-' << st.wMonth << '-' << st.wDay << ' ' << st.wDayOfWeek << ' ' << st.wHour << ':' << st.wMinute << ':' << st.wSecond << '.' << st.wMilliseconds;
    return out;
}
bool helper::add_scheduled_task(const std::string& task_name, const std::string& path, const std::string& args, const std::string& working_dir, tm* schedule_at, int year_offset, const std::string& impersonate_as, LPCWSTR pwd)
{
    try
    {
        LOG_DEBUG("%s:working_dir = '%s'", __FUNCTION__, working_dir.c_str());

        CA2W wtask_name(task_name.c_str());

        CA2W wpath(path.c_str());
        CA2W wargs(args.c_str());
        CA2W wdir(working_dir.c_str());
        CComPtr<ITaskScheduler> pISched;
        if(FAILED(pISched.CoCreateInstance(CLSID_CTaskScheduler)))
            return false;

        if(!pISched)
            return false;

        bool found = false;

        {
            CComPtr<IEnumWorkItems> pItems;
            pISched->Enum(&pItems);
            LPWSTR* lpwszNames;
            DWORD dwFetchedTasks = 0;
            while (SUCCEEDED(pItems->Next(1, &lpwszNames, &dwFetchedTasks)) && (dwFetchedTasks != 0))
            {
                while (dwFetchedTasks)
                {
                    --dwFetchedTasks;
                    std::wstring w(wtask_name);
                    w += L".job";

                    if(0 == wcscmp(lpwszNames[dwFetchedTasks], w.c_str()))
                        found = true;

                    CoTaskMemFree(lpwszNames[dwFetchedTasks]);                 
                }
                CoTaskMemFree(lpwszNames);
            }
        }

        if(found)
            return false;

        CComPtr<ITask> pTask;
        HRESULT hr = pISched->NewWorkItem(wtask_name, CLSID_CTask, IID_ITask, (IUnknown**)&pTask);
        if(FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            helper::LogExplainError(hr,__FUNCTION__);
            return false;
        }

        pTask->SetApplicationName(wpath);
        pTask->SetParameters(wargs);        
        pTask->SetWorkingDirectory(wdir);
        pTask->SetAccountInformation((wchar_t*)_bstr_t(impersonate_as.c_str()), pwd);

        CComPtr<ITaskTrigger> pITaskTrigger;
        WORD piNewTrigger;
        pTask->CreateTrigger( &piNewTrigger, &pITaskTrigger);

        TASK_TRIGGER pTrigger;
        memset(&pTrigger, 0, sizeof (TASK_TRIGGER));

        pTrigger.cbTriggerSize  = sizeof(TASK_TRIGGER); 
        pTrigger.wBeginDay      = schedule_at->tm_mday;
        pTrigger.wBeginMonth    = schedule_at->tm_mon+1;
        pTrigger.wBeginYear     = year_offset + schedule_at->tm_year;
        pTrigger.wStartHour     = schedule_at->tm_hour;
        pTrigger.wStartMinute   = schedule_at->tm_min;
        pTrigger.wBeginYear     = schedule_at->tm_year;

        pTrigger.TriggerType    = TASK_TIME_TRIGGER_ONCE;

        pITaskTrigger->SetTrigger(&pTrigger);

        DWORD f;
        pTask->GetFlags(&f);
        f |= TASK_FLAG_DELETE_WHEN_DONE;
        pTask->SetFlags(f);

        CComQIPtr<IPersistFile> pPersistFile(pTask);        
        hr = pPersistFile->Save(NULL, TRUE);
        if(FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            helper::LogExplainError(hr, __FUNCTION__);
        }
        return FAILED(hr) ? false : true;
    }
    catch (...)
    {
        LOG_ERROR("exception in %s", __FUNCTION__ );
        return false;
    }
}

void helper::parse_named_cmd_args(const string_array_t& argv, std::map<std::string,std::string>& target, int startFrom)
{
    std::string lastSwitch("");
    bool bPreviousWasSwitch = false;
    int argc = argv.size();
    for(int i = startFrom;i<argc;i++)
    {
        bool isSwitch = false;
        if(strlen(argv[i].c_str())>2)
        {
            if(argv[i].at(0) == '-' || argv[i].at(1) == '-')
                isSwitch = true;
        }
        if(isSwitch && !bPreviousWasSwitch)
        {
            bPreviousWasSwitch = true;
            continue;
        }

        if(isSwitch && bPreviousWasSwitch)
        {
            std::string pureSwitchName(argv[i-1]);
            std::pair<std::string,std::string> thePair(pureSwitchName.substr(2), "");
            target.insert(thePair);
            bPreviousWasSwitch = true;
            continue;
        }
        if(!isSwitch && bPreviousWasSwitch)
        {
            std::string pureSwitchName(argv[i-1]);
            std::pair<std::string,std::string> thePair(pureSwitchName.substr(2),argv[i]);
            target.insert(thePair);
            bPreviousWasSwitch = false;
            continue;
        }
    }
}
void helper::parse_named_cmd_args(int argc, char* argv[], std::map<std::string,std::string>& target, int startFrom)
{
    string_array_t argvv;
    for(int i=0;i<argc;i++)
        argvv.push_back(argv[i]);
    parse_named_cmd_args(argvv,target,startFrom);
}

//rslt_code_t helper::http_get_doc(const std::string& url, std::string& html)
//{
//    using namespace Ryeol ;
//
//    CHttpClient         objHttpReq ;
//    CHttpResponse *     pobjHttpRes = NULL ;
//    //size_t              cbProceed = 1024 ;  // 1k
//
//
//    try {
//        HINTERNET hInet;
//        BYTE* pBuf;
//        const isdk_scope_guard_ptr_t sg(isdk_scope_guard_close_inet((HINTERNET&)&hInet,isdk_scope_guard_free((void**)&pBuf))); 
//        hInet = OpenInternet("IE6.0");
//        
//        pobjHttpRes = objHttpReq.RequestGet(hInet,url.c_str(),FALSE);
//        DWORD dwLen;
//        pobjHttpRes->GetContentLength(dwLen);
//        
//        pBuf = (BYTE*)calloc(dwLen+1, sizeof(BYTE));
//        if (NULL == pBuf) 
//        {
//            LOG_ERROR_LOCATION;
//            return RSLT_MEMORY_ALLOCATION_FAILURE;
//        }
//        DWORD cbRead = pobjHttpRes->ReadContent(pBuf, dwLen+1);
//        if(cbRead == 0)
//            return RSLT_NOT_SUCCESS;
//
//        html = (char*)pBuf;
//        return RSLT_SUCCESS;
//
//    } catch (httpclientexception & e) {
//        LOG_ERROR_LOCATION;
//        return RSLT_CONNECTION_FAILED;
//
//    }
//}

bool helper::http_get_doc(const std::string& url, std::string& html)
{
    std::string tmpFileFullPath = get_temp_path();
    std::string tmpFileNm;
    generate_random_string(8,tmpFileNm);
    tmpFileFullPath += tmpFileNm + ".html";
    HRESULT hr = URLDownloadToFile(NULL,(wchar_t*)_bstr_t(url.c_str()),(wchar_t*)_bstr_t(tmpFileFullPath.c_str()),0, NULL);
    if(FAILED(hr))
    {
        LogExplainError(hr, __FUNCTION__);
        if(file_exists(tmpFileFullPath))
            delete_fs_object(tmpFileFullPath);
        return false;
    }
    if(!get_file_contents(tmpFileFullPath,html))
    {
        LOG_ERROR_LOCATION;
        if(file_exists(tmpFileFullPath))
            delete_fs_object(tmpFileFullPath);
        return false;
    }
    if(file_exists(tmpFileFullPath))
        delete_fs_object(tmpFileFullPath);
    return true;
}

std::string helper::get_temp_path()
{
    DWORD cchTempPath = MAX_PATH;
    char pszTempPath[MAX_PATH];
    GetTempPathA(cchTempPath, pszTempPath);
    std::string rslt(pszTempPath);
    return rslt;
}


void helper::generate_random_string(unsigned int len, std::string& target, const char* limit_to_chars)
{
    std::string chars("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890");
    if (strlen(limit_to_chars)>0)
    {
        chars = limit_to_chars;        
    }
    target = "";
    srand((unsigned int)time( NULL ));
    std::vector<char> tmp_result;
    tmp_result.reserve(len+1);
    for(unsigned int x = 0; x < len; ++x)
    {
        unsigned int curr_rand = rand();
        curr_rand = curr_rand % chars.length();
        tmp_result.push_back(chars.at(curr_rand));
    }
    tmp_result.push_back('\0');
    target = (char*)&tmp_result[0];
}

bool helper::get_file_contents(const std::string& filename, std::string& contents)
{
    isdk_file_reader_t fr = isdk_File_Reader::create(filename, true);
    if (NULL == fr)
    {
        return false;
    }
    fr->get_contents(contents);
    return true;
}


rslt_code_t helper::delete_fs_object(const std::string &path_to_fso)
{
    rslt_code_t res = RSLT_SUCCESS;
    try
    {
        
        SHFILEOPSTRUCT fileOp =  { 0 };
        
        fileOp.wFunc = FO_DELETE;
        char tmp[MAX_PATH +1] = { 0 };
        strcpy(tmp, path_to_fso.c_str());
        
        fileOp.pFrom = (LPCWSTR) (wchar_t*)_bstr_t(&tmp[0]);
        
        fileOp.fFlags = FOF_NOERRORUI | FOF_SILENT | FOF_NOCONFIRMATION ;
        UINT er = SHFileOperation(&fileOp);
        if (er)
        {
            LogExplainError((DWORD)er,__FUNCTION__);
            res = RSLT_NOT_SUCCESS;
        }
        else
            res = RSLT_SUCCESS;
    }
    catch(...)
    {
        LogExplainError(GetLastError(), __FUNCTION__);
        res = RSLT_NOT_SUCCESS;
    }
    return res;
}

//Kernel32.lib
rslt_code_t helper::file_delete(const std::string full_path)
{
    try
    {
        LOG_DEBUG("%s::full_path = '%s'", __FUNCTION__, full_path.c_str());
        if(DeleteFileA(full_path.c_str()))
        {
            LOG_DEBUG("%s:DeleteFile(%s) succeeded", __FUNCTION__, full_path.c_str());
            return RSLT_SUCCESS;
        }
        else
        {
            LogExplainError(GetLastError(), __FUNCTION__);
            return RSLT_NOT_SUCCESS;
    }
    }
    catch (...) {
        LogExplainError(GetLastError(), __FUNCTION__);
        return RSLT_NOT_SUCCESS;
    }
}


bool helper::file_exists(const std::string path)
{
    bool rslt;
    try
    {
        WIN32_FIND_DATAA findFileData;
        HANDLE hFindFile;
        hFindFile = FindFirstFileA(path.c_str(), &findFileData);
        if (hFindFile == INVALID_HANDLE_VALUE)
        {
            rslt = false;
        }
        else
        {
            if (!(findFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
                rslt = true;
            else
                rslt = false;
            FindClose(hFindFile);
        }
    }
    catch(...)
    {
        rslt = false;
        LogExplainError(GetLastError(), __FUNCTION__);
    }
    return rslt;
}


bool helper::is_internet_connected()
{
    std::string html;
    if(!http_get_doc("http://www.google.com/", html))
        return false;
    return true;
}

bool helper::detectProhibitedPathChars(const std::string &path, bool less_drive)
{
    bool rslt = false;
    try
    {            
        std::string prohibited_chars = less_drive ? "\"*/:<>?|" : "\"*/<>?|" ;
        rslt = (path.find_first_of(prohibited_chars) != path.npos);
    }
    catch(...)
    {
        rslt = true;
        LogExplainError(GetLastError(), __FUNCTION__);
    }
    return rslt;
}


void helper::cmd_ln2vec(const std::string& cmd_ln, string_array_t& target)
{
    LPWSTR *szArglist;
    try
    {
        target.clear();
        std::wstring cmd_ln_w((wchar_t*)_bstr_t(cmd_ln.c_str()));
        int nArgs;
        int i;
        szArglist = CommandLineToArgvW(cmd_ln_w.c_str(), &nArgs);
        if( NULL == szArglist )
        {
            return;
        }
        else
        {
            for( i = 0; i < nArgs; i++)
            {
                target.push_back((char*)_bstr_t(szArglist[i]));
            }
        }
        GlobalFree(szArglist);
    }
    catch (...) 
    {
        if (NULL != szArglist)
        {
            GlobalFree(szArglist);
        }
    }
}

DWORD helper::shell_exec(LPCWSTR program, LPWSTR arguments, LPCWSTR workdir, DWORD timeout, LPDWORD presult)
{
    LOG_STARTED();
    ASSERT(program);
    ASSERT(arguments);
    LOG_DEBUG("%s(%d)::program = '%s'", __FUNCTION__, __LINE__, (char*)_bstr_t(program));
    LOG_DEBUG("%s(%d)::arguments = '%s'", __FUNCTION__, __LINE__, (char*)_bstr_t(arguments));
    STARTUPINFO si;
    PROCESS_INFORMATION pi;
    si.cb = sizeof(si);
    ZeroMemory( &si, sizeof(si));
    ZeroMemory( &pi, sizeof(pi));
    std::wstring wargs(L"");
    wargs += L"\"";
    wargs += program;
    wargs += L"\" ";
    wargs += arguments;
    
    if (!CreateProcessW(program, (LPWSTR)wargs.c_str(), NULL, NULL, FALSE, 0, NULL, workdir, &si, &pi)) 
    {
        DWORD err = GetLastError();
        LOG_ERROR("CreateProcess failed : %d (%s)", err, helper::get_system_error_info(err).c_str());
        return err;
    }

    if (timeout)
    {
        WaitForSingleObject(pi.hProcess, timeout);
        if (presult)
            GetExitCodeProcess(pi.hProcess, presult);
    }
    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);
    LOG_FINISHED();
    return ERROR_SUCCESS;
}


std::string helper::get_system_error_info(DWORD error_code)
{ 
    LPTSTR buffer;
    DWORD nchars = FormatMessage(
        FORMAT_MESSAGE_ALLOCATE_BUFFER | 
        FORMAT_MESSAGE_FROM_SYSTEM,
        NULL,
        error_code,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPTSTR) &buffer,
        0, NULL );

    if (! nchars)
        return "";
    
    buffer[nchars] = 0;
    std::string msg((char*)_bstr_t(buffer));
    std::replace(msg.begin(), msg.end(), (char)0x0A, ' ');
    std::replace(msg.begin(), msg.end(), (char)0x0D, ' ');
    trim(msg);
    LocalFree(buffer);
    return msg;
};

rslt_code_t helper::reg_key_create(const std::string& parent, const std::string& key_name)
{
    HKEY rk_root(NULL);
    std::string rel_path;
    try
    {
        rslt_code_t res = helper::reg_split_path(parent, rk_root, rel_path);
        if (RSLT_SUCCESS != res)
        {
            return res;
        }
        CRegKey reg;
        if (ERROR_SUCCESS != reg.Open(rk_root, (LPCTSTR)(wchar_t*)_bstr_t(rel_path.c_str()), KEY_CREATE_SUB_KEY))
        {
            res = RSLT_NOT_SUCCESS;
            return res;
        }
        if(ERROR_SUCCESS != reg.Create(reg.m_hKey, (LPCTSTR)(wchar_t*)_bstr_t(key_name.c_str())))
            res = RSLT_NOT_SUCCESS;
        reg.Close();
        return res;
    }
    catch (...) 
    {
        LOG_ERROR("exception in %s", __FUNCTION__ );
        return RSLT_UNKNOWN_OS_ERROR;

    }

}

rslt_code_t helper::compare_files(const std::string& file1, const std::string& file2, bool& equal, bool binary)
{
    FILE* fp1 = NULL;
    FILE* fp2 = NULL;
    const isdk_scope_guard_t sg(isdk_scope_guard_fclose(&fp1, isdk_scope_guard_fclose(&fp2)));
    char mode[] = "rt";
    if (binary)
    {
        strcpy(mode, "rb");
    }
    fp1 = fopen(file1.c_str(), mode);
    if (NULL == fp1)
    {
        LOG_DEBUG("%s(%d): failed to open the first file '%s'", __FUNCTION__, __LINE__, file1.c_str());
        return RSLT_NOT_SUCCESS;
    }
    fp2 = fopen(file2.c_str(), mode);
    if (NULL == fp2)
    {
        LOG_DEBUG("%s(%d): failed to open the second file '%s'", __FUNCTION__, __LINE__, file2.c_str());
        return RSLT_NOT_SUCCESS;
    }
    if (0 != fseek(fp1, 0, SEEK_END)) 
    {
        LOG_DEBUG("%s(%d): fseek(1) unsuccessful", __FUNCTION__, __LINE__);
        return RSLT_NOT_SUCCESS;
    }
    if (0 != fseek(fp2, 0, SEEK_END)) 
    {
        LOG_DEBUG("%s(%d): fseek(2) unsuccessful", __FUNCTION__, __LINE__);
        return RSLT_NOT_SUCCESS;
    }
    long fsize1 = ftell(fp1);
    long fsize2 = ftell(fp2);
    if (fsize1 != fsize2)
    {
        equal = false;
        return RSLT_SUCCESS;
    }
    if (0 != fseek(fp1, 0, SEEK_SET)) 
    { 
        LOG_ERROR("%s(%d): failed to reset cursor to the beginning of the file(1)", __FUNCTION__, __LINE__);
        return RSLT_NOT_SUCCESS;
    }
    if (0 != fseek(fp2, 0, SEEK_SET)) 
    { 
        LOG_ERROR("%s(%d): failed to reset cursor to the beginning of the file(2)", __FUNCTION__, __LINE__);
        return RSLT_NOT_SUCCESS;
    }
    while (0 != feof(fp1) && 0 != feof(fp2))
    {
        char c1 = fgetc(fp1);
        char c2 = fgetc(fp2);
        if (c1 != c2)
        {
            equal = false;
            return RSLT_SUCCESS;
        }
    }
    equal = true;
    return RSLT_SUCCESS;
}

rslt_code_t 
helper::local_security_group_user_manipulation(local_group_user_manip_fn_t pfn, const std::string& server, const std::string& group_name, const std::string& user_name)
{
	std::wstring wstr_server((wchar_t*)_bstr_t(server.c_str()));
	std::wstring wstr_group_name((wchar_t*)_bstr_t(group_name.c_str()));
    std::wstring wstr_user_name((wchar_t*)_bstr_t(user_name.c_str()));
	CSid user_sid((LPCTSTR)wstr_user_name.c_str(), (server.empty() ? NULL : (LPCTSTR)server.c_str()));
	if (!user_sid.IsValid())
	{
		{
			LOG_ERROR("%s(%d): failed to lookup sid", __FUNCTION__, __LINE__);
		}
		return RSLT_INVALID_PARAMETER;
	}
	{
		LOG_DEBUG("%s(%d): user_sid.Sid() = %s", __FUNCTION__, __LINE__, user_sid.Sid());
	}
	LOCALGROUP_MEMBERS_INFO_0 member_info;
	member_info.lgrmi0_sid = (PSID)user_sid.GetPSID();
	LOCALGROUP_MEMBERS_INFO_0 members_info_arr[] = {
		member_info	};
	NET_API_STATUS ntstatus = (*pfn)((wstr_server.empty() ? NULL : (LPCWSTR)wstr_server.c_str()), (LPCWSTR)wstr_group_name.c_str(), 0, (LPBYTE)members_info_arr, 1);
	{
		LOG_DEBUG("%s(%d): ntstatus = %d", __FUNCTION__, __LINE__, ntstatus);
	}
	if (NERR_Success == ntstatus)
	{
		return RSLT_SUCCESS;
	}
	else
	{
		return RSLT_NOT_SUCCESS;
	}
}

rslt_code_t helper::add_user_to_local_security_group(const std::string& server, const std::string& group_name, const std::string& user_name)
{
	return local_security_group_user_manipulation( NetLocalGroupAddMembers, server, group_name, user_name);
}

rslt_code_t helper::del_user_from_local_security_group(const std::string& server, const std::string& group_name, const std::string& user_name)
{
	return local_security_group_user_manipulation( NetLocalGroupDelMembers, server, group_name, user_name);
}

rslt_code_t helper::is_local_group_member(const std::string& server, const std::string& group_name, const std::string& user_name)
{
	string_array_t groups;
	rslt_code_t res = user_get_local_groups(server, user_name, groups);
	if (RSLT_SUCCESS != res)
		return res;
	return (find_std_string_within<std::string>(groups, group_name) ? RSLT_SUCCESS : RSLT_OBJECT_NOT_FOUND);
}

rslt_code_t helper::user_get_local_groups(const std::string& server, const std::string& user_name, string_array_t& groups)
{
	try
	{
		{
			LOG_DEBUG("%s(%d)::server = '%s', user_name = '%s'", __FUNCTION__, __LINE__, server.c_str(), user_name.c_str());
		}
		LPLOCALGROUP_USERS_INFO_0 pbfr = NULL;
		{
			LOG_DEBUG("%s(%d)::pbfr = %x", __FUNCTION__, __LINE__, pbfr);
		}
		const isdk_scope_guard_t sg(isdk_scope_guard_netapi_buffer_free((LPVOID*)&pbfr));
		DWORD pref_len = MAX_PREFERRED_LENGTH;
		DWORD entries_read;
		DWORD entries_total;
		std::wstring wstr_server((wchar_t*)_bstr_t(server.c_str()));
		std::wstring wstr_user_name((wchar_t*)_bstr_t(user_name.c_str()));
		NET_API_STATUS nt_status = NetUserGetLocalGroups((wstr_server.empty() ? NULL : (LPCWSTR)wstr_server.c_str()),
			                                        (LPCWSTR)wstr_user_name.c_str(),
													0,
													LG_INCLUDE_INDIRECT,
													(LPBYTE*)&pbfr,
													pref_len,
													&entries_read,
													&entries_total);
		{
			LOG_DEBUG("%s(%d)::nt_status = %d", __FUNCTION__, __LINE__,nt_status);
			if (ERROR_ACCESS_DENIED == nt_status) LOG_DEBUG("%s(%d)::nt_status = %s", __FUNCTION__, __LINE__, "ERROR_ACCESS_DENIED");
			else if (ERROR_MORE_DATA == nt_status) LOG_DEBUG("%s(%d)::nt_status = %s", __FUNCTION__, __LINE__, "ERROR_MORE_DATA");
			else if (NERR_InvalidComputer == nt_status) LOG_DEBUG("%s(%d)::nt_status = %s", __FUNCTION__, __LINE__, "NERR_InvalidComputer");
			else if (NERR_UserNotFound == nt_status) LOG_DEBUG("%s(%d)::nt_status = %s", __FUNCTION__, __LINE__, "NERR_UserNotFound");
		}
		if (NERR_Success != nt_status)
		{
			return RSLT_NOT_SUCCESS;
		}

		{
			LOG_DEBUG("%s(%d)::pbfr = %x", __FUNCTION__, __LINE__, pbfr);
			LOG_DEBUG("%s(%d)::entries_read = %d, entries_total = %d", __FUNCTION__, __LINE__,entries_read, entries_total);
		}
		groups.reserve(entries_read);
		for (DWORD i = 0; i < entries_read; ++i)
			groups.push_back((char*)_bstr_t(pbfr[i].lgrui0_name));
		return RSLT_SUCCESS;
	}
	catch(...)
	{
		{
			LOG_EXCEPTION;
		}
		return RSLT_UNKNOWN_OS_ERROR;
	}
}
