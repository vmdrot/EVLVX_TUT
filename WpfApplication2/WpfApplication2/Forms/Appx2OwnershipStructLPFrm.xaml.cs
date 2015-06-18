using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.EmpiricalData.Examples;

namespace WpfApplication2.Forms
{
    /// <summary>
    /// Логика взаимодействия для Appx2OwnershipStructLPFrm.xaml
    /// </summary>
    public partial class Appx2OwnershipStructLPFrm : Window
    {
        public Appx2OwnershipStructLPFrm()
        {
            InitializeComponent();
            _questionnaire = (new GrantBank()).Appx2Questionnaire;
            quCtrl.Content = Questionnaire;
        }


        private Appx2OwnershipStructLP _questionnaire;
        public Appx2OwnershipStructLP Questionnaire 
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
