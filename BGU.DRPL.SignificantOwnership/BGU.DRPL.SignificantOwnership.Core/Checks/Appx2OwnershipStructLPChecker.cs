using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Checks
{
    public class Appx2OwnershipStructLPChecker : IQuestionnaireChecker
    {
        #region field(s)
        private decimal _unknownOwnersTolerancePct = 3M;
        private bool _unknownOwnersTolerancePctSet = false;
        private Appx2OwnershipStructLP _questio;
        private Dictionary<GenericPersonID, decimal> _detectedUltimateBeneficiaries;
        private Dictionary<GenericPersonID, Dictionary<BGU.DRPL.SignificantOwnership.Core.Spares.EntityType, decimal>> _ownerAggrByEntityType;
        #endregion


        #region IQuestionnaireChecker members
        public IQuestionnaire Questionnaire 
        {
            get { if (_questio == null) return null; return (IQuestionnaire)_questio; }
            set
            {
                if (_questio != null)
                    throw new ArgumentException("You can set the questionnaire only once");
                if(!(_questio is Appx2OwnershipStructLP))
                    throw new ArgumentException("Not supported questionnaire format");
                _questio = (Appx2OwnershipStructLP)value;
            } 
        }

        decimal UnkownOwnersTolerancePct 
        {
            get { return _unknownOwnersTolerancePct; }
            set
            {
                if (_unknownOwnersTolerancePctSet)
                    throw new ArgumentException("Unknown owners tolerance % can be set only once.");
                _unknownOwnersTolerancePct = value;
                _unknownOwnersTolerancePctSet = true;
            }
        }

        public bool CheckOwnershipCompleteness()
        {
            return false;
        }

        public bool CheckMissingPersons()
        {
            throw new NotImplementedException();
        }

        public bool CheckMissingPersonsLinks()
        {
            throw new NotImplementedException();
        }

        public List<Spares.Data.OwnershipStructure> ListUltimateBeneficiaries()
        {
            List<GenericPersonID> physPersons = QuestionnaireCheckUtils.ExtractPhysicsOnly(_questio.BankExistingCommonImplicitOwners);
            return null; //todo
        }

        public List<Spares.Data.OwnershipStructure> ListUltimateBeneficiaries(Spares.Data.GenericPersonID forEntity)
        {
            throw new NotImplementedException();
        }
        #endregion


        decimal IQuestionnaireChecker.UnkownOwnersTolerancePct
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Dictionary<Spares.EntityType, decimal> AggregateBankOwnersByEntityType()
        {
            throw new NotImplementedException();
        }
    }
}
