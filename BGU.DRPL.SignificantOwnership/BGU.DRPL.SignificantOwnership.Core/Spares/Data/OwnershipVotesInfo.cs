using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.OwnershipVotesInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class OwnershipVotesInfo
    {
        public decimal Pct { get; set; }
        public int Votes { get; set; }
    }
}
