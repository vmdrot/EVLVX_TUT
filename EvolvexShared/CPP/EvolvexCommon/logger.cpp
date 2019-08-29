#include "evolvex_common_lib.h"
#include "logger.h"

/**
 *	specific-string literals.
 */

//
#define LOG_OUTPUT_BUFFER_SIZE (1024)

// classic log-level prefixes
#define LOGGER_STR_DEBUG   "DEBUG "
#define LOGGER_STR_INFO    "INFO  "
#define LOGGER_STR_WARN    "WARN  "
#define LOGGER_STR_ERROR   "ERROR "
#define LOGGER_STR_FATAL   "FATAL "

// additional log-level prefixes
#define LOGGER_STR_FAIL    "[FAILED]"
#define LOGGER_STR_SUCCESS "[SUCCEEDED]"

//////////////////////////////////////////////////////////////////////////
// Logger implementation
//////////////////////////////////////////////////////////////////////////

Logger::Logger(void)
{
}

Logger::~Logger(void)
{
	close();
}

// initializes logging into a file
bool Logger::init(const std::string& filename, long log_level)
{
    close();    
    m_file.open(filename.c_str(), std::ios::out | std::ios_base::app );
	set_level(log_level);
	return m_file.is_open();
}

bool Logger::logfmt(const std::string& prefix, const char * format, va_list args) const
{
    try 
    {
        if (! m_file.is_open())
            return false;
        char buffer[LOG_OUTPUT_BUFFER_SIZE];
        vsnprintf(buffer, sizeof(buffer), format, args);  
        m_file << get_time() << prefix << ": " << buffer << std::endl;   
        return true;
    }
    catch(...) {
        return false;
    }
}

bool Logger::log(const std::string& prefix, const std::string& msg) const
{
    if (! m_file.is_open())
		return false;
	m_file << get_time() << prefix << ": " << msg << std::endl;
	return true;
}

bool Logger::close(void)
{
    if (m_file.is_open())
		m_file.close();
    return !m_file.is_open();
}

void Logger::set_level(long log_level_flags) 
{
	m_loglevel = log_level_flags;
}

bool Logger::debug(const std::string& msg) const 
{
	return ISBITSET(m_loglevel, LOG_LEVEL_DEBUG) ? log(LOGGER_STR_DEBUG, msg) : false;
}

bool Logger::debug(const char * format, ...)  const
{
    ASSERT(format);
    if (! format)
        return false;

    va_list args;
    va_start(args, format);
    bool result = ISBITSET(m_loglevel, LOG_LEVEL_DEBUG) ? logfmt(LOGGER_STR_DEBUG, format, args) : false;
    va_end(args);
    return result;
}

bool Logger::info(const std::string& msg) const 
{
	return ISBITSET(m_loglevel, LOG_LEVEL_INFO) ? log(LOGGER_STR_INFO, msg) : false;
}

bool Logger::info(const char * format, ...)  const
{
    ASSERT(format);
    if (! format)
        return false;
    
    va_list args;
    va_start(args, format);
    bool result = ISBITSET(m_loglevel, LOG_LEVEL_INFO) ? logfmt(LOGGER_STR_INFO, format, args) : false;
    va_end(args);
    return result;
}

bool Logger::warn(const std::string& msg) const
{
	return ISBITSET(m_loglevel, LOG_LEVEL_WARN) ? log(LOGGER_STR_WARN, msg) : false;
}

bool Logger::warn(const char * format, ...) const
{
    ASSERT(format);
    if (! format)
        return false;

    va_list args;
    va_start(args, format);
    bool result = ISBITSET(m_loglevel, LOG_LEVEL_WARN) ? logfmt(LOGGER_STR_WARN, format, args) : false;
    va_end(args);
    return result;
}

bool Logger::error(const std::string& msg) const
{
	return ISBITSET(m_loglevel, LOG_LEVEL_ERROR) ? log(LOGGER_STR_ERROR, msg) : false;
}

bool Logger::error(const char * format, ...)  const
{
    ASSERT(format);
    if (! format)
        return false;

    va_list args;
    va_start(args, format);
    bool result = ISBITSET(m_loglevel, LOG_LEVEL_ERROR) ? logfmt(LOGGER_STR_ERROR, format, args) : false;
    va_end(args);
    return result;
}

bool Logger::fatal(const std::string& msg)  const
{
	return ISBITSET(m_loglevel, LOG_LEVEL_FATAL) ? log(LOGGER_STR_FATAL, msg) : false;
}

bool Logger::fatal(const char * format, ...)  const
{
    ASSERT(format);
    if (! format)
        return false;

    va_list args;
    va_start(args, format);
    bool result = ISBITSET(m_loglevel, LOG_LEVEL_FATAL) ? logfmt(LOGGER_STR_FATAL, format, args) : false;
    va_end(args);
    return result;
}

bool Logger::fail(const std::string& msg) const
{
	return log(LOGGER_STR_FAIL, msg);
}

bool Logger::fail(const char * format, ...) const
{
    ASSERT(format);
    if (! format)
        return false;

    va_list args;
    va_start(args, format);
    bool result = logfmt(LOGGER_STR_FAIL, format, args);
    va_end(args);
    return result;
}

bool Logger::success(const std::string& msg) const
{
	return log(LOGGER_STR_SUCCESS, msg);
}

bool Logger::success(const char * format, ...) const
{
    ASSERT(format);
    if (! format)
        return false;

    va_list args;
    va_start(args, format);
    bool result = logfmt(LOGGER_STR_SUCCESS, format, args);
    va_end(args);
    return result;
}

bool Logger::write(const std::string& msg) const
{
	return log("", msg);
}

bool Logger::write(const char * format, ...) const
{
    ASSERT(format);
    if (! format)
        return false;
    
    va_list args;
    va_start(args, format);
    bool result = logfmt("", format, args);
    va_end(args);
    return result;
}

bool Logger::endl(void) const
{
	return log("", "");
}

std::string& Logger::get_time() const
{
    unsigned char time[64];
    SYSTEMTIME tod;
    GetLocalTime(&tod);
    wsprintfA((char*)time, "%0.4d/%0.2d/%0.2d-%0.2d:%0.2d:%0.2d.%0.3d ",
        tod.wYear, tod.wMonth, tod.wDay,
        tod.wHour, tod.wMinute, tod.wSecond, tod.wMilliseconds );		
    m_time = (char*)time;
    return m_time;
}
