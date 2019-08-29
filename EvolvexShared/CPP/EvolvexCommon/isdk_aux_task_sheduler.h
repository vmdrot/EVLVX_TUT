#ifndef __ISDK_AUX_TASK_SHEDULER_H__
#define __ISDK_AUX_TASK_SHEDULER_H__
#include "evolvex_common_lib.h"
#include <string>
#include <vector>
#include <iostream>
#include "isdk_task_sheduler.h"
#include <mstask.h>

bool
isdk_aux_is_task_exist(const std::vector<std::wstring>& tasks, const std::wstring& task_name);

bool
isdk_aux_list_tasks(std::vector<std::wstring>& tasks);

bool
isdk_aux_add_once_execution_task(
    const std::wstring& task_name,
    const std::string&  path,
    const std::string&  args,
    time_t              st,
    const std::string& runAsUser = "",
    const std::string& runAsPwd = "", const std::string& comments = "");

bool
isdk_add_msde_uninstall_task();

bool isdk_aux_show_task_info(const std::wstring& task_name,std::ostream& out, isdk_task_info_t* pinfo = NULL);

std::ostream& operator<<(std::ostream& out, const isdk_task_info_t& task_info);

std::ostream& operator<<(std::ostream& out, const TASK_TRIGGER& trigger);

std::ostream& operator<<(std::ostream& out, const TRIGGER_TYPE_UNION& tu);

bool isdk_aux_run_task( const std::wstring& task_name);

#endif //__ISDK_AUX_TASK_SHEDULER_H__