#include "stdafx.h"
#include "UnArj.h"
#include "environ.h"
#include "decode.h"

UnArj::UnArj(void): m_pDecoder(NULL),m_bQuiet(false)
{
    strcpy(m_sArg3, "");
    strcpy(m_sArg4, "");
}

UnArj::~UnArj(void)
{}

/* Functions */

void UnArj::make_crctable()
{
    uint i, j;
    UCRC r;

    for (i = 0; i <= UCHAR_MAX_VAL; i++)
    {
        r = i;
        for (j = CHAR_BIT_VAL; j > 0; j--)
        {
            if (r & 1)
                r = (r >> 1) ^ CRCPOLY;
            else
                r >>= 1;
        }
        crctable[i] = r;
    }
}

void UnArj::crc_buf(char *str, int len){
    while (len--)
        UPDATE_CRC(&crc, *str++);
}

void
UnArj::disp_clock()
{
    static char clock_str[4] = { '|', '/', '-', '\\' };

    if(!m_bQuiet) printf("(%c)\b\b\b", clock_str[clock_inx]);
    clock_inx = (clock_inx + 1) & 0x03;
}

void UnArj::error(const char *fmt, char *arg)
{
    if(!m_bQuiet)
    {
        putc('\n', stdout);
        printf(fmt, arg, error_count);
        putc('\n', stdout);
    }
    exit(EXIT_FAILURE);
}

void UnArj::strparity(uchar *p)
{
    while (*p)
    {
        FIX_PARITY(*p);
        p++;
    }
}

FILE* UnArj::fopen_msg(char *name, char *mode)
{
    FILE *fd;

    fd = file_open(name, mode);
    if (fd == NULL)
        error((char*)M_CANTOPEN, name);
    return fd;
}

int UnArj::fget_byte(FILE *f)
{
    int c;

    if ((c = getc(f)) == EOF)
        error(M_CANTREAD, "");
    return c & 0xFF;
}

uint UnArj::fget_word(FILE *f)
{
    uint b0, b1;

    b0 = fget_byte(f);
    b1 = fget_byte(f);
    return (b1 << 8) + b0;
}

ulong UnArj::fget_longword(FILE *f)
{
    ulong b0, b1, b2, b3;

    b0 = fget_byte(f);
    b1 = fget_byte(f);
    b2 = fget_byte(f);
    b3 = fget_byte(f);
    return (b3 << 24) + (b2 << 16) + (b1 << 8) + b0;
}

void UnArj::fread_crc(uchar *p, int n, FILE *f)
{
    n = file_read((char *)p, 1, n, f);
    origsize += n;
    crc_buf((char *)p, n);
}

void UnArj::fwrite_txt_crc(uchar *p, int n)
{
    uchar c;

    crc_buf((char *)p, n);
    if (no_output)
        return;

    if (file_type == TEXT_TYPE)
    {
        while (n--)
        {
            c = *p++;
            if (host_os != OS)
            {
                FIX_PARITY(c);
            }
            if (putc((int) c, outfile) == EOF)
                error(M_CANTWRIT, "");
        }
    }
    else
    {
        if (file_write((char *)p, 1, n, outfile) != n)
            error(M_CANTWRIT, "");
    }
}

void UnArj::init_getbits()
{
    bitbuf = 0;
    subbitbuf = 0;
    bitcount = 0;
    fillbuf(2 * CHAR_BIT_VAL);
}


void UnArj::fillbuf(int n)
{
    bitbuf = (bitbuf << n) & 0xFFFF;  /* lose the first n bits */
    while (n > bitcount)
    {
        bitbuf |= subbitbuf << (n -= bitcount);
        if (compsize != 0)
        {
            compsize--;
            subbitbuf = (uchar) getc(arcfile);
        }
        else
            subbitbuf = 0;
        bitcount = CHAR_BIT_VAL;
    }
    bitbuf |= subbitbuf >> (bitcount -= n);
}

ushort UnArj::getbits(int n)
{
    ushort x;

    x = bitbuf >> (2 * CHAR_BIT_VAL - n);
    fillbuf(n);
    return x;
}

void UnArj::decode_path(char *name)
{
    for ( ; *name; name++)
    {
        if (*name == ARJ_PATH_CHAR)
            *name = PATH_CHAR;
    }
}

void UnArj::get_date_str(char *str, ulong tstamp)
{
    sprintf(str, "%04u-%02u-%02u %02u:%02u:%02u",
           ts_year(tstamp), ts_month(tstamp), ts_day(tstamp),
           ts_hour(tstamp), ts_min(tstamp), ts_sec(tstamp));
}

int UnArj::parse_path(char *pathname, char *path, char *entry)
{
    char *cptr, *ptr, *fptr;
    short pos;

    fptr = NULL;
    for (cptr = (char*)PATH_SEPARATORS; *cptr; cptr++)
    {
        if ((ptr = strrchr(pathname, *cptr)) != NULL &&
                (fptr == NULL || ptr > fptr))
            fptr = ptr;
    }
    if (fptr == NULL)
        pos = 0;
    else
        pos = fptr + 1 - pathname;
    if (path != NULL)
    {
       strncpy(path, pathname, pos);
       path[pos] = NULL_CHAR;
    }
    if (entry != NULL)
       strcpy(entry, &pathname[pos]);
    return pos;
}

void UnArj::strncopy(char *to, char *from, int len)
{
    int i;

    for (i = 1; i < len && *from; i++)
        *to++ = *from++;
    *to = NULL_CHAR;
}

void UnArj::strlower(char *s)
{
    while (*s)
    {
        *s = (char) tolower(*s);
        s++;
    }
}

void UnArj::strupper(char *s)
{
    while (*s)
    {
        *s = (char) toupper(*s);
        s++;
    }
}

voidp* UnArj::malloc_msg(int size)
{
    char *p;

    if ((p = (char *)xmalloc(size)) == NULL)
        error(M_NOMEMORY, "");
    return (voidp *)p;
}

uint UnArj::get_word()
{
    uint b0, b1;

    b0 = get_byte();
    b1 = get_byte();
    return (b1 << 8) + b0;
}

ulong UnArj::get_longword()
{
    ulong b0, b1, b2, b3;

    b0 = get_byte();
    b1 = get_byte();
    b2 = get_byte();
    b3 = get_byte();
    return (b3 << 24) + (b2 << 16) + (b1 << 8) + b0;
}

long UnArj::find_header(FILE *fd)
{
    long arcpos, lastpos;
    int c;

    arcpos = file_tell(fd);
    file_seek(fd, 0L, SEEK_END);
    lastpos = file_tell(fd) - 2;
    if (lastpos > MAXSFX)
        lastpos = MAXSFX;
    for ( ; arcpos < lastpos; arcpos++)
    {
        file_seek(fd, arcpos, SEEK_SET);
        c = fget_byte(fd);
        while (arcpos < lastpos)
        {
            if (c != HEADER_ID_LO)  /* low order first */
                c = fget_byte(fd);
            else if ((c = fget_byte(fd)) == HEADER_ID_HI)
                break;
            arcpos++;
        }
        if (arcpos >= lastpos)
            break;
        if ((headersize = fget_word(fd)) <= HEADERSIZE_MAX)
        {
            crc = CRC_MASK;
            fread_crc(header, (int) headersize, fd);
            if ((crc ^ CRC_MASK) == fget_crc(fd))
            {
                file_seek(fd, arcpos, SEEK_SET);
                return arcpos;
            }
        }
    }
    return -1;          /* could not find a valid header */
}

int UnArj::read_header(int first, FILE* fd, char* name)
{
    ushort extheadersize, header_id;

    header_id = fget_word(fd);
    if (header_id != HEADER_ID)
    {
        if (first)
            error(M_NOTARJ, name);
        else
            error(M_BADHEADR, "");
    }

    headersize = fget_word(fd);
    if (headersize == 0)
        return 0;               /* end of archive */
    if (headersize > HEADERSIZE_MAX)
        error(M_BADHEADR, "");

    crc = CRC_MASK;
    fread_crc(header, (int) headersize, fd);
    header_crc = fget_crc(fd);
    if ((crc ^ CRC_MASK) != header_crc)
        error(M_HEADRCRC, "");

    setup_get(header);
    first_hdr_size = get_byte();
    arj_nbr = get_byte();
    arj_x_nbr = get_byte();
    host_os = get_byte();
    arj_flags = get_byte();
    method = get_byte();
    file_type = get_byte();
    (void)get_byte();
    time_stamp = get_longword();
    compsize = get_longword();
    origsize = get_longword();
    file_crc = get_crc();
    entry_pos = get_word();
    file_mode = get_word();
    host_data = get_word();

    if (origsize < 0 || compsize < 0)
        error(M_HEADRCRC, "");

    hdr_filename = (char *)&header[first_hdr_size];
    strncopy(filename, hdr_filename, sizeof(filename));
    if (host_os != OS)
        strparity((uchar *)filename);
    if ((arj_flags & PATHSYM_FLAG) != 0)
        decode_path(filename);

    hdr_comment = (char *)&header[first_hdr_size + strlen(hdr_filename) + 1];
    strncopy(comment, hdr_comment, sizeof(comment));
    if (host_os != OS)
        strparity((uchar *)comment);

    /* if extheadersize == 0 then no CRC */
    /* otherwise read extheader data and read 4 bytes for CRC */

    while ((extheadersize = fget_word(fd)) != 0)
        file_seek(fd, (long) (extheadersize + 4), SEEK_CUR);

    return 1;                   /* success */
}

void UnArj::skip()
{
    file_seek(arcfile, compsize, SEEK_CUR);
}

void UnArj::unstore()
{
    int n;
    long pos;
    char *buffer;

    buffer = (char *)malloc_msg(BUFFERSIZE);
    pos = file_tell(arcfile);
    disp_clock();
    n = (int)(BUFFERSIZE - (pos % BUFFERSIZE));
    n = compsize > (long)n ? n : (int)compsize;
    while (compsize > 0)
    {
        if (file_read(buffer, 1, n, arcfile) != n)
            error(M_CANTREAD, "");
        disp_clock();
        compsize -= n;
        fwrite_txt_crc((uchar *)buffer, n);
        n = compsize > BUFFERSIZE ? BUFFERSIZE : (int)compsize;
    }
    free(buffer);
}

int UnArj::check_flags()
{
    if (arj_x_nbr > ARJ_X_VERSION)
    {
        if(!m_bQuiet)
        {
            printf(M_UNKNVERS, arj_x_nbr);
            printf(M_SKIPPED, filename);
        }
        skip();
        return -1;
    }
    if ((arj_flags & GARBLE_FLAG) != 0)
    {
        if(!m_bQuiet)
        {
            printf(M_ENCRYPT);
            printf(M_SKIPPED, filename);
        }
        skip();
        return -1;
    }
    if (method < 0 || method > MAXMETHOD || (method == 4 && arj_nbr == 1))
    {
        if(!m_bQuiet)
        {
            printf(M_UNKNMETH, method);
            printf(M_SKIPPED, filename);
        }
        skip();
        return -1;
    }
    if (file_type != BINARY_TYPE && file_type != TEXT_TYPE)
    {
        if(!m_bQuiet)
        {
            printf(M_UNKNTYPE, file_type);
            printf(M_SKIPPED, filename);
        }
        skip();
        return -1;
    }
    return 0;
}


int UnArj::extract()
{
    char name[FNAME_MAX];

    if (check_flags())
    {
        error_count++;
        return 0;
    }

    no_output = 0;
    if (command == 'E')
    {
        bool bExtract2CurrDir = true;
        if(strlen(m_sArg3)>0)
        {
            if(stricmp(&filename[entry_pos],m_sArg3)!= 0)
            {
                skip();
                return 0;
            }
            if(strlen(m_sArg4)>0)
            {
                strcpy(name, m_sArg4);
                bExtract2CurrDir = false;
            }
        }
        if(bExtract2CurrDir)
            strcpy(name, &filename[entry_pos]);
    }
    else
    {
        strcpy(name, DEFAULT_DIR);
        strcat(name, filename);
    }

    if (host_os != OS)
        default_case_path(name);

    if (file_exists(name))
    {
        if(!m_bQuiet)
        {
            printf(M_FEXISTS, name);
            printf(M_SKIPPED, name);
        }
        skip();
        error_count++;
        return 0;
    }
    outfile = file_open(name, writemode[file_type & 1]);
    if (outfile == NULL)
    {
        if(!m_bQuiet)
        {
            printf(M_CANTOPEN, name);
            putchar('\n');
        }
        skip();
        error_count++;
        return 0;
    }
    if(!m_bQuiet)
    {
        printf(M_EXTRACT, name);
        if (host_os != OS && file_type == BINARY_TYPE)
            printf(M_DIFFHOST);
        printf("  ");
    }

    crc = CRC_MASK;

    if (method == 0)
        unstore();
    else if (method == 1 || method == 2 || method == 3)
        get_Decoder()->decode();
    else if (method == 4)
        get_Decoder()->decode_f();
    fclose(outfile);

    set_ftime_mode(name, time_stamp, file_mode, (uint) host_os);

    if ((crc ^ CRC_MASK) == file_crc)
    {
        if(!m_bQuiet) printf(M_CRCOK);
    }
    else
    {
        if(!m_bQuiet) printf(M_CRCERROR);
        error_count++;
    }
    return 1;
}


int UnArj::test()
{
    if (check_flags())
        return 0;

    no_output = 1;
    if(!m_bQuiet)
    {
        printf(M_TESTING, filename);
        printf("  ");
    }

    crc = CRC_MASK;

    if (method == 0)
        unstore();
    else if (method == 1 || method == 2 || method == 3)
        get_Decoder()->decode();
    else if (method == 4)
        get_Decoder()->decode_f();

    if ((crc ^ CRC_MASK) == file_crc)
    {
        if(!m_bQuiet) printf(M_CRCOK);
    }
    else
    {
        if(!m_bQuiet) printf(M_CRCERROR);
        error_count++;
    }
    return 1;
}


uint UnArj::ratio(long a, long b)
{
   int i;

   for (i = 0; i < 3; i++)
       if (a <= LONG_MAX_VAL / 10)
           a *= 10;
       else
           b /= 10;
   if ((long) (a + (b >> 1)) < a)
   {
       a >>= 1;
       b >>= 1;
   }
   if (b == 0)
       return 0;
   return (uint) ((a + (b >> 1)) / b);
}

void UnArj::list_start()
{
    if(!m_bQuiet)
    {
        printf("Filename       Original Compressed Ratio DateTime modified CRC-32   AttrBTPMGVX\n");
        printf("------------ ---------- ---------- ----- ----------------- -------- -----------\n");
    }
}

void UnArj::list_arc(int count)
{
    uint r;
    int garble_mode, path_mode, volume_mode, extfil_mode, ftype, bckf_mode;
    char date_str[20], fmode_str[10];
    static char mode[5] = { 'B', 'T', '?', 'D', 'V' };
    static char pthf[2] = { ' ', '+' };
    static char pwdf[2] = { ' ', 'G' };  /* plain, encrypted */
    static char volf[2] = { ' ', 'V' };
    static char extf[2] = { ' ', 'X' };
    static char bckf[2] = { ' ', '*' };

    if (count == 0)
        list_start();

    garble_mode = ((arj_flags & GARBLE_FLAG) != 0);
    volume_mode = ((arj_flags & VOLUME_FLAG) != 0);
    extfil_mode = ((arj_flags & EXTFILE_FLAG) != 0);
    bckf_mode   = ((arj_flags & BACKUP_FLAG) != 0);
    path_mode   = (entry_pos > 0);
    r = ratio(compsize, origsize);
    torigsize += origsize;
    tcompsize += compsize;
    ftype = file_type;
    if (ftype != BINARY_TYPE && ftype != TEXT_TYPE && ftype != DIR_TYPE &&
            ftype != LABEL_TYPE)
        ftype = 3;
    get_date_str(date_str, time_stamp);
    strcpy(fmode_str, "    ");
    if (host_os == OS)
        get_mode_str(fmode_str, (uint) file_mode);
    if(!m_bQuiet)
    {
        if (strlen(&filename[entry_pos]) > 12)
            printf("%-12s\n             ", &filename[entry_pos]);
        else
            printf("%-12s ", &filename[entry_pos]);
        printf("%10ld %10ld %u.%03u %s %08lX %4s%c%c%c%u%c%c%c\n",
            origsize, compsize, r / 1000, r % 1000, &date_str[2], file_crc,
            fmode_str, bckf[bckf_mode], mode[ftype], pthf[path_mode], method,
            pwdf[garble_mode], volf[volume_mode], extf[extfil_mode]);
    }
}



void UnArj::execute_cmd()
{
    int file_count;
    char date_str[22];
    uint r;

    first_hdr_pos = 0;
    time_stamp = 0;
    first_hdr_size = FIRST_HDR_SIZE;

    arcfile = fopen_msg(arc_name, "rb");

    if(!m_bQuiet) printf(M_PROCARC, arc_name);

    first_hdr_pos = find_header(arcfile);
    if (first_hdr_pos < 0)
        error(M_NOTARJ, arc_name);
    file_seek(arcfile, first_hdr_pos, SEEK_SET);
    if (!read_header(1, arcfile, arc_name))
        error(M_BADCOMNT, "");
    get_date_str(date_str, time_stamp);
    if(!m_bQuiet) printf(M_ARCDATE, date_str);
    if (arj_nbr >= ARJ_M_VERSION)
    {
        get_date_str(date_str, (ulong) compsize);
        if(!m_bQuiet) printf(M_ARCDATEM, date_str);
    }
    if(!m_bQuiet) printf("\n");

    file_count = 0;
    while (read_header(0, arcfile, arc_name))
    {
        switch (command)
        {
        case 'E':
        case 'X':
            {
                if (extract())
                    file_count++;
            }
            break;
        case 'L':
            list_arc(file_count++);
            skip();
            break;
        case 'T':
            if (test())
                file_count++;
            break;
        }
    }

    if (command == 'L')
    {
        if(!m_bQuiet) printf("------------ ---------- ---------- ----- -----------------\n");
        r = ratio(tcompsize, torigsize);
        if(!m_bQuiet) printf(" %5d files %10ld %10ld %u.%03u %s\n",
            file_count, torigsize, tcompsize, r / 1000, r % 1000, &date_str[2]);
    }
    else
    {
        if(!m_bQuiet) printf(M_NBRFILES, file_count);
    }

    fclose(arcfile);
}


void UnArj::help()
{
    int i;

    for (i = 0; M_USAGE[i] != NULL; i++)
        printf(M_USAGE[i]);
}

//???!
int UnArj::do_main(int  argc, char* argv[])
{
    int i, j, lastc;
    char *arc_p;

#ifdef THINK_C
    argc = ccommand(&argv);
#endif

    if(!m_bQuiet) printf(M_VERSION);

    if (argc == 1)
    {
        help();
        return EXIT_SUCCESS;
    }
    else if (argc == 2)
    {
        command = 'L';
        arc_p = argv[1];
    }
    else if (argc >= 3)
    {
        if (strlen(argv[1]) > 1)
            error(M_BADCOMND, argv[1]);
        command = toupper(*argv[1]);
        if (strchr("ELTX", command) == NULL)
            error(M_BADCOMND, argv[1]);
        arc_p = argv[2];
        for(int i = 3;i<argc;i++)
        {
            switch(i)
            {
            case 3:
                strcpy(m_sArg3, argv[i]);
                break;
            case 4:
                strcpy(m_sArg4, argv[i]);
                break;
            default:
                break;
            }
        }
    }
    else
    {
        help();
        return EXIT_FAILURE;
    }

    strncopy(arc_name, arc_p, FNAME_MAX);
    case_path(arc_name);
    i = strlen(arc_name);
    j = parse_path(arc_name, (char *)NULL, (char *)NULL);
    lastc = arc_name[i - 1];
    if (lastc == ARJ_DOT)
        arc_name[i - 1] = NULL_CHAR;
    else if (strchr(&arc_name[j], ARJ_DOT) == NULL)
        strcat(arc_name, M_SUFFIX);

    make_crctable();

    error_count = 0;
    clock_inx = 0;
    arcfile = NULL;
    outfile = NULL;

    execute_cmd();

    if (error_count > 0)
        error(M_ERRORCNT, "");

    return EXIT_SUCCESS;
}




UnArjDecoder* UnArj::get_Decoder()
{
    if(m_pDecoder == NULL)
    {
        m_pDecoder = new UnArjDecoder(this);
    }
    return m_pDecoder;
}


bool UnArj::get_Quiet()
{
    return m_bQuiet;
}

void UnArj::set_Quiet(bool val)
{
    m_bQuiet = val;
}
