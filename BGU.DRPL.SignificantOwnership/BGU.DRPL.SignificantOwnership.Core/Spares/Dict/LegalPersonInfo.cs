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

        public LegalPersonInfo()
        {
            ResidenceCountry = CountryInfo.UKRAINE;
        }
        /// <summary>
        /// search - http://irc.gov.ua/ua/Poshuk-v-YeDR.html
        /// </summary>
        [DisplayName("Податковий №")]
        [Description("ЄДРПОУ/Податковий ID/HandelsregisterNr.(для нерезидентів)")]
        public string TaxCodeOrHandelsRegNr { get; set; }
        [DisplayName("Найменування")]
        [Description("Найменування юридичної особи (оригінальною мовою)")]
        public string Name { get; set; }
        [DisplayName("Найменування українською")]
        [Description("Найменування юридичної особи українською мовою (для нерезидентів)")]
        public string NameUkr { get; set; }
        [DisplayName("Місцезнаходження")]
        [Description("місцезнаходження юридичної особи")]
        public LocationInfo Address { get; set; }
        /// <summary>
        /// Країна резидентності
        /// </summary>
        [DisplayName("Країна юрисдикції")]
        [Description("Країна юрисдикції юридичної особи")]
        public CountryInfo ResidenceCountry { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Держорган-реєстратор")]
        [Description("Державний орган, який здійснив реєстрацію юридичної особи")]
        public RegistrarAuthority Registrar { get; set; }
        /// <summary>
        /// Якщо передбачений представник
        /// </summary>
        [DisplayName("Представник юрособи")]
        [Description("Особа, що представляє юрособу")]
        public GenericPersonID RepresentedBy { get; set; }
        /// <summary>
        /// Якщо анкетою вимагається
        /// </summary>
        [DisplayName("Статутний капітал")]
        [Description("Статутний фонд/капітал")]
        public CurrencyAmount Equity { get; set; }
        /// <summary>
        /// Вид діяльності - якщо вимагається в анкеті; логічно його притулити 
        /// було до самої структури інформації про юр.особу
        /// </summary>
        [DisplayName("Основний вид діяльності")]
        [Description("Основний(-і) вид(-и) діяльності юрособи")]
        public string PrincipalActivities { get; set; }
        [Browsable(false)]
        public GenericPersonID GenericID { get { return new GenericPersonID() { CountryISO3Code = ResidenceCountry.CountryISONr, PersonCode = TaxCodeOrHandelsRegNr, PersonType = EntityType.Legal }; } }

        public override string ToString()
        {
            return Name;
        }
    }
}
