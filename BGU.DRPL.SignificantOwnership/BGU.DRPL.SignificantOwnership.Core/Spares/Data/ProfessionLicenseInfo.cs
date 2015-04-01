using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.ProfessionLicenseInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class ProfessionLicenseInfo
    {
        public ProfessionLicenseInfo()
        {
            this.LicenseQualifications = new List<string>();
        }

        public ProfessionLicensingBodyInfo LicenseIssuer { get; set; }
        public DateTime LicenseIssueDate { get; set; }
        public DateTime LicenseValidTill { get; set; }
        public string LicenseIDNr { get; set; }
        public List<string> LicenseQualifications { get; set; }
        public string LicenseClause { get; set; }
    }
}
