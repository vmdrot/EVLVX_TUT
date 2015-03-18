using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    public class LegalPersonInfo
    {
        /// <summary>
        /// search - http://irc.gov.ua/ua/Poshuk-v-YeDR.html
        /// </summary>
        public string TaxCodeOrHandelsRegNr { get; set; }
        public string Name { get; set; }
        public LocationInfo Address { get; set; }
        public CountryInfo ResidenceCountry { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RegistrarAuthority Registrar { get; set; }
        public PhysicalPersonInfo RepresentedBy { get; set; }
    }
}
