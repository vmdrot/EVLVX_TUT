using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.OwnershipStructure_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class OwnershipStructure
    {
        public GenericPersonID Asset { get; set; }
        public GenericPersonID Owner { get; set; }
        public OwnershipType OwnershipKind { get; set; }
        public decimal Share { get; set; }
        public decimal SharePct { get; set; }
        public int Votes { get; set; }
    }
}
