using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.RegLicAppx4PhysPQuest_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class RegLicAppx4PhysPQuest : IQuestionnaire
    {
        /// <summary>
        /// Bank name
        /// </summary>
        [DisplayName("Банк")]
        [Description("Банк")]
        public BankInfo BankRef { get; set; }


        [DisplayName("Фізособа-заявник")]
        [Description("1. Інформація про особу")]
        public PhysicalPersonInfo Acquiree { get; set; }

        public List<BankAccountInfo> BankingAccounts { get; set; }

        public BankInfo ReferenceBank { get; set; }

        public List<EmploymentRecordInfo> EmploymentRecords { get; set; }

        public List<EducationRecordInfo> EducationRecords { get; set; }
        public List<ProfessionLicenseInfo> ProfessionalLicenses { get; set; }

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
        public List<PaymentModeInfo> PaymentDeadlines { get; set; }

        public List<CouncilBodyInfo> LinkedBanksGoverningBodiesMembers { get; set; }

        public bool HasBreachesOfLaw { get; set; }
        public List<BreachOfLawRecordInfo> BreachesOfLaw { get; set; }
        //todo...
        [DisplayName("Реквізити осіб-фігурантів анкети")]
        [Description("Повна інформація про осіб, що згадуються в анкеті")]
        public List<GenericPersonInfo> MentionedIdentities { get; set; }
        [DisplayName("Зв'язки між особами-фігурантами анкети")]
        [Description("Відомості про пов'язаних осіб, що згадуються в анкеті")]
        public List<PersonsAssociation> PersonsLinks { get; set; }
        [DisplayName("Підписант")]
        [Description("Відомості по особу, що підписала анкету")]
        public SignatoryInfo Signatory { get; set; }
        [DisplayName("Контакти")]
        [Description("Контактні дані відправника анкети")]
        public ContactInfo ContactPerson { get; set; }
    }
}
