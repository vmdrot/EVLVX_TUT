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
using WpfApplication2.Data;

namespace WpfApplication2.Forms
{
    /// <summary>
    /// Логика взаимодействия для TreeViewTestFrm.xaml
    /// </summary>
    public partial class TreeViewTestFrm : Window
    {
        public TreeViewTestFrm()
        {
            InitializeComponent();
        }


        protected void cbxBank_SelectionChanged(object sender, EventArgs ea)
        {
            DataModule.SelectedBank = (BankInfo)cbxBank.SelectedValue;
            trvw.ItemsSource = DataModule.HierarchedBankDepts;
        }
    }
}
