using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Evolvex.PHP2PootleConverterLib.Data;
using Evolvex.PHP2PootleConverterLib;

namespace Evolvex.PHP2PootleConverter.UI
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }


        private void MainFrm_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void InitForm()
        {
            cbxSaveAsEncoding.DisplayMember = "DisplayName";
            cbxSaveAsEncoding.ValueMember = "CodePage";
            List<EncodingInfo> lst = new List<EncodingInfo>(Encoding.GetEncodings());
            lst.Sort((a,b) => String.Compare(a.DisplayName, b.DisplayName));
            cbxSaveAsEncoding.DataSource = lst;
            //Encoding enc = Encoding.GetEncoding(encs[0].CodePage);
            ConversionSettings defaultSettings = new ConversionSettings();
            if(defaultSettings.Direction == ConvertDirection.PHP2Pootle)
                rbPHP2Po.Checked = true;
            else
                rbPo2PHP.Checked = true;
            chkTargetDirSameAsSource.Checked = true;
            cbxSaveAsEncoding.SelectedValue = (object)defaultSettings.SaveAsEncoding.CodePage;
            edSourceLanguageName.Text = defaultSettings.SourceLanguageName;
            edTargetLanguageName.Text = defaultSettings.TargetLanguageName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DoTheJob();
        }

        private void DoTheJob()
        {
            PHP2PootleDirProcessor p = new PHP2PootleDirProcessor();
            Cursor prev = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                p.Process(SerializeUI());
            }
            catch (Exception exc)
            {
                MessageBox.Show(String.Format("An error encountered while trying to perform the conversion, details:\n\r {0}", exc), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = prev;
            }
        }

        private ConversionSettings SerializeUI()
        {
            ConversionSettings rslt = new ConversionSettings();
            rslt.SourceDir = edSourceDir.Text.Trim();
            rslt.TargetDir = chkTargetDirSameAsSource.Checked ? rslt.SourceDir : edTargetDir.Text.Trim();
            rslt.Direction = rbPHP2Po.Checked ? ConvertDirection.PHP2Pootle : ConvertDirection.Pootle2PHP;
            rslt.SourceLanguageName = edSourceLanguageName.Text.Trim();
            rslt.TargetLanguageName = edTargetLanguageName.Text.Trim();
            rslt.DeleteSourceFiles = chkDeleteSourceFiles.Checked;
            rslt.SaveAsEncoding = Encoding.GetEncoding((int)cbxSaveAsEncoding.SelectedValue);
            return rslt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkTargetDirSameAsSource_CheckedChanged(object sender, EventArgs e)
        {
            edTargetDir.Enabled = btnTargetDirBrowse.Enabled = !chkTargetDirSameAsSource.Checked;
        }

        private void btnSourceDirBrowse_Click(object sender, EventArgs e)
        {
            SelectFolderWorker(edSourceDir);
        }

        private void btnTargetDirBrowse_Click(object sender, EventArgs e)
        {
            SelectFolderWorker(edTargetDir);
        }

        private void SelectFolderWorker(TextBox target)
        {
            if (folderDlg.ShowDialog() != DialogResult.OK)
                return;
            target.Text = folderDlg.SelectedPath;

        }
    }
}
