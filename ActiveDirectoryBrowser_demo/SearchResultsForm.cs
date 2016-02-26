using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleAdBrowser
{
    public partial class SearchResultsForm : Form
    {
        private DirectoryEntry _AdRootDSE = null;
        private DirectoryEntry _AdRoot = null;

        public string RootDSE
        {
            get { return edSearchADRoot.Text; }
            set { edSearchADRoot.Text = value; }
        }
        public SearchResultsForm()
        {
            InitializeComponent();
            dgvw.AutoGenerateColumns = true;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(edQuery.Text))
            {
                dgvw.DataSource = GetADUsers(edSearchADRoot.Text.Trim(), string.Empty, edQuery.Text.Trim());
                dgvw.Focus();
            }
            else if (!string.IsNullOrEmpty(edEmail.Text))
            {
                dgvw.DataSource = GetADUsers(edSearchADRoot.Text.Trim(), edEmail.Text.Trim());
                dgvw.Focus();
            }
            else
                MessageBox.Show("No search criteria set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static SearchResult GetADUser(string domainPath, string usr)
        {
            DirectoryEntry searchRoot = new DirectoryEntry(domainPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = string.Format("(&(objectClass=user)(objectCategory=person)(samaccountname={0}))", usr);
            return search.FindOne();
        }

        public List<SearchResult> GetADUsers(string adRoot, string email)
        {
            return GetADUsers(adRoot, email, string.Format("(&(objectClass=user)(objectCategory=person)(mail={0}))", email));
        }

        public static List<SearchResult> GetADComputerByUser(string adRoot, string empId, string empDispName)
        {
            return GetADUsers(adRoot, string.Empty, string.Format("(&(objectCategory=computer)(name=*-{0})(description=*{1}))", empId, empDispName));
        }

        public static List<SearchResult> GetADUsers(string adRoot, string email, string filter)
        {
            try
            {
                List<SearchResult> rslt = new List<SearchResult>();
                //string DomainPath = "LDAP://DC=xxxx,DC=com";
                string DomainPath = adRoot;
                DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                search.Filter = filter;
                //search.Filter = email;
                #region props to load - uncomment if not all props are needed
                //search.PropertiesToLoad.Add("samaccountname");
                //search.PropertiesToLoad.Add("mail");
                //search.PropertiesToLoad.Add("usergroup");
                //search.PropertiesToLoad.Add("displayname");//first name
                #endregion
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                //SearchResult result = search.FindOne();
                //rslt.Add(result);
                //SearchResultCollection resultCol = search.FindOne();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        //if (result.Properties.Contains("samaccountname") &&
                        //         result.Properties.Contains("mail") &&
                        //    result.Properties.Contains("displayname"))
                        //{
                            rslt.Add(result);
                            //Users objSurveyUsers = new Users();
                            //objSurveyUsers.Email = (String)result.Properties["mail"][0] + 
                            //  "^" + (String)result.Properties["displayname"][0];
                            //objSurveyUsers.UserName = (String)result.Properties["samaccountname"][0];
                            //objSurveyUsers.DisplayName = (String)result.Properties["displayname"][0];
                            //lstADUsers.Add(objSurveyUsers);
                        //}
                    }
                }
                return rslt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex));
                return null;
            }
        }

        private void dgvw_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowUserProps(e.RowIndex);
        }


        private void ShowUserProps(int iRow)
        {
            List<SearchResult> ds = (List<SearchResult>)dgvw.DataSource;
            AdBrowserForm.ShowUserPropertiesForm(ds[iRow]);
        }
        private void dgvw_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control || e.KeyCode != Keys.Enter)
            { 
                e.Handled = false;
                return;
            }
            e.Handled = true;
            ShowUserProps(dgvw.SelectedCells[0].RowIndex);
        }

        private void dgvw_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //e.
        }

        private void dgvw_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            List<SearchResult> ds = (List<SearchResult>)dgvw.DataSource;
            int iRow = e.RowIndex;
            DirectoryEntry de = ds[iRow].GetDirectoryEntry();
            if(!ds[iRow].Properties.Contains("useraccountcontrol"))
                return;

            ResultPropertyValueCollection usrAcctPropVal = ds[iRow].Properties["useraccountcontrol"];

            if (usrAcctPropVal.Count == 0)
                return;
            if ((int)usrAcctPropVal[0] != 512)
                e.CellStyle.BackColor = Color.Red;
        }

    }
}