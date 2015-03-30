using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.OwnershipStructure_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class OwnershipStructure
    {
        [Description("Об'єкт власності")]
        public GenericPersonID Asset { get; set; }
        [Description("Власник")]
        public GenericPersonID Owner { get; set; }
        [Description("Тип володіння")]
        public OwnershipType OwnershipKind { get; set; }
        [Description("Частка (сума)")]
        public decimal Share { get; set; }
        [Description("Частка (%)")]
        public decimal SharePct { get; set; }
        [Description("Кількість голосів в управлінні")]
        public int Votes { get; set; }

        public override string ToString()
        {
            return string.Format("in {0}, by {1} ({2}) {3:N0} {4}% {5:N0}", Asset, Owner, OwnershipKind, Share, SharePct, Votes);
        }
    }
}
