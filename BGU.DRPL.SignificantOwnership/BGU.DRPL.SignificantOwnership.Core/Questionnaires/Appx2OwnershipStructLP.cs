using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.Appx2OwnershipStructLP_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class Appx2OwnershipStructLP : IQuestionnaire
    {
        /// <summary>
        /// Bank name
        /// </summary>
        public BankInfo BankRef { get; set; }
        
        /// <summary>
        /// p.1.1 , p.1.2 , p.1.3 , p.1.6
        /// </summary>
        public LegalPersonInfo Acquiree { get; set; }

        /// <summary>
        /// p.1.4
        /// </summary>
        [Browsable(true)]
        public bool IsSupervisoryCouncilPresent { get; set; }
        [Browsable(true)]
        public CouncilBodyInfo SupervisoryCouncil { get; set; }
        
        /// <summary>
        /// p.1.5
        /// </summary>
        /// [Browsable(true)]
        public CouncilBodyInfo Executives { get; set; }
        [Browsable(true)]
        public bool IsExecutivesPresent { get; set; }
        
        /// <summary>
        /// p.1.7
        /// </summary>
        public decimal TotalOwneshipPct { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public TotalOwnershipDetailsInfo TotalOwneshipDetails { get; set; } //todo - how many rows

        /// <summary>
        /// p.1.8
        /// </summary>
        //public List<CommonOwnershipInfo> BankExistingCommonImplicitOwners { get; set; }
        public List<OwnershipStructure> BankExistingCommonImplicitOwners { get; set; }

        /// <summary>
        /// p.1.10
        /// </summary>
        public List<PurchasedVoteInfo> SharesAppliedFor {get;set;}



        /// <summary>
        /// p.2.1
        /// </summary>
        public List<OwnershipStructure> ApplicantOwnershipStructure { get; set; }

        public List<GenericPersonInfo> MentionedIdentities { get; set; }
        public List<PersonsAssociation> PersonsLinks { get; set; }

        public SignatoryInfo Signatory { get; set; }
        public PhysicalPersonInfo ContactPerson { get; set; }
        public string ContactPhone { get; set; }
    }
}
