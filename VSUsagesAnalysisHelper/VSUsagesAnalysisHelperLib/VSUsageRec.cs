using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class VSUsageRec
    {
        public VSUsageRec()
        {
            IsNoReferences = false;
        }
        public string UsageNode { get; set; }
        public string Code { get; set; }
        public string File { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public string Project { get; set; }
        public string Kind { get; set; }
        public string ContainingMember { get; set; }
        public string ContainingMemberSignature { get; set; }
        public string ContainingType { get; set; }
        public string ContainingNamespace { get; set; }

        public string DefFile { get; set; }
        public string DefType { get; set; }
        public string DefMember { get; set; }
        public string DefMemberSignature { get; set; }
        public bool IsNoReferences { get; set; }
        public string UDF01 { get; set; }
        public string Sln { get; set; }
        public string SlnPath { get; set; }
    }
}
