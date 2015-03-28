﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.TypeEditors;
using BGU.DRPL.SignificantOwnership.BasicUILib.Forms;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.BasicUILib
{
    public class Appx2OwnershipStructLPEditFormFactoryBasic : IAppx2OwnershipStructLPEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<Appx2OwnershipStructLP>(); } }
    public class RegLicAppx14NewSvcEditFormFactoryBasic : IRegLicAppx14NewSvcEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<RegLicAppx14NewSvc>(); } }
    public class RegLicAppx2LPQuestEditFormFactoryBasic : IRegLicAppx2LPQuestEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<RegLicAppx2LPQuest>(); } }
    public class RegLicAppx4PhysPQuestEditFormFactoryBasic : IRegLicAppx4PhysPQuestEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<RegLicAppx4PhysPQuest>(); } }
    public class RegLicAppx7ShareAcqIntentEditFormFactoryBasic : IRegLicAppx7ShareAcqIntentEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<RegLicAppx7ShareAcqIntent>(); } }
    public class AttachmentInfoEditFormFactoryBasic : IAttachmentInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<AttachmentInfo>(); } }
    public class BankingLicensedActivityInfoEditFormFactoryBasic : IBankingLicensedActivityInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<BankingLicensedActivityInfo>(); } }
    public class BankingLicenseInfoEditFormFactoryBasic : IBankingLicenseInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<BankingLicenseInfo>(); } }
    public class CommonOwnershipInfoEditFormFactoryBasic : ICommonOwnershipInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<CommonOwnershipInfo>(); } }
    public class ContactInfoEditFormFactoryBasic : IContactInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<ContactInfo>(); } }
    public class CouncilBodyInfoEditFormFactoryBasic : ICouncilBodyInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<CouncilBodyInfo>(); } }
    public class CouncilMemberInfoEditFormFactoryBasic : ICouncilMemberInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<CouncilMemberInfo>(); } }
    public class CurrencyAmountEditFormFactoryBasic : ICurrencyAmountEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<CurrencyAmount>(); } }
    public class GenericPersonIDEditFormFactoryBasic : IGenericPersonIDEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<GenericPersonID>(); } }
    public class GenericPersonIDShareEditFormFactoryBasic : IGenericPersonIDShareEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<GenericPersonIDShare>(); } }
    public class GenericPersonInfoEditFormFactoryBasic : IGenericPersonInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<GenericPersonInfo>(); } }
    public class LegalPersonShareInfoEditFormFactoryBasic : ILegalPersonShareInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<LegalPersonShareInfo>(); } }
    public class OwnershipStructureEditFormFactoryBasic : IOwnershipStructureEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<OwnershipStructure>(); } }
    public class OwnershipSummaryInfoEditFormFactoryBasic : IOwnershipSummaryInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<OwnershipSummaryInfo>(); } }
    public class OwnershipVotesInfoEditFormFactoryBasic : IOwnershipVotesInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<OwnershipVotesInfo>(); } }
    public class PersonsAssociationEditFormFactoryBasic : IPersonsAssociationEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<PersonsAssociation>(); } }
    public class PhoneInfoEditFormFactoryBasic : IPhoneInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<PhoneInfo>(); } }
    public class PhysicalPersonShareInfoEditFormFactoryBasic : IPhysicalPersonShareInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<PhysicalPersonShareInfo>(); } }
    public class PurchasedVoteInfoEditFormFactoryBasic : IPurchasedVoteInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<PurchasedVoteInfo>(); } }
    public class SignatoryInfoEditFormFactoryBasic : ISignatoryInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<SignatoryInfo>(); } }
    public class TotalOwnershipDetailsInfoEditFormFactoryBasic : ITotalOwnershipDetailsInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<TotalOwnershipDetailsInfo>(); } }
    public class BankInfoEditFormFactoryBasic : IBankInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { System.Windows.Forms.Form rslt = new DummyForm<BankInfo>(); rslt.Size = new System.Drawing.Size(500, 400); return rslt; } }
    public class CountryInfoEditFormFactoryBasic : ICountryInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<CountryInfo>(); } }
    public class LegalPersonInfoEditFormFactoryBasic : ILegalPersonInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<LegalPersonInfo>(); } }
    public class LocationInfoEditFormFactoryBasic : ILocationInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<LocationInfo>(); } }
    public class PhysicalPersonInfoEditFormFactoryBasic : IPhysicalPersonInfoEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<PhysicalPersonInfo>(); } }
    public class RegistrarAuthorityEditFormFactoryBasic : IRegistrarAuthorityEditFormFactory { public System.Windows.Forms.Form SpawnInstance() { return new DummyForm<RegistrarAuthority>(); } }
}

