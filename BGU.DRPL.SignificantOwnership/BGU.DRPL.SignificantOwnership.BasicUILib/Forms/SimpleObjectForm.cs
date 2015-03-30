using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.TypeEditors;

namespace BGU.DRPL.SignificantOwnership.BasicUILib.Forms
{
    public partial class SimpleObjectForm<T> : Form, IDataSourcedForm<T>
    {
        public SimpleObjectForm()
        {
            InitializeComponent();
        }

        public T DataSource
        {
            get
            {
                return (T)propGrid.SelectedObject;
            }
            set
            {
                propGrid.SelectedObject = value;
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DummyForm_ResizeEnd(object sender, EventArgs e)
        {
            statusLbl.Text = string.Format("{0}x{1}", this.Size.Width, this.Size.Height);
        }

        private void DummyForm_Resize(object sender, EventArgs e)
        {
            statusLbl.Text = string.Format("{0}x{1}", this.Size.Width, this.Size.Height);
        }

        private void DummyForm_Load(object sender, EventArgs e)
        {

        }

        private T InstantiateNewDataSource()
        {
            object o = Activator.CreateInstance(typeof(T));
            return (T)o;
        }
        private void btnFillObject_Click(object sender, EventArgs e)
        {
            DataSource = InstantiateNewDataSource();
        }

        public event Core.TypeEditors.FormCloseHandler<T> FormClose;
    }
}
