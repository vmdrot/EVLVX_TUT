#ifndef __UN_ARJ_ENVIRON_H_
#define __UN_ARJ_ENVIRON_H_
#include "stdafx.h"
#include "UnArj.h"
;
using namespace UnArjConstants;
FILE* file_open(char *name, char *mode);
int file_read(char *buf, int  size, int  nitems, FILE *stream);
int file_seek(FILE *stream, long offset, int  mode);
long file_tell(FILE *stream);
int file_write(char *buf, int  size, int  nitems, FILE *stream);
voidp* xmalloc(int size);
void case_path(char* name);
void default_case_path(char *name);
int file_exists(char* name);
void get_mode_str(char *str, uint mode);
int set_ftime_mode(char  *name, ulong tstamp, uint  attribute, uint  host);


#endif //__UN_ARJ_ENVIRON_H_