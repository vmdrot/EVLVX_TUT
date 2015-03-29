using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BGU.DRPL.SignificantOwnership.Core.TypeEditors;

namespace BGU.DRPL.SignificantOwnership.BasicUILib.Forms
{
    public partial class LookupObjectForm<T> : Form, IDataSourcedForm<T>
    {
        public LookupObjectForm()
        {
            InitializeComponent();
        }

        private void rbExisting_CheckedChanged(object sender, EventArgs e)
        {
            rbExistingNewCheckedChangedCommon();
        }

        private void rbExistingNewCheckedChangedCommon()
        {
            propGrid.Enabled = rbNew.Checked;
            cbxSelectExistingObj.Enabled = rbExisting.Checked;
        }

        private IEnumerable<T> _listSource;
        public IEnumerable<T> ListSource
        {
            get 
            {
                return _listSource;
            }
            set 
            {
                _listSource = value;
            }
        }


        public T DataSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
