/**
* Copyright (c) 2006
* Asempra Technologies, Inc.
* 
* @file        isdk_file_reader.h
* @author      Valery Drotenko
* 
* $Id$
* 
* 
* File reading operation wrapper.
* Could have been, of course, achieved using the procedural style, but
* would seem less consistent and robust. The main sense is in controlling 
* memory allocation and freeing.
*/

#ifndef _ISDK_FILE_READER_H_
#define _ISDK_FILE_READER_H_
#include "evolvex_common_lib.h"
#include "isdk_smart_ptr.h"

class isdk_File_Reader;

typedef isdk_Smart_Ptr<isdk_File_Reader> isdk_file_reader_t;

class isdk_File_Reader
{
public:
    virtual const char* get_contents()                                       = 0;
    virtual void        get_contents(std::string& target)                    = 0;
    virtual bool        get_contents(char* target)                           = 0;
    virtual size_t      get_file_size()                                      = 0;
    virtual bool        char_at(size_t index, char& target)                  = 0;
    static isdk_file_reader_t create(const std::string& file_path, bool binary_mode = false);
protected:
    isdk_File_Reader();
};


#endif //~_ISDK_FILE_READER_H_