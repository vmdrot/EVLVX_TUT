// Evolvex.Platizhka.Utility.CommonPInvokeLib.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "Evolvex.Platizhka.Utility.CommonPInvokeLib.h"
#include <evolvex_common_lib.h>

#ifdef _MANAGED
#pragma managed(push, off)
#endif

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
    return TRUE;
}

#ifdef _MANAGED
#pragma managed(pop)
#endif

// This is an example of an exported variable
EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_API int nEvolvexPlatizhkaUtilityCommonPInvokeLib=0;

// This is an example of an exported function.
EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_API int fnEvolvexPlatizhkaUtilityCommonPInvokeLib(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see Evolvex.Platizhka.Utility.CommonPInvokeLib.h for the class definition
CEvolvexPlatizhkaUtilityCommonPInvokeLib::CEvolvexPlatizhkaUtilityCommonPInvokeLib()
{
	return;
}


bool _stdcall cmp_files(const char* path1, const char* path2, bool bBinary)
{
    //rslt_code_t helper::compare_files(const std::string& file1, const std::string& file2, bool& equal, bool binary)
    bool rslt;
    if(RSLT_SUCCESS != helper::compare_files(path1,path2,rslt,bBinary))
        return false;
    return rslt;
}