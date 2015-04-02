using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares
{
    [Flags]
    public enum  EntityType
    {
        None = 0,
        [Description("Фізична особа")]
        Physical,
        [Description("Юридична особа")]
        Legal
    }

    public enum OwnershipType
    {
        None = 0,
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
    public enum SexType
    {
        None = 0,
        [Description("Чол.")]
        Male,
        [Description("Жін.")]
        Female
    }

    [Flags]
    public enum EmploymentState
    {
        None = 0,
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
    public enum EmploymentTerminationType
    {
        None = 0,
        PromotedOrRelocated,
        VoluntaryQuit,
        Dismissed,
        Retired,
        MaternityLeave,
        MilitaryServiceLeave,
        HealthConditionLeave
    }

    [Flags]
    public enum HigherEducationDegreeType
    {
        None = 0,
        Bachelor,
        Master,
        CandidateDoctor,
        PhD,
        DoctorOfSciences,
        DoctorOfLetters,
        Professor

    }

    [Flags]
    public enum DegreeHonourType
    {
        None = 0,
        Rite,
        CumLaude,
        Honoured,
        MagnaCumLaude,
        SummaCumLaude,
        EgregiaCumLaude,
        MaximaCumLaude
    }

    [Flags]
    public enum FundsOriginType
    {
        None = 0,
        WagesSalaries,
        Royalties,
        Dividends,
        PassiveIncomes,
        Inherited,
        OtherIncomes
    }

    [Flags]
    public enum PaymentType
    {
        None = 0,
        Cash,
        WireTransfer,
        Other
    }

    [Flags]
    public enum FinancialGuarantorRoleType
    {
        None = 0,
        Guarantor,
        Pledger,
        Attorney
    }

    [Flags]
    public enum BreachOfLawType
    {
        None = 0,
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
    public enum SentenceType
    {
        None = 0,
        Jailed,
        Fined,
        Dismissed,
        LicenseRevoked,
        LicenseSuspended
    }
}
