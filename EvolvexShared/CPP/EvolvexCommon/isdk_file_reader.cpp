#include "isdk_file_reader.h"
#include <memory.h>
#include <io.h>

isdk_File_Reader::isdk_File_Reader(){}

class isdk_File_Reader_Impl: public isdk_File_Reader
{
private:
    char* m_Buffer;
    size_t m_FileSize;
    bool m_fValid;

public:
    isdk_File_Reader_Impl(const std::string& file_path, bool binary_mode);
    ~isdk_File_Reader_Impl();
    size_t get_file_size()
    {
        return m_FileSize;
    }
    const char* get_contents()
    {
        return m_Buffer;
    }
    void get_contents(std::string& target)
    {
        target = m_Buffer;
    }
    bool get_contents(char* target)
    {
        void* rslt;
        try
        {
            if (NULL == target)
            {
                return false;
            }
            rslt = memcpy((void*)target, (const void*)m_Buffer, m_FileSize);
            if (NULL == rslt )
            {
                return false;
            }
            
        }
        catch(...)
        {
            return false;
        }
        return true;
    }
    bool is_valid()
    {
        return m_fValid;
    }
    bool char_at(size_t index, char& target)
    {
        if (m_fValid && index < m_FileSize)
        {
            target = m_Buffer[index];
            return true;
        }
        else
        {
            return false;
        }
        
    }
};

isdk_File_Reader_Impl::isdk_File_Reader_Impl(const std::string& file_path, bool binary_mode): m_Buffer(NULL), m_FileSize(0), m_fValid(false)
{
    try
    {
        long filesize;
        FILE* fp = NULL;
        const isdk_scope_guard_t sg(isdk_scope_guard_fclose(&fp));
        if(-1 == _access(file_path.c_str(), 0))
            return;
        char mode[] = "rt";
        if (binary_mode)
            strcpy(mode, "rb");
        fp = fopen(file_path.c_str(), mode);
        if (NULL == fp)
            return ;

        // get the size of a content
        if (fseek(fp, 0, SEEK_END)) 
        {
            return;
        }
        filesize = ftell(fp);
        if (filesize < 1)
        {
            return;
        }
        m_FileSize = (size_t)filesize;
        if (fseek(fp, 0, SEEK_SET)) 
        { 
            return;
        }
        m_Buffer = (char*)calloc(m_FileSize + 1, 1);
        if(NULL == m_Buffer)
        {
            return;
        }
        if(m_FileSize != fread(m_Buffer, 1, m_FileSize, fp))
        {
            free(m_Buffer);
            m_Buffer = NULL;
            return;
        }
        m_fValid = true;
        return;
    }
    catch (...) 
    {
        return;
    }
}

isdk_File_Reader_Impl::~isdk_File_Reader_Impl()
{
    if (NULL != m_Buffer)
    {
        free(m_Buffer);
        m_Buffer = NULL;
    }
}

isdk_file_reader_t isdk_File_Reader::create(const std::string& file_path, bool binary_mode)
{
    try
    {
        isdk_file_reader_t sp(new isdk_File_Reader_Impl(file_path, binary_mode));
        if(!sp)
            return sp;
        return (((isdk_File_Reader_Impl*)(sp.get()))->is_valid() ? sp : isdk_file_reader_t(NULL));
    }
    catch (...)
    {
        return isdk_file_reader_t(NULL);
    }
    
}