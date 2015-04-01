using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.LoanInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class LoanInfo : IndebtnessInfoBase
    {
        public string AgreementNr { get; set; }
        public DateTime AgreementDt { get; set; }
        public DateTime? OverdueSince { get; set; }
    }
}
