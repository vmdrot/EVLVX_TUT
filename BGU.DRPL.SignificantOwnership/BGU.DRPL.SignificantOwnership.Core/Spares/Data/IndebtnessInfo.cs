using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.IndebtnessInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class IndebtnessInfo : IndebtnessInfoBase
    {
        public bool IsRepaymentPlanned { get; set; }
        public DateTime? PlannedRepaymentDate { get; set; }
        public string RepaymentPlans { get; set; }
    }
}
