using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.RegLicAppx7ShareAcqIntent_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class RegLicAppx7ShareAcqIntent : IQuestionnaire
    {
        public RegLicAppx7ShareAcqIntent()
        {
            ExistingOwnership = new List<OwnershipStructure>();
            MentionedIdentities = new List<GenericPersonInfo>();
            PersonsLinks = new List<PersonsAssociation>();
        }

        public BankInfo BankRef { get; set; }
        public GenericPersonInfo Acquiree { get; set; }
        public TotalOwnershipDetailsInfo ExistingOwnershipSummary { get; set; }
        public List<OwnershipStructure> ExistingOwnership { get; set; }
        public List<GenericPersonInfo> MentionedIdentities { get; set; }
        public List<PersonsAssociation> PersonsLinks { get; set; }
        public TotalOwnershipDetailsInfo TargetedOwnershipSummary { get; set; }

        //public decimal TargetedOwnershipSharePct { get; set; }
        //public decimal TargetedOwnershipShareAmt { get; set; }
        public SignatoryInfo Signee { get; set; }


        public string SuggestSaveAsFileName()
        {
            throw new NotImplementedException();
        }
    }
}
