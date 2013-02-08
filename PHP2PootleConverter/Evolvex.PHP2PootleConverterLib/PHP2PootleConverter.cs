using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Evolvex.PHP2PootleConverterLib
{
    public class PHP2PootleConverter
    {
        private const string RGX_HEADER = @"<\?php\s+|\s+\?>";
        private const string RGX_TOKEN_PHP = @"(\/\/) ([a-zA-Z]+)
#. $1 $2\nmsgid ""$2""\nmsgstr ""// $2""";
        private const string RGX_TOKEN_PO = @"\$_\['([\w]+)'\]\s+=\s+'(.+)'
#. $_['$1']\nmsgid ""$2""\nmsgstr """;

        public bool ProcessFile(string path, ConvertDirection dir)
        {
            return dir == ConvertDirection.PHP2Pootle ? PHP2Pootle(path) : Pootle2PHP(path);
        }

        private bool PHP2Pootle(string path)
        {
            String txt = File.ReadAllText(path);
            Regex rHdr = new Regex(RGX_HEADER);
            string pureTxt = rHdr.Match(txt).Groups[0].Captures[0].Value;
            string trimmed = pureTxt.Substring(6);
            trimmed = trimmed.Substring(0, trimmed.Length - 2);
            Regex rxFind = new Regex(RGX_TOKEN_PHP);
            Regex rxRepl = new Regex(RGX_TOKEN_PO);
            rxFind.Replace(trimmed);
        }


        private bool Pootle2PHP(string path)
        {
            return false;
        }

    }
}
