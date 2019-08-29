/**
 * Copyright (c) 2006
 * Asempra Technologies, Inc.
 * 
 * @file        isdk_ivariant_private.h
 * @author      Yury Gaidenko
 * 
 * This file contains declarations of IVariant internal implementation and 
 * it only for internal using.
 *
 */

#ifdef __ISDK_IVARIANT_PRIVATE_H__
#error isdk_ivariant_private.h illegal used! Only isdk_variant.h can include it.
#else
#ifndef __ISDK_VARIANT_H__
#error isdk_ivariant_private.h illegal used! Only isdk_variant.h can include it.
#endif
#define __ISDK_IVARIANT_PRIVATE_H__

/**
 * Redefinition of void* pointer for use to point on data of unknown type.
 */
typedef void* isdk_variant_internal_t;

enum isdk_rslt_code_t
{
    ISDK_SUCCESS        = 0,
    ISDK_NOT_SUCCESS    = 1
};

enum ISDK_TYPE
{
    ISDK_TYPE_UNKNOWN = 0,
    ISDK_TYPE_INT8    = 1,
    ISDK_TYPE_UINT8   = 2,
    ISDK_TYPE_INT16   = 3,
    ISDK_TYPE_UINT16  = 4,
    ISDK_TYPE_INT32   = 5,
    ISDK_TYPE_UINT32  = 6,
    ISDK_TYPE_INT64   = 7,
    ISDK_TYPE_UINT64  = 8, 
    ISDK_TYPE_FLOAT   = 9,
    ISDK_TYPE_DOUBLE  = 10,
    ISDK_TYPE_STRING  = 11,
    ISDK_TYPE_GUID    = 12,

    ISDK_TYPE_HEAPLIST32    = 13,
    ISDK_TYPE_MODULEENTRY32 = 14,
    ISDK_TYPE_THREADENTRY32 = 15,
    ISDK_TYPE_PROCESSENTRY32= 16
};

/**
 * Defines interface for variant implementation.
 * Through variant_internal_t can deal with any type.
 */
class isdk_IVariant_Core
{
public:

    /** 
     * Derived types can be deleted through this interface, 
     * so we must provide virtual destructor.
     */
    virtual ~isdk_IVariant_Core();
private:

    friend class isdk_IVariant;

    /** 
     * Default constructor is private.
     * It suppress creation of this type by types other than isdk_IVariant.
     */
    isdk_IVariant_Core();

    /**
     * @return  type of data stored in object.
     */
    virtual ISDK_TYPE        core_get_type()                         const = 0;

    /**
     * Get value for any type of variant, used in derived types in implementation.
     * @return  operation result code, RSLT_SUCCES if type of data matched, RSLT_NOT_SUCCESS otherwise.
     */
    virtual isdk_rslt_code_t core_get_value(ISDK_TYPE, isdk_variant_internal_t) const = 0;
};

#endif //_GWDP_IVARIANT_PRIVATE_H_