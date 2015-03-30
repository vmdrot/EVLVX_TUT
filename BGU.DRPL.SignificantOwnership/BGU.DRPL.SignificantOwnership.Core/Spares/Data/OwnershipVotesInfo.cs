using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.OwnershipVotesInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class OwnershipVotesInfo
    {
        [Description("%")]
        public decimal Pct { get; set; }
        [Description("Кількість голосів")]
        public int Votes { get; set; }

        public override string ToString()
        {
            return string.Format("{0}% {1:N0}", Pct, Votes);
        }
    }
}
