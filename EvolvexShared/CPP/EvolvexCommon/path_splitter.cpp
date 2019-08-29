#include "evolvex_common_lib.h"

std::string path_splitter::get_drive() const
{
    return std::string(m_drive);
}
std::string path_splitter::get_dir() const
{
    return std::string(m_dir);
}
std::string path_splitter::get_fname() const
{
    return std::string(m_fname);
}
std::string path_splitter::get_ext() const
{
    return std::string(m_ext);
}
path_splitter::path_splitter(const char* path_buffer)
{
    try
    {
        strcpy(m_path_buffer, path_buffer);
        //{LOG_DEBUG("%s(%d)", __FUNCTION__, __LINE__);}
        _splitpath(m_path_buffer, m_drive, m_dir, m_fname, m_ext);
        //{LOG_DEBUG("%s(%d)", __FUNCTION__, __LINE__);}
    }
    catch (...) 
    {
        LOG_ERROR("exception in %s", __FUNCTION__);
    }
}

path_splitter::~path_splitter()
{
    return;
}

