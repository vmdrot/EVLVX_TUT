using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    /// <summary>
    /// 
    /// for company registers, see: http://en.wikipedia.org/wiki/List_of_company_registers
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.RegistrarAuthority_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class RegistrarAuthority
    {
        public CountryInfo JurisdictionCountry { get; set; }
        public LocationInfo Address { get; set; }
        public string RegistrarCode { get; set; }
        public string RegistrarName { get; set; }
        public EntityType EntitiesHandled { get; set; }
        public override string ToString()
        {
            return RegistrarName;
        }
    }
}
