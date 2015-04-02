using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.OwnershipSummaryInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class OwnershipSummaryInfo
    {
        [DisplayName("%")]
        [Description("%")]
        public decimal Pct { get; set; }
        [DisplayName("Cума")]
        [Description("Cума")]
        public decimal Amount { get; set; }
        [DisplayName("Кількість голосів")]
        [Description("Кількість голосів")]
        public int Votes { get; set; }

        public override string ToString()
        {
            return string.Format("{0}% {1:N0} {2:N0}", Pct, Amount, Votes);
        }
    }
}
