using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    /// <summary>
    /// 
    /// for company registers, see: http://en.wikipedia.org/wiki/List_of_company_registers
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.RegistrarAuthority_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class RegistrarAuthority
    {
        public RegistrarAuthority()
        {
            JurisdictionCountry = CountryInfo.UKRAINE;
        }

        [DisplayName("Країна юрисдикції")]
        [Description("Країна юрисдикції")]
        public CountryInfo JurisdictionCountry { get; set; }
        [DisplayName("Місцезнаходження")]
        [Description("Місцезнаходження")]
        public LocationInfo Address { get; set; }
        [DisplayName("Код держоргану (якщо існує)")]
        [Description("Код держоргану (якщо існує)")]
        public string RegistrarCode { get; set; }
        [DisplayName("Назва держоргану")]
        [Description("Назва держоргану")]
        public string RegistrarName { get; set; }
        [DisplayName("Тип осіб, що реєструє")]
        [Description("Тип осіб, що реєструє")]
        public EntityType EntitiesHandled { get; set; }
        public override string ToString()
        {
            return RegistrarName;
        }
    }
}
