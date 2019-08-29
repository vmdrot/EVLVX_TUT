/**
 * Copyright (c) 2006 
 * Asempra Technologies, Inc.
 * 
 * @file        isdk_variant.h
 * @author      Yury Gaidenko
 * 
 * This file contains declarations of isdk_IVariant and isdk_IVariantFactory.
 * isdk_IVariant NV Interface. You don't inheritance it.
 */

#ifndef __ISDK_VARIANT_H__
#define __ISDK_VARIANT_H__

#include "evolvex_common_lib.h"
#include "isdk_ivariant_private.h"
#include "isdk_smart_ptr.h"

#include <string>
#include <windows.h>
#include <tlhelp32.h>

//typedef signed char         int8_t;
typedef unsigned char       uint8_t;

typedef signed short        int16_t;
typedef unsigned short      uint16_t;

//typedef signed int          int32_t;
//typedef unsigned int        uint32_t;

typedef signed __int64      int64_t;
typedef unsigned __int64    uint64_t;

typedef GUID                guid_t;

#ifndef _UNICODE
typedef std::string         string_t;
#else
typedef std::wstring        string_t;
#endif

/**
 * Forward declaration of isdk_IVariant type 
 */
class isdk_IVariant;

/**
 * Declaration of public isdk_variant_t type through smart pointer.
 * Only this type should be used in all user code.
 */
typedef isdk_Smart_Ptr<isdk_IVariant> isdk_variant_t;

/**
 * Represent interface for storing values of different types in single object.
 * Provide "read only" access to data. 
 * Not allowed to use isdk_IVariant type in user code, only through smart pointer isdk_variant_t.
 */
class isdk_IVariant : private isdk_IVariant_Core
{
public:    
    /**
     * @return  type of data stored in object.
     */
    ISDK_TYPE    get_type() const;

    /**
     * Get int8_t value for type of variant ISDK_TYPE_INT8
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_int8(int8_t&)                           const;

    /**
     * Get uint8_t value for type of variant ISDK_TYPE_UINT8
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_uint8(uint8_t&)                         const;

    /**
     * Get int16_t value for type of variant ISDK_TYPE_INT16
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_int16(int16_t&)                         const;

    /**
     * Get uint16_t value for type of variant ISDK_TYPE_UINT16
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_uint16(uint16_t&)                       const;

    /**
     * Get int32_t value for type of variant ISDK_TYPE_INT32
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_int32(int32_t&)                         const;

    /**
     * Get uint32_t value for type of variant ISDK_TYPE_UINT32
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_uint32(uint32_t&)                       const;

    /**
     * Get int64_t value for type of variant ISDK_TYPE_INT64
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_int64(int64_t&)                         const;

    /**
     * Get uint64_t value for type of variant ISDK_TYPE_UINT64
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_uint64(uint64_t&)                       const;

    /**
     * Get string_t value for type of variant ISDK_TYPE_STRING
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_string(string_t&)                       const;

    /**
     * Get float value for type of variant ISDK_TYPE_FLOAT
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_float(float&)                           const;

    /**
     * Get double value for type of variant ISDK_TYPE_DOUBLE
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_double(double&)                         const;

    /**
     * Get guid_t value for type of variant ISDK_TYPE_GUID
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_guid(guid_t&)                           const;

    /**
     * Get guid_t value for type of variant ISDK_TYPE_HEAPLIST32
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_heap_list_32(HEAPLIST32&)               const;

    /**
     * Get guid_t value for type of variant ISDK_TYPE_MODULEENTRY32
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_module_entry_32(MODULEENTRY32&)         const;

    /**
     * Get guid_t value for type of variant ISDK_TYPE_THREADENTRY32
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_thread_entry_32(THREADENTRY32&)         const;

    /**
     * Get guid_t value for type of variant ISDK_TYPE_PROCESSENTRY32
     * @return  operation result code, ISDK_SUCCES if type of data matched, ISDK_NOT_SUCCESS otherwise.
     */
    isdk_rslt_code_t get_process_entry_32(PROCESSENTRY32&)         const;

protected:

    /**
     * Default constructor.
     * Is inaccessible.
     * Use isdk_IVariantFactory for creation of isdk_IVariant object, for example:
     *     isdk_variant obj = isdk_IVariantFactory::create_[...]([...]);
     * @see isdk_IVariantFactory
     */
    isdk_IVariant();
};



/**
 * Creates isdk_variant_t objects, storing defined data.
 * This is a only way to create isdk_variant_t objects.
 */
class isdk_IVariantFactory
{
public:

    /**
     * Create variant object from int8_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_int8(int8_t);

    /**
     * Create variant object from uint8_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_uint8(uint8_t);

    /**
     * Create variant object from int16_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_int16(int16_t);

    /**
     * Create variant object from uint16_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_uint16(uint16_t);

    /**
     * Create variant object from int32_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_int32(int32_t);

    /**
     * Create variant object from uint32_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_uint32(uint32_t);

    /**
     * Create variant object from int64_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_int64(int64_t);

    /**
     * Create variant object from uint64_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_uint64(uint64_t);

    /**
     * Create variant object from string_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_string(const string_t&);

    /**
     * Create variant object from float type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_float(float);

    /**
     * Create variant object from double type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_double(double);

    /**
     * Create variant object from guid_t type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_guid(const guid_t&);   

    /**
     * Create variant object from HEAPLIST32 type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_heap_list_32(const HEAPLIST32&);   

    /**
     * Create variant object from MODULEENTRY32 type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_module_entry_32(const MODULEENTRY32&);   

    /**
     * Create variant object from THREADENTRY32 type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_thread_entry_32(const THREADENTRY32&);   

    /**
     * Create variant object from ISDK_TYPE_PROCESSENTRY32 type.
     * @return  constructed object storing the data specified.
     */
    static isdk_variant_t create_process_entry_32(const PROCESSENTRY32&);


    /**
     * Create variant object without data. 
     * @return  constructed object.
     */
    static isdk_variant_t create_pure();
};

#endif //__ISDK_VARIANT_H__