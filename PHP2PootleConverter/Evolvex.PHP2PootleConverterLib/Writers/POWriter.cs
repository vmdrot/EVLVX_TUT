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
                    sb.AppendLine(String.Format("#. {0}", te.Comment));
                if(!String.IsNullOrEmpty(te.MsgId))
                    sb.AppendLine(String.Format("msgid \"{0}\"", te.MsgId)); 
                if(!String.IsNullOrEmpty(te.MsgStr))
                    sb.AppendLine(String.Format("msgstr \"{0}\"", te.MsgStr)); 
            }
        }

        #endregion
    }
}
