using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.IndebtnessInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class IndebtnessInfo : IndebtnessInfoBase
    {
        [DisplayName("Чи планую погашення?")]
        [Description("Чи планую погашення боргу/виконання зобов'язань?")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public bool IsRepaymentPlanned { get; set; }
        [DisplayName("Планована дата погашення")]
        public DateTime? PlannedRepaymentDate { get; set; }
        [DisplayName("Плани щодо погашення")]
        public string RepaymentPlans { get; set; }
    }
}
