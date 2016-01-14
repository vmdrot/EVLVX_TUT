using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Any2FbWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            any_2_fb2.Any2FB2Class converter = new any_2_fb2.Any2FB2Class();
            object rslt = converter.ConvertInteractive(0, true);

        }
    }
}
