using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApplication2.Data;

namespace WpfApplication2.Forms
{
    /// <summary>
    /// Логика взаимодействия для SelectedObjectFrm.xaml
    /// </summary>
    public partial class SelectedObjectFrm : Window
    {
        private Microsoft.Win32.SaveFileDialog _saveFileDgl;
        private Microsoft.Win32.OpenFileDialog _openFileDlg;

        Dictionary<String, List<DependencyObject>> _controlsByTag;

        public SelectedObjectFrm()
        {
            InitializeComponent();
            _saveFileDgl = new Microsoft.Win32.SaveFileDialog();
            _openFileDlg = new Microsoft.Win32.OpenFileDialog();
            this.Loaded += SelectedObjectFrm_Loaded;
        }

        protected void SelectedObjectFrm_Loaded(object sender, RoutedEventArgs e)
        {
            if (ShowOrHideControls == null)
                return;
            ApplyShowOrHideControlsBasedOnTags();
        }

        private void ApplyShowOrHideControlsBasedOnTags()
        {
            
        }

        public object DataSource
        {
            get { return quCtrl.Content; }
            set { quCtrl.Content = value; }
        }

        public ShowHideControlsByTagInfo ShowOrHideControls
        {
            get;
            set;
        }


        private string CurrentSave2File { get; set; }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }


        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoOpen();
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoOpen();
            this.CurrentSave2File = _openFileDlg.FileName;
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
        }
        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoSaveAs();
        }

        private void DoOpen()
        {
            _openFileDlg.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            _openFileDlg.FilterIndex = 0;

            _openFileDlg.Multiselect = false;

            bool? userClickedOK = _openFileDlg.ShowDialog();

            // Process input if the user clicked OK.
            if ((bool)userClickedOK)
            {
                // Open the selected file to read.
                try
                {
                    object newDS = BGU.DRPL.SignificantOwnership.Utility.Tools.ReadXML(_openFileDlg.FileName, DataSource.GetType());
                    object[] args = new object[] { newDS, this };
                    MyCommands.ReOpenSelectedObjectFormCommand.Execute((object)args, null);
                    //if (DataSource is IQuestionnaire)
                    //    DataModule.CurrentQuestionnare = (IQuestionnaire)newDS;
                    //DataSource = newDS;

                    //RefreshGenericPersonsCombos();
                }
                catch (Exception exc)
                {
                    System.Windows.MessageBox.Show(string.Format("Не вдалося прочитати файл '{0}', деталі - '{1}'", _openFileDlg.FileName, exc.Message), "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RefreshGenericPersonsCombos()
        {
            //below didn't work
            //System.Windows.Controls.ComboBox cbx = (System.Windows.Controls.ComboBox)quCtrl.Template.FindName("cbxAcquiree", quCtrl);
            //if (cbx != null)
            //    cbx.Items.Refresh();

            //this works, but cbx.ItemsSource == null
            IEnumerable<System.Windows.Controls.ComboBox> cbxs = ControlUtils.FindVisualChildren<System.Windows.Controls.ComboBox>(this);
            foreach (System.Windows.Controls.ComboBox cbx in cbxs)
            {
                if (cbx.Name == "cbxAcquiree")
                    cbx.Items.Refresh();
            }
        }

        private void DoSave()
        {
            if (!string.IsNullOrEmpty(CurrentSave2File))
            {

                try
                {
                    BGU.DRPL.SignificantOwnership.Utility.Tools.WriteXML(DataSource, CurrentSave2File);
                }
                catch (Exception exc)
                {
                    System.Windows.MessageBox.Show(string.Format("Не вдалося зберегти файл '{0}', деталі - '{1}'", _saveFileDgl.FileName, exc.Message), "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                DoSaveAs();
        }

        private void DoSaveAs()
        {
            _saveFileDgl.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            _saveFileDgl.FilterIndex = 0;

            if (!string.IsNullOrEmpty(CurrentSave2File))
                _saveFileDgl.FileName = CurrentSave2File;
            else if (DataSource != null && DataSource is IQuestionnaire)
                _saveFileDgl.FileName = ((IQuestionnaire)DataSource).SuggestSaveAsFileName();

            bool? userClickedOK = _saveFileDgl.ShowDialog();

            // Process input if the user clicked OK.
            if ((bool)userClickedOK)
            {
                // Open the selected file to read.
                try
                {
                    BGU.DRPL.SignificantOwnership.Utility.Tools.WriteXML(DataSource, _saveFileDgl.FileName);
                    CurrentSave2File = _saveFileDgl.FileName;
                }
                catch (Exception exc)
                {
                    System.Windows.MessageBox.Show(string.Format("Не вдалося зберегти файл '{0}', деталі - '{1}'", _saveFileDgl.FileName, exc.Message), "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            InventorizeControlsTags();
            ApplyShowOrHideControlsBasedOnTags();
        }

        private void InventorizeControlsTags()
        {
            _controlsByTag = new Dictionary<string, List<DependencyObject>>();
        }

        private void KeyUp_Grid(object sender, EventArgs e)
        {
            int i = 0;
        }

    }
}
