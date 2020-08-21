using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class NugetPackageRec
    {
        public string SolutionName { get; set; }
        public string SolutionPath { get; set; }
        public string ProjectName { get; set; }
        public string Id { get; set; }
        public string Version { get; set; }
        public string LatestStableVersion { get; set; }
    }
}
