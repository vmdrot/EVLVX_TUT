#include "stdafx.h"
#include "UnArj.h"
#include "decode.h"
#include "environ.h"

UnArjDecoder::UnArjDecoder(UnArj* pUnArj):m_UnArjPtr(pUnArj), text(NULL)
{}
UnArjDecoder::~UnArjDecoder()
{}

/* Huffman decode routines */

void UnArjDecoder::make_table(int nchar, uchar *bitlen, int tablebits, ushort *table, int tablesize)
{
    ushort count[17], weight[17], start[18], *p;
    uint i, k, len, ch, jutbits, avail, nextcode, mask;

    for (i = 1; i <= 16; i++)
        count[i] = 0;
    for (i = 0; (int)i < nchar; i++)
        count[bitlen[i]]++;

    start[1] = 0;
    for (i = 1; i <= 16; i++)
        start[i + 1] = start[i] + (count[i] << (16 - i));
    if (start[17] != (ushort) (1 << 16))
        m_UnArjPtr->error(M_BADTABLE, "");

    jutbits = 16 - tablebits;
    for (i = 1; (int)i <= tablebits; i++)
    {
        start[i] >>= jutbits;
        weight[i] = 1 << (tablebits - i);
    }
    while (i <= 16)
    {
        weight[i] = 1 << (16 - i);
        i++;
    }

    i = start[tablebits + 1] >> jutbits;
    if (i != (ushort) (1 << 16))
    {
        k = 1 << tablebits;
        while (i != k)
            table[i++] = 0;
    }

    avail = nchar;
    mask = 1 << (15 - tablebits);
    for (ch = 0; (int)ch < nchar; ch++)
    {
        if ((len = bitlen[ch]) == 0)
            continue;
        k = start[len];
        nextcode = k + weight[len];
        if ((int)len <= tablebits)
        {
            if (nextcode > (uint)tablesize)
                m_UnArjPtr->error(M_BADTABLE, "");
            for (i = start[len]; i < nextcode; i++)
                table[i] = ch;
        }
        else
        {
            p = &table[k >> jutbits];
            i = len - tablebits;
            while (i != 0)
            {
                if (*p == 0)
                {
                    right[avail] = left[avail] = 0;
                    *p = avail++;
                }
                if (k & mask)
                    p = &right[*p];
                else
                    p = &left[*p];
                k <<= 1;
                i--;
            }
            *p = ch;
        }
        start[len] = nextcode;
    }
}

void UnArjDecoder::read_pt_len(int nn, int nbit, int i_special)
{
    int i, n;
    short c;
    ushort mask;

    n = m_UnArjPtr->getbits(nbit);
    if (n == 0)
    {
        c = m_UnArjPtr->getbits(nbit);
        for (i = 0; i < nn; i++)
            pt_len[i] = 0;
        for (i = 0; i < 256; i++)
            pt_table[i] = c;
    }
    else
    {
        i = 0;
        while (i < n)
        {
            c = (m_UnArjPtr->bitbuf) >> (13);
            if (c == 7)
            {
                mask = 1 << (12);
                while (mask & (m_UnArjPtr->bitbuf))
                {
                    mask >>= 1;
                    c++;
                }
            }
            m_UnArjPtr->fillbuf((c < 7) ? 3 : (int)(c - 3));
            pt_len[i++] = (uchar)c;
            if (i == i_special)
            {
                c = m_UnArjPtr->getbits(2);
                while (--c >= 0)
                    pt_len[i++] = 0;
            }
        }
        while (i < nn)
            pt_len[i++] = 0;
        make_table(nn, pt_len, 8, pt_table, PTABLESIZE);  /* replaced sizeof */
    }
}

void UnArjDecoder::read_c_len()
{
    short i, c, n;
    ushort mask;

    n = m_UnArjPtr->getbits(CBIT);
    if (n == 0)
    {
        c = m_UnArjPtr->getbits(CBIT);
        for (i = 0; i < NC; i++)
            c_len[i] = 0;
        for (i = 0; i < CTABLESIZE; i++)
            c_table[i] = c;
    }
    else
    {
        i = 0;
        while (i < n)
        {
            c = pt_table[(m_UnArjPtr->bitbuf) >> (8)];
            if (c >= NT)
            {
                mask = 1 << (7);
                do
                {
                    if ((m_UnArjPtr->bitbuf) & mask)
                        c = right[c];
                    else
                        c = left[c];
                    mask >>= 1;
                } while (c >= NT);
            }
            m_UnArjPtr->fillbuf((int)(pt_len[c]));
            if (c <= 2)
            {
                if (c == 0)
                    c = 1;
                else if (c == 1)
                    c = m_UnArjPtr->getbits(4) + 3;
                else
                    c = m_UnArjPtr->getbits(CBIT) + 20;
                while (--c >= 0)
                    c_len[i++] = 0;
            }
            else
                c_len[i++] = (uchar)(c - 2);
        }
        while (i < NC)
            c_len[i++] = 0;
        make_table(NC, c_len, 12, c_table, CTABLESIZE);  /* replaced sizeof */
    }
}

ushort UnArjDecoder::decode_c()
{
    ushort j, mask;

    if (blocksize == 0)
    {
        blocksize = m_UnArjPtr->getbits(16);
        read_pt_len(NT, TBIT, 3);
        read_c_len();
        read_pt_len(NP, PBIT, -1);
    }
    blocksize--;
    j = c_table[(m_UnArjPtr->bitbuf) >> 4];
    if (j >= NC)
    {
        mask = 1 << (3);
        do
        {
            if ((m_UnArjPtr->bitbuf) & mask)
                j = right[j];
            else
                j = left[j];
            mask >>= 1;
        } while (j >= NC);
    }
    m_UnArjPtr->fillbuf((int)(c_len[j]));
    return j;
}

ushort UnArjDecoder::decode_p()
{
    ushort j, mask;

    j = pt_table[(m_UnArjPtr->bitbuf) >> (8)];
    if (j >= NP)
    {
        mask = 1 << (7);
        do
        {
            if (m_UnArjPtr->bitbuf & mask)
                j = right[j];
            else
                j = left[j];
            mask >>= 1;
        } while (j >= NP);
    }
    m_UnArjPtr->fillbuf((int)(pt_len[j]));
    if (j != 0)
    {
        j--;
        j = (1 << j) + m_UnArjPtr->getbits((int)j);
    }
    return j;
}

void UnArjDecoder::decode_start()
{
    blocksize = 0;
    m_UnArjPtr->init_getbits();
}

void UnArjDecoder::decode()
{
    short i;
    short j;
    short c;
    short r;
    long count;

#ifdef KEEP_WINDOW
    if (text == (uchar *) NULL)
        text = (uchar *)malloc_msg(DDICSIZ);
#else
    text = (uchar *)m_UnArjPtr->malloc_msg(DDICSIZ);
#endif

    m_UnArjPtr->disp_clock();
    decode_start();
    count = 0;
    r = 0;

    while (count < m_UnArjPtr->origsize)
    {
        if ((c = decode_c()) <= UCHAR_MAX_VAL)
        {
            text[r] = (uchar) c;
            count++;
            if (++r >= DDICSIZ)
            {
                r = 0;
                m_UnArjPtr->disp_clock();
                m_UnArjPtr->fwrite_txt_crc(text, DDICSIZ);
            }
        }
        else
        {
            j = c - (UCHAR_MAX_VAL + 1 - THRESHOLD);
            count += j;
            i = decode_p();
            if ((i = r - i - 1) < 0)
                i += DDICSIZ;
            if (r > i && r < DDICSIZ - MAXMATCH - 1)
            {
                while (--j >= 0)
                    text[r++] = text[i++];
            }
            else
            {
                while (--j >= 0)
                {
                    text[r] = text[i];
                    if (++r >= DDICSIZ)
                    {
                        r = 0;
                        m_UnArjPtr->disp_clock();
                        m_UnArjPtr->fwrite_txt_crc(text, DDICSIZ);
                    }
                    if (++i >= DDICSIZ)
                        i = 0;
                }
            }
        }
    }
    if (r != 0)
        m_UnArjPtr->fwrite_txt_crc(text, r);

#ifndef KEEP_WINDOW
    free((char *)text);
#endif
}


short UnArjDecoder::decode_ptr()
{
    short c;
    short width;
    short plus;
    short pwr;

    plus = 0;
    pwr = 1 << (STRTP);
    for (width = (STRTP); width < (STOPP) ; width++)
    {
        GETBIT(&c);
        if (c == 0)
            break;
        plus += pwr;
        pwr <<= 1;
    }
    if (width != 0)
        GETBITS(&c, width);
    c += plus;
    return c;
}

short UnArjDecoder::decode_len()
{
    short c;
    short width;
    short plus;
    short pwr;

    plus = 0;
    pwr = 1 << (STRTL);
    for (width = (STRTL); width < (STOPL) ; width++)
    {
        GETBIT(&c);
        if (c == 0)
            break;
        plus += pwr;
        pwr <<= 1;
    }
    if (width != 0)
        GETBITS(&c, width);
    c += plus;
    return c;
}

void UnArjDecoder::decode_f()
{
    short i;
    short j;
    short c;
    short r;
    short pos;
    long count;

#ifdef KEEP_WINDOW
    if (text == (uchar *) NULL)
        text = (uchar *)malloc_msg(DDICSIZ);
#else
    text = (uchar *)m_UnArjPtr->malloc_msg(DDICSIZ);
#endif

    m_UnArjPtr->disp_clock();
    m_UnArjPtr->init_getbits();
    getlen = getbuf = 0;
    count = 0;
    r = 0;

    while (count < m_UnArjPtr->origsize)
    {
        c = decode_len();
        if (c == 0)
        {
            GETBITS(&c, CHAR_BIT_VAL);
            text[r] = (uchar)c;
            count++;
            if (++r >= DDICSIZ)
            {
                r = 0;
                m_UnArjPtr->disp_clock();
                m_UnArjPtr->fwrite_txt_crc(text, DDICSIZ);
            }
        }
        else
        {
            j = c - 1 + THRESHOLD;
            count += j;
            pos = decode_ptr();
            if ((i = r - pos - 1) < 0)
                i += DDICSIZ;
            while (j-- > 0)
            {
                text[r] = text[i];
                if (++r >= DDICSIZ)
                {
                    r = 0;
                    m_UnArjPtr->disp_clock();
                    m_UnArjPtr->fwrite_txt_crc(text, DDICSIZ);
                }
                if (++i >= DDICSIZ)
                    i = 0;
            }
        }
    }
    if (r != 0)
        m_UnArjPtr->fwrite_txt_crc(text, r);

#ifndef KEEP_WINDOW
    free((char *)text);
#endif
}
/* Huffman decode routines */
