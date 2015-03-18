using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class CommonOwnershipInfo
    {
        public List<GenericPersonInfo> Partners { get; set; }
        public CommonOwnershipType OwnershipType { get; set; }
        public string OwnershipTestimony { get; set; }
        public decimal OwnershipPct { get; set; }
    }
}
