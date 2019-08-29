// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once


#define WIN32_LEAN_AND_MEAN		// Exclude rarely-used stuff from Windows headers
//#undef UNICODE
#include <iostream>
#include <string>
#include <vector>
#include <Windows.h>
#include <map>
#include <comutil.h>
#include <atlcomcli.h>
#include <fstream>
#include <sstream>
#include <atltime.h>
#include "rslt_codes_pub.h"
#include <Mstask.h>
#include <Lm.h>

typedef std::vector<std::string> string_array_t;
/* general types */
typedef char                    char_t;
typedef char                    int8_t;
typedef unsigned char           uint8_t;
typedef short                   int16_t;
typedef unsigned short          uint16_t;
typedef long                    int32_t;
typedef unsigned long           uint32_t;
typedef __int64                 int64_t;
typedef unsigned __int64        uint64_t;

#define MAX_REGISTRY_STRING_SIZE    4096

#include "logger.h"
#include "isdk_scope_guard.h"
#include "isdk_scope_guard_aux.h"
#include "path_splitter.h"
#include "IniFileWrapper.h"
#include "TimeZonesConverter.h"


#define DEFAULT_LOG_FILENAME "Sniper.log"
#define TRC(code) std::cout << #code << std::endl; code;

typedef NET_API_STATUS (NET_API_FUNCTION* local_group_user_manip_fn_t)(
    IN  LPCWSTR     servername OPTIONAL,
    IN  LPCWSTR     groupname,
    IN  DWORD      level,
    IN  LPBYTE     buf,
    IN  DWORD      totalentries
    );

namespace helper //todo: modify to be functional for an .exe 
{
    static std::string ini_file_path("");
    static Logger * plogger(NULL);
    Logger* get_logger();
    void destroy_logger();
    static IniFileWrapper* pIniFile(NULL);
    IniFileWrapper* get_ini_file();
    void destroy_ini_file();
    bool get_log_file_path(HINSTANCE dllHandle, std::string& target, const std::string& ini_file_name);
    bool ini_file_get_string(const std::string& path_to_ini_file, const std::string& section_name, const std::string& key_name, std::string& target);
    void get_current_dir(std::string& target);
    void get_dll_path(HINSTANCE dllHandle, std::string& path);
    void LogExplainError(HRESULT hr, const char* tszFunction);
    void LogExplainError(DWORD err, const char* tszFunction);
    void ExplainError(DWORD hr, std::string &dest);
    bool ini_file_get_sections(const std::string& path_to_ini_file, string_array_t& target);
    bool ini_file_get_section(const std::string& path_to_ini_file, const std::string& section_name, string_array_t& target);
    bool ini_file_get_section_key_names(const std::string& path_to_ini_file, const std::string& section_name, string_array_t& target);
    bool ini_file_get_string(const std::string& path_to_ini_file, const std::string& section_name, const std::string& key_name, std::string& target);
    bool ini_file_extended_output(const std::string& path_to_ini_file, std::ostream& target);
    bool ini_file_set_string(const std::string& path_to_ini_file, const std::string& section_name, const std::string& key_name, const std::string& src);
    void double_sz_to_string_array(LPCSTR doublesz_str, string_array_t& target);
    bool get_file_size(const std::string& path, int32_t& target);
    bool parse_us_time_str(const std::string& strTime, struct tm* tmDttm);
    bool parse_us_time_str(const std::string& strTime, ATL::CTime& dttm, std::string* TZ = NULL);
    void ltrim(std::string& src, const std::string& white_chars = "\t\n ");
    void rtrim(std::string& src, const std::string& white_chars = "\t\n ");
    void trim(std::string& src, const std::string& white_chars = "\t\n ");
    void trim(string_array_t &array, const std::string& white_chars = "\t\n ");
    int GetHoursCorrectionAMPM(std::string sAMPM, int hours);
    void time_to_str(const ATL::CTime& tm, std::string& target);
    bool terminate_process(DWORD process_id);
    rslt_code_t reg_split_path(const std::string &regpath, HKEY &regkey, std::string &path);
    rslt_code_t reg_read(const std::string &keypath, const std::string &key, std::string &value, HKEY* phOpenKey = NULL);
    rslt_code_t reg_read(const std::string &keypath, const std::string &key, string_array_t& multisz_values, HKEY* phOpenKey = NULL);
    rslt_code_t reg_delete(const std::string &keypath, const std::string &key);
    rslt_code_t reg_key_create(const std::string& parent, const std::string& key_name);
    rslt_code_t reg_write(const std::string& keypath, const std::string& value_name, const std::string &value);
    rslt_code_t reg_write(const std::string& keypath, const std::string& value_name, const string_array_t& multisz_values);
    rslt_code_t reg_key_exists(const std::string &keypath);
    rslt_code_t reg_enum_sub_keys(const std::string& keypath, string_array_t& target);
    rslt_code_t reg_read_raw(const std::string &keypath, const std::string &key, DWORD* key_type, BYTE* pValueBuf, DWORD* pValueBufSize, HKEY* phOpenKey = NULL);
    bool add_scheduled_task(const std::string& task_name, const std::string& path, const std::string& args, const std::string& working_dir, tm* schedule_at, int year_offset = 1900,const std::string& impersonate_as = "", LPCWSTR pwd = NULL);
    void parse_named_cmd_args(int argc, char* argv[], std::map<std::string,std::string>& target, int startFrom = 1);
    void parse_named_cmd_args(const string_array_t& argv, std::map<std::string,std::string>& target, int startFrom = 1);
    //rslt_code_t http_get_doc(const std::string& url, std::string& html);
    bool http_get_doc(const std::string& url, std::string& html);
    std::string get_temp_path();
    void generate_random_string(unsigned int len, std::string& target, const char* limit_to_chars = "");
    /**
     * get_file_contents
     *
     * reads entire file text and returns it as a string
     *
     * @param filename          [in] text file name
     * @return                  file contents
     */
    bool get_file_contents(const std::string& filename, std::string& contents);
    rslt_code_t delete_fs_object(const std::string &path_to_fso);
    rslt_code_t file_delete(const std::string full_path);
    bool file_exists(const std::string path);
    bool is_internet_connected();
    bool detectProhibitedPathChars(const std::string &path, bool less_drive);
    ///
    ///   cmd_ln2vec
    ///  Splits the monolit command line into array of strings 
    ///  Required for non main(argc, argv[])-styled applications
    ///
    ///  @param [in]       cmd_ln const std::string & - lpCmdLine (as is)
    ///  @param [in, out]  target string_array_t &  - 
    ///
    ///  This function doesn't return a value
    ///
    ///
    void cmd_ln2vec(const std::string& cmd_ln, string_array_t& target);
    /**
     * shell_exec
     *
     * process launching function.
     *
     * @param program           program to execute
     * @param arguments         command line arguments
     * @param workdir           working directory for the process
     * @param timeout           time to wait for the process to finish: INFINITE will block, 0 returns immediately
     * @param presult           optional result value storage pointer
     * @return                  Win32 error code (ERROR_SUCCESS indicates successful execution)
     */
    DWORD shell_exec(LPCWSTR program, LPWSTR arguments, LPCWSTR workdir, DWORD timeout, LPDWORD presult);
    /**
     * get_system_error_info
     * returns win32 error description
     * @param   error_code
     * @return  error description
     */
    std::string get_system_error_info(DWORD error_code);
    
    bool get_file_version(const std::string& file_path, std::string& target);
        ///  compare_files
    ///
    /// Byte-by-byte file comparison
    ///
    /// @param file1   [in]   const std::string &  - full path the file # 1
    /// @param file2   [in]   const std::string &  - full path the file # 2
    /// @param equal   [out]  bool &               - comparison result flag (true - files are identical, false - otherwise)
    /// @param binary  [in]   bool  [=false]       - flag indicating whether to compare files as text or to perform binary comparison
    ///
    /// @return values:
    /// rslt_code_t of the operation (RSLT_SUCCESS, normally)
    rslt_code_t compare_files(const std::string& file1, const std::string& file2, bool& equal, bool binary = false);

    static rslt_code_t local_security_group_user_manipulation(local_group_user_manip_fn_t pfn, const std::string& server, const std::string& group_name, const std::string& user_name);
    rslt_code_t add_user_to_local_security_group(const std::string& server, const std::string& group_name, const std::string& user_name);
    rslt_code_t del_user_from_local_security_group(const std::string& server, const std::string& group_name, const std::string& user_name);
    rslt_code_t is_local_group_member(const std::string& server, const std::string& group_name, const std::string& user_name);
    rslt_code_t user_get_local_groups(const std::string& server, const std::string& user_name, string_array_t& groups);
}
template<class std_tstring>
void str_lowercase(const std_tstring& src, std_tstring& dest)
{
    dest.reserve(src.length());
    for(unsigned int i = 0; i < src.length(); ++i)
        dest += tolower(src.at(i));
}

template<class std_tstring>
void str_uppercase(const std_tstring& src, std_tstring& dest)
{
    dest.reserve(src.length());
    for(unsigned int i = 0; i < src.length(); ++i)
        dest += toupper(src.at(i));
}
std::ostream& operator<<(std::ostream& out, const SYSTEMTIME& st);


template<class T>
std::wstring to_wstring(T &value)
{
    std::wstringstream wss;
    wss << value;
    return wss.str();
}

/// global  string_replace
///
/// Replaces all occurrences of the str_find inside the haystack with str_replace
///
/// @param haystack    [in] const std_tstring &  - subject string where to look for and replace
/// @param str_find    [in] const std_tstring &  - what to find
/// @param str_replace [in] const std_tstring &  - what to replace with
///
/// @return values:
/// std_tstring  which is already undergone the replacements

template<class std_tstring>
std_tstring string_replace(const std_tstring& haystack, const std_tstring& str_find, const std_tstring& str_replace)
{
    try
    {
        std_tstring rslt(haystack);
        if (haystack.empty() || str_find.empty())
        {
            return rslt;
        }
        size_t lst_pos = rslt.find(str_find);
        while (rslt.npos != lst_pos)
        {
            rslt.replace(lst_pos, str_find.length(), str_replace);
            lst_pos = rslt.find(str_find);
        }    
        return rslt;

    }
    catch (...) 
    {
        return haystack;
    }
}
/// global  find_std_string_within
///
/// std::find equivalent, with case insensitivity add-on; only std::string/std::wstring vectors
/// finds the first occurrence and returns true, or false if no occurrences found
///
/// @param hay_stack      [in] const std::vector<std_tstring> &  - vector to look inside
/// @param needle         [in] const std_tstring &               - string to look for
/// @param case_sensitive [in] bool  [=false]                    - case sensitivity flag
///
/// @return values:
/// true if found, false otherwise

template<class std_tstring>
bool find_std_string_within(const std::vector<std_tstring>& hay_stack, const std_tstring& needle, bool case_sensitive = false)
{
    const std::vector<std_tstring>::const_iterator i_end = hay_stack.end();
    for (std::vector<std_tstring>::const_iterator i = hay_stack.begin(); i != i_end; ++i)
    {
        if (case_sensitive)
        {
            if ((*i).compare(needle) == 0)
            {
                return true;
            }
        }
        else
        {
            std_tstring needle_lower;
            str_lowercase(needle, needle_lower);
            std_tstring i_lower;
            str_lowercase(*i, i_lower);
            if (i_lower.compare(needle_lower) == 0)
            {
                return true;
            }
        }
    }
    return false;
}

// TODO: reference additional headers your program requires here
