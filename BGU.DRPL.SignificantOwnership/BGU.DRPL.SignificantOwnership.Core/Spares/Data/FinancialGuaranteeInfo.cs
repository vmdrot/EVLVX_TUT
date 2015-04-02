using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.FinancialGuaranteeInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class FinancialGuaranteeInfo
    {
        [DisplayName("Особа, щодо якої гарантую/ручаюся,тощо")]
        public GenericPersonID Person { get; set; }
        [DisplayName("Роль")]
        [Description("Роль (гарантор, довірена особа, тощо)")]
        public FinancialGuarantorRoleType Role { get; set; }
        [DisplayName("Сума гарантії/поруки/тощо")]
        public CurrencyAmount PledgeAmt { get; set; }
        [DisplayName("Деталі")]
        [Description("з яких питань")]
        public string GuaranteeDetails { get; set; }
    }
}
