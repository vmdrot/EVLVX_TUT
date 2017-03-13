#ifndef __UN_ARJ_COMMON_DEFS_H_
#define __UN_ARJ_COMMON_DEFS_H_
#include "stdafx.h"
#include <stdio.h>


typedef char voidp;
typedef unsigned char  uchar;   /*  8 bits or more */
typedef unsigned int   uint;    /* 16 - 32 bits or more */
typedef unsigned short ushort;  /* 16 bits or more */
typedef unsigned long  ulong;   /* 32 bits or more */
typedef ulong UCRC;     /* CRC-32 */

typedef enum tagERROR_DEFINES
{
  ERROR_OK       = 0,       /* success */
  ERROR_WARN     = 1,       /* minor problem (file not found) */
  ERROR_FAIL     = 2,       /* fatal error */
  ERR_CRC      = 3,       /* CRC error */
  ERROR_SECURE   = 4,       /* ARJ security invalid or not found */
  ERROR_WRITE    = 5,       /* disk full */
  ERROR_OPEN     = 6,       /* can't open file */
  ERROR_USER     = 7,       /* user specified bad parameters */
  ERROR_MEMORY   = 8,       /* not enough memory */
} ERROR_DEFINES;

#ifndef FIX_PARITY
#define FIX_PARITY(c)       c &= ASCII_MASK
#endif
/* Timestamp macros */

#define get_tx(m,d,h,n) (((ulong)m<<21)+((ulong)d<<16)+((ulong)h<<11)+(n<<5))
#define get_tstamp(y,m,d,h,n,s) ((((ulong)(y-1980))<<25)+get_tx(m,d,h,n)+(s/2))

#define ts_year(ts)  ((uint)((ts >> 25) & 0x7f) + 1980)
#define ts_month(ts) ((uint)(ts >> 21) & 0x0f)      /* 1..12 means Jan..Dec */
#define ts_day(ts)   ((uint)(ts >> 16) & 0x1f)      /* 1..31 means 1st..31st */
#define ts_hour(ts)  ((uint)(ts >> 11) & 0x1f)
#define ts_min(ts)   ((uint)(ts >> 5) & 0x3f)
#define ts_sec(ts)   ((uint)((ts & 0x1f) * 2))

namespace UnArjConstants
{
    #pragma region Constants (ex-defines)
    const uint UCHAR_MAX_VAL = 255;
    const uint CHAR_BIT_VAL  = 8;
    const ulong LONG_MAX_VAL = 0x7FFFFFFFL;
    const uint USHRT_BIT = CHAR_BIT_VAL * sizeof(ushort);
    const ulong MAXSFX   = 500000L;
    const uint FNAME_MAX = 512;
    
    const char ARJ_DOT           = '.';
    
    const uint OS                = 2;
    
    const char PATH_CHAR         = '/';
    const uint CODE_BIT          = 16;
    const char NULL_CHAR       = '\0';
    const uint MAXMETHOD         = 4;

    const uint ARJ_VERSION       =3;
    const uint ARJ_M_VERSION     =6;    /* ARJ version that supports modified date. */
    const uint ARJ_X_VERSION     = 3;    /* decoder version */
    const uint ARJ_X1_VERSION    = 1;
    const uint DEFAULT_METHOD    =1;
    const uint DEFAULT_TYPE      = 0;    /* if type_sw is selected */
    const uint HEADER_ID     = 0xEA60;
    const uint HEADER_ID_HI    = 0xEA;
    const uint HEADER_ID_LO    = 0x60;
    const uint FIRST_HDR_SIZE  =  30;
    const uint FIRST_HDR_SIZE_V =  34;
    const uint COMMENT_MAX     = 2048;
    const uint HEADERSIZE_MAX   = FIRST_HDR_SIZE + 10 + FNAME_MAX + COMMENT_MAX;
    const uint BINARY_TYPE      =  0;    /* This must line up with binary/text strings */
    const uint TEXT_TYPE        =  1;
    const uint COMMENT_TYPE     =  2;
    const uint DIR_TYPE         =  3;
    const uint LABEL_TYPE       =  4;

    const uint GARBLE_FLAG    = 0x01;
    const uint VOLUME_FLAG    = 0x04;
    const uint EXTFILE_FLAG   = 0x08;
    const uint PATHSYM_FLAG   = 0x10;
    const uint BACKUP_FLAG    = 0x20;

    const ulong CRC_MASK      = 0xFFFFFFFFL;

    const char ARJ_PATH_CHAR   ='/';

    const uint FA_RDONLY      = 0x01;            /* Read only attribute */
    const uint FA_HIDDEN      = 0x02;            /* Hidden file */
    const uint FA_SYSTEM      = 0x04;            /* System file */
    const uint FA_LABEL       = 0x08;            /* Volume label */
    const uint FA_DIREC       = 0x10;            /* Directory */
    const uint FA_ARCH        = 0x20;            /* Archive */
    const uint BUFFERSIZE     = 4096;

    const uint ASCII_MASK     = 0x7F;

    const ulong CRCPOLY       =  0xEDB88320L;
    #pragma endregion
#pragma region Message formats
static char* M_USAGE  [] =
{
"Usage:  UNARJ archive[.arj]    (list archive)\n",
"        UNARJ e archive        (extract archive)\n",
"        UNARJ l archive        (list archive)\n",
"        UNARJ t archive        (test archive)\n",
"        UNARJ x archive        (extract with pathnames)\n",
"\n",
"This is an ARJ demonstration program and ** IS NOT OPTIMIZED ** for speed.\n",
"You may freely use, copy and distribute this program, provided that no fee\n",
"is charged for such use, copying or distribution, and it is distributed\n",
"ONLY in its original unmodified state.  UNARJ is provided as is without\n",
"warranty of any kind, express or implied, including but not limited to\n",
"the implied warranties of merchantability and fitness for a particular\n",
"purpose.  Refer to UNARJ.DOC for more warranty information.\n",
"\n",
"ARJ Software, Inc.      Internet address:  robjung@world.std.com\n",
"P.O. Box 249                    Web site:  www.arjsoftware.com\n",
"Norwood MA 02062\n",
"USA\n",
NULL
};
const char ARJ_SUFFIX[]       = ".arj";
const char DEFAULT_DIR[]      = "";
const char PATH_SEPARATORS[]  = "/";

const char M_VERSION [] = "UNARJ (Demo version) 2.65 Copyright (c) 1991-2002 ARJ Software, Inc.\n\n";

const char M_ARCDATE [] = "Archive created: %s";
const char M_ARCDATEM[] = ", modified: %s";
const char M_BADCOMND[] = "Bad UNARJ command: %s";
const char M_BADCOMNT[] = "Invalid comment header";
const char M_BADHEADR[] = "Bad header";
const char M_BADTABLE[] = "Bad file data";
const char M_CANTOPEN[] = "Can't open %s";
const char M_CANTREAD[] = "Can't read file or unexpected end of file";
const char M_CANTWRIT[] = "Can't write file. Disk full?";
const char M_CRCERROR[] = "CRC error!\n";
const char M_CRCOK   [] = "CRC OK\n";
const char M_DIFFHOST[] = "  Binary file!";
const char M_ENCRYPT [] = "File is password encrypted, ";
const char M_ERRORCNT[] = "%sFound %5d error(s)!";
const char M_EXTRACT [] = "Extracting %-25s";
const char M_FEXISTS [] = "%-25s exists, ";
const char M_HEADRCRC[] = "Header CRC error!";
const char M_NBRFILES[] = "%5d file(s)\n";
const char M_NOMEMORY[] = "Out of memory";
const char M_NOTARJ  [] = "%s is not an ARJ archive";
const char M_PROCARC [] = "Processing archive: %s\n";
const char M_SKIPPED [] = "Skipped %s\n";
#define M_SUFFIX ARJ_SUFFIX
const char M_TESTING [] = "Testing    %-25s";
const char M_UNKNMETH[] = "Unsupported method: %d, ";
const char M_UNKNTYPE[] = "Unsupported file type: %d, ";
const char M_UNKNVERS[] = "Unsupported version: %d, ";


#pragma endregion

static char*  writemode[2]  = { "wb",  "w" };
};
#endif //__UN_ARJ_COMMON_DEFS_H_