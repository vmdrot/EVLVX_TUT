using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.FinancialOversightAuthorityInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class FinancialOversightAuthorityInfo
    {
        public CountryInfo Jurisdiction { get; set; }
        public string AuthorityName { get; set; }
        public string AuthorityNameUkr { get; set; }
        public LocationInfo Address { get; set; }
        public string SWIFTBIC { get; set; }
        public ContactInfo Contacts { get; set; }
    }
}
