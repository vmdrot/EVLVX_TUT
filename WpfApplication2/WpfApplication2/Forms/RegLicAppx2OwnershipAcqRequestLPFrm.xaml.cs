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
            quCtrl.Content = Questionnaire;
            //quCtrl.Content = new BankInfo();
            //quCtrl.Content = new SignificantOwnershipAcquisitionWaysInfo();
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
            MessageBox.Show(Questionnaire.Signatory.ToString());
        }
    }
}
