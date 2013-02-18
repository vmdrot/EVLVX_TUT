using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.PHP2PootleConverterLib.Data
{
    public class ConversionSettings
    {
        public ConversionSettings()
        {
            Direction = ConvertDirection.PHP2Pootle;
            DeleteSourceFiles = false;
            SaveAsEncoding = Encoding.UTF8;
            SourceLanguageName = "english";
            TargetLanguageName = "russian";
        }
        public string SourceDir { get; set; }
        public string TargetDir { get; set; }
        public ConvertDirection Direction { get; set; }
        public string SourceLanguageName { get; set; }
        public string TargetLanguageName { get; set; }
        public bool DeleteSourceFiles { get; set; }
        public Encoding SaveAsEncoding { get; set; }
    }
}
