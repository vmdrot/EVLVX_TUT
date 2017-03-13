using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Evolvex.Utility.UnArj
{
    public class UnArjUtil
    {
        [DllImport("UNARJ32J.DLL", CallingConvention = CallingConvention.StdCall)]
        extern static public long UnarjQueryFunctionList(long iFunction);
        
        [DllImport("UNARJ32J.DLL", CallingConvention = CallingConvention.StdCall)]
        extern static public long UnarjCheckArchive(string szFilename, long iMode );

        [DllImport("UNARJ32J.DLL", CallingConvention = CallingConvention.StdCall)]
        extern static public long UnarjOpenArchive(long hWnd, string szFilename, long dwMode);
        [DllImport("UNARJ32J.DLL", CallingConvention = CallingConvention.StdCall)]
        extern static public long UnarjCloseArchive(long harc);
        [DllImport("UNARJ32J.DLL", CallingConvention = CallingConvention.StdCall)]
        extern static public long Unarj(long hWnd, string szCmdLine, StringBuilder szOutput, long wSize);

        static public long Unarj(string sArchiveFullPath, string extractToPath)
        {
            string currDir = System.IO.Directory.GetCurrentDirectory();
            string sCmd = string.Format("e {0}", sArchiveFullPath);
            StringBuilder sbOutput = new StringBuilder(1024);
            //System.IO.Directory.SetCurrentDirectory(extractToPath);
            long lRslt = UnArjUtil.Unarj(0, sCmd, sbOutput, 1024);
            //System.IO.Directory.SetCurrentDirectory(currDir);
            return lRslt;
        }

        /*
         * seems like above stuff is not working
         * we'll, very probably, stick back to console like:
         * UNARJ.EXE e d:\tmp\RcuKru.arj
         */

    }
}
