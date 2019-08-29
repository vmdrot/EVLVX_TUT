/**
* Copyright (c) 2006
* Asempra Technologies, Inc.
* 
* @file        isdk_scope_guard.h
* @author      Yuri Gaidenko
* 
* $Id: isdk_scope_guard.h,v 1.1 2006/08/30 11:53:00 vdrotenko Exp $
* 
* Worker and base classes to facilitate Win API resources management (e.g. ensure closing 
* handles on exiting the scope).
* 
*/

#ifndef __ISDK_SCOPE_GUARD_H__
#define __ISDK_SCOPE_GUARD_H__

#include "evolvex_common_lib.h"
class isdk_Scope_Guard;

typedef isdk_Scope_Guard* isdk_scope_guard_ptr_t;

///
///  abstract isdk_Scope_Guard
///  class that embraces Win API resources management
///     May be enabled to manage whatever kinds of resources, which release fits 
///  the following paradigm : resource is being released by a single Win API function 
///  call, taking one single parameter (i.e. resource handle).
///     Supports a number of various types of resources to be closed, using the 
///  technique of constructors nesting.
///     In order to extend, just add a resource closing function prototype, add a 
///  member variable of this type and an inline function to allow customized 
///  constructor for the resource guard of your type (see also isdk_scope_guard_aux.h).
///
///
class isdk_Scope_Guard
{
public:
	virtual void cleanup() const = 0;
    virtual ~isdk_Scope_Guard()
	{
		if(NULL != m_sg)
		{
            try
            {
			    delete m_sg;
		    	m_sg = NULL;
            }
            catch(...)
            {
            }
        }
	}
protected:
    isdk_Scope_Guard(isdk_scope_guard_ptr_t sg)
		: m_sg(sg)
    {
	}
	isdk_scope_guard_ptr_t m_sg;
};

typedef std::auto_ptr<isdk_Scope_Guard> isdk_scope_guard_t;

template <typename TProc, typename TArg>
class isdk_Scope_Guard_Proc : public isdk_Scope_Guard
{
public:
	virtual void cleanup() const
	{
		if (NULL != *m_arg)
        {
            //LOG_DEBUG("%s:: *m_arg = %x", __FUNCTION__, *m_arg);
            m_proc(*m_arg);
            *m_arg = NULL;  
        }
	}
	virtual ~isdk_Scope_Guard_Proc()
	{
		cleanup();
	}
	static isdk_scope_guard_ptr_t create(TProc proc, TArg* arg, isdk_scope_guard_ptr_t sg = NULL)
	{
		return new isdk_Scope_Guard_Proc(proc, arg, sg);
	}
protected:
    isdk_Scope_Guard_Proc(const TProc& proc, TArg* arg, isdk_scope_guard_ptr_t sg = NULL)
    : isdk_Scope_Guard(sg), m_proc(proc), m_arg(arg) 
    {
	}
private:
    TProc		m_proc;
    TArg*	m_arg;
};

#endif //__ISDK_SCOPE_GUARD_H__