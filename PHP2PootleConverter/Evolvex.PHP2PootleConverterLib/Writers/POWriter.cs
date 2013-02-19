using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.PHP2PootleConverterLib.Interfaces;
using Evolvex.PHP2PootleConverterLib.Data;
using System.IO;

namespace Evolvex.PHP2PootleConverterLib.Writers
{
    public class POWriter : IWriter
    {
        #region IWriter Members

        public void WriteAll(List<TranslationEntry> entries, StringBuilder sb)
        {
            foreach (TranslationEntry te in entries)
            {
                if (!string.IsNullOrEmpty(te.Comment))
                    AppendComment(sb, te.Comment);
                if (!String.IsNullOrEmpty(te.MsgStr))
                    AppendComment(sb, te.MsgStr);
                if (!String.IsNullOrEmpty(te.MsgId))
                {
                    sb.AppendLine(String.Format("msgid \"{0}\"", te.MsgId));
                    sb.AppendLine("msgstr \"\"");
                }
            }
        }

        private static void AppendComment(StringBuilder target, string commentText)
        {
            target.AppendLine(String.Format("#. {0}", commentText));
        }

        public static string EscapeForPO(string php)
        {
            return php.Replace("\"", "\\\"");
        }

        #endregion
    }
}
