#include "evolvex_common_lib.h"
#include "isdk_aux_task_sheduler.h"
#include <algorithm>
#include <time.h>
#include <atltime.h>
#include "isdk_task_sheduler.h"

bool
isdk_aux_is_task_exist(const std::vector<std::wstring>& tasks, const std::wstring& task_name)
{
    std::wstring w(task_name);
    w += L".job";
    
    return std::find(tasks.begin(), tasks.end(), w) != tasks.end();
}

bool
isdk_aux_list_tasks(std::vector<std::wstring>& tasks)
{
    isdk_task_sheduler_t sheduler = isdk_Task_Sheduler::create();
    if(!sheduler)
        return false;
    sheduler->get_task_list(tasks);
    return true;
}


bool
isdk_aux_add_once_execution_task(
    const std::wstring& task_name,
    const std::string&  path,
    const std::string&  args,
    __time64_t          st ,
    const std::string& runAsUser,
    const std::string& runAsPwd,
    const std::string& comments)
{   
    LOG_STARTED();
    CA2W wpath(path.c_str());
    CA2W wargs(args.c_str());

    std::wstring msde_uninstall_launcher(wpath);
    std::wstring msde_uninstall_launcher_args(wargs);

    isdk_task_sheduler_t sheduler = isdk_Task_Sheduler::create();
    if(!sheduler)
        return false;
    LOG_DEBUG_VISITED;
    std::vector<std::wstring> tasks;

    sheduler->get_task_list(tasks);
       
    if(isdk_aux_is_task_exist(tasks, task_name))
        if(!sheduler->delete_task(task_name.c_str()))
            return false;
    LOG_DEBUG_VISITED;
    isdk_Task_Sheduler::isdk_task_t task = sheduler->create_task(
        isdk_Task_Sheduler::isdk_Task::FLAG_DELETE_WHEN_DONE, task_name
    );

    task->set_application_name(msde_uninstall_launcher.c_str());
    task->set_parameters(msde_uninstall_launcher_args.c_str());

    if(runAsUser.length() == 0)
        task->set_account_information_as_local();
    else
        task->set_account_information((wchar_t*)_bstr_t(runAsUser.c_str()), (wchar_t*)_bstr_t(runAsPwd.c_str()));

    isdk_Task_Sheduler::isdk_Task::isdk_trigger_t trigger = task->create_trigger(
        isdk_Task_Sheduler::isdk_Task::isdk_Trigger::TYPE_ONCE, st
    );
    if(comments.length() > 0)
    {
        task->set_comments(std::wstring((wchar_t*)_bstr_t(comments.c_str())));
    }
    bool rslt = trigger ? task->save_state() : false;
    LOG_DEBUG_VISITED;
    if(!rslt)
        return rslt;
    LOG_FINISHED();
    return rslt;
}

bool
isdk_add_msde_uninstall_task()
{
    const HRESULT result = CoInitialize(NULL);
    
    if(SUCCEEDED(result))
    {
        CTime t(_time64(NULL));
        t += CTimeSpan(0, 0, 1, 0);

        __time64_t st = t.GetTime();

        const bool add_result = 
            isdk_aux_add_once_execution_task(L"MSDE Uninstall Task", "msde_uninstall_launcher.exe", "\"args\"", st);

        if(S_OK == result)
            CoUninitialize();

        return add_result;
    }

    return false;
}

bool
isdk_aux_show_task_info(const std::wstring& task_name,std::ostream& out, isdk_task_info_t* pinfo)
{
    isdk_task_sheduler_t scheduler = isdk_Task_Sheduler::create();
    if(!scheduler)
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    isdk_task_info_t info;
    if (!scheduler->get_task_info(task_name,info))
        return false;
    out << info;
    if(pinfo != NULL)
        *pinfo = info;
    return true;
}


std::ostream& operator<<(std::ostream& out, const isdk_task_info_t& task_info)
{
    out << '{' << "dwFlags: " << task_info.dwFlags << '(';
    DWORD aFlags[] = {TASK_FLAG_INTERACTIVE,TASK_FLAG_DELETE_WHEN_DONE, TASK_FLAG_DISABLED,TASK_FLAG_START_ONLY_IF_IDLE,TASK_FLAG_KILL_ON_IDLE_END,TASK_FLAG_DONT_START_IF_ON_BATTERIES,TASK_FLAG_KILL_IF_GOING_ON_BATTERIES,TASK_FLAG_RUN_ONLY_IF_DOCKED,TASK_FLAG_HIDDEN,TASK_FLAG_RUN_IF_CONNECTED_TO_INTERNET,TASK_FLAG_RESTART_ON_IDLE_RESUME,TASK_FLAG_SYSTEM_REQUIRED,TASK_FLAG_RUN_ONLY_IF_LOGGED_ON};
    char* aFlagsStr[] = {"TASK_FLAG_INTERACTIVE","TASK_FLAG_DELETE_WHEN_DONE", "TASK_FLAG_DISABLED","TASK_FLAG_START_ONLY_IF_IDLE","TASK_FLAG_KILL_ON_IDLE_END","TASK_FLAG_DONT_START_IF_ON_BATTERIES","TASK_FLAG_KILL_IF_GOING_ON_BATTERIES","TASK_FLAG_RUN_ONLY_IF_DOCKED","TASK_FLAG_HIDDEN","TASK_FLAG_RUN_IF_CONNECTED_TO_INTERNET","TASK_FLAG_RESTART_ON_IDLE_RESUME","TASK_FLAG_SYSTEM_REQUIRED","TASK_FLAG_RUN_ONLY_IF_LOGGED_ON"};
    int flagsCnt = sizeof(aFlags)/sizeof(aFlags[0]);
    int upFlagsCnt = 0;
    for(int i=0;i<flagsCnt;i++)
    {
        if((task_info.dwFlags & aFlags[i]) != aFlags[i])
            continue;
        if(upFlagsCnt > 0)
            out << " | ";
        out << aFlagsStr[i];
        upFlagsCnt++;
    }
    out << std::endl;
    out << "dwTriggersCnt: " << task_info.dwTriggersCnt << std::endl;
#if SKIP    
    out << " { Triggers:" << std::endl;
    for(int i=0;i<task_info.dwTriggersCnt;i++)
    {
        out << '[' << i << "]:" << task_info.aTriggers[i] << std::endl;
    }
    out << " } END Triggers" << std::endl;
#endif
    out << "ApplicationName:" << (char*)_bstr_t(task_info.ApplicationName) << std::endl;
    out << "Comment:" << (char*)_bstr_t(task_info.Comment) << std::endl;
    out << "Parameters:" << (char*)_bstr_t(task_info.Parameters) << std::endl;
    out << '}';
    return out;
}

std::ostream& operator<<(std::ostream& out, const TASK_TRIGGER& trigger)
{
    out << '{' << "cbTriggerSize:" << trigger.cbTriggerSize << std::endl;
    out << " cbTriggerSize:" << trigger.cbTriggerSize << std::endl;
    out << " Reserved1:" << trigger.Reserved1 << std::endl;
    out << " wBeginYear:" << trigger.wBeginYear << std::endl;
    out << " wBeginMonth:" << trigger.wBeginMonth << std::endl;
    out << " wBeginDay:" << trigger.wBeginDay << std::endl;
    out << " wEndYear:" << trigger.wEndYear << std::endl;
    out << " wEndMonth:" << trigger.wEndMonth << std::endl;
    out << " wEndDay:" << trigger.wEndDay << std::endl;
    out << " wStartHour:" << trigger.wStartHour << std::endl;
    out << " wStartMinute:" << trigger.wStartMinute << std::endl;
    out << " MinutesDuration:" << trigger.MinutesDuration << std::endl;
    out << " MinutesInterval:" << trigger.MinutesInterval << std::endl;
    out << " rgFlags:" << trigger.rgFlags << std::endl;
    out << " TriggerType:" << trigger.TriggerType << std::endl;
    out << " Type:" << trigger.Type << std::endl;
    out << " Reserved2:" << trigger.Reserved2 << std::endl;
    out << " wRandomMinutesInterval:" << trigger.wRandomMinutesInterval << std::endl;
    out << '}';
    return out;
}

std::ostream& operator<<(std::ostream& out, const TRIGGER_TYPE_UNION& tu)
{
    out << "  {" << std::endl;
    out << "Daily:" << tu.Daily.DaysInterval << std::endl;
    out << "Weekly:" << tu.Weekly.rgfDaysOfTheWeek << ", " <<  tu.Weekly.WeeksInterval << std::endl;
    out << "MonthlyDate:" << tu.MonthlyDate.rgfDays << ", " << tu.MonthlyDate.rgfMonths << std::endl;
    out << "MonthlyDOW:" << tu.MonthlyDOW.rgfDaysOfTheWeek << ", " << tu.MonthlyDOW.rgfMonths << ", " << tu.MonthlyDOW.wWhichWeek << std::endl;
    out << "  }" << std::endl;
    return out;
}

bool isdk_aux_run_task( const std::wstring& task_name)
{
    isdk_task_sheduler_t scheduler = isdk_Task_Sheduler::create();
    if(!scheduler)
    {
        LOG_ERROR_LOCATION;
        return false;
    }
    return scheduler->run_task(task_name);
}