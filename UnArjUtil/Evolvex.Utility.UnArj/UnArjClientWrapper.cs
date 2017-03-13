using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Evolvex.Utility.UnArj
{
    public class UnArjClientWrapper
    {
        [DllImport("UnArjClient.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "unarj_extract")]
        extern static private int DoExtractFile(bool bQuietMode, string arcName, string fileInArc, string saveAs);

        static public bool ExtractFile(bool echoOff, string archive, string file, string saveTo)
        {
            return (0 == DoExtractFile(echoOff, archive, file, saveTo));
        }
        
        static public bool ExtractFile(bool echoOff, string archive, string file)
        {
            return (0 == DoExtractFile(echoOff, archive, file, string.Empty));
        }

        static public bool ExtractFile(bool echoOff, string archive)
        {
            return (0 == DoExtractFile(echoOff, archive, string.Empty, string.Empty));
        }

        static public bool ExtractFile(string archive, string file, string saveTo)
        {
            return (0 == DoExtractFile(true, archive, file, saveTo));
        }

        static public bool ExtractFile(string archive, string file)
        {
            return (0 == DoExtractFile(true, archive, file, string.Empty));
        }

        static public bool ExtractFile(string archive)
        {
            return (0 == DoExtractFile(true, archive, string.Empty, string.Empty));
        }
    }
}
