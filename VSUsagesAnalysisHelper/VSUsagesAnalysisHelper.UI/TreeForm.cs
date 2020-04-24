using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSUsagesAnalysisHelperLib;

namespace VSUsagesAnalysisHelper.UI
{
    public partial class TreeForm : Form
    {
        public List<VSUsageRec> DataSource { get; set; }

        private Dictionary<string, bool> _alreadyPrintedNodes;

        public string CentralClass
        {
            get { return edClass.Text; }
            set { edClass.Text = value; }
        }
        public TreeForm()
        {
            InitializeComponent();

        }

        protected void BuildTree()
        {
            _alreadyPrintedNodes = new Dictionary<string, bool>();
            if (string.IsNullOrWhiteSpace(CentralClass) || DataSource == null || DataSource.Count == 0)
                return;
            trvw.Nodes.Clear();
            TreeNode rootNode = FormatNode(CentralClass, string.Empty, string.Empty);
            trvw.Nodes.Add(rootNode);
            UnwindUsageGraphs(CentralClass, string.Empty, string.Empty, DataSource, trvw, rootNode);
            //var directUsages = DataSource.Where(r => r.DefType == CentralClass);
            //var distinctDefs = directUsages.Distinct(new VSUsageRecDefFileTypeMethodEqComparer());
            //foreach (var vsu in distinctDefs)
            //{
            //    TreeNode currNode = PrintUsageLine(vsu, trvw, rootNode, out bool bBreak);
            //    UnwindUsageGraphs(vsu.DefType, vsu.DefMember, vsu.DefFile, DataSource, trvw, currNode);
            //}

        }


        private void UnwindUsageGraphs(string defType, string defMember, string defFile, List<VSUsageRec> dataSource, TreeView rslt, TreeNode putUnderNode)
        {
            var directUsages = dataSource.Where(r => (r.DefFile == defFile || string.IsNullOrWhiteSpace(defFile)) && (r.DefType == defType || string.IsNullOrWhiteSpace(defType)) && (r.DefMember == defMember || string.IsNullOrWhiteSpace(defMember)));
            var distinctDefs = directUsages.Distinct(new VSUsageRecDefFileTypeMethodEqComparer());
            
            foreach (var def in distinctDefs)
            {
                bool bBreak;
                TreeNode currNode = PrintUsageLine(def, rslt, putUnderNode, out bBreak);
                if (bBreak)
                    continue;
                var distinctUsages = directUsages.Distinct(new VSUsageRecFileContTypeMethodEqComparer());
                foreach (var usg in distinctUsages) 
                {
                    if (!def.IsNoReferences)
                        UnwindUsageGraphs(usg.ContainingType, usg.ContainingMember, usg.File, dataSource, rslt, currNode);
                }
            }
        }

        private TreeNode PrintUsageLine(VSUsageRec vsu, TreeView rslt, TreeNode putUnderNode, out bool bBreak)
        {
            TreeNode node = FormatNode(vsu.DefMember, vsu.DefType, vsu.DefFile);
            if (!_alreadyPrintedNodes.ContainsKey(node.Text))
            {
                _alreadyPrintedNodes.Add(node.Text, true);
                bBreak = false;
            }
            else
            {
                bBreak = true;
                return node;
            }
            putUnderNode.Nodes.Add(node);
            return node;
        }

        private TreeNode FormatNode(string defMember, string defType, string defFile)
        {
            if (string.IsNullOrWhiteSpace(defMember) && string.IsNullOrWhiteSpace(defType) && string.IsNullOrWhiteSpace(defFile))
                return null;
            TreeNode rslt = new TreeNode();
            var refs = DataSource.Where(vsu => vsu.DefType == defType && (string.IsNullOrWhiteSpace(defFile) || vsu.DefFile == defFile)).Distinct(new VSUsageRecFileContTypeMethodEqComparer());
            string project = refs.FirstOrDefault()?.Project;
            if (string.IsNullOrWhiteSpace(project)) project = "<no project>";
            string dispName = string.Format("{0}.{0}", project, defType);
            if (!string.IsNullOrWhiteSpace(defMember))
                dispName += "." + defMember;
            int realRefsCount = refs.Where(r => !r.IsNoReferences).Count();
            bool isNoRefsOnly = realRefsCount == 0;
            rslt.Text = string.Format("{0} ({1})", dispName, isNoRefsOnly ? "no refs" : string.Format("{0} refs", realRefsCount.ToString()));
            return rslt;
        }

        private void btnBuildTree_Click(object sender, EventArgs e)
        {
            BuildTree();
        }


        private void expandAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            trvw.ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            trvw.CollapseAll();
        }
    }
}
