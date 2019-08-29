/**
 * Copyright (c) 2006 
 * Asempra Technologies, Inc.
 * 
 * @file        isdk_toolhelp_snapshot.h
 * @author      Yury Gaidenko
 * 
 * This file contains implementation of isdk_Toolhelp_Snapshot_Impl class based on isdk_Toolhelp_Snapshot interface,
 * it wrapped MS Toolhelp32 API
 */

#include "isdk_toolhelp_snapshot.h"
#include "evolvex_common_lib.h"

isdk_Toolhelp_Snapshot::isdk_Toolhelp_Snapshot(){}
isdk_Toolhelp_Snapshot::~isdk_Toolhelp_Snapshot(){}

class isdk_Toolhelp_Snapshot_Impl : public isdk_Toolhelp_Snapshot
{
public:
    isdk_Toolhelp_Snapshot_Impl(snap_flags_t f, DWORD pid = 0)
    {
        const DWORD flags = translate_flags_to_toolhelp32API_flags(f);
        m_hSnap = ::CreateToolhelp32Snapshot(flags, pid);
		if(INVALID_HANDLE_VALUE == m_hSnap)
		{
			helper::LogExplainError(GetLastError(), __FUNCTION__);
		}
        m_flags = f;
    }

    virtual ~isdk_Toolhelp_Snapshot_Impl()
    {
        if(!is_valid())
            return;

        CloseHandle(m_hSnap);
        m_hSnap = NULL;
    }

    bool
    is_valid() const
    {
        return NULL != m_hSnap;
    }

    bool static
    snapped_heap_list(snap_flags_t flags){ return (SNAP_HEAP_LIST & flags) ? true : false; }
    bool static
    snapped_module(snap_flags_t flags){ return (SNAP_MODULE & flags) ? true : false; }
    bool static
    snapped_process(snap_flags_t flags){ return (SNAP_PROCESS & flags) ? true : false; }
    bool static
    snapped_thread(snap_flags_t flags){ return (SNAP_THREAD & flags) ? true : false; }

    static DWORD
    translate_flags_to_toolhelp32API_flags(snap_flags_t flags)
    {
        DWORD th_flags = 0;

        if(snapped_heap_list(flags))
            th_flags |= TH32CS_SNAPHEAPLIST;
        if(snapped_process(flags))
            th_flags |= TH32CS_SNAPPROCESS;
        if(snapped_thread(flags))
            th_flags |= TH32CS_SNAPTHREAD;
        if(snapped_module(flags))
            th_flags |= TH32CS_SNAPMODULE;

        return th_flags;
    }

    virtual snap_flags_t
    get_flags() const
    {
        return m_flags;
    }

    virtual bool
    get_heap_list(variant_list_t& list) const
    {
        if(!snapped_heap_list(m_flags))
            return false;

        return isdk_internal_get_list<HEAPLIST32, BOOL (WINAPI *)(HANDLE, LPHEAPLIST32)>
            (list, m_hSnap, Heap32ListFirst, Heap32ListNext, &isdk_IVariantFactory::create_heap_list_32);
    }

    virtual bool
    get_thread_list(variant_list_t& list) const
    {
        if(!snapped_thread(m_flags))
            return false;

        return isdk_internal_get_list<THREADENTRY32, BOOL (WINAPI *)(HANDLE, LPTHREADENTRY32)>
            (list, m_hSnap, Thread32First, Thread32Next, &isdk_IVariantFactory::create_thread_entry_32);
    }

    virtual bool
    get_module_list(variant_list_t& list) const
    {
        if(!snapped_module(m_flags))
            return false;

        return isdk_internal_get_list<MODULEENTRY32, BOOL (WINAPI *)(HANDLE, LPMODULEENTRY32)>
            (list, m_hSnap, Module32First, Module32Next, &isdk_IVariantFactory::create_module_entry_32);
    }

    virtual bool
    get_process_list(variant_list_t& list) const
    {
        if(!snapped_process(m_flags))
            return false;

        return isdk_internal_get_list<PROCESSENTRY32, BOOL (WINAPI *)(HANDLE, LPPROCESSENTRY32)>
            (list, m_hSnap, Process32First, Process32Next, &isdk_IVariantFactory::create_process_entry_32);
    }

private:

    template <typename T, typename F>
    static bool
    isdk_internal_get_list(variant_list_t& list, const HANDLE& hSnap, F f, F n, isdk_variant_t (*c)(const T&))
    {
        list.clear();

        T mod;
	    if((*f)(hSnap, &mod))
	    {
            list.push_back((*c)(mod));
		    while((*n)(hSnap, &mod)) 
                list.push_back((*c)(mod));
	    }

        return true;
    }

    HANDLE          m_hSnap;
    snap_flags_t    m_flags;
};

isdk_toolhelp_snapshot_t
isdk_Toolhelp_Snapshot::create(snap_flags_t f, DWORD pid)
{
    isdk_toolhelp_snapshot_t sp(new isdk_Toolhelp_Snapshot_Impl(f, pid));
    return ((isdk_Toolhelp_Snapshot_Impl*)(sp.get()))->is_valid() ? sp : isdk_toolhelp_snapshot_t(NULL);
}