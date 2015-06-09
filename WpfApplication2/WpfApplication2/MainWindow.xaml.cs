using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BGU.DRPL.SignificantOwnership.Core.Spares;
using BGU.DRPL.SignificantOwnership.Utility;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cbxBank.ValueMember = "LCID";
            cbxBank.DisplayMember = "NativeName";
            cbxBank.ListSource = CultureInfo.GetCultures(CultureTypes.AllCultures);
            cbxBank.SelectedValue = CultureInfo.GetCultureInfoByIetfLanguageTag("de").LCID;

            cbxBreach.ValueMember = "EnumValue";
            cbxBreach.DisplayMember = "Value";
            cbxBreach.ListSource = EnumType.GetEnumList(typeof(BGU.DRPL.SignificantOwnership.Core.Spares.BreachOfLawType));
            cbxBreach.SelectedValue = (Enum)BreachOfLawType.Criminal;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanelWindow wnd = new StackPanelWindow();
            wnd.ShowDialog();
        }

    }
}
