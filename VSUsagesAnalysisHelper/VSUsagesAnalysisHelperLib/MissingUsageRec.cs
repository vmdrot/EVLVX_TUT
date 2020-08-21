using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class MissingUsageRec
    {
        public string ContainingMember { get; set; }
        public string ContainingType { get; set; }
        public string ContainingNamespace { get; set; }
        public string Project { get; set; }
        public string File { get; set; }
    }
}
