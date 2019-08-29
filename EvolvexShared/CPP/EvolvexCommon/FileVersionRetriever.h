#ifndef __EVOLVEX_COMMON_LIB_FILE_VERSION_RETRIEVER_H_
#define __EVOLVEX_COMMON_LIB_FILE_VERSION_RETRIEVER_H_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <Windows.h>
#include <string>

class CFileVersionRetriever
{
public:
    CFileVersionRetriever(void);
    ~CFileVersionRetriever(void);
    static bool GetFileVersion(const std::string& file_path, std::string& target);
};

#endif //__EVOLVEX_COMMON_LIB_FILE_VERSION_RETRIEVER_H_