#ifndef _PATH_SPLITTER_H__
#define _PATH_SPLITTER_H__
#include "evolvex_common_lib.h"

class path_splitter
{
public:
    std::string get_drive() const;
    std::string get_dir() const;
    std::string get_fname() const;
    std::string get_ext() const;
    path_splitter(const char* path_buffer);
    ~path_splitter();
private:
    char m_path_buffer[_MAX_PATH+1];
    char m_drive[_MAX_DRIVE+1];
    char m_dir[_MAX_DIR+1];
    char m_fname[_MAX_FNAME+1];
    char m_ext[_MAX_EXT+1];
};

#endif