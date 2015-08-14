using BGU.DRPL.SignificantOwnership.Core.EKDRBU.Legacy;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System;
using System.Collections;
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
            DoUnfilter();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            DepListTreeFilterCriteria searchCrit = SerializeFilter();
            if (IsFilterEmpty())
                DoUnfilter();
            else
                DoFilter(searchCrit);
            
        }

        private DepListTreeFilterCriteria SerializeFilter()
        {
            LogicalOperator lop = LogicalOperator.None;
            string lopStr = (cbxFilterCondition.SelectedItem as ComboBoxItem).Tag as string;

            switch(lopStr.ToLower())
            {
                case "and": lop = LogicalOperator.And; break;
                case "or": lop = LogicalOperator.Or; break;
                case "notand": lop = LogicalOperator.NotAnd; break;
                case "notor": lop = LogicalOperator.NotOr; break;
                default: break;
            }
            return new DepListTreeFilterCriteria() { SearchText = edSearchTxt.Text, OblastCode = cbxOblast.SelectedValue as string, Operator = lop};
        }

        private void DoFilter(DepListTreeFilterCriteria searchCrit)
        {
            DataModule.SelectedBank = (BankInfo)cbxBank.SelectedValue;
            var dsRaw = DataModule.HierarchedBankDepts;
            List<DeptListEntry> lstRaw = DataModule.FilterDeptsByBank(DataModule.SelectedBank, DataModule.ReadAllDeptList());
            //lstRaw.AddRange((IEnumerable<DeptListEntry>)dsRaw);
            List<DeptListEntry> filtered = ApplyFilter(lstRaw, searchCrit);
            trvw.ItemsSource = (IEnumerable)BGU.DRPL.SignificantOwnership.Facade.EKDRBU.DeptListUtil.BuildHierarchy(filtered, lstRaw).Where(dle => (dle.ParentCode == string.Empty));
            UpdateItemsCountLbl(filtered.Count);
        }

        private List<DeptListEntry> ApplyFilter(System.Collections.IEnumerable dsRaw, DepListTreeFilterCriteria searchCrit)
        {
            List<DeptListEntry> rslt = new List<DeptListEntry>();
            foreach (var vdle in dsRaw)
            {
                DeptListEntry dle = (DeptListEntry)vdle;
                int matchCnt = 0;
                if (!string.IsNullOrEmpty(searchCrit.OblastCode) && dle.KOF == searchCrit.OblastCode)
                    matchCnt++;
                if (!string.IsNullOrEmpty(searchCrit.SearchText) && dle.NAMEF.ToLower().IndexOf(searchCrit.SearchText.ToLower())!= -1)
                    matchCnt++;
                if ((searchCrit.Operator == LogicalOperator.Or && matchCnt == 0) || (searchCrit.Operator == LogicalOperator.And && matchCnt < 2))
                    continue;
                rslt.Add(dle);
            }
            return rslt;
        }

        private bool IsFilterEmpty()
        {
            return string.IsNullOrEmpty(edSearchTxt.Text) && cbxOblast.SelectedIndex == -1;
        }

        private void DoUnfilter()
        {
            DataModule.SelectedBank = (BankInfo)cbxBank.SelectedValue;
            trvw.ItemsSource = DataModule.HierarchedBankDepts;
            int dsCount = 0;
            if(DataModule.HierarchedBankDepts != null)
            {
                foreach(DeptListEntry dle in DataModule.HierarchedBankDepts)
                { 
                    dsCount = dle.HierarchySource.Count;
                    break;
                }
            }
            UpdateItemsCountLbl(dsCount);
        }


        public int TreeViewItemsCount
        {
            get
            {
                return trvw.Items.Count;
            }
        }


        #region inner type(s)

        public enum LogicalOperator
        { 
            None = 0,
            And,
            Or,
            NotAnd,
            NotOr
        }
        public class DepListTreeFilterCriteria
        {
            public string SearchText { get; set; }
            
            public string OblastCode { get; set; }

            public LogicalOperator Operator { get; set; }

            public bool IsEmpty
            {
                get
                {
                    return string.IsNullOrEmpty(SearchText) && string.IsNullOrEmpty(OblastCode);
                }
            }
        }

        private void UpdateItemsCountLbl(int cnt)
        {
            edFoundItemsCnt.Text = cnt.ToString();
        }
        
        #endregion
    }
}
