using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication2.Forms
{
    /// <summary>
    /// Логика взаимодействия для RegLicAppx2OwnershipAcqRequestLPFrm.xaml
    /// </summary>
    public partial class RegLicAppx2OwnershipAcqRequestLPFrm : Window
    {
        public RegLicAppx2OwnershipAcqRequestLPFrm()
        {
            InitializeComponent();
            _questionnaire = new RegLicAppx2OwnershipAcqRequestLP();
            BGU.DRPL.SignificantOwnership.Utility.ReflectionUtil.InstantiateAllProps(_questionnaire, _questionnaire.GetType().Assembly);
            quCtrl.Content = Questionnaire;
            //quCtrl.Content = new BankInfo();
            //quCtrl.Content = new SignificantOwnershipAcquisitionWaysInfo();
            //quCtrl.Content = new BankInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new BankruptcyInvestigationInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new BreachOfLawRecordInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new ContactInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new CountryInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new CourtDecisionInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new CourtInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new CreditRatingInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new CurrencyAmount(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new EconomicActivityType(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new EmailInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new FinancialOversightAuthorityInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new GenericPersonID(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new GenericPersonInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new ImperfectBusinessReputationInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new IncomeOriginInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new IndebtnessInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new IPOSharesPurchaseInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new LegalPersonInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new LegalTransactionInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new LiquidatedEntityOwnershipInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new LoanInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new LocationInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new LPRegisteredDateRecordId(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new MissingInformationResonInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new OwnershipStructure(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new OwnershipSummaryInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new OwnershipVotesInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new PaymentDeadlineInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new PersonsAssociation(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new PhoneInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new PhysicalPersonInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new PowerOfAttorneyInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new PowerOfAttorneySharesPurchaseInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new RatingAgencyInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new RegistrarAuthority(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new RegLicAppx2OwnershipAcqRequestLP(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new SecondaryMarketSharesPurchaseInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new SignatoryInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new SignificantOrDecisiveInfulenceInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new SignificantOwnershipAcquisitionWaysInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new TotalOwnershipDetailsInfo(); MessageBox.Show("Press OK to continue...");
            //quCtrl.Content = new TotalOwnershipSummaryInfo(); MessageBox.Show("Press OK to continue...");

        }


        private RegLicAppx2OwnershipAcqRequestLP _questionnaire;
        public RegLicAppx2OwnershipAcqRequestLP Questionnaire
        {
            get
            {
                return _questionnaire;
            }
            set
            {
                _questionnaire = value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Questionnaire.Signatory.ToString());
            //Questionnaire.BankRef = new BankInfo();
            //Questionnaire.Signatory = new SignatoryInfo();
            /*
            quCtrl.Content = new BankInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new BankruptcyInvestigationInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new BreachOfLawRecordInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new ContactInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new CountryInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new CourtDecisionInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new CourtInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new CreditRatingInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new CurrencyAmount(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new EconomicActivityType(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new EmailInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new FinancialOversightAuthorityInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new GenericPersonID(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new GenericPersonInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new ImperfectBusinessReputationInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new IncomeOriginInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new IndebtnessInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new IPOSharesPurchaseInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new LegalPersonInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new LegalTransactionInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new LiquidatedEntityOwnershipInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new LoanInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new LocationInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new LPRegisteredDateRecordId(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new MissingInformationResonInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new OwnershipStructure(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new OwnershipSummaryInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new OwnershipVotesInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new PaymentDeadlineInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new PersonsAssociation(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new PhoneInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new PhysicalPersonInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new PowerOfAttorneyInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new PowerOfAttorneySharesPurchaseInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new RatingAgencyInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new RegistrarAuthority(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new RegLicAppx2OwnershipAcqRequestLP(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new SecondaryMarketSharesPurchaseInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new SignatoryInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new SignificantOrDecisiveInfulenceInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new SignificantOwnershipAcquisitionWaysInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new TotalOwnershipDetailsInfo(); MessageBox.Show("Press OK to continue...");
            quCtrl.Content = new TotalOwnershipSummaryInfo(); MessageBox.Show("Press OK to continue...");
             */
        }
    }
}
