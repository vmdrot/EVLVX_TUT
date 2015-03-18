using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    public class TotalOwnershipDetailsInfo
    {
        public OwnershipSummaryInfo DirectOwnership { get; set; }
        public OwnershipSummaryInfo ImplicitOwnership { get; set; }
        public OwnershipVotesInfo AcquiredVotes { get; set; }
        public decimal TotalCapitalSharePct { get; set; }
        public int TotalVotes { get; set; }
    }
}
