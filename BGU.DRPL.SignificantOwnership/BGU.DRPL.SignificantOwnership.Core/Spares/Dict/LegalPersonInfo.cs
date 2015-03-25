using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

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
        public CurrencyAmount Equity { get; set; }
        public GenericPersonID GenericID { get { return new GenericPersonID() { CountryISO3Code = ResidenceCountry.CountryISONr, PersonCode = TaxCodeOrHandelsRegNr, PersonType = EntityType.Legal }; } }
    }
}
