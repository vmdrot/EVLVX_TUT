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
}
