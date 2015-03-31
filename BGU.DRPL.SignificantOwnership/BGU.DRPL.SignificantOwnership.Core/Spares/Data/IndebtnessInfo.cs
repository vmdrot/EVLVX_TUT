using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class IndebtnessInfo : IndebtnessInfoBase
    {
        public bool IsRepaymentPlanned { get; set; }
        public DateTime? PlannedRepaymentDate { get; set; }
        public string RepaymentPlans { get; set; }
    }
}
