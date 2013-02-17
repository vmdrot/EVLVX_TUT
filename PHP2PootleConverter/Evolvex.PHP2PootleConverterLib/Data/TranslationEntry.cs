using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.PHP2PootleConverterLib.Data
{
    public class TranslationEntry
    {
        public String Comment { get; set; }
        public String MsgId { get; set; }
        public String MsgStr { get; set; }

        public TranslationEntry() 
        { }

        public TranslationEntry(string comment, string msgId, string msgStr)
        {
            Comment = comment; MsgId = msgId; MsgStr = msgStr;
        }
    }
}
