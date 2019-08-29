/**
 * Copyright (C) 2006 Asempra Technologies
 * All rights reserved.
 *
 * @file        isdk_variant.cpp
 * @author      Yury Gaidenko
 * 
 * This file contains implementation of isdk_IVariant interface and isdk_IVariantFactory class.
 */

#include "evolvex_common_lib.h"
#include "isdk_variant.h"

#include <map>

#include <crtdbg.h>


isdk_IVariant_Core::isdk_IVariant_Core(){}
isdk_IVariant_Core::~isdk_IVariant_Core(){}

isdk_IVariant::isdk_IVariant(){}


ISDK_TYPE
isdk_IVariant::get_type() const
{ 
    return core_get_type();
}

isdk_rslt_code_t
isdk_IVariant::get_int8(int8_t& v) const
{
    return core_get_value(ISDK_TYPE_INT8, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_uint8(uint8_t& v) const
{
    return core_get_value(ISDK_TYPE_UINT8, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_int16(int16_t& v) const
{
    return core_get_value(ISDK_TYPE_INT16, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_uint16(uint16_t& v) const
{
    return core_get_value(ISDK_TYPE_UINT16, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_int32(int32_t& v) const
{
    return core_get_value(ISDK_TYPE_INT32, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_uint32(uint32_t& v) const
{
    return core_get_value(ISDK_TYPE_UINT32, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_int64(int64_t& v) const
{
    return core_get_value(ISDK_TYPE_INT64, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_uint64(uint64_t& v) const
{
    return core_get_value(ISDK_TYPE_UINT64, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_string(string_t& v) const
{
    return core_get_value(ISDK_TYPE_STRING, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_float(float& v) const
{
    return core_get_value(ISDK_TYPE_FLOAT, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_double(double& v) const
{
    return core_get_value(ISDK_TYPE_DOUBLE, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_guid(guid_t& v) const
{
    return core_get_value(ISDK_TYPE_GUID, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_heap_list_32(HEAPLIST32& v) const
{
    return core_get_value(ISDK_TYPE_HEAPLIST32, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_module_entry_32(MODULEENTRY32& v) const
{
    return core_get_value(ISDK_TYPE_MODULEENTRY32, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_thread_entry_32(THREADENTRY32& v) const
{
    return core_get_value(ISDK_TYPE_THREADENTRY32, (isdk_variant_internal_t)&v);
}

isdk_rslt_code_t
isdk_IVariant::get_process_entry_32(PROCESSENTRY32& v) const
{
    return core_get_value(ISDK_TYPE_PROCESSENTRY32, (isdk_variant_internal_t)&v);
}

/**
 * Implement isdk_IVariant interface. Do not hold any data.
 */
class isdk_Variant_Pure : public isdk_IVariant
{
public:   
    virtual ISDK_TYPE core_get_type() const
    {
        return ISDK_TYPE_UNKNOWN;
    }
    virtual isdk_rslt_code_t core_get_value(ISDK_TYPE type, isdk_variant_internal_t data) const
    {
        (void)type;
        (void)data;

        _ASSERT(0 && "invalid type cast");
        return ISDK_SUCCESS;
    }

protected:

   friend isdk_variant_t isdk_IVariantFactory::create_pure();

    isdk_Variant_Pure(){}
};

/**
 * Implement isdk_IVariant interface. Do not hold any data.
 */
template <class TYPE_OF_DATA, ISDK_TYPE TYPE_OF_VARIANT>
class isdk_Variant_Generic : public isdk_Variant_Pure
{
public:
    ISDK_TYPE core_get_type() const
    {
        return TYPE_OF_VARIANT;
    }
    
    isdk_rslt_code_t core_get_value(ISDK_TYPE type, isdk_variant_internal_t v) const
    {
        if(get_type() != type)
            return ISDK_NOT_SUCCESS;

        *((TYPE_OF_DATA*)v) = m_value;

        return ISDK_SUCCESS;
    }

private:

    friend class isdk_IVariantFactory;

    /**
     * Constructor from TYPE_OF_DATA, any template type.
     */
    isdk_Variant_Generic(const TYPE_OF_DATA& v) : m_value(v)
    {
    }

    TYPE_OF_DATA m_value;    /**< data of type TYPE_OF_DATA stored in object*/  
};


isdk_variant_t
isdk_IVariantFactory::create_pure()
{
    return isdk_variant_t(new isdk_Variant_Pure() );
}

isdk_variant_t
isdk_IVariantFactory::create_int8(int8_t value)
{
    return isdk_variant_t(new isdk_Variant_Generic<int8_t, ISDK_TYPE_INT8>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_uint8(uint8_t value)
{
    return isdk_variant_t(new isdk_Variant_Generic<uint8_t, ISDK_TYPE_UINT8>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_int16(int16_t value)
{
    return isdk_variant_t(new isdk_Variant_Generic<int16_t, ISDK_TYPE_INT16>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_uint16(uint16_t value)
{
    return isdk_variant_t(new isdk_Variant_Generic<uint16_t, ISDK_TYPE_UINT16>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_int32(int32_t value)
{
    return isdk_variant_t(new isdk_Variant_Generic<int32_t, ISDK_TYPE_INT32>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_uint32(uint32_t value)
{
    return isdk_variant_t(new isdk_Variant_Generic<uint32_t, ISDK_TYPE_UINT32>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_int64(int64_t value)
{
    return isdk_variant_t(new isdk_Variant_Generic<int64_t, ISDK_TYPE_INT64>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_uint64(uint64_t value)
{
    return isdk_variant_t(new isdk_Variant_Generic<uint64_t, ISDK_TYPE_UINT64>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_string(const string_t& value)
{
    return isdk_variant_t(new isdk_Variant_Generic<string_t, ISDK_TYPE_STRING>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_float(float value)
{
    return isdk_variant_t(new isdk_Variant_Generic<float, ISDK_TYPE_FLOAT>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_double(double value)
{
    return isdk_variant_t(new isdk_Variant_Generic<double, ISDK_TYPE_DOUBLE>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_guid(const guid_t& value)
{
    return isdk_variant_t(new isdk_Variant_Generic<guid_t, ISDK_TYPE_GUID>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_heap_list_32(const HEAPLIST32& value)
{
    return isdk_variant_t(new isdk_Variant_Generic<HEAPLIST32, ISDK_TYPE_HEAPLIST32>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_module_entry_32(const MODULEENTRY32& value)
{
    return isdk_variant_t(new isdk_Variant_Generic<MODULEENTRY32, ISDK_TYPE_MODULEENTRY32>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_thread_entry_32(const THREADENTRY32& value)
{
    return isdk_variant_t(new isdk_Variant_Generic<THREADENTRY32, ISDK_TYPE_THREADENTRY32>(value));
}

isdk_variant_t
isdk_IVariantFactory::create_process_entry_32(const PROCESSENTRY32& value)
{
    return isdk_variant_t(new isdk_Variant_Generic<PROCESSENTRY32, ISDK_TYPE_PROCESSENTRY32>(value));
}