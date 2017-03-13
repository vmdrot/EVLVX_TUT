#include <iostream>
#include "../UnArjLib/UnArj.h"

int main(int argc, char* argv[])
{
    UnArj uArj;
    uArj.set_Quiet(true);
    return uArj.do_main(argc, argv);
}
