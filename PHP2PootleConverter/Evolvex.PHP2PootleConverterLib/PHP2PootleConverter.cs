using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Evolvex.PHP2PootleConverterLib.Readers;
using Evolvex.PHP2PootleConverterLib.Writers;

namespace Evolvex.PHP2PootleConverterLib
{
    public class PHP2PootleConverter
    {
        private const string RGX_HEADER = @"<\?php\s+|\s+\?>";
        private const string PHP_HEADER_TOKEN = "<?php";
        private const string PHP_TRAIL_TOKEN = "?>";


        public bool ProcessFile(string srcPath, string targetPath, ConvertDirection dir)
        {
            return dir == ConvertDirection.PHP2Pootle ? PHP2Pootle(srcPath, targetPath) : Pootle2PHP(srcPath, targetPath);
        }

        private bool PHP2Pootle(string srcPath, string targetPath)
        {
            PHPReader r = new PHPReader();
            POWriter w = new POWriter();
            StringBuilder sb = new StringBuilder();
            w.WriteAll(r.ReadAll(File.ReadAllLines(srcPath)), sb);
            File.WriteAllText(targetPath, sb.ToString());
            return true;
        }


        private bool Pootle2PHP(string srcPath, string targetPath)
        {
            POReader r = new POReader();
            PHPWriter w = new PHPWriter();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(PHP_HEADER_TOKEN);
            w.WriteAll(r.ReadAll(File.ReadAllLines(srcPath)), sb);
            sb.AppendLine(PHP_TRAIL_TOKEN);
            File.WriteAllText(targetPath, sb.ToString());
            return true;
        }

    }
}
