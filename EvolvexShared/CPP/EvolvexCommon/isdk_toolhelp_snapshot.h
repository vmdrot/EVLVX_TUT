/**
 * Copyright (c) 2006 
 * Asempra Technologies, Inc.
 * 
 * @file        isdk_toolhelp_snapshot.h
 * @author      Yury Gaidenko
 * 
 * This file contains declarations of isdk_Toolhelp_Snapshot interface.
 */

#ifndef __ISDK_TOOLHELP_SNAPSHOT_H__
#define __ISDK_TOOLHELP_SNAPSHOT_H__

#include <vector>
#include <string>

#include "isdk_variant.h"

class isdk_Toolhelp_Snapshot;

typedef isdk_Smart_Ptr<isdk_Toolhelp_Snapshot> isdk_toolhelp_snapshot_t;

class isdk_Toolhelp_Snapshot
{
public:
    enum SNAP
    {
        SNAP_HEAP_LIST  = 1<<0,
        SNAP_PROCESS    = 1<<1,
        SNAP_THREAD     = 1<<2,
        SNAP_MODULE     = 1<<3,
        SNAP_ALL        = SNAP_HEAP_LIST | SNAP_PROCESS | SNAP_THREAD | SNAP_MODULE
    };

    typedef unsigned snap_flags_t;

    virtual ~isdk_Toolhelp_Snapshot();

    typedef std::vector<isdk_variant_t> variant_list_t;

    /// Gets heap list if Snapshot was created with SNAP_HEAP_LIST flag
    virtual bool get_heap_list(variant_list_t&)     const = 0;
    
    /// Gets heap list if Snapshot was created with SNAP_THREAD flag
    virtual bool get_thread_list(variant_list_t&)   const = 0;

    /// Gets heap list if Snapshot was created with SNAP_MODULE flag
    virtual bool get_module_list(variant_list_t&)   const = 0;

    /// Gets heap list if Snapshot was created with SNAP_PROCESS flag
    virtual bool get_process_list(variant_list_t&)  const = 0;

    /// Gets flags with which was created Snapshot
    virtual snap_flags_t get_flags() const = 0;

    static isdk_toolhelp_snapshot_t create(snap_flags_t flags = SNAP_ALL, DWORD pid = 0);

protected:
    /// Protected constructor, object must be created by create static method.
    isdk_Toolhelp_Snapshot();
};

#endif //__ISDK_TOOL_HELP_API_H__