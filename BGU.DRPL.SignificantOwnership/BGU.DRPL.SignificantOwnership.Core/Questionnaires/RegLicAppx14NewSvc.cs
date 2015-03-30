using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.RegLicAppx14NewSvc_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class RegLicAppx14NewSvc : IQuestionnaire
    {
        public RegLicAppx14NewSvc()
        {
            NewActivitiesList = new List<BankingLicensedActivityInfo>();
            Attachements = new List<AttachmentInfo>();
        }
        [Description("Дата")]
        public DateTime QuestDate { get; set; }
        [Description("номер")]
        public string Ref { get; set; }
        [Description("Банк")]
        public BankInfo BankRef { get; set; }
        [Description("має  банківську ліцензію")]
        public BankingLicenseInfo ExistingLicenseSpecs {get;set;}
        [Description("перелік нових видів діяльності або нових видів фінансових послуг")]
        public List<BankingLicensedActivityInfo> NewActivitiesList { get; set; }
        [Description("Банк має достатні фінансові  можливості  для  початку  нового виду діяльності та/або виду фінансових послуг,  зазначених у цьому повідомленні")]
        public bool HasSufficientFinancialCapacity { get; set; }
        [Description("Банк не має збитків протягом останніх ... місяців")]
        public int MonthsWOLosses { get; set; }
        [Description("Банк сформував фонди та резерви відповідно до вимог законодавства України, у тому числі нормативно-правових актів Національного банку України")]
        public bool HasFormedEnoughFundsAndReserves { get; set; }
        [Description("Банк дотримується нормативу обов'язкового резервування протягом останніх ... місяців")]
        public int MonthsObligatoryReservesMet { get; set; }
        [Description("Банк дотримується економічних нормативів протягом останніх ... місяців")]
        public int MonthsEconomicNormativesReqsMet { get; set; }
        [Description("Рівень регулятивного капіталу Банку відповідає вимогам Національного банку України")]
        public bool RegulatoryCapitalReqsMet { get; set; }
        [Description("Рівень регулятивного капіталу Банку становить ... грн.")]
        public CurrencyAmount RegulatoryCapital { get; set; }
        [Description("Банк не є об'єктом застосування заходів впливу (крім письмового застереження)")]
        public bool NoPenaltiesWithinLast3Months { get; set; }
        [Description("У Банку створено відповідні підрозділи, що здійснюватимуть зазначений в повідомленні новий вид діяльності та/або вид фінансових послуг, а також підрозділ внутрішнього аудиту та підрозділ з управління ризиками, функції та повноваження яких відповідають вимогам Закону України \"Про банки і банківську діяльність\" та нормативно-правових актів Національного банку України. Банк забезпечений спеціалістами, які відповідають вимогам нормативно-правових актів Національного банку України. Відповідні відомості додаються.")]
        public bool HasDeptsAllocatedForNewBusLines { get; set; }
        [Description("Банк забезпечений належним банківським обладнанням, комп'ютерною технікою, програмним забезпеченням та комунікаційними засобами, що відповідають вимогам Національного банку України")]
        public bool HasFacilitiesForNewBusLines { get; set; }
        [Description("Банком виконано спеціальні вимоги законодавства України, у тому числі нормативно-правових актів Національного банку України, щодо нового виду діяльності та/або нового виду фінансових послуг")]
        public bool IsGenericLawReqsMetForNewBusLines { get; set; }
        [Description("Банк готовий розпочати новий вид діяльності та/або вид фінансових послуг згідно з вимогами банківського законодавства України, нормативно-правових актів Національного банку України")]
        public bool IsBankIndustrySpecificLawReqsMetForNewBusLines { get; set; }
        [Description("Копії внутрішніх положень, що регулюють здійснення банком нових видів діяльності або надання нового виду фінансових  послуг, додаються")]
        public bool IsInternalRegulationCopiesAttachedForNewBusLines { get; set; }
        [Description("Я несу персональну відповідальність за достовірність усіх поданих документів")]
        public bool IsSigneePersonallyLiableForCopiesAttached { get; set; }
        [Description("У разі виникнення будь-яких питань щодо цього повідомлення або документів, що додаються до нього, прошу звертатися до (прізвище, адреса, номер телефону, факсу)")]
        public ContactInfo Contacts { get; set; }
        [Description("Додатки")]
        public List<AttachmentInfo> Attachements { get; set; }
        [Description("Інформація про підписанта (Голова правління Банку)")]
        public SignatoryInfo Signee { get; set; }
    }
}
