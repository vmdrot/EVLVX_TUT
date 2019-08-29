#include "FileVersionRetriever.h"

CFileVersionRetriever::CFileVersionRetriever(void)
{
}

CFileVersionRetriever::~CFileVersionRetriever(void)
{
}

bool CFileVersionRetriever::GetFileVersion(const std::string& file_path, std::string& target)
{
    DWORD dwHandle;
    DWORD dwVISz = GetFileVersionInfoSizeA((LPCSTR)file_path.c_str(), &dwHandle);
    HANDLE hMem = NULL;
    hMem = GlobalAlloc(GMEM_MOVEABLE, dwVISz);
    if(!hMem)
        return false;
    LPSTR lpstrVI = (char*)GlobalLock(hMem);
    if(!GetFileVersionInfoA((LPCSTR)file_path.c_str(), dwHandle, dwVISz,(LPVOID)lpstrVI))
    {
        GlobalUnlock(hMem);
        GlobalFree(hMem);
        return false;
    }
    
    UINT uVersionLen = 0;
    LPSTR lpVersion = NULL;
    if(!VerQueryValueA((LPVOID)lpstrVI,
       "\\StringFileInfo\\040904B0\\FileVersion",
        (LPVOID *)&lpVersion,
        (PUINT)&uVersionLen))
    {
        GlobalUnlock(hMem);
        GlobalFree(hMem);
        return false;
    }
    

    if (!(uVersionLen && lpVersion))
    {
        GlobalUnlock(hMem);
        GlobalFree(hMem);
        return false;
    }
    unsigned int v[4] = { 0 };
    char szVersionTrimmed[32] = { 0 };
    sscanf((char*)lpVersion, "%d, %d, %d, %d", &v[0], &v[1], &v[2], &v[3]);
    sprintf(szVersionTrimmed, "%d.%d.%d.%d", v[0], v[1], v[2], v[3]);
    target = (char*)szVersionTrimmed;
    GlobalUnlock(hMem);
    GlobalFree(hMem);
    return true;
}