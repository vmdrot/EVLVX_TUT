using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.FinancialGuaranteeInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class FinancialGuaranteeInfo
    {
        public GenericPersonID Person { get; set; }
        public FinancialGuarantorRoleType Role { get; set; }
        public CurrencyAmount PledgeAmt { get; set; }
        public string GuaranteeDetails { get; set; }
    }
}
