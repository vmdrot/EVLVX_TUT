using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.PHP2PootleConverterLib.Data;
using System.IO;

namespace Evolvex.PHP2PootleConverterLib.Interfaces
{
    public interface IWriter
    {
        void WriteAll(List<TranslationEntry> entries, StringBuilder target);
    }
}
