using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    public class Appx2OwnershipStructLP
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
        public bool IsSupervisoryCouncilPresent { get; set; }
        public CouncilBodyInfo SupervisoryCouncil { get; set; }
        
        /// <summary>
        /// p.1.5
        /// </summary>
        public CouncilBodyInfo Executives { get; set; }
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
        public List<CommonOwnershipInfo> BankExistingCommonImplicitOwners { get; set; }

        /// <summary>
        /// p.1.10
        /// </summary>
        public List<PurchasedVoteInfo> SharesAppliedFor {get;set;}

        /// <summary>
        /// p.2.1
        /// </summary>
        public List<PhysicalPersonShareInfo> SignificantSharesPhysicalPersons { get; set; }

        /// <summary>
        /// p.2.2
        /// </summary>
        public List<LegalPersonShareInfo> SignificantSharesLegalPersons { get; set; }

        public List<CommonOwnershipInfo> AcquireeCommonImplicitOwners { get; set; }

        public SignatoryInfo Signatory { get; set; }
        public PhysicalPersonInfo ContactPerson { get; set; }
        public string ContactPhone { get; set; }
    }
}
