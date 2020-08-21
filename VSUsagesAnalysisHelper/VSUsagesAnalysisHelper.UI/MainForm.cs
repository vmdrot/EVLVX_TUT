using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSUsagesAnalysisHelperLib;

namespace VSUsagesAnalysisHelper.UI
{
    public partial class MainForm : Form
    {
        #region field(s)
        private VSUsageRecSession _session;
        #endregion
        public MainForm()
        {
            InitializeComponent();
            dgvwMissingFiles.AutoGenerateColumns = true;
            _session = new VSUsageRecSession();
        }


        private void btnParse_Click(object sender, EventArgs e)
        {

            List<VSUsageRec> recs = (new VSUsagesParser()).Parse(txtRaw.Text);
            FillDefFileType(recs);
            txtJsonTemp.Text = JsonConvert.SerializeObject(recs, Formatting.Indented);
            txtJsonTempCnt.Text = recs.Count.ToString();
            List<VSUsageRec> recs1 = new List<VSUsageRec> (recs.Distinct(new VSUsageRecEqComparer()));
            txtTrimmedJSON.Text = JsonConvert.SerializeObject(recs1, Formatting.Indented);
            txtTrimmedJSONCnt.Text = recs1.Count.ToString();
            _session.DefFiles.Add(new DefFileTypeRec() { DefFile = edDefFile.Text.Trim(), DefType = edDefType.Text.Trim(), IsEndUserFacing = chkIsEnd.Checked });
            _session.Usages.AddRange(recs1);
            ReCalcMissing();
        }

        private void ReCalcMissing_old()
        {
            var all = (from usg in _session.Usages select new DefFileTypeRec() { DefFile = usg.File, DefType = usg.ContainingType }).Distinct(new DefFileTypeRecEqComparer()).ToList();
            var missing = all.Where( r => _session.DefFiles.FirstOrDefault(q => q.DefFile == r.DefFile && q.DefType == r.DefType) != null).Distinct(new DefFileTypeRecEqComparer()).ToList();
            bindingSource1.DataSource = missing;
        }

        private void ReCalcMissing()
        {
            var all = _session.Usages.Where(u => !u.IsNoReferences).Distinct(new VSUsageRecFileContTypeMethodEqComparer()).ToList();
            var missing = all.Where(r => !_session.SkipClasses.Contains(r.ContainingType) && _session.Usages.FirstOrDefault(q => q.DefFile == r.File && q.DefType == r.ContainingType && q.DefMember == r.ContainingMember) == null).Distinct(new VSUsageRecFileContTypeMethodEqComparer()).ToList();
            var ds = from m in missing
                     select new { ContainingMember = m.ContainingMember, ContainingType = m.ContainingType, File = m.File };
            bindingSource1.DataSource = ds.ToList();
        }

        private void FillDefFileType(List<VSUsageRec> recs)
        {
            string defFile = edDefFile.Text.Trim();
            string defType = edDefType.Text.Trim();
            if (string.IsNullOrWhiteSpace(defFile) && string.IsNullOrWhiteSpace(defType))
                return;

            for (int i = 0; i < recs.Count; i++)
            {
                recs[i].DefFile = defFile;
                recs[i].DefType = defType;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkAppend.Checked && File.Exists(edRecordAs.Text.Trim()))
                MergeWithExistingSession();
            DoSave();
        }

        private void DoSave()
        {
            SerializeSkipClasses();
            File.WriteAllText(edRecordAs.Text.Trim(), JsonConvert.SerializeObject(_session, Formatting.Indented), Encoding.UTF8);
        }

        private void MergeWithExistingSession()
        {
            VSUsageRecSession oldSession = JsonConvert.DeserializeObject<VSUsageRecSession>(File.ReadAllText(edRecordAs.Text.Trim()));
            _session.DefFiles.AddRange(oldSession.DefFiles);
            _session.Usages.AddRange(oldSession.Usages);
            SerializeSkipClasses();
            _session.SkipClasses.AddRange(oldSession.SkipClasses);
            _session.SkipClasses = _session.SkipClasses.Distinct().ToList();
            edSkipClasses.Text = string.Join(",", _session.SkipClasses.ToArray());
        }

        private void SerializeSkipClasses()
        {
            if (!string.IsNullOrWhiteSpace(edSkipClasses.Text.Trim()))
            _session.SkipClasses = new List<string>(edSkipClasses.Text.Trim().Split(',')).Distinct().ToList();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(edRecordAs.Text.Trim()) || !File.Exists(edRecordAs.Text.Trim()))
                return;
            MergeWithExistingSession();
            ReCalcMissing();
        }

        private void btnCopyFileMethodFromGrid_Click(object sender, EventArgs e)
        {
            if (dgvwMissingFiles.SelectedCells.Count > 0)
            {
                edDefFile.Text = dgvwMissingFiles.SelectedCells[0].OwningRow.Cells["File"].Value as string;
                edDefType.Text = dgvwMissingFiles.SelectedCells[0].OwningRow.Cells["ContainingType"].Value as string;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnViewTree_Click(object sender, EventArgs e)
        {
            TreeForm frm = new TreeForm();
            frm.DataSource = _session.Usages;
            frm.Show();
        }
    }
}
