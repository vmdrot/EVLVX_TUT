﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Interfaces;
using Microsoft.Practices.Unity;
using BGU.DRPL.SignificantOwnership.Core.TypeEditors;
using BGU.DRPL.SignificantOwnership.BasicUILib;


namespace BGU.DRPL.SignificantOwnership.UI.Modules
{
    public class BasicUIModule : IModule
    {
        public void Initialize(IUnityContainer cont)
        {
            cont.RegisterInstance<IAppx2OwnershipStructLPEditFormFactory>(new Appx2OwnershipStructLPEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IRegLicAppx14NewSvcEditFormFactory>(new RegLicAppx14NewSvcEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IRegLicAppx2LPQuestEditFormFactory>(new RegLicAppx2LPQuestEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IRegLicAppx4PhysPQuestEditFormFactory>(new RegLicAppx4PhysPQuestEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IRegLicAppx7ShareAcqIntentEditFormFactory>(new RegLicAppx7ShareAcqIntentEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IAttachmentInfoEditFormFactory>(new AttachmentInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IBankingLicensedActivityInfoEditFormFactory>(new BankingLicensedActivityInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IBankingLicenseInfoEditFormFactory>(new BankingLicenseInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ICommonOwnershipInfoEditFormFactory>(new CommonOwnershipInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IContactInfoEditFormFactory>(new ContactInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ICouncilBodyInfoEditFormFactory>(new CouncilBodyInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ICouncilMemberInfoEditFormFactory>(new CouncilMemberInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ICurrencyAmountEditFormFactory>(new CurrencyAmountEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IGenericPersonIDEditFormFactory>(new GenericPersonIDEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IGenericPersonIDShareEditFormFactory>(new GenericPersonIDShareEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IGenericPersonInfoEditFormFactory>(new GenericPersonInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ILegalPersonShareInfoEditFormFactory>(new LegalPersonShareInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IOwnershipStructureEditFormFactory>(new OwnershipStructureEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IOwnershipSummaryInfoEditFormFactory>(new OwnershipSummaryInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IOwnershipVotesInfoEditFormFactory>(new OwnershipVotesInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IPersonsAssociationEditFormFactory>(new PersonsAssociationEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IPhoneInfoEditFormFactory>(new PhoneInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IPhysicalPersonShareInfoEditFormFactory>(new PhysicalPersonShareInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IPurchasedVoteInfoEditFormFactory>(new PurchasedVoteInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ISignatoryInfoEditFormFactory>(new SignatoryInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ITotalOwnershipDetailsInfoEditFormFactory>(new TotalOwnershipDetailsInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IBankInfoEditFormFactory>(new BankInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ICountryInfoEditFormFactory>(new CountryInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ILegalPersonInfoEditFormFactory>(new LegalPersonInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<ILocationInfoEditFormFactory>(new LocationInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IPhysicalPersonInfoEditFormFactory>(new PhysicalPersonInfoEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
            cont.RegisterInstance<IRegistrarAuthorityEditFormFactory>(new RegistrarAuthorityEditFormFactoryBasic(), new ContainerControlledLifetimeManager());
        }
    }
}
