using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtilLib.EDataGovUA
{
    public class EDataGovUaDisposerInfo
    {
        public bool IsFound { get; set; }
        public long InternalID { get; set; }
        public string YeDRPOU { get; set; }
        public string DisposerName { get; set; }
        public string CabinetStatus { get; set; }
        public DateTime CheckedDttm { get; set; }
    }
}
