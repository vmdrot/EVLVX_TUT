/**
 * Copyright (c) 2006 
 * Asempra Technologies, Inc.
 * 
 * @file        isdk_task_sheduler.cpp
 * @author      Yury Gaidenko
 * 
 * This file contains implementation of isdk_Task_Sheduler interface,
 * it warapped the MS Task Scheduler API.
 */
#include "evolvex_common_lib.h"
#include "isdk_task_sheduler.h"

#include <atlbase.h>
#include <atlcom.h>
#include <atlconv.h>
#include <mstask.h>

#include <time.h>

/// Empty protected constructors and destructors
isdk_Task_Sheduler::isdk_Task_Sheduler(){}
isdk_Task_Sheduler::~isdk_Task_Sheduler(){}
isdk_Task_Sheduler::isdk_Task::isdk_Task(){}
isdk_Task_Sheduler::isdk_Task::~isdk_Task(){}
isdk_Task_Sheduler::isdk_Task::isdk_Trigger::isdk_Trigger(){}
isdk_Task_Sheduler::isdk_Task::isdk_Trigger::~isdk_Trigger(){}

/// Declaration of the isdk_Task_Sheduler_Impl class, based on isdk_Task_Sheduler interface,
/// it wrapped ITaskScheduler COM interface.
class isdk_Task_Sheduler_Impl : public isdk_Task_Sheduler
{
public:

    /// Declaration of the isdk_Task_Impl sub-class, based on isdk_Task interface,
    /// it wrapped ITask COM interface.
    class isdk_Task_Impl : public isdk_Task
    {
    public:

        /// Declaration of the isdk_Trigger_Impl sub-class, based on isdk_Trigger interface,
        /// it wrapped ITrigger COM interface.
        class isdk_Trigger_Impl : public isdk_Trigger
        {
        public:
            /// Assigns ITaskTrigger pointer to isdk_Trigger_Impl member
            isdk_Trigger_Impl(CComPtr<ITaskTrigger> p) : m_pTaskTrigger(p){}
            
            /// Returns mstask (TASK_TRIGGER_TYPE) type from isdk_Trigger type
            static TASK_TRIGGER_TYPE
            translate_type(TYPE type)
            {
                ATLASSERT(type >= TYPE_ONCE && type <= TYPE_AT_LOGON);

                switch(type)
                {
                    case TYPE_ONCE:             return TASK_TIME_TRIGGER_ONCE;
                    case TYPE_DAILY:            return TASK_TIME_TRIGGER_DAILY;
                    case TYPE_WEEKLY:           return TASK_TIME_TRIGGER_WEEKLY;
                    case TYPE_MONTHLYDATE:      return TASK_TIME_TRIGGER_MONTHLYDATE;
                    case TYPE_MONTHLYDOW:       return TASK_TIME_TRIGGER_MONTHLYDOW;
                    case TYPE_ON_IDLE:          return TASK_EVENT_TRIGGER_ON_IDLE;
                    case TYPE_AT_SYSTEMSTART:   return TASK_EVENT_TRIGGER_AT_SYSTEMSTART;
                    case TYPE_AT_LOGON:         return TASK_EVENT_TRIGGER_AT_LOGON;
                }
            }

        private:
            CComPtr<ITaskTrigger> m_pTaskTrigger;
        };

        /// Assigns ITask pointer, flags to isdk_Task_Impl members;
        isdk_Task_Impl(CComPtr<ITask> p, int f) : m_pTask(p), m_flags(f){}

        /// Returns mstask flags from isdk_Task flags
        static DWORD
        translate_flags(isdk_task_flags_t f)
        {
            isdk_task_flags_t result = 0;

            if(FLAG_INTERACTIVE & f)
                result |= TASK_FLAG_INTERACTIVE;

            if(FLAG_DELETE_WHEN_DONE & f)
                result |= TASK_FLAG_DELETE_WHEN_DONE;

            if(FLAG_DISABLED & f)
                result |= TASK_FLAG_DISABLED;

            if(FLAG_START_ONLY_IF_IDLE & f)
                result |= TASK_FLAG_START_ONLY_IF_IDLE;

            if(FLAG_KILL_ON_IDLE_END & f)
                result |= TASK_FLAG_KILL_ON_IDLE_END;

            if(FLAG_DONT_START_IF_ON_BATTERIES & f)
                result |= TASK_FLAG_DONT_START_IF_ON_BATTERIES;

            if(FLAG_KILL_IF_GOING_ON_BATTERIES & f)
                result |= TASK_FLAG_KILL_IF_GOING_ON_BATTERIES;

            if(FLAG_RUN_ONLY_IF_DOCKED & f)
                result |= TASK_FLAG_RUN_ONLY_IF_DOCKED;

            if(FLAG_HIDDEN & f)
                result |= TASK_FLAG_HIDDEN;

            if(FLAG_RUN_IF_CONNECTED_TO_INTERNET & f)
                result |= TASK_FLAG_RUN_IF_CONNECTED_TO_INTERNET;

            if(FLAG_RESTART_ON_IDLE_RESUME & f)
                result |= TASK_FLAG_RESTART_ON_IDLE_RESUME;

            if(FLAG_SYSTEM_REQUIRED & f)
                result |= TASK_FLAG_SYSTEM_REQUIRED;

            if(FLAG_RUN_ONLY_IF_LOGGED_ON & f)
                result |= TASK_FLAG_RUN_ONLY_IF_LOGGED_ON;

            return result;
        }

        virtual bool
        set_application_name(const std::wstring& name) const
        {
            return SUCCEEDED(m_pTask->SetApplicationName(name.c_str()));
        }

        virtual bool
        set_parameters(const std::wstring& params) const
        {
            return SUCCEEDED(m_pTask->SetParameters(params.c_str()));
        }

        virtual bool
        set_account_information(const std::wstring& user, const std::wstring& password) const
        {
            return SUCCEEDED(m_pTask->SetAccountInformation(user.c_str(), password.c_str()));
        }

        virtual bool
        set_account_information_as_local() const
        {
            return SUCCEEDED(m_pTask->SetAccountInformation(L"", NULL));
        }

        virtual isdk_trigger_t
        create_trigger(isdk_Trigger::TYPE type, __time64_t st) const
        {
            std::string strTime4Dbg;
            helper::time_to_str(CTime(st),strTime4Dbg);
            LOG_DEBUG("%s::st = %s", __FUNCTION__, strTime4Dbg.c_str());
            CComPtr<ITaskTrigger> pITaskTrigger;
            WORD piNewTrigger;
            if(FAILED(m_pTask->CreateTrigger( &piNewTrigger, &pITaskTrigger)))
                return isdk_trigger_t(NULL);

            TASK_TRIGGER pTrigger;
            memset(&pTrigger, 0, sizeof (TASK_TRIGGER));
                
            pTrigger.cbTriggerSize  = sizeof(TASK_TRIGGER); 

            struct tm* tPtr = _localtime64(&st);
            
            pTrigger.wBeginDay      = tPtr->tm_mday;
            pTrigger.wBeginMonth    = tPtr->tm_mon + 1;
            pTrigger.wBeginYear     = 1900 + tPtr->tm_year;
            pTrigger.wStartHour     = tPtr->tm_hour;
            pTrigger.wStartMinute   = tPtr->tm_min;

            pTrigger.TriggerType    = isdk_Trigger_Impl::translate_type(type);

            if(FAILED(pITaskTrigger->SetTrigger(&pTrigger)))
                return isdk_trigger_t(NULL);

            return isdk_trigger_t(new isdk_Trigger_Impl(pITaskTrigger));
        }

        virtual bool
        save_state() const
        {
            CComQIPtr<IPersistFile> pPersistFile(m_pTask);

            return FAILED(pPersistFile->Save(NULL, TRUE)) ? false : true;
        }
        
        virtual bool 
        set_comments(const std::wstring& comments) const
        {
            return (SUCCEEDED(m_pTask->SetComment((LPCWSTR)comments.c_str())) ? true : false);
        }
    private:
        CComPtr<ITask>  m_pTask;
        int             m_flags;
    };

    isdk_Task_Sheduler_Impl() : m_pISched(NULL){}

    virtual ~isdk_Task_Sheduler_Impl(){}

    /// Creates COM ITaskScheduler instance
    bool
    init()
    {
        return SUCCEEDED(m_pISched.CoCreateInstance(CLSID_CTaskScheduler));
    }

    virtual bool
    delete_task(const std::wstring& name) const
    {
        return SUCCEEDED(m_pISched->Delete(name.c_str()));
    }

    virtual isdk_task_t
    create_task(isdk_Task::isdk_task_flags_t flags, const std::wstring& name) const
    {
        CComPtr<ITask> pTask;
        if(FAILED(m_pISched->NewWorkItem(name.c_str(), CLSID_CTask, IID_ITask, (IUnknown**)&pTask)))
            return isdk_task_t(NULL);

        DWORD f;
        if(FAILED(pTask->GetFlags(&f)))
            return isdk_task_t(NULL);

        f |= isdk_Task_Impl::translate_flags(flags);
        //f |= TASK_FLAG_DONT_START_IF_ON_BATTERIES;
        //f |= TASK_FLAG_KILL_IF_GOING_ON_BATTERIES;

        if(FAILED(pTask->SetFlags(f)))
            return isdk_task_t(NULL);

        return isdk_task_t(new isdk_Task_Impl(pTask, flags));
    }

    virtual void
    get_task_list(std::vector<std::wstring>& lst) const
    {
        CComPtr<IEnumWorkItems> pItems;
	    m_pISched->Enum(&pItems);
	    LPWSTR* lpwszNames;
	    DWORD dwFetchedTasks = 0;
	    while (SUCCEEDED(pItems->Next(1, &lpwszNames, &dwFetchedTasks)) && (dwFetchedTasks != 0))
	    {
		    while (dwFetchedTasks)
		    {
                --dwFetchedTasks;

                lst.push_back(lpwszNames[dwFetchedTasks]);

                CoTaskMemFree(lpwszNames[dwFetchedTasks]);                 
		    }
            CoTaskMemFree(lpwszNames);
        }
    }

    virtual bool activate_task(const std::wstring& name) const
    {
        CComPtr<IUnknown> pItem;
        return SUCCEEDED(m_pISched->Activate(name.c_str(),IID_ITask, &pItem));
    }
    
    virtual bool run_task(const std::wstring& name) const
    {
        CComPtr<IUnknown> pItem;
        if(FAILED(m_pISched->Activate(name.c_str(),IID_ITask, &pItem)))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        CComPtr<ITask> pTask;
        if(FAILED(pItem->QueryInterface(IID_ITask, (void**)&pTask)))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        return (SUCCEEDED(pTask->Run()));
    }

    virtual bool get_task_info(const std::wstring& name, isdk_task_info_t& info) const
    {
        CComPtr<IUnknown> pItem;
        HRESULT hr = m_pISched->Activate(name.c_str(),IID_ITask, &pItem);
        if (FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        CComPtr<ITask> pTask;
        hr = pItem->QueryInterface(IID_ITask, (void**)&pTask);
        if (FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        DWORD dwFlags;
        hr = pTask->GetFlags(&dwFlags);
        if (FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        info.dwFlags = dwFlags;
        WORD dwTriggersCnt;
        hr = pTask->GetTriggerCount(&dwTriggersCnt);
        if (FAILED(hr))
        {
            LOG_ERROR_LOCATION;
            return false;
        }
        info.dwTriggersCnt = dwTriggersCnt;
#if SKIP
        for(int i = 0;i<dwTriggersCnt;i++)
        {
            CComPtr<ITaskTrigger> pTrigger;
            hr = pTask->GetTrigger((WORD)i,&pTrigger);
            if (FAILED(hr))
            {
                LOG_ERROR_LOCATION;
                return false;
            }
            TASK_TRIGGER tt;
            hr = pTrigger->GetTrigger(&tt);
            if (FAILED(hr))
            {
                LOG_ERROR_LOCATION;
                return false;
            }
            info.aTriggers.push_back(tt);
        }
#endif
        {
            LPWSTR wszAppName; 
            pTask->GetApplicationName(&wszAppName);
            wcscpy(info.ApplicationName,wszAppName);
            CoTaskMemFree((LPVOID) wszAppName);
        }
        {
            LPWSTR wszCmdLnParams;
            pTask->GetParameters(&wszCmdLnParams);
            wcscpy(info.Parameters,wszCmdLnParams);
            CoTaskMemFree((LPVOID)wszCmdLnParams);
        }
        {
            LPWSTR wszComment;
            pTask->GetComment(&wszComment);
            wcscpy(info.Comment, wszComment);
            CoTaskMemFree((LPVOID)wszComment);
        }
        return true;
    }
    
private:
    CComPtr<ITaskScheduler> m_pISched;
};

isdk_task_sheduler_t
isdk_Task_Sheduler::create()
{
    isdk_task_sheduler_t sp(new isdk_Task_Sheduler_Impl);
    return ((isdk_Task_Sheduler_Impl*)sp.get())->init() ? sp : isdk_task_sheduler_t(NULL);
}
