using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class DefFileTypeRec
    {
        public DefFileTypeRec()
        {
            IsEndUserFacing = false;
        }
        public string DefFile { get; set; }
        public string DefType { get; set; }
        public bool IsEndUserFacing { get; set; }
    }
}
