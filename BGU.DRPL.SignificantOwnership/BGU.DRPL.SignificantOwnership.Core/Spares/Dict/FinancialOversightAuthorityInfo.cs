using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    /// <summary>
    /// Банківський/фінансовий наглядовий/регуляторний орган 
    /// (згадується у декотрих анкетах)
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.FinancialOversightAuthorityInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class FinancialOversightAuthorityInfo
    {
        public FinancialOversightAuthorityInfo()
        {
            Jurisdiction = CountryInfo.UKRAINE;
        }

        [DisplayName("Країна юрисдикції")]        
        public CountryInfo Jurisdiction { get; set; }
        [DisplayName("Назва органу")]
        public string AuthorityName { get; set; }
        [DisplayName("Назва органу(українською)")]
        public string AuthorityNameUkr { get; set; }
        [DisplayName("Адреса")]
        public LocationInfo Address { get; set; }
        [DisplayName("Адреса SWIFT")]
        public string SWIFTBIC { get; set; }
        [DisplayName("Контакти")]
        public ContactInfo Contacts { get; set; }
    }
}
