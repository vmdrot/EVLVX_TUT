using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BankingLicenseInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BankingLicenseInfo
    {
        [Description("№ ліцензії")]
        public string LicenseNr { get; set; }
        [Description("Дата видачі ліцензії")]
        public DateTime IssueDate { get; set; }

        public override string ToString()
        {
            return string.Format("{0} dd {1}", LicenseNr, IssueDate);
        }
    }
}
