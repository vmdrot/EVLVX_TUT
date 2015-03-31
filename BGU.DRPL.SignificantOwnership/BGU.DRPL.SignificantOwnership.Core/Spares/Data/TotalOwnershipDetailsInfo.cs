using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.TotalOwnershipDetailsInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class TotalOwnershipDetailsInfo
    {
        [Description("Пряма власність")]
        public OwnershipSummaryInfo DirectOwnership { get; set; }
        [Description("Опосередкована власність")]
        public OwnershipSummaryInfo ImplicitOwnership { get; set; }
        [Description("Власність, що набувається")]
        public OwnershipVotesInfo AcquiredVotes { get; set; }
        [Description("Усього % у загальній власності")]
        public decimal TotalCapitalSharePct { get; set; }
        [Description("Усього голосів")]
        public int TotalVotes { get; set; }

        public override string ToString()
        {
            return string.Format("Direct: {0}, Implicit: {1}, Acquired: {2}, {3}%, {4:N0}", DirectOwnership, ImplicitOwnership, AcquiredVotes, TotalCapitalSharePct, TotalVotes);
        }
    }
}
