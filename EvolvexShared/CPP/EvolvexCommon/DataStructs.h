#ifndef __SNIPER_DATA_STRUCTS_H_
#define __SNIPER_DATA_STRUCTS_H_
#include "evolvex_common_lib.h"
#include <exdisp.h>
#include <mshtml.h>

typedef struct tagIEInfo
{
    int ProcessID;
    HWND hwndIE;
    IWebBrowser2* pBrowserItfc;
    IHTMLDocument2* pDoc;
    std::string BuyerID;
    std::string BuyerPwd;
    std::string loginUrl;
} IEInfo;
#endif