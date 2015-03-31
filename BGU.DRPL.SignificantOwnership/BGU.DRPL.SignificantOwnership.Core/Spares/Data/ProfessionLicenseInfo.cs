using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class ProfessionLicenseInfo
    {
        public ProfessionLicensingBodyInfo LicenseIssuer { get; set; }
        public DateTime LicenseIssueDate { get; set; }
        public DateTime LicenseValidTill { get; set; }
        public string LicenseIDNr { get; set; }
        public List<string> LicenseQualifications { get; set; }
        public string LicenseClause { get; set; }
    }
}
