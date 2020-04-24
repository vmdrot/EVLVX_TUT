using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class VSUsageRecSession
    {
        public VSUsageRecSession()
        {
            Usages = new List<VSUsageRec>();
            DefFiles = new List<DefFileTypeRec>();
            SkipClasses = new List<string>();
        }
        public List<VSUsageRec> Usages { get; set; }
        public List<DefFileTypeRec> DefFiles { get; set; }
        public List<string> SkipClasses { get; set; }
    }
}
