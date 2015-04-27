using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Spares2
{
    public class BankingLicenseInfoEx
    {
        public DateTime? RevocationDate { get; set; }
        public List<BankingLicensedActivityInfo> Activities { get; set; }
    }
}
