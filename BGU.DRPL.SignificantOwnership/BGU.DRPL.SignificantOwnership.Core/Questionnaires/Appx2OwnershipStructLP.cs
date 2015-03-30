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
        [Description("1.4. Чи існує виконавчий орган юридичної особи")]
        public bool IsExecutivesPresent { get; set; }
        [Browsable(true)]
        [Description("1.5. Голова та члени виконавчого органу юридичної особи")]
        public CouncilBodyInfo Executives { get; set; }
        
        /// <summary>
        /// p.1.7
        /// </summary>
        [Description("1.7. Відсоток участі в банку становить ... відсотків статутного капіталу банку")]
        public decimal TotalOwneshipPct { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Description("1.7. Відсоток участі в банку становить, у т.ч.:")]
        public TotalOwnershipDetailsInfo TotalOwneshipDetails { get; set; } //todo - how many rows

        /// <summary>
        /// p.1.8
        /// </summary>
        //public List<CommonOwnershipInfo> BankExistingCommonImplicitOwners { get; set; }
        [Description("1.8. Інформація про спільне володіння (=розкриття усього ланцюжка власників, у т.ч. й пов'язаних юросіб)")]
        public List<OwnershipStructure> BankExistingCommonImplicitOwners { get; set; }

        /// <summary>
        /// p.1.10
        /// </summary>
        [Description("1.10. Інформація про набуття права голосу")]
        public List<PurchasedVoteInfo> SharesAppliedFor {get;set;}



        /// <summary>
        /// p.2.1
        /// </summary>
        [Description("2.1. Інформація про фізичних осіб, які володіють істотною участю в юридичній особі\n(якщо не було розкрито у п.1.8.)")]
        public List<OwnershipStructure> ApplicantOwnershipStructure { get; set; }

        [Description("Повна інформація про осіб, що згадуються в анкеті")]
        public List<GenericPersonInfo> MentionedIdentities { get; set; }
        [Description("Відомості про пов'язаних осіб, що згадуються в анкеті")]
        public List<PersonsAssociation> PersonsLinks { get; set; }
        [Description("Відомості по особу, що підписала анкету")]
        public SignatoryInfo Signatory { get; set; }
        [Description("Контактні дані відправника анкети")]
        public ContactInfo ContactPerson { get; set; }

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

                if (ContactPerson != null && ContactPerson.Person != null && !rslt.ContainsKey(ContactPerson.Person.GenericID.HashID))
                {
                    rslt.Add(ContactPerson.Person.GenericID.HashID, new GenericPersonInfo(ContactPerson.Person));
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
