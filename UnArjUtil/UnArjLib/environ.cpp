#include "stdafx.h"
#include "environ.h"

FILE* file_open(char* name, char* mode)
{
    return fopen(name, mode);
}

int
file_read(char *buf, int  size, int  nitems, FILE *stream)
{
    return fread(buf, (int) size, (int) nitems, stream);
}

int file_seek(FILE *stream, long offset, int  mode)
{
    return fseek(stream, offset, mode);
}

long file_tell(FILE *stream)
{
    return ftell(stream);
}

int file_write(char *buf, int  size, int  nitems, FILE *stream)
{
    return fwrite(buf, (int) size, (int) nitems, stream);
}

voidp* xmalloc(int size)
{
    return (voidp *)malloc((uint) size);
}

void case_path(char* name)
{
    (char *) name;
}

void default_case_path(char *name)
{
    (char *) name;
}

int file_exists(char* name)
{
    FILE *fd;

    if ((fd = fopen(name, "rb")) == NULL)
        return 0;
    fclose(fd);
    return 1;
}

void get_mode_str(char *str, uint mode)
{
    strcpy(str, "---W");
    if (mode & FA_ARCH)
        str[0] = 'A';
    if (mode & FA_SYSTEM)
        str[1] = 'S';
    if (mode & FA_HIDDEN)
        str[2] = 'H';
    if (mode & FA_RDONLY)
        str[3] = 'R';
}

int set_ftime_mode(char  *name, ulong tstamp, uint  attribute, uint  host)
{
    (char *) name;
    (ulong) tstamp;
    (uint) attribute;
    (uint) host;
    return 0;
}
