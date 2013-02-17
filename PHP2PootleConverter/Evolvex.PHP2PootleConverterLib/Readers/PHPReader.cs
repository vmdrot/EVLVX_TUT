using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.PHP2PootleConverterLib.Interfaces;
using Evolvex.PHP2PootleConverterLib.Data;
using System.Text.RegularExpressions;

namespace Evolvex.PHP2PootleConverterLib.Readers
{
    public class PHPReader : IReader
    {

        private const string RGX_TOKEN_KEYVAL = @"\$_\['([\w]+)'\]\s+=\s+'(.+)'";
        
        #region IReader Members

        public List<TranslationEntry> ReadAll(string[] lines)
        {
            List<TranslationEntry> rslt = new List<TranslationEntry>();
            string prevComment = string.Empty;
            foreach (string ln in lines)
            {
                if (string.IsNullOrEmpty(ln))
                {
                    if (!string.IsNullOrEmpty(prevComment))
                        rslt.Add(new TranslationEntry() { Comment = prevComment});
                    prevComment = string.Empty;
                    continue;
                }
                bool isComment = IsComment(ln);
                if (isComment)
                {
                    if (!string.IsNullOrEmpty(prevComment))
                        rslt.Add(new TranslationEntry() { Comment = prevComment });
                    prevComment = TrimComment(ln);
                }
                else
                {
                    string key;
                    string val;
                    if (TryExtractKeyVal(ln, out key, out val))
                    {
                        rslt.Add(new TranslationEntry(prevComment, key, val));
                        prevComment = string.Empty;
                    }
                }
            }

            return rslt;
        }

        public bool TryExtractKeyVal(string ln, out string key, out string val)
        {
            Regex r = new Regex(RGX_TOKEN_KEYVAL);
            Match m = r.Match(ln);
            if (m.Success && m.Groups.Count >= 3)
            {
                key = m.Groups[1].Captures[0].Value;
                val = m.Groups[2].Captures[0].Value;
            }
            else
            {
                key = string.Empty;
                val = string.Empty;
            }
            return m.Success;
        }

        private bool IsComment(string ln)
        {
            return ln.Trim().IndexOf("//") == 0;
        }

        private string TrimComment(string ln)
        {
            return ln.Trim().Substring(2).Trim();
        }

        #endregion
    }
}
