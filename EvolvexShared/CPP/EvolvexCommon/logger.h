#ifndef _LOGGER_H_
#define _LOGGER_H_

#pragma once

#include "evolvex_common_lib.h"
#include "defs.h"
//#include "./inst_types.h"
//#include "./inst_defs.h"
//#include "./inst_utils.h"

//////////////////////////////////////////////////////////////////////////
// Logger interface
//////////////////////////////////////////////////////////////////////////

/**
* Logger
*/
class Logger
{
public:
	enum log_level_t {
		LOG_LEVEL_NONE   = 0x00000000L,
        LOG_LEVEL_FATAL  = 0x00000001L, 
		LOG_LEVEL_ERROR  = 0x00000002L, 
		LOG_LEVEL_WARN   = 0x00000004L, 
		LOG_LEVEL_INFO   = 0x00000008L, 
		LOG_LEVEL_DEBUG  = 0x00000010L,
		LOG_LEVEL_ALL    = 0xFFFFFFFFL
	};
	
	Logger(void);
	~Logger(void);
	
	// initializes logging into a file
	bool init(const std::string& filename, long log_level = LOG_LEVEL_ALL);
	
    // set log-level (note - log level is a bitmask)
    void set_level(long log_level_flags);
	
	// classic methods: log-level dependent output.
	bool debug(const std::string& msg) const;
	bool info(const std::string& msg) const;
	bool warn(const std::string& msg) const;
	bool error(const std::string& msg) const;
	bool fatal(const std::string& msg) const;
    
    bool debug(const char * format, ...) const;
    bool info(const char * format, ...) const;
    bool warn(const char * format, ...) const;
    bool error(const char * format, ...) const;
    bool fatal(const char * format, ...) const;
	
	// additional methods: do not depend on log-level.
	bool fail(const std::string& msg) const;
    bool fail(const char * format, ...) const;
	bool success(const std::string& msg) const;
    bool success(const char * format, ...) const;
	bool write(const std::string& msg) const;
    bool write(const char * format, ...) const;
	bool endl(void) const;
    
private:
	mutable std::string m_time;
    mutable std::fstream m_file;
	long m_loglevel;

	std::string& get_time() const;

	// classic message logging internals
	bool log(const std::string& prefix, const std::string& msg) const;
    bool logfmt(const std::string& prefix, const char * format, va_list args) const;
	
	// closes the log file
	bool close(void);
};


// debug definitions
#define LOGGER     if(helper::get_logger())helper::get_logger()
#define LOG_DEBUG       LOGGER->debug
#define LOG_ERROR       LOGGER->error
#define LOG_INFO        LOGGER->info
#define LOG_WARN        LOGGER->warn
#define LOG_FAIL        LOGGER->fail
#define LOG_SUCCESS     LOGGER->success

#define LOG_STARTED()   LOG_DEBUG(__FUNCTION__ " : started")
#define LOG_FINISHED()  LOG_DEBUG(__FUNCTION__ " : finished")
#define endOnError(errCode) if (FAILED(errCode)) {helper::LogExplainError(errCode, __FUNCTION__); goto end;}
#define endOnError1(errCode, stage) if (FAILED(errCode)) {helper::LogExplainErrorStage(errCode, __FUNCTION__, stage); goto end;}

/* frequently used logging shortcuts */
#define LOG_EXCEPTION        LOG_ERROR("%s(%d) - exception", __FUNCTION__, __LINE__)
#define LOG_ERROR_LOCATION   LOG_ERROR("%s(%d)",__FUNCTION__, __LINE__)
#define LOG_DEBUG_VISITED    LOG_DEBUG("%s(%d)", __FUNCTION__, __LINE__)

#endif//_LOGGER_H_