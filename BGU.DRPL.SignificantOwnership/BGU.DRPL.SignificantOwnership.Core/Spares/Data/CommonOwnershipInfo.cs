using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class CommonOwnershipInfo
    {
        [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.CommonOwnershipInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
        public GenericPersonID Property { get; set; }
        public List<GenericPersonID> Partners { get; set; }
        public OwnershipType OwnershipType { get; set; }
        public string OwnershipTestimony { get; set; }
        public decimal OwnershipPct { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Property.HashID, Partners[0].HashID, OwnershipType, OwnershipPct);
        }
    }
}
