using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.ProfessionLicenseInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class ProfessionLicenseInfo
    {
        public ProfessionLicenseInfo()
        {
            this.LicenseQualifications = new List<string>();
        }

        [DisplayName("Ліцензор")]
        public ProfessionLicensingBodyInfo LicenseIssuer { get; set; }
        [DisplayName("Дата видачі")]
        public DateTime LicenseIssueDate { get; set; }
        [DisplayName("Дійсна до")]
        public DateTime LicenseValidTill { get; set; }
        [DisplayName("№ ліцензії")]
        public string LicenseIDNr { get; set; }
        [DisplayName("Кваліфікації/види діяльності за ліцензією")]
        public List<string> LicenseQualifications { get; set; }
        [DisplayName("Формулювання/текст ліцензії")]
        [Description("Додаткові суттєві відомості про ліцензію")]
        public string LicenseClause { get; set; }
    }
}
