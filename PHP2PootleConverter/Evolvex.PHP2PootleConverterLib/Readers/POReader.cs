
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.PHP2PootleConverterLib.Interfaces;
using Evolvex.PHP2PootleConverterLib.Data;
using System.Text.RegularExpressions;

namespace Evolvex.PHP2PootleConverterLib.Readers
{
    public class POReader : IReader
    {
        private const string RGX_TOKEN_KEY = "msgid \"([\\w]+)\"";
        private const string RGX_TOKEN_VAL = "msgstr \"(.+)\"";

        #region IReader Members

        public List<TranslationEntry> ReadAll(string[] lines)
        {
            List<TranslationEntry> rslt = new List<TranslationEntry>();
            string prevComment = string.Empty;
            string lastKey = string.Empty;

            foreach (string ln in lines)
            {
                string key;
                string val;
                if (string.IsNullOrEmpty(ln))
                {
                    if (!string.IsNullOrEmpty(prevComment))
                        rslt.Add(new TranslationEntry() { Comment = prevComment });
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
                else if(TryExtractKey(ln, out key))
                {
                    lastKey = key;
                }
                else if (TryExtractVal(ln, out val))
                {

                    rslt.Add(new TranslationEntry() { Comment = prevComment, MsgId = lastKey, MsgStr = EscapeFromPO( val) });
                    prevComment = string.Empty;

                }

            }

            return rslt;
        }
        public static string EscapeFromPO(string po)
        {
            return po.Replace("\\\"", "\"");
        }


        public bool TryExtractVal(string ln, out string val)
        {
            return TryExtractKeyValWorker(ln, RGX_TOKEN_VAL, out val);
        }

        public bool TryExtractKey(string ln, out string key)
        {
            return TryExtractKeyValWorker(ln, RGX_TOKEN_KEY, out key);
        }

        private bool TryExtractKeyValWorker(string ln, string regexp, out string keyOrval)
        {
            Regex r = new Regex(regexp);
            Match m = r.Match(ln);
            if (m.Success && m.Groups.Count >= 2)
            {
                keyOrval = m.Groups[1].Captures[0].Value;
            }
            else
            {
                keyOrval = string.Empty;
            }
            return m.Success;
        }

        private bool IsComment(string ln)
        {
            return ln.Trim().IndexOf("#") == 0;
        }

        private string TrimComment(string ln)
        {
            return ln.Trim().Substring(2);
        }

        #endregion
    }
}
