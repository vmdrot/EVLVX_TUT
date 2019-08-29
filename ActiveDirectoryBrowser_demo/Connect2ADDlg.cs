using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleAdBrowser
{
    public partial class Connect2ADDlg : Form
    {
        public Connect2ADDlg()
        {
            InitializeComponent();
        }


        public ADConnectParameters DataSource
        {
            get 
            { 
                return SerializeUI(); 
            }
            set 
            {
                BindUI(value);
            }
        }

        private void BindUI(ADConnectParameters value)
        {
            edADDomain.Text = value.Domain;
            edADRoot.Text = value.Root;
            edUsr.Text = value.Usr;
            edPwd.Text = value.Pwd;
        }

        private ADConnectParameters SerializeUI()
        {
            return new ADConnectParameters() { Domain = edADDomain.Text.Trim(), Root = edADRoot.Text.Trim(), Usr = edUsr.Text.Trim(), Pwd = edPwd.Text };
        }
        private void Connect2ADDlg_Load(object sender, EventArgs e)
        {
            //
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
