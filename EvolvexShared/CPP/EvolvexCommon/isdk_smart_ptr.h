#ifndef __ISDK_SMART_PTR_H__
#define __ISDK_SMART_PTR_H__

/**
* counter
*
* This class implements base operations with counter for smartPtr template.
*/
class isdk_Counter
{
    typedef unsigned long count_t;
    count_t * pcounter;
protected:
    isdk_Counter(): pcounter(new count_t(1)) { }
    isdk_Counter(const isdk_Counter & c): pcounter(c.pcounter)
    {
        ++*pcounter;
    }
    bool last(void) { return *pcounter == 1; }
    ~isdk_Counter()
    {
        if( last() )
            delete pcounter;
        else
            --*pcounter;
    }
    // supplanting
    void operator=(const isdk_Counter & c)
    {
        --*pcounter;
        pcounter = c.pcounter;
        ++*pcounter;
    }
};
/**
* smartPtr
*
* It is very useful template which uses as smart pointer.
* If customer creates an instance of class T once, [s]he can forget it.
* This template remove instance then removes last reference to it.
* Be careful! Do not use delete operator for instance which was saved
* in this template class instance, because in this case it will be destroyed
* twice and it'll damage Your program.
*
*/
template < class T > class isdk_Smart_Ptr : public isdk_Counter
{
    typedef T* lpT;
    T * psaved;
public:
    explicit isdk_Smart_Ptr(T* p) : isdk_Counter(), psaved(p) { }
    isdk_Smart_Ptr() : isdk_Counter(), psaved(0) { }
    isdk_Smart_Ptr(const isdk_Smart_Ptr<T> &sptr) : isdk_Counter(sptr), psaved(sptr.psaved) { }
    ~isdk_Smart_Ptr()
    {
        if(!psaved) return;
        if( last() ) delete psaved;
        psaved = 0;
    }
    isdk_Smart_Ptr<T> &operator=(const isdk_Smart_Ptr<T> &sptr)
    {
        if(this != &sptr)
        {
            isdk_Smart_Ptr<T> temp(*this);
            isdk_Counter::operator =(sptr);
            psaved = sptr.psaved;
        }
        return *this;
    }
    T&  operator*   () const    {return *get(); }
    T*  operator->  () const    {return get(); }
        operator lpT() const    {return get(); }
    T*  get         () const    {return psaved; }
    bool operator!  () const    {return psaved == 0;}
    operator    bool() const    {return psaved == 0 ? false : true; }

    bool operator == (const isdk_Smart_Ptr<T> &sptr) const
    {
        return psaved == sptr.psaved;
    }
    bool operator == (T* p) const 
    {
        return psaved == p;
    }
    T*  get_n_clear() // workaround for move from sptr of one type to another
    {
        T* obj = psaved;
        psaved = 0;
        return obj;
    }

    bool empty() const{ return psaved==0;}
};

#endif //__ISDK_SMART_PTR_H__