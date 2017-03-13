#ifndef __UN_ARJ_H_
#define __UN_ARJ_H_
//#include "stdafx.h"
#include <stdio.h>
#include "common_defs.h"
class UnArjDecoder;


using namespace UnArjConstants;
class UnArj
{
friend class UnArjDecoder;
private:
//#pragma region Private member vars
    UCRC   crc;
    FILE   *arcfile;
    FILE   *outfile;
    ushort bitbuf;
    long   compsize;
    long   origsize;
    uchar  subbitbuf;
    uchar  header[HEADERSIZE_MAX];
    char   arc_name[FNAME_MAX];
    int    command;
    int    bitcount;
    int    file_type;
    int    no_output;
    int    error_count;
    uchar* get_ptr;
    UnArjDecoder* m_pDecoder;
    char   m_sArg3[FNAME_MAX];
    char   m_sArg4[FNAME_MAX];
    bool   m_bQuiet;
//#pragma endregion Private member vars


//#pragma region Inline Methods
    ulong get_longword();
    inline ulong get_crc(){return get_longword();};
    inline ulong fget_crc(FILE* f){return fget_longword(f);};
    ulong fget_longword(FILE* f);
    inline void setup_get(uchar* PTR) {get_ptr = PTR;}
    inline uchar get_byte(){return (uchar)(*get_ptr++ & 0xff);}
    inline void UPDATE_CRC(UCRC* r,char c)
    {
        *r = crctable[((uchar)(*r)^(uchar)(c))&0xff]^((*r)>>CHAR_BIT_VAL);
    }

    UnArjDecoder* get_Decoder();
//#pragma endregion

//#pragma region Member Vars
/* Local variables */

char   filename[FNAME_MAX];
char   comment[COMMENT_MAX];
char*  hdr_filename;
char*  hdr_comment;

ushort headersize;
uchar  first_hdr_size;
uchar  arj_nbr;
uchar  arj_x_nbr;
uchar  host_os;
uchar  arj_flags;
short  method;
uint   file_mode;
ulong  time_stamp;
short  entry_pos;
ushort host_data;
//static uchar* get_ptr;
UCRC   file_crc;
UCRC   header_crc;

long   first_hdr_pos;
long   torigsize;
long   tcompsize;

int    clock_inx;



UCRC   crctable[UCHAR_MAX_VAL + 1];
//#pragma endregion
public:
    UnArj(void);
    ~UnArj(void);
//#pragma region Methods
/* Functions */
void make_crctable();
void crc_buf(char *str, int len);
void disp_clock();
void error(const char *fmt, char *arg);
static void strparity(uchar *p);
FILE* fopen_msg(char *name, char *mode);
int fget_byte(FILE *f);
uint fget_word(FILE *f);
//static ulong fget_longword(FILE *f);
void fread_crc(uchar *p, int n, FILE *f);
void fwrite_txt_crc(uchar *p, int n);
void init_getbits();
/* Shift bitbuf n bits left, read n bits */
void fillbuf(int n);
ushort getbits(int n);
static void decode_path(char *name);
static void get_date_str(char *str, ulong tstamp);
static int parse_path(char *pathname, char *path, char *entry);
static void strncopy(char *to, char *from, int len);
static void strlower(char *s);
static void strupper(char *s);
voidp* malloc_msg(int size);
uint get_word();
//static ulong get_longword();
long find_header(FILE *fd);
int read_header(int first, FILE* fd, char* name);
void skip();
void unstore();
int check_flags();
int extract();
int test();
static uint ratio(long a, long b);
void list_start();
void list_arc(int count);
void execute_cmd();
static void help();
int do_main(int  argc, char* argv[]);
bool get_Quiet();
void set_Quiet(bool val);
//#pragma endregion
};
#endif