#include "ProcessFilterer.h"
#include <windows.h>
#include <psapi.h>
#include "evolvex_common_lib.h"
#include <algorithm>


CProcessFilterer::CProcessFilterer(const std::string& imgName)
{
	str_lowercase(imgName, m_sFilterByImgNameLower);
	DoEnum();
}

CProcessFilterer::~CProcessFilterer(void)
{
}

void CProcessFilterer::EnumEntryWorker(DWORD processID)
{
    char szProcessName[MAX_PATH] = "<unknown>";
    HANDLE hProcess = OpenProcess( PROCESS_QUERY_INFORMATION |
                                   PROCESS_VM_READ,
                                   FALSE, processID );

    if (NULL != hProcess )
    {
        HMODULE hMod;
        DWORD cbNeeded;

        if ( EnumProcessModules( hProcess, &hMod, sizeof(hMod), 
             &cbNeeded) )
        {
            GetModuleBaseNameA( hProcess, hMod, szProcessName, 
                               sizeof(szProcessName)/sizeof(char) );
        }
    }


    CloseHandle( hProcess );
	std::string strProcssNm(szProcessName);
	std::string strProcsNmLower;
	str_lowercase(strProcssNm, strProcsNmLower);
	if(strProcsNmLower == m_sFilterByImgNameLower)
		m_filteredPsIds.push_back(processID);
}
void CProcessFilterer::DoEnum()
{
   // Get the list of process identifiers.

    DWORD aProcesses[1024], cbNeeded, cProcesses;
    unsigned int i;

    if ( !EnumProcesses( aProcesses, sizeof(aProcesses), &cbNeeded ) )
        return;

    // Calculate how many process identifiers were returned.

    cProcesses = cbNeeded / sizeof(DWORD);

    // Print the name and process identifier for each process.

    for ( i = 0; i < cProcesses; i++ )
        if( aProcesses[i] != 0 )
			EnumEntryWorker( aProcesses[i] );
}


std::vector<DWORD>& CProcessFilterer::get_filtered_process_ids()
{
	return m_filteredPsIds;
}

DWORD CProcessFilterer::DetectNew(const std::vector<DWORD> oldLst)
{
	for(std::vector<DWORD>::const_iterator it = m_filteredPsIds.begin();it != m_filteredPsIds.end();it++)
	{
		if(oldLst.end() == std::find(oldLst.begin(),oldLst.end(),*it))
			return *it;
		//for(std::vector<DWORD>::const_iterator ot = oldLst.begin();ot != oldLst.end();ot++)
		//{}
	}
	return 0L;
}