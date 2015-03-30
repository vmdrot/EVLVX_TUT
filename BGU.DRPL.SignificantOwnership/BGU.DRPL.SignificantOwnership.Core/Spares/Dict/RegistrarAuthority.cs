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
        [Description("Країна юрисдикції")]
        public CountryInfo JurisdictionCountry { get; set; }
        [Description("Місцезнаходження")]
        public LocationInfo Address { get; set; }
        [Description("Код держоргану (якщо існує)")]
        public string RegistrarCode { get; set; }
        [Description("Назва держоргану")]
        public string RegistrarName { get; set; }
        [Description("Тип осіб, що реєструє")]
        public EntityType EntitiesHandled { get; set; }
        public override string ToString()
        {
            return RegistrarName;
        }
    }
}
