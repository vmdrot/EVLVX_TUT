using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    [Description("АНКЕТА юридичної особи (у тому числі банку),Додаток 2 до Положення про порядок подання відомостей про структуру власності")]
    [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.Appx2OwnershipStructLP_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class Appx2OwnershipStructLP : IQuestionnaire, IGenericPersonsService, IAddressesService
    {

        public Appx2OwnershipStructLP()
        {
            SupervisoryCouncil = new CouncilBodyInfo();
            this.Executives = new CouncilBodyInfo();
            this.BankExistingCommonImplicitOwners = new List<OwnershipStructure>();
            this.SharesAppliedFor = new List<PurchasedVoteInfo>();
            this.ApplicantOwnershipStructure = new List<OwnershipStructure>();
            this.MentionedIdentities = new List<GenericPersonInfo>();
            this.PersonsLinks = new List<PersonsAssociation>();
        }

        /// <summary>
        /// Bank name
        /// </summary>
        [Description("Банк")]
        public BankInfo BankRef { get; set; }
        
        /// <summary>
        /// p.1.1 , p.1.2 , p.1.3 , p.1.6
        /// </summary>
        [Description("1. Інформація про юридичну особу")]
        public LegalPersonInfo Acquiree { get; set; }

        /// <summary>
        /// p.1.4
        /// </summary>
        [Browsable(true)]
        [Description("1.4. Чи існує наглядова (спостережна) рада юридичної особи")]
        public bool IsSupervisoryCouncilPresent { get; set; }
        [Browsable(true)]
        [Description("1.4. Голова та члени наглядової (спостережної) ради юридичної особи")]
        public CouncilBodyInfo SupervisoryCouncil { get; set; }
        
        /// <summary>
        /// p.1.5
        /// </summary>
        [Browsable(true)]
        [Description("1.5. Голова та члени виконавчого органу юридичної особи")]
        public CouncilBodyInfo Executives { get; set; }
        [Browsable(true)]
        [Description("1.4. Чи існує виконавчий орган юридичної особи")]
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

        [Browsable(false)]
        public IEnumerable<GenericPersonInfo> MentionedGenericPersons
        {
            get 
            {
                Dictionary<string, GenericPersonInfo> rslt = new Dictionary<string, GenericPersonInfo>();
                if (Acquiree != null)
                {
                    GenericPersonInfo gpi = new GenericPersonInfo(Acquiree);
                    rslt.Add(gpi.ID.HashID, gpi);
                }

                foreach (GenericPersonInfo gpi in MentionedIdentities)
                {
                    if (gpi != null && gpi.ID != null && !rslt.ContainsKey(gpi.ID.HashID))
                        rslt.Add(gpi.ID.HashID, gpi);
                }

                if (ContactPerson != null && !rslt.ContainsKey(ContactPerson.GenericID.HashID))
                {
                    rslt.Add(ContactPerson.GenericID.HashID, new GenericPersonInfo(ContactPerson));
                }
                return new List<GenericPersonInfo>(rslt.Values);
            } 
        }

        [Browsable(false)]
        public IEnumerable<LocationInfo> MentionedAddresses
        {
            get 
            {
                Dictionary<string, LocationInfo> rslt = new Dictionary<string, LocationInfo>();

                if (BankRef != null && BankRef.LegalPerson != null && BankRef.LegalPerson.Address != null && !rslt.ContainsKey(BankRef.LegalPerson.Address.ToString()))
                    rslt.Add(BankRef.LegalPerson.Address.ToString(), BankRef.LegalPerson.Address);
                if (Acquiree != null && Acquiree.Address != null && !rslt.ContainsKey(Acquiree.Address.ToString()))
                    rslt.Add(Acquiree.Address.ToString(), Acquiree.Address);
                foreach(GenericPersonInfo gpi in MentionedIdentities)
                {
                    if(gpi.Address != null && !rslt.ContainsKey(gpi.Address.ToString()))
                        rslt.Add(gpi.Address.ToString(), gpi.Address);
                }

                return new List<LocationInfo>(rslt.Values);
            }
        }
    }
}
