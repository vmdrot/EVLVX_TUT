using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares
{
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [Flags]
    public enum  EntityType
    {
        [Description("Не вказано")]None = 0,
        [Description("Фізична особа")]
        Physical,
        [Description("Юридична особа")]
        Legal
    }

    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum OwnershipType
    {
        [Description("Не вказано")]None = 0,
        [Description("Пряма власність")]
        Direct,
        [Description("Опосередкована власність")]
        Implicit,
        [Description("Власність через пов'язану особу")]
        Associated,
        [Description("Власність через угоду/договір")]
        Agreement,
        [Description("Власність по довіреності")]
        Attorney
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum SexType
    {
        [Description("Не вказано")]None = 0,
        [Description("Чол.")]
        Male,
        [Description("Жін.")]
        Female
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum EmploymentState
    {
        [Description("Не вказано")]None = 0,
        [Description("Найманий працівник")]
        Employed,
        [Description("Самозайнятість")]
        Selfemployed,
        [Description("Фрілансер")]
        Freelance,
        [Description("Безробітний")]
        Unemployed
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum EmploymentTerminationType
    {
        [Description("Не вказано")]None = 0,
        [Description("Підвищення/переміщення")]
        PromotedOrRelocated,
        [Description("Звільнення за вл.баж.")]
        VoluntaryQuit,
        [Description("Звільнення (з ініціативи працедавця")]
        Dismissed,
        [Description("Вихід на пенсію")]
        Retired,
        [Description("Декретна відпустка")]
        MaternityLeave,
        [Description("Військова служба")]
        MilitaryServiceLeave,
        [Description("За станом здоров'я")]
        HealthConditionLeave,
        [Description("Інше")]
        OtherLeaveType
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum HigherEducationDegreeType
    {
        [Description("Не вказано")]None = 0,
        [Description("Бакалавр/Спеціаліст")]
        Bachelor,
        [Description("Магістр")]
        Master,
        [Description("MBA")]
        MBA,
        [Description("Кандидат наук")]
        CandidateDoctor,
        [Description("PhD")]
        PhD,
        [Description("Доктор наук (техн.)")]
        DoctorOfSciences,
        [Description("Доктор наук (гум.)")]
        DoctorOfLetters,
        [Description("Професор")]
        Professor

    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum DegreeHonourType
    {
        [Description("Не вказано")]None = 0,
        [Description("Звичайний (без відзнаки)")]
        Rite,
        [Description("З відзнакою (Cum laude)")]
        CumLaude,
        [Description("З відзнакою (\"червоний\"), до-болонська система")]
        Honoured,
        [Description("Зі значною відзнакою (magna cum laude)")]
        MagnaCumLaude,
        [Description("З найвищою відзнакою (summa cum laude)")]
        SummaCumLaude,
        [Description("З вийнятковою відзнакою (egregia cum laude)")]
        EgregiaCumLaude,
        [Description("З максимальною відзнакою (maxima cum laude)")]
        MaximaCumLaude
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum FundsOriginType
    {
        [Description("Не вказано")]None = 0,
        [Description("Зарплатня")]
        WagesSalaries,
        [Description("Роялті")]
        Royalties,
        [Description("Дивіденди")]
        Dividends,
        [Description("Пасивний дохід")]
        PassiveIncomes,
        [Description("Спадщина")]
        Inherited,
        [Description("Інші доходи")]
        OtherIncomes
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum PaymentType
    {
        [Description("Не вказано")]None = 0,
        [Description("Готівка")]
        Cash,
        [Description("Безготівка")]
        WireTransfer,
        [Description("Інше")]
        Other
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum FinancialGuarantorRoleType
    {
        [Description("Не вказано")]None = 0,
        [Description("Гарантор")]
        Guarantor,
        [Description("Заставодавець/поручитель")]
        Pledger,
        [Description("Довірена особа")]
        Attorney
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum BreachOfLawType
    {
        [Description("Не вказано")]None = 0,
        [Description("Кримінальне")]
        Criminal,
        [Description("Анти-монопольне")]
        Antitrust,
        [Description("Податкове")]
        Taxation,
        [Description("Банківське")]
        Banking,
        [Description("Фінансове")]
        Financial,
        [Description("Валютне")]
        ForeignCurrency,
        [Description("Біржове")]
        StockExchange,
        [Description("Інше адміністративне")]
        OtherAdministrative
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum SentenceType
    {
        [Description("Не вказано")]None = 0,
        [Description("Ув'язнення")]
        Jailed,
        [Description("Виправні роботи")]
        RemedialWorks,
        [Description("Штраф")]
        Fined,
        [Description("Звільнення")]
        Dismissed,
        [Description("Відкликання/позбавлення ліцензії")]
        LicenseRevoked,
        [Description("Призупинення ліцензії")]
        LicenseSuspended
    }

    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum TypicalApplicationAttachement
    {
        [Description("Не вказано")]
        None = 0,
        [Description("Копії/скан внутрішніх положень")]
        InternalReglementCopiesScans,
        [Description("Копії/скан трудової книги")]
        WorkbookCopyScan,
        [Description("Копії/скан паспорта")]
        PassportCopyScan,
        [Description("Копії/скан статуту")]
        CharterCopyScan,
        [Description("Інше (вказати додатково у деталях)")]
        Other
    }


    #region Financial services types
    #region Legislative wording and listing
    /*
     * Розділ II. УМОВИ НАДАННЯ ФІНАНСОВИХ ПОСЛУГ 


Стаття 4. Фінансові послуги 

1. Фінансовими вважаються такі послуги: 

1) випуск платіжних документів, платіжних карток, дорожніх чеків та/або їх обслуговування, кліринг, інші форми забезпечення розрахунків; 

2) довірче управління фінансовими активами; 

3) діяльність з обміну валют; 

4) залучення фінансових активів із зобов'язанням щодо наступного їх повернення; 

5) фінансовий лізинг; 

6) надання коштів у позику, в тому числі і на умовах фінансового кредиту; 

7) надання гарантій та поручительств; 

8) переказ коштів; 

(пункт 8 частини першої статті 4 із змінами, внесеними
 згідно із Законом України від 02.12.2010 р. N 2756-VI)

9) послуги у сфері страхування та у системі накопичувального пенсійного забезпечення; 

(пункт 9 частини першої статті 4 із змінами, внесеними
 згідно із Законом України від 08.07.2011 р. N 3668-VI)

10) професійна діяльність на ринку цінних паперів, що підлягає ліцензуванню;

(пункт 10 частини першої статті 4 у редакції
 Закону України від 02.06.2011 р. N 3462-VI)

11) факторинг; 

11 1) адміністрування фінансових активів для придбання товарів у групах;

(частину першу статті 4 доповнено пунктом 11 1 згідно
 із Законом України від 02.06.2011 р. N 3462-VI)

12) управління майном для фінансування об'єктів будівництва та/або здійснення операцій з нерухомістю відповідно до Закону України "Про фінансово-кредитні механізми і управління майном при будівництві житла та операціях з нерухомістю";

(пункт 12 частини першої статті 4 у редакції
 Закону України від 10.10.2013 р. N 643-VII)

13) операції з іпотечними активами з метою емісії іпотечних цінних паперів;

(частину першу статті 4 доповнено пунктом 13
 згідно із Законом України від 10.10.2013 р. N 643-VII)

14) банківські та інші фінансові послуги, що надаються відповідно до Закону України "Про банки і банківську діяльність".

     */
    #endregion

    public enum FinancialServicesType
    {
        [Description("Не вказано")]
        None = 0,
        [Description("п.1 випуск платіжних документів, платіжних карток, дорожніх чеків та/або їх обслуговування, кліринг, інші форми забезпечення розрахунків (ст.4 з-ну Про фінпослуги)")]
        PayDocsIssuance = 1,
        [Description("п.2 довірче управління фінансовими активами (ст.4 з-ну Про фінпослуги)")]
        Trust = 2,
        [Description("п.3 діяльність з обміну валют (ст.4 з-ну Про фінпослуги)")]
        CurrencyExchange = 3,
        [Description("п.4 залучення фінансових активів із зобов'язанням щодо наступного їх повернення (ст.4 з-ну Про фінпослуги)")]
        FinanceAssetsLiabilities = 4,
        [Description("п.5 фінансовий лізинг (ст.4 з-ну Про фінпослуги)")]
        FinancialLeasing = 5,
        [Description("п.6 надання коштів у позику, в тому числі і на умовах фінансового кредиту (ст.4 з-ну Про фінпослуги)")]
        Lending = 6,
        [Description("п.7 надання гарантій та поручительств (ст.4 з-ну Про фінпослуги)")]
        Guarantees = 7,
        [Description("п.8 переказ коштів (ст.4 з-ну Про фінпослуги)")]
        FundsTransfer = 8,
        [Description("п.9 послуги у сфері страхування та у системі накопичувального пенсійного забезпечення (ст.4 з-ну Про фінпослуги)")]
        InsuranceAndPensionSavings = 9,
        [Description("п.10 професійна діяльність на ринку цінних паперів, що підлягає ліцензуванню (ст.4 з-ну Про фінпослуги)")]
        StockExchangeActivities = 10,
        [Description("п.11 факторинг (ст.4 з-ну Про фінпослуги)")]
        Factoring = 11,
        [Description("п.11.1 адміністрування фінансових активів для придбання товарів у групах (ст.4 з-ну Про фінпослуги)")]
        FinAssetsAdministeringGroupsPurchase = 12,
        [Description("п.12 управління майном для фінансування об'єктів будівництва та/або здійснення операцій з нерухомістю відповідно до Закону України Про фінансово-кредитні механізми і управління майном при будівництві житла та операціях з нерухомістю (ст.4 з-ну Про фінпослуги)")]
        ConstructionAssetsManagement = 13,
        [Description("п.13 операції з іпотечними активами з метою емісії іпотечних цінних паперів (ст.4 з-ну Про фінпослуги)")]
        MortgageSecuritiesMngtIssue = 14,
        [Description("п.14 банківські та інші фінансові послуги, що надаються відповідно до Закону України Про банки і банківську діяльність (ст.4 з-ну Про фінпослуги)")]
        OtherFinBankServices = 15,
    }

    #endregion

    #region Types of banking activities
    #region Legislative wording and listing

    /*
Глава 8. ВИМОГИ ДО ДІЯЛЬНОСТІ БАНКУ 


Стаття 47. Види діяльності банку 

Банк має право надавати банківські та інші фінансові послуги (крім послуг у сфері страхування), а також здійснювати іншу діяльність, визначену в цій статті. 

Банк має право здійснювати банківську діяльність на підставі банківської ліцензії шляхом надання банківських послуг. 

До банківських послуг належать: 

1) залучення у вклади (депозити) коштів та банківських металів від необмеженого кола юридичних і фізичних осіб; 

2) відкриття та ведення поточних (кореспондентських) рахунків клієнтів, у тому числі у банківських металах; 

3) розміщення залучених у вклади (депозити), у тому числі на поточні рахунки, коштів та банківських металів від свого імені, на власних умовах та на власний ризик. 

Банківські послуги дозволяється надавати виключно банку. Центральний депозитарій цінних паперів має право провадити окремі банківські операції на підставі ліцензії на здійснення окремих банківських операцій, що видається у встановленому Національним банком України порядку.

(частина четверта статті 47 у редакції
 Закону України від 06.07.2012 р. N 5178-VI,
 із змінами, внесеними згідно із
 Законом України від 04.07.2013 р. N 401-VII)

Банк має право надавати своїм клієнтам (крім банків) фінансові послуги, у тому числі шляхом укладення з юридичними особами (комерційними агентами) агентських договорів. Перелік фінансових послуг, що банк має право надавати своїм клієнтам (крім банків) шляхом укладення агентських договорів, встановлюється Національним банком України. Банк зобов'язаний повідомити Національний банк України про укладені ним агентські договори. Національний банк веде реєстр комерційних агентів банків та встановлює вимоги до них. Банк має право укладати агентський договір з юридичною особою, яка відповідає встановленим Національним банком України вимогам. 

Частину шосту статті 47 виключено

(згідно із Законом України
 від 19.05.2011 р. N 3394-VI)

Частину сьому статті 47 виключено

(згідно із Законом України
 від 19.05.2011 р. N 3394-VI)

Банк, крім надання фінансових послуг, має право здійснювати також діяльність щодо: 

1) інвестицій; 

2) випуску власних цінних паперів; 

3) випуску, розповсюдження та проведення лотерей; 

4) зберігання цінностей або надання в майновий найм (оренду) індивідуального банківського сейфа; 

5) інкасації коштів та перевезення валютних цінностей; 

6) ведення реєстрів власників іменних цінних паперів (крім власних акцій); 

7) надання консультаційних та інформаційних послуг щодо банківських та інших фінансових послуг. 
     */
    #endregion

    public enum BankingActivityType
    {
        [Description("Не вказано")]
        None = 0,
        [Description("п.1 залучення у вклади (депозити) коштів та банківських металів від необмеженого кола юридичних і фізичних осіб (ч.1-а ст.47 З-ну Про БіБД)")]
        DepositsTaking = 1,
        [Description("п.2 відкриття та ведення поточних (кореспондентських) рахунків клієнтів, у тому числі у банківських металах (ч.1-а ст.47 З-ну Про БіБД)")]
        AccountsMgmt = 2,
        [Description("п.3 розміщення залучених у вклади (депозити), у тому числі на поточні рахунки, коштів та банківських металів від свого імені, на власних умовах та на власний ризик (ч.1-а ст.47 З-ну Про БіБД)")]
        DepositedFundsPlacement = 3,
        [Description("п.1 інвестицій (ч.8-а ст.47 З-ну Про БіБД)")]
        Investments = 4,
        [Description("п.2 випуску власних цінних паперів (ч.8-а ст.47 З-ну Про БіБД)")]
        ProprietarySecuritiesIssue = 5,
        [Description("п.3 випуску, розповсюдження та проведення лотерей (ч.8-а ст.47 З-ну Про БіБД)")]
        Lotteries = 6,
        [Description("п.4 зберігання цінностей або надання в майновий найм (оренду) індивідуального банківського сейфа (ч.8-а ст.47 З-ну Про БіБД)")]
        SafeCustody = 7,
        [Description("п.5 інкасації коштів та перевезення валютних цінностей (ч.8-а ст.47 З-ну Про БіБД)")]
        CashCollectionTransportation = 8,
        [Description("п.6 ведення реєстрів власників іменних цінних паперів (крім власних акцій) (ч.8-а ст.47 З-ну Про БіБД)")]
        SecuritiesCustody = 9,
        [Description("п.7 надання консультаційних та інформаційних послуг щодо банківських та інших фінансових послуг (ч.8-а ст.47 З-ну Про БіБД)")]
        ConsultancyOnBankFinServices = 10,

    }
    #endregion
}
