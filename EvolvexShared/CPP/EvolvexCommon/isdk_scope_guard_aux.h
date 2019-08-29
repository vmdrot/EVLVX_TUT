/**
* Copyright (c) 2006
* Asempra Technologies, Inc.
* 
* @file        isdk_scope_guard_aux.h
* @author      Yuri Gaidenko
* 
* $Id: isdk_scope_guard_aux.h,v 1.1 2006/08/30 11:53:00 vdrotenko Exp $
* 
* Customized resources cleanup controllers
* The functions are extensible; most suitable to control the closing of Win API
* resources.
* 
*/

#ifndef __ISDK_SCOPE_GUARD_AUX_H__
#define __ISDK_SCOPE_GUARD_AUX_H__
#include "evolvex_common_lib.h"
#include "isdk_scope_guard.h"
#include <wininet.h>
#include "RyeolHttpClient.h"
/// global inline  isdk_scope_guard_fclose
///
/// A customized function that constructs an isdk_Scope_Guard_Proc object behind the 
/// scenes and lets him (i.e. this object) manage a file handle; 
///
/// @param f    [in]  FILE **  - pointer to a file handle pointer to be closed on exiting the scope
/// @param sg   [in]  isdk_scope_guard_ptr_t  [=0]  - scope guard auxiliary object pointer
///
/// @return values:
/// guard object pointer

inline isdk_scope_guard_ptr_t 
isdk_scope_guard_fclose(FILE** f, isdk_scope_guard_ptr_t sg = NULL)
{
	return isdk_Scope_Guard_Proc<int (*)(FILE*), FILE*>::create(fclose, f, sg);
}

inline isdk_scope_guard_ptr_t 
isdk_scope_guard_free(void** p, isdk_scope_guard_ptr_t sg = NULL)
{
	return isdk_Scope_Guard_Proc<void (*)(void*), void*>::create(free, p, sg);
}

inline isdk_scope_guard_ptr_t 
isdk_scope_guard_netapi_buffer_free(LPVOID* info, isdk_scope_guard_ptr_t sg = NULL)
{
    return isdk_Scope_Guard_Proc<NET_API_STATUS (NET_API_FUNCTION *)(LPVOID), LPVOID>::create(NetApiBufferFree, info, sg);
}

//inline isdk_scope_guard_ptr_t 
//isdk_scope_guard_close_inet(void** p, isdk_scope_guard_ptr_t sg = NULL)
//{
//    return isdk_Scope_Guard_Proc<BOOLAPI (*)(HINTERNET), HINTERNET>::create(InternetCloseHandle, p, sg);
//}

//inline isdk_scope_guard_ptr_t 
//isdk_scope_guard_globalfree(void** p, isdk_scope_guard_ptr_t sg = NULL)
//{
//	return isdk_Scope_Guard_Proc<void (*)(HGLOBAL), HGLOBAL>::create(GlobalFree, p, sg);
//}


/// Usage example
/*
* func1()
* {
*     FILE* file = NULL;
*     void* mem = malloc(1024);
* 
*     SC_HANDLE schSCManager = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);
* 
* 
*
*     so, on cleanup should be inoking by one scope:
*     1) fclose(file);
*     2) free(mem);
*     3) CloseServiceHandle(schSCManager);
*
*     {
*     
*                 const isdk_scope_guard_t sg(
*                     isdk_scope_guard_fclose(&file, 
*                     isdk_scope_guard_free(&mem, 
*                     isdk_scope_guard_close_service_handle(&schSCManager)
*                     )
*                     )
*                     );
*     
*                 file = fopen(FILE_NAME, "rb");
*     }
* 
*<--- Scope Guarg should cleanup all resources; NOTE: in our order: 1) -> 2) -> 3)
* }
*/
#endif //__ISDK_SCOPE_GUARD_AUX_H__