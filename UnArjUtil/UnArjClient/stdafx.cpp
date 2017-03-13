// stdafx.cpp : source file that includes just the standard includes
// UnArjClient.pch will be the pre-compiled header
// stdafx.obj will contain the pre-compiled type information

#include "stdafx.h"
#include "../UnArjLib/UnArj.h"

int unarj_extract(bool bQuietMode, char* arc_name, char* file_in_arc, char* save_as)
{
    UnArj uArj;
    uArj.set_Quiet(bQuietMode);

    if(strlen(save_as) == 0)
    {
        char* argv[] = {"", "e",arc_name, file_in_arc};
        return uArj.do_main(4,argv);
    }
    if(strlen(file_in_arc) == 0)
    {
        char* argv[] = {"", "e",arc_name};
        return uArj.do_main(3,argv);
    }
    else
    {
        char* argv[] = {"", "e",arc_name, file_in_arc, save_as};
        return uArj.do_main(5,argv);
    }
}

// TODO: reference any additional headers you need in STDAFX.H
// and not in this file
