#ifndef _DEFS_H__
#define _DEFS_H__

#define ISBITSET(bitmask, bit)      ((bitmask & bit) == bit)
#define ISBITRESET(bitmask, bit)    (!ISBITSET(bitmask, bit))
#define SETBIT(bitmask, bit)        bitmask &= bit;
#define RESETBIT(bitmask, bit)      bitmask &= ~bit;

/* assert macro */
#if !defined(ASSERT)
/* DDK has ASSERT() macro */
#define ASSERT(X) \
if(!(X)) { \
        fprintf(stderr, "ASSERT: %s\nline %d file %s\n", #X, __LINE__, __FILE__); \
        _asm { int 3 }; \
    }
#endif

#endif