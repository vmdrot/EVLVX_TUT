#ifndef __EVOLVEX_COMMON_LIB_CHAR_SET_SHUFFLER_H_
#define __EVOLVEX_COMMON_LIB_CHAR_SET_SHUFFLER_H_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <map>

class CCharSetShuffler
{
private:
    int m_iUBound;
    int m_iLBound;
    std::map<unsigned int, unsigned int> m_encodingMap;
    std::map<unsigned int, unsigned int> m_decodingMap;
    static const unsigned int DEFAULT_LBOUND = 0;
    static const unsigned int DEFAULT_UBOUND = 255;
    void recordRand(unsigned int i, unsigned int curr_rand);
    void pickUpFirstUnused(unsigned int i);
public:
    CCharSetShuffler(void);
    CCharSetShuffler(unsigned int iLBound, unsigned int iUBound);
    ~CCharSetShuffler(void);
    void Shuffle(std::map<unsigned int, unsigned int>& encoder, std::map<unsigned int, unsigned int>& decoder);
};

#endif //__EVOLVEX_COMMON_LIB_CHAR_SET_SHUFFLER_H_