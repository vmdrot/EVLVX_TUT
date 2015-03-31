using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.Spares;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.RegLicAppx4PhysPQuest_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class RegLicAppx4OwnershipAcqRequestPP : IQuestionnaire
    {
        public RegLicAppx4OwnershipAcqRequestPP()
        {
            this.BankingAccounts = new List<BankAccountInfo>();
            this.EmploymentRecords = new List<EmploymentRecordInfo>();
            this.EducationRecords = new List<EducationRecordInfo>();
            this.ProfessionalLicenses = new List<ProfessionLicenseInfo>();
            this.ExistingOwnershipWithBankDetails = new List<OwnershipStructure>();
            this.TargetedOwnershipWithBankDiffDetails = new List<OwnershipStructure>();
            this.TargetedOwnershipWithBankDetails = new List<OwnershipStructure>();
            this.FundsOrigins = new List<IncomeOriginInfo>();
            this.PaymentModes = new List<PaymentModeInfo>();
            this.PaymentDeadlines = new List<PaymentDeadlineInfo>();
            this.LinkedBanksGoverningBodiesMembers = new List<CouncilBodyInfo>();
            this.BreachesOfLaw = new List<BreachOfLawRecordInfo>();
            this.OtherLiabilities = new List<IndebtnessInfo>();
            this.LiquidatedSignOwnershipLastYear = new List<LiquidatedEntityOwnershipInfo>();
            this.MentionedIdentities = new List<GenericPersonInfo>();
            this.PersonsLinks = new List<PersonsAssociation>();
        }
        /// <summary>
        /// Bank name
        /// </summary>
        [DisplayName("Банк")]
        [Description("Банк")]
        public BankInfo BankRef { get; set; }


        [DisplayName("Фізособа-заявник")]
        [Description("1. Інформація про особу")]
        public PhysicalPersonInfo Acquiree { get; set; }

        [DisplayName("Рахунки в банках")]
        [Description("1.6. Перелік банків, у яких відкрито рахунки")]
        public List<BankAccountInfo> BankingAccounts { get; set; }

        [DisplayName("Банк, що відрекомендовує іноземця")]
        [Description("1.7. Банк, який надає інформацію про наявні кошти і репутацію власника рахунку (для іноземців)")]
        public BankInfo ReferenceBank { get; set; }


        [DisplayName("Досвід роботи")]
        [Description("1.8. Займані посади за останні п'ять років")]
        public List<EmploymentRecordInfo> EmploymentRecords { get; set; }

        [DisplayName("Освіта")]
        [Description("1.10. Освіта (освіта, науковий ступінь, номер, дата видачі диплома, ким виданий, спеціальність і кваліфікація)")]
        public List<EducationRecordInfo> EducationRecords { get; set; }
        [DisplayName("Кваліфікація")]
        [Description("1.10. Кваліфікація (професійна ліцензія, номер, дата видачі, ким видана, спеціальність і кваліфікація)")]
        public List<ProfessionLicenseInfo> ProfessionalLicenses { get; set; }


        [DisplayName("Кваліфікація")]
        [Description("1.10. Кваліфікація (професійна ліцензія, номер, дата видачі, ким видана, спеціальність і кваліфікація)")]
        public bool IsForeignFinOversightBodyApprovalRequired { get; set; }
        public FinancialOversightAuthorityInfo ForeignFinOversightBody { get; set; }

        public PublicationInfo IssuePublication { get; set; }
        public bool IsUnderwritten { get; set; }
        public GenericPersonID Underwriter { get; set; }
        public TotalOwnershipDetailsInfo ExistingOwnershipWithBankSummary { get; set; }

        public List<OwnershipStructure> ExistingOwnershipWithBankDetails { get; set; }
        
        public TotalOwnershipDetailsInfo TargetedOwnershipWithBankSummaryDiff { get; set; }
        public List<OwnershipStructure> TargetedOwnershipWithBankDiffDetails { get; set; }
        public OwnershipSummaryInfo TargetedForeignInvestmentSummaryDiff { get; set; }

        public TotalOwnershipDetailsInfo TargetedOwnershipWithBankSummary { get; set; }
        public List<OwnershipStructure> TargetedOwnershipWithBankDetails { get; set; }

        public bool IsAssociatedPersonWithIssuer { get; set; }

        /// <summary>
        /// 3.1. Кількість акцій, які Ви маєте намір придбати ...
        /// </summary>
        public SharesAcquisitionInfo SharesToBePurchased { get; set; }

        /// <summary>
        /// 3.2. Джерело походження коштів для сплати 
        /// </summary>
        public List<IncomeOriginInfo> FundsOrigins { get; set; }

        /// <summary>
        /// 3.3. Порядок сплати 
        /// </summary>
        public List<PaymentModeInfo> PaymentModes { get; set; }

        /// <summary>
        /// 3.4. Терміни сплати 
        /// </summary>
        public List<PaymentDeadlineInfo> PaymentDeadlines { get; set; }

        public List<CouncilBodyInfo> LinkedBanksGoverningBodiesMembers { get; set; }

        public bool HasBreachesOfLaw { get; set; }
        public List<BreachOfLawRecordInfo> BreachesOfLaw { get; set; }
        public List<IndebtnessInfo> OtherLiabilities { get; set; }
        public bool HasLiquidatedSignOwnershipLastYear { get; set; }
        public List<LiquidatedEntityOwnershipInfo> LiquidatedSignOwnershipLastYear { get; set; }
        public bool IsMoneyLaunderingEtcLawsKept { get; set; }
        public bool CanProveMoneyOrigins { get; set; }
        public bool IsApplicationInfoAccurateAndTrue { get; set; }


        [DisplayName("Реквізити осіб-фігурантів анкети")]
        [Description("Повна інформація про осіб, що згадуються в анкеті")]
        public List<GenericPersonInfo> MentionedIdentities { get; set; }
        [DisplayName("Зв'язки між особами-фігурантами анкети")]
        [Description("Відомості про пов'язаних осіб, що згадуються в анкеті:\n1.5. Перелік асоційованих осіб")]
        public List<PersonsAssociation> PersonsLinks { get; set; }
        [DisplayName("Підписант")]
        [Description("Відомості по особу, що підписала анкету")]
        public SignatoryInfo Signatory { get; set; }
        [DisplayName("Контакти")]
        [Description("Контактні дані відправника анкети")]
        public ContactInfo ContactPerson { get; set; }
    }
}
