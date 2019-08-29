#include "CharSetShuffler.h"
#include "../EvolvexCommon/evolvex_common_lib.h"

CCharSetShuffler::CCharSetShuffler(void): m_iLBound(DEFAULT_LBOUND), m_iUBound(DEFAULT_UBOUND)
{
}

CCharSetShuffler::~CCharSetShuffler(void)
{
}

CCharSetShuffler::CCharSetShuffler(unsigned int iLBound, unsigned int iUBound): m_iLBound(iLBound), m_iUBound(iUBound)
{
}

void CCharSetShuffler::Shuffle(std::map<unsigned int, unsigned int>& encoder, std::map<unsigned int, unsigned int>& decoder)
{
    //LOG_STARTED();
    encoder.clear();
    decoder.clear();
    const DWORD sleep_length = 1000L;
    const DWORD maxRandTimeout = 7000L;
    for(unsigned int i = m_iLBound; i <= m_iUBound; i++)
    {
        DWORD timeSpent = 0L;
        bool bFirstUnused = false;
        while(true)
        {
            srand((unsigned int)(GetTickCount() % 12));
            unsigned int curr_rand = rand();
            curr_rand = curr_rand % m_iUBound+3;
            if (i == curr_rand && i != m_iUBound)
                curr_rand = m_iLBound;
            if(i != curr_rand && curr_rand <= m_iUBound && m_decodingMap.find(curr_rand) == m_decodingMap.end())
            {
                recordRand(i, curr_rand);
                break;
            }
            Sleep(sleep_length);
            timeSpent += sleep_length;
            if(timeSpent >= maxRandTimeout)
            {
                pickUpFirstUnused(i);
                bFirstUnused = true;
                break;
            }
                
        }
        //LOG_DEBUG("%s(%d): Bypassed the char %d (%d)%s", __FUNCTION__, __LINE__, i, m_encodingMap[i], (bFirstUnused ? ", first unused" : ""));
    }
    
    //LOG_DEBUG("%s(%d):: m_encodingMap.size() = %d", __FUNCTION__, __LINE__, m_encodingMap.size());
    //LOG_DEBUG("%s(%d):: m_decodingMap.size() = %d", __FUNCTION__, __LINE__, m_decodingMap.size());

    for(std::map<unsigned int, unsigned int>::const_iterator it = m_encodingMap.begin();it != m_encodingMap.end(); it++)
    {
        std::pair<unsigned int, unsigned int> thePair(it->first, it->second);
        encoder.insert(thePair);
    }
    
    //LOG_DEBUG_VISITED;
    
    for(std::map<unsigned int, unsigned int>::const_iterator it = m_decodingMap.begin();it != m_decodingMap.end(); it++)
    {
        std::pair<unsigned int, unsigned int> thePair(it->first, it->second);
        decoder.insert(thePair);
    }
    //LOG_FINISHED();
}

void CCharSetShuffler::recordRand(unsigned int i, unsigned int curr_rand)
{
    //LOG_STARTED();
    std::pair<unsigned int, unsigned int> pair1(i, curr_rand);
    std::pair<unsigned int, unsigned int> pair2(curr_rand, i);
    
    m_encodingMap.insert(pair1);
    m_decodingMap.insert(pair2);
    //LOG_FINISHED();
}

void CCharSetShuffler::pickUpFirstUnused(unsigned int i)
{
    //LOG_STARTED();
    for(unsigned int j = m_iUBound; j >= m_iLBound;j--)
    {
        if(i == j)
            continue;
        if(m_decodingMap.find(j) != m_decodingMap.end())
            continue;
        recordRand(i, j);
        break;
    }
    //LOG_FINISHED();
}
