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
        PromotedOrRelocated,
        VoluntaryQuit,
        Dismissed,
        Retired,
        MaternityLeave,
        MilitaryServiceLeave,
        HealthConditionLeave
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum HigherEducationDegreeType
    {
        [Description("Не вказано")]None = 0,
        Bachelor,
        Master,
        CandidateDoctor,
        PhD,
        DoctorOfSciences,
        DoctorOfLetters,
        Professor

    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum DegreeHonourType
    {
        [Description("Не вказано")]None = 0,
        Rite,
        CumLaude,
        Honoured,
        MagnaCumLaude,
        SummaCumLaude,
        EgregiaCumLaude,
        MaximaCumLaude
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum FundsOriginType
    {
        [Description("Не вказано")]None = 0,
        WagesSalaries,
        Royalties,
        Dividends,
        PassiveIncomes,
        Inherited,
        OtherIncomes
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum PaymentType
    {
        [Description("Не вказано")]None = 0,
        Cash,
        WireTransfer,
        Other
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum FinancialGuarantorRoleType
    {
        [Description("Не вказано")]None = 0,
        Guarantor,
        Pledger,
        Attorney
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum BreachOfLawType
    {
        [Description("Не вказано")]None = 0,
        Criminal,
        Antitrust,
        Taxation,
        Banking,
        Financial,
        ForeignCurrency,
        StockExchange,
        OtherAdministrative
    }

    [Flags]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum SentenceType
    {
        [Description("Не вказано")]None = 0,
        Jailed,
        Fined,
        Dismissed,
        LicenseRevoked,
        LicenseSuspended
    }
}
