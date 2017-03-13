#ifndef __UN_ARJ_DECODE_H_
#define __UN_ARJ_DECODE_H_
#include "stdafx.h"
#include "UnArj.h"

//using  namespace UnArjConstants;

#define THRESHOLD    3
#define DDICSIZ      26624
#define MAXDICBIT   16
#define MATCHBIT     8
#define MAXMATCH   256
#define NC          (UCHAR_MAX_VAL + MAXMATCH + 2 - THRESHOLD)
#define NP          (MAXDICBIT + 1)
#define CBIT         9
#define NT          (CODE_BIT + 3)
#define PBIT         5
#define TBIT         5

#if NT > NP
#define NPT NT
#else
#define NPT NP
#endif

#define CTABLESIZE  4096
#define PTABLESIZE   256

#define STRTP          9
#define STOPP         13

#define STRTL          0
#define STOPL          7

class UnArj;

class UnArjDecoder
{
private:
    UnArj* m_UnArjPtr;

    uchar  *text;// = NULL;

    short  getlen;
    short  getbuf;
    ushort left[2 * NC - 1];
    ushort right[2 * NC - 1];
    uchar  c_len[NC];
    uchar  pt_len[NPT];
    ushort c_table[CTABLESIZE];
    ushort pt_table[PTABLESIZE];
    ushort blocksize;

    /* Local functions */
    void make_table(int nchar, uchar *bitlen, int tablebits, ushort *table, int tablesize);
    void read_pt_len(int nn, int nbit, int i_special);
    void read_c_len();
    ushort decode_c();
    ushort decode_p();
    void decode_start();
    short decode_ptr();
    short decode_len();
    /* Macros */

    void inline BFIL() {getbuf|=(m_UnArjPtr->bitbuf)>>getlen;m_UnArjPtr->fillbuf(CODE_BIT-getlen);getlen=CODE_BIT;}
    void inline GETBIT(short* c) {if(getlen<=0)BFIL(); *c=(getbuf&0x8000)!=0;getbuf<<=1;getlen--;}
    void inline BPUL(short l) {getbuf<<=l;getlen-=l;}
    void inline GETBITS(short* c, short l) {if(getlen<l)BFIL(); *c=(ushort)getbuf>>(CODE_BIT-l);BPUL(l);}

public:
    UnArjDecoder(UnArj* pUnArj);
    ~UnArjDecoder();
    void decode();
    void decode_f();

};
#endif //__UN_ARJ_DECODE_H_