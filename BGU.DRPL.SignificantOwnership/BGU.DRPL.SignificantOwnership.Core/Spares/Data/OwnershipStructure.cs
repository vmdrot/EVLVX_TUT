using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class OwnershipStructure
    {
        public GenericPersonID Asset { get; set; }
        public List<GenericPersonIDShare> Owners { get; set; }
        public OwnershipType OwnershipKind { get; set; }
        public decimal Share { get; set; }
        public int Votes { get; set; }
    }
}
