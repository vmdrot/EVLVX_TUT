﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.TypeEditors;
using System.IO;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;

namespace BGU.DRPL.SignificantOwnership.BasicUILib.Forms
{
    public partial class SimpleObjectForm<T> : Form, IDataSourcedForm<T>
    {
        public SimpleObjectForm()
        {
            InitializeComponent();
        }

        private string CurrentSave2File { get; set; }

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


        public bool ShowMenu
        {
            get { return menuStrip1.Visible; }
            set { menuStrip1.Visible = value; }
        }

        private bool _isRootObjectEditForm = false;
        public bool IsRootObjectEditForm
        {
            get { return _isRootObjectEditForm; }
            set
            {
                if (value == true)
                    SyncRootQuestionnaire();
                _isRootObjectEditForm = value;
            }
        }

        private void SyncRootQuestionnaire()
        {
            if ((object)DataSource == null || !(DataSource is IQuestionnaire))
                return;
            TypeEditorsDispatcher.LastQuestionnaire = (IQuestionnaire)DataSource;
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpen();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveAs();
        }

        private void DoOpen()
        {
            openFileDlg.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            openFileDlg.FilterIndex = 0;

            openFileDlg.Multiselect = false;
            
            DialogResult userClickedOK = openFileDlg.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == System.Windows.Forms.DialogResult.OK)
            {
                // Open the selected file to read.
                try
                {
                    T obj = BGU.DRPL.SignificantOwnership.Utility.Tools.ReadXML<T>(openFileDlg.FileName);
                    DataSource = obj;
                    this.CurrentSave2File = openFileDlg.FileName;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(string.Format("Failed to read file '{0}', error - '{1}'", openFileDlg.FileName,  exc.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DoSave()
        {
            if (!string.IsNullOrEmpty(CurrentSave2File))
            {

                try
                {
                    BGU.DRPL.SignificantOwnership.Utility.Tools.WriteXML<T>(DataSource, CurrentSave2File);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(string.Format("Failed to save file '{0}', error - '{1}'", openFileDlg.FileName, exc.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                DoSaveAs();
        }

        private void DoSaveAs()
        {
            saveFileDgl.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDgl.FilterIndex = 0;
            
            if(!string.IsNullOrEmpty(CurrentSave2File))
                saveFileDgl.FileName = CurrentSave2File;

            DialogResult userClickedOK = saveFileDgl.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == System.Windows.Forms.DialogResult.OK)
            {
                // Open the selected file to read.
                try
                {
                    BGU.DRPL.SignificantOwnership.Utility.Tools.WriteXML<T>(DataSource, saveFileDgl.FileName);
                    CurrentSave2File = saveFileDgl.FileName;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(string.Format("Failed to save file '{0}', error - '{1}'", openFileDlg.FileName, exc.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void propGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if(_isRootObjectEditForm)
                SyncRootQuestionnaire();
        }
    }
}
