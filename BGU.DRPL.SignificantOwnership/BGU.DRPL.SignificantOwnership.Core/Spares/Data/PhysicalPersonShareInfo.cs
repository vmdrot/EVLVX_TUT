using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class PhysicalPersonShareInfo
    {
        public PhysicalPersonInfo Person { get; set; }
        public decimal SharePct { get; set; }
    }
}
