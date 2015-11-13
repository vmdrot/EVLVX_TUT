using BGU.DRPL.SignificantOwnership.Core.Messages;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
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
using WpfApplication2.Forms;

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
        }



        #region Menu items click handlers
        private void RegLicAppx2OwnershipAcqRequestLP_MenuItemClick(object sender, RoutedEventArgs e)
        {
            ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, new RegLicAppx2OwnershipAcqRequestLP());
            //ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, BGU.DRPL.SignificantOwnership.Utility.Tools.ReadXML(@"D:\home\vmdrot\Testing\regLicDod2IstUchYO.855664.1231564684.xml", typeof(RegLicAppx2OwnershipAcqRequestLP)));
        }

        private void RegLicAppx3MemberCandidateAppl_MenuItemClick(object sender, RoutedEventArgs e)
        {
            ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, new RegLicAppx3MemberCandidateAppl());
        }

        private void RegLicAppx4OwnershipAcqRequestPP_MenuItemClick(object sender, RoutedEventArgs e)
        {
            ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, new RegLicAppx4OwnershipAcqRequestPP());
        }

        private void RegLicAppx12HeadCandidateAppl_MenuItemClick(object sender, RoutedEventArgs e)
        {
            ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, new RegLicAppx12HeadCandidateAppl());
        }

        private void RegLicAppx14NewSvc_MenuItemClick(object sender, RoutedEventArgs e)
        {
            ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, new RegLicAppx14NewSvc());
        }

        private void Post315AppxBankAssocPersons_MenuItemClick(object sender, RoutedEventArgs e)
        {
            ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, new Post315AppxBankAssocPersons());
        }

        private void RegLicAppx2OwnershipAcqRequestLP_MoneyOriginsLtr_MenuItemClick(object sender, RoutedEventArgs e)
        {
            ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, new RegLicAppx2OwnershipAcqRequestLP_MoneyOriginsLtr());
        }

        private void StateBankRegistryItem_Click(object sender, RoutedEventArgs e)
        {
            ShowQuestionnaireEditForm((System.Windows.Controls.MenuItem)sender, new BGU.DRPL.SignificantOwnership.Core.EKDRBU.StateBankRegistryEntry());
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //todo
            MessageBox.Show("Прототип для анкет, повідомлень та стуктурування\nіншої інформації у рамках розбудови Бази пруденційної \nінформації НБУ.\n\n Департамент реєстраційних питань і ліцензування,\n Національний банк © 2015");
        }

        private void TestShowItemsByTag_Click(object sender, RoutedEventArgs e)
        {
            SelectedObjectFrm frm = new SelectedObjectFrm();
            frm.DataSource = new GenericPersonInfo();
            frm.ShowOrHideControls = new ShowHideControlsByTagInfo() { ShowOrHide = false, ControlPropNames = "" };
            frm.ShowDialog();
        }

        private void TreeViewTest_Click(object sender, RoutedEventArgs e)
        {
            TreeViewTestFrm frm = new TreeViewTestFrm();
            frm.ShowDialog();
        }
        #endregion

        private void ShowQuestionnaireEditForm(System.Windows.Controls.MenuItem menuItem, object questio)
        {
            ShowQuestionnaireEditForm(menuItem, questio, false);
        }
        private void ShowQuestionnaireEditForm(System.Windows.Controls.MenuItem menuItem, object questio, bool showMoreMenu)
        {
            BGU.DRPL.SignificantOwnership.Utility.ReflectionUtil.InstantiateAllProps(questio, questio.GetType().Assembly);
            if (questio is IQuestionnaire)
                DataModule.CurrentQuestionnare = (IQuestionnaire)questio;
            SelectedObjectFrm frm = new SelectedObjectFrm();
            frm.DataSource = questio;
            StringBuilder sbFormCaption = new StringBuilder();
            if (menuItem != null && menuItem.Parent != null)
                sbFormCaption.AppendFormat("{0}: ", ((System.Windows.Controls.MenuItem)menuItem.Parent).Header);
            if (menuItem != null)
                sbFormCaption.Append(menuItem.Header);
            frm.Title = sbFormCaption.ToString();
            //frm.ShowMenu = true;
            //frm.ShowMoreMenu = showMoreMenu;
            //frm.IsRootObjectEditForm = true;
            //frm.Size = new System.Drawing.Size(1208, 728);
            frm.Icon = this.Icon;
            frm.ShowDialog();
        }



    }
}
