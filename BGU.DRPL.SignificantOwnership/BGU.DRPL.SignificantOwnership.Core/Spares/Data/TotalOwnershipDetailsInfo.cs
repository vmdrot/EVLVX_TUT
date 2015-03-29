using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.TotalOwnershipDetailsInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class TotalOwnershipDetailsInfo
    {
        public OwnershipSummaryInfo DirectOwnership { get; set; }
        public OwnershipSummaryInfo ImplicitOwnership { get; set; }
        public OwnershipVotesInfo AcquiredVotes { get; set; }
        public decimal TotalCapitalSharePct { get; set; }
        public int TotalVotes { get; set; }

        public override string ToString()
        {
            return string.Format("Direct: {0}, Implicit: {1}, Acquired: {2}, {3}%, {4:N0}", DirectOwnership, ImplicitOwnership, AcquiredVotes, TotalCapitalSharePct, TotalVotes);
        }
    }
}
