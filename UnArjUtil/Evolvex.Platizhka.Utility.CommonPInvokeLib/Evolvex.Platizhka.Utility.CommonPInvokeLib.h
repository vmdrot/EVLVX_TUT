#ifndef __EVOLVEX_PLATIZHKA_UTILITY_COMMONPINVOKELIB_H_
#define __EVOLVEX_PLATIZHKA_UTILITY_COMMONPINVOKELIB_H_
// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_EXPORTS
#define EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_API __declspec(dllexport)
#else
#define EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_API __declspec(dllimport)
#endif

// This class is exported from the Evolvex.Platizhka.Utility.CommonPInvokeLib.dll
class EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_API CEvolvexPlatizhkaUtilityCommonPInvokeLib {
public:
	CEvolvexPlatizhkaUtilityCommonPInvokeLib(void);
	// TODO: add your methods here.
};

extern EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_API int nEvolvexPlatizhkaUtilityCommonPInvokeLib;

EVOLVEXPLATIZHKAUTILITYCOMMONPINVOKELIB_API int fnEvolvexPlatizhkaUtilityCommonPInvokeLib(void);

bool _stdcall cmp_files(const char* path1, const char* path2);

#endif