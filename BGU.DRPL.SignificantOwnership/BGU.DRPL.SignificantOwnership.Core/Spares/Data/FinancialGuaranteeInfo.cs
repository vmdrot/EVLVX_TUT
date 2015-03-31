using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class FinancialGuaranteeInfo
    {
        public GenericPersonID Person { get; set; }
        public FinancialGuarantorRoleType Role { get; set; }
        public CurrencyAmount PledgeAmt { get; set; }
        public string GuaranteeDetails { get; set; }
    }
}
