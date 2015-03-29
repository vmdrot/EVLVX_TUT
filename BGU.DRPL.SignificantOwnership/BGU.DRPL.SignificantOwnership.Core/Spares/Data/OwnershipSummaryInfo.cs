using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.OwnershipSummaryInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class OwnershipSummaryInfo
    {
        public decimal Pct { get; set; }
        public decimal Amount { get; set; }
        public int Votes { get; set; }

        public override string ToString()
        {
            return string.Format("{0}% {1:N0} {2:N0}", Pct, Amount, Votes);
        }
    }
}
