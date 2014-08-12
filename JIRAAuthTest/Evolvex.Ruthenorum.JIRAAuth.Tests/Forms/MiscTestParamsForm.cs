using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Evolvex.Ruthenorum.JIRAAuth.Tests.Forms
{
    public partial class MiscTestParamsForm : Form
    {
        public MiscTestParamsForm()
        {
            InitializeComponent();
        }

        public string JSON
        { 
            get { return this.edJson.Text; }
            set { this.edJson.Text = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
