using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.LoanInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class LoanInfo : IndebtnessInfoBase
    {
        [DisplayName("№ договору")]
        public string AgreementNr { get; set; }
        [DisplayName("Дата договору")]
        public DateTime AgreementDt { get; set; }
        [DisplayName("Прострочено з ... (дата)")]
        public DateTime? OverdueSince { get; set; }
    }
}
