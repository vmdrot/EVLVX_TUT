using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class CommonOwnershipInfo
    {
        public GenericPersonID Property { get; set; }
        public List<GenericPersonID> Partners { get; set; }
        public OwnershipType OwnershipType { get; set; }
        public string OwnershipTestimony { get; set; }
        public decimal OwnershipPct { get; set; }
    }
}
