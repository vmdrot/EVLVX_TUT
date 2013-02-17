using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.PHP2PootleConverterLib.Data;

namespace Evolvex.PHP2PootleConverterLib.Interfaces
{
    public interface IReader
    {
        List<TranslationEntry> ReadAll(string[] lines);
    }
}
