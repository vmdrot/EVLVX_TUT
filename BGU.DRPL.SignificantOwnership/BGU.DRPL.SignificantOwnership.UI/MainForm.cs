using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BGU.DRPL.SignificantOwnership.UI.Questionnaires;
using BGU.DRPL.SignificantOwnership.EmpiricalData.Examples;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.BasicUILib.Forms;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;

namespace BGU.DRPL.SignificantOwnership.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void appx2LPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appx2OwnStruLPFrm frm = new Appx2OwnStruLPFrm();
            frm.Datasource = (new GrantBank()).Appx2Questionnaire;
            frm.ShowDialog();
        }

        private void acqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appx2OwnStruLPFrm frm = new Appx2OwnStruLPFrm();
            frm.LegalPersonDS = (new GrantBank()).Appx2Questionnaire.Acquiree;
            frm.ShowDialog();

        }

        private void genericPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appx2OwnStruLPFrm frm = new Appx2OwnStruLPFrm();
            frm.GenericPersonDS = (new GrantBank()).Appx2Questionnaire.MentionedIdentities[0];
            frm.ShowDialog();

        }

        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appx2OwnStruLPFrm frm = new Appx2OwnStruLPFrm();
            frm.AddressDS = (new GrantBank()).Appx2Questionnaire.MentionedIdentities[0].PhysicalPerson.Address;
            frm.ShowDialog();

        }

        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appx2OwnStruLPFrm frm = new Appx2OwnStruLPFrm();
            frm.CountryDS = CountryInfo.AALAND_ISLANDS;
            frm.ShowDialog();

        }

        private void appx2LPproperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowQuestionnaireEditForm<BGU.DRPL.SignificantOwnership.Core.Questionnaires.Appx2OwnershipStructLP>(appx2LPproperToolStripMenuItem, (new GrantBank()).Appx2Questionnaire);
            //SimpleObjectForm<BGU.DRPL.SignificantOwnership.Core.Questionnaires.Appx2OwnershipStructLP> frm = new SimpleObjectForm<BGU.DRPL.SignificantOwnership.Core.Questionnaires.Appx2OwnershipStructLP>();
            //frm.DataSource = (new GrantBank()).Appx2Questionnaire;
            //frm.Text = string.Format(appx2LPproperToolStripMenuItem.GetCurrentParent().Text, appx2LPproperToolStripMenuItem.Text);
            //frm.ShowDialog();
        }

        private void ShowQuestionnaireEditForm<T>(ToolStripMenuItem menuItem, T questio)
        {
            SimpleObjectForm<T> frm = new SimpleObjectForm<T>();
            frm.DataSource = questio;
            StringBuilder sbFormCaption = new StringBuilder();
            if (menuItem != null && menuItem.GetCurrentParent() != null)
                sbFormCaption.AppendFormat("{0}: ", menuItem.GetCurrentParent().Text);
            if (menuItem != null)
                sbFormCaption.Append( menuItem.Text);
            frm.Text = sbFormCaption.ToString();
            frm.ShowMenu = true;
            frm.IsRootObjectEditForm = true;
            frm.ShowDialog();
        }

        private void regLicAppx7ShareAcqIntentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowQuestionnaireEditForm<BGU.DRPL.SignificantOwnership.Core.Questionnaires.RegLicAppx7ShareAcqIntent>(regLicAppx7ShareAcqIntentToolStripMenuItem, new RegLicAppx7ShareAcqIntent());
        }

        private void regLicAppx14NewSvcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowQuestionnaireEditForm<BGU.DRPL.SignificantOwnership.Core.Questionnaires.RegLicAppx14NewSvc>(regLicAppx14NewSvcToolStripMenuItem, new RegLicAppx14NewSvc());
        }

        private void regLicAppx4PhysPQuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowQuestionnaireEditForm<BGU.DRPL.SignificantOwnership.Core.Questionnaires.RegLicAppx4PhysPQuest>(regLicAppx4PhysPQuestToolStripMenuItem, new RegLicAppx4PhysPQuest());
        }
    }
}
