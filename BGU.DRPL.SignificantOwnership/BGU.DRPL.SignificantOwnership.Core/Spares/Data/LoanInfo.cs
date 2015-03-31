using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class LoanInfo : IndebtnessInfoBase
    {
        public string AgreementNr { get; set; }
        public DateTime AgreementDt { get; set; }
        public DateTime? OverdueSince { get; set; }
    }
}
