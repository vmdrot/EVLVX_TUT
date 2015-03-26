using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    public class RegLicAppx14NewSvc: IQuestionnaire
    {
        public DateTime QuestDate { get; set; }
        public string Ref { get; set; }
        public BankInfo BankRef { get; set; }
        public BankingLicenseInfo ExistingLicenseSpecs {get;set;}
        public List<BankingLicensedActivityInfo> NewActivitiesList { get; set; }
        public bool HasSufficientFinancialCapacity { get; set; }
        public int MonthsWOLosses { get; set; }
        public bool HasFormedEnoughFundsAndReserves { get; set; }
        public int MonthsObligatoryReservesMet { get; set; }
        public bool RegulatoryCapitalReqsMet { get; set; }
        public CurrencyAmount RegulatoryCapital { get; set; }
        public bool NoPenaltiesWithinLast3Months { get; set; }
        public bool HasDeptsAllocatedForNewBusLines { get; set; }
        public bool HasFacilitiesForNewBusLines { get; set; }
        public bool IsGenericLawReqsMetForNewBusLines { get; set; }
        public bool IsBankIndustrySpecificLawReqsMetForNewBusLines { get; set; }
        public bool IsInternalRegulationCopiesAttachedForNewBusLines { get; set; }
        public bool IsSigneePersonallyLiableForCopiesAttached { get; set; }
        public ContactInfo Contacts { get; set; }
        public List<AttachmentInfo> Attachements { get; set; }
        public SignatoryInfo Signee { get; set; }
    }
}
