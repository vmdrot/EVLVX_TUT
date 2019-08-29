#ifndef __EVOLVEX_COMMON_LIB_PROCESS_FILTERER_H_
#define __EVOLVEX_COMMON_LIB_PROCESS_FILTERER_H_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <string> 
#include <vector>
#include <windows.h>

class CProcessFilterer
{
private:
	std::string m_sFilterByImgNameLower;
	std::vector<DWORD> m_filteredPsIds;
	void DoEnum();
	void EnumEntryWorker(DWORD processID);
public:
	CProcessFilterer(const std::string& imgName);
	~CProcessFilterer(void);
	std::vector<DWORD>& get_filtered_process_ids();
	DWORD DetectNew(const std::vector<DWORD> oldLst);

};
#endif //__EVOLVEX_COMMON_LIB_PROCESS_FILTERER_H_