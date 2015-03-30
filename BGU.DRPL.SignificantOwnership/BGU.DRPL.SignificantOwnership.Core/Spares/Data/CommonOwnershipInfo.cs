using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.CommonOwnershipInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class CommonOwnershipInfo
    {
        public CommonOwnershipInfo()
        {
            Partners = new List<GenericPersonID>();
        }
        [Description("Об'єкт власності")]
        public GenericPersonID Property { get; set; }
        [Description("Співвласники")]
        public List<GenericPersonID> Partners { get; set; }
        [Description("Тип спільної власності")]
        public OwnershipType OwnershipType { get; set; }
        [Description("На підставі (якщо релевантно)")]
        public string OwnershipTestimony { get; set; }
        [Description("Частка власності, %")]
        public decimal OwnershipPct { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Property.HashID, Partners[0].HashID, OwnershipType, OwnershipPct);
        }
    }
}
