using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class LiquidatedEntityOwnershipInfo
    {
        public GenericPersonID Asset { get; set; }
        public TotalOwnershipDetailsInfo Stake { get; set; }
        public DateTime LiquidationDat { get; set; }
        public string LiquidationReason { get; set; }
        public string LiquidationPretext { get; set; }
    }
}
