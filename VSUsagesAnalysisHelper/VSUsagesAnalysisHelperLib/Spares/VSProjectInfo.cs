using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib.Spares
{
    public class VSProjectInfo
    {
        public string Name { get; set; }
        public string TargetFramework {get;set;}
        public int BuildOrder { get; set; }
    }
}
