using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.LegalPersonInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class LegalPersonInfo
    {
        /// <summary>
        /// search - http://irc.gov.ua/ua/Poshuk-v-YeDR.html
        /// </summary>
        [Description("ЄДРПОУ/Податковий ID/HandelsregisterNr.(для нерезидентів)")]
        public string TaxCodeOrHandelsRegNr { get; set; }
        [Description("Найменування юридичної особи")]
        public string Name { get; set; }
        [Description("місцезнаходження юридичної особи")]
        public LocationInfo Address { get; set; }
        [Description("Країна юрисдикції юридичної особи")]
        public CountryInfo ResidenceCountry { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("Державний орган, який здійснив реєстрацію юридичної особи")]
        public RegistrarAuthority Registrar { get; set; }
        [Description("Особа, що представляє юрособу")]
        public PhysicalPersonInfo RepresentedBy { get; set; }
        [Description("Статутний фонд/капітал")]
        public CurrencyAmount Equity { get; set; }
        [Browsable(false)]
        public GenericPersonID GenericID { get { return new GenericPersonID() { CountryISO3Code = ResidenceCountry.CountryISONr, PersonCode = TaxCodeOrHandelsRegNr, PersonType = EntityType.Legal }; } }

        public override string ToString()
        {
            return Name;
        }
    }
}
