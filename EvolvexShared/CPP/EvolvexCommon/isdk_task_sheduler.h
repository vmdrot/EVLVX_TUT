/**
 * Copyright (c) 2006 
 * Asempra Technologies, Inc.
 * 
 * @file        isdk_task_sheduler.h
 * @author      Yury Gaidenko
 * 
 * This file contains declarations of isdk_Task_Sheduler interface.
 */

#ifndef __ISDK_MSTASK_H__
#define __ISDK_MSTASK_H__
#pragma once
#include <string>
#include <vector>

#include "isdk_smart_ptr.h"

typedef struct isdk_task_info_t
{
    DWORD dwFlags;
    DWORD dwPriority;
    DWORD dwMaxRunTime;
    DWORD dwTriggersCnt;
    /*std::wstring */ wchar_t ApplicationName[256];// = { 0 };
    /*std::wstring*/ wchar_t Parameters[1024];// = { 0 };
    //std::wstring WorkingDir;
    /*std::wstring*/ wchar_t Comment[760];// = { 0 };
    //~isdk_task_info_t()
    //{
    //    LOG_DEBUG("%s", __FUNCTION__); 
    //}
    //std::vector<TASK_TRIGGER> aTriggers;
} isdk_task_info_t;
class isdk_Task_Sheduler;

typedef isdk_Smart_Ptr<isdk_Task_Sheduler> isdk_task_sheduler_t;

/// Declaration of the isdk_Task_Sheduler interface
class isdk_Task_Sheduler
{
public:

    class isdk_Task;

    typedef isdk_Smart_Ptr<isdk_Task> isdk_task_t;

    /// Declaration of the isdk_Task interface
    class isdk_Task
    {
    public:

        typedef unsigned isdk_task_flags_t;

        enum FLAG
        {
            FLAG_INTERACTIVE                    = 1<<0,
            FLAG_DELETE_WHEN_DONE               = 1<<1,
            FLAG_DISABLED                       = 1<<2,
            FLAG_START_ONLY_IF_IDLE             = 1<<3,
            FLAG_KILL_ON_IDLE_END               = 1<<4,
            FLAG_DONT_START_IF_ON_BATTERIES     = 1<<5,
            FLAG_KILL_IF_GOING_ON_BATTERIES     = 1<<6,
            FLAG_RUN_ONLY_IF_DOCKED             = 1<<7,
            FLAG_HIDDEN                         = 1<<8,
            FLAG_RUN_IF_CONNECTED_TO_INTERNET   = 1<<9,
            FLAG_RESTART_ON_IDLE_RESUME         = 1<<10,
            FLAG_SYSTEM_REQUIRED                = 1<<12,
            FLAG_RUN_ONLY_IF_LOGGED_ON          = 1<<13
        };

        class isdk_Trigger;

        typedef isdk_Smart_Ptr<isdk_Trigger> isdk_trigger_t;

        /// Declaration of the isdk_Trigger interface
        class isdk_Trigger
        {
        public:

            enum TYPE
            { 
                TYPE_ONCE             = 0,
                TYPE_DAILY            = 1,
                TYPE_WEEKLY           = 2,
                TYPE_MONTHLYDATE      = 3,
                TYPE_MONTHLYDOW       = 4,
                TYPE_ON_IDLE          = 5,
                TYPE_AT_SYSTEMSTART   = 6,
                TYPE_AT_LOGON         = 7
            };

            /// isdk_Trigger its interface, so declared virtual detructor
            virtual ~isdk_Trigger();

        protected:
            /// Protected constructor, object must be created by isdk_Task::create_trigger static method.
            isdk_Trigger();
        };

        /// isdk_Task its interface, so declared virtual detructor
        virtual ~isdk_Task();

        /// Assign application path to task object
        virtual bool set_application_name(const std::wstring& name) const = 0;
        
        /// Assign execution args for apllication assigned to the task
        virtual bool set_parameters(const std::wstring& params) const = 0;
        
        /// Set account information (user name and user password) for accept right execution of task
        virtual bool set_account_information(const std::wstring& user, const std::wstring& password) const = 0;

        /// Set account information under current user
        virtual bool set_account_information_as_local() const = 0;        

        /// Create task by specified type on the specified time
        virtual isdk_trigger_t create_trigger(isdk_Trigger::TYPE, __time64_t) const = 0;

        /// Stores configuration of task object in system
        virtual bool save_state() const = 0;
        
        virtual bool set_comments(const std::wstring& comments) const = 0;

    protected:
        /// Protected constructor, object must be created by isdk_Task_Sheduler::create static method.
        isdk_Task();
    };

    /// isdk_Task_Sheduler its interface, so declared virtual detructor
    virtual ~isdk_Task_Sheduler();

    /// Returns list of the task assigned for current user
    virtual void get_task_list(std::vector<std::wstring>& lst) const = 0;

    /// Removes task from list of the task in system
    virtual bool delete_task(const std::wstring& name) const = 0;

    /// Create new task with specified flags and name.
    virtual isdk_task_t create_task(isdk_Task::isdk_task_flags_t flags, const std::wstring& name) const = 0;
    
    virtual bool activate_task(const std::wstring& name) const = 0;
    /// runs a task 
    virtual bool run_task(const std::wstring& name) const = 0;

    virtual bool get_task_info(const std::wstring& name, isdk_task_info_t& info) const = 0;
    /// Create task sheduler
    static isdk_task_sheduler_t create();

protected:

    /// Protected constructor, object must be created by create static method.
    isdk_Task_Sheduler();
};

#endif //__ISDK_MSTASK_H__