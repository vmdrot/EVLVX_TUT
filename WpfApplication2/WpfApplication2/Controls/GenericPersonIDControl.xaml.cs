using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication2.Data;
using WpfApplication2.Forms;

namespace WpfApplication2.Controls
{
    /// <summary>
    /// Логика взаимодействия для GenericPersonIDControl.xaml
    /// </summary>
    public partial class GenericPersonIDControl : UserControl
    {

        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(GenericPersonID), typeof(GenericPersonIDControl));

        
        
        public GenericPersonIDControl()
        {
            InitializeComponent();
            cbx.ItemsSource = DataModule.CurrentMentionedIdentities;
        }

        [Browsable(true)]
        public string DescriptionLabelText { get; set; }

        [Browsable(true)]
        public string DisplayNameLabelText { get; set; }

        [Browsable(true)]
        public GenericPersonID SelectedValue
        {
            get
            {
                //if(cbx.SelectedItem != null && cbx.SelectedItem is GenericPersonInfo)
                //    return ((GenericPersonInfo)cbx.SelectedItem).ID;
                //return GenericPersonID.Empty;
                return (GenericPersonID)cbx.SelectedValue;
            }

            set
            {
                cbx.SelectedValue = value;
                //foreach (var item in cbx.Items)
                //{
                //    if (!(item is GenericPersonInfo))
                //        continue;
                //    if(((GenericPersonInfo)item).ID == value)
                //        cbx.Sele
                //}
            }
        }



        private void SetSelectedValue(GenericPersonID val)
        {
            for (int i = 0; i < cbx.Items.Count; i++)
            {
                if (cbx.Items[i] == null || !(cbx.Items[i] is GenericPersonInfo))
                    continue;
                if (((GenericPersonInfo)cbx.Items[i]).ID != val)
                    continue;
                cbx.SelectedIndex = i;
                break;
            }
        }

        protected void btnClick(object sender, RoutedEventArgs e)
        {
            GenericPersonInfo gpi = new GenericPersonInfo();

            BGU.DRPL.SignificantOwnership.Utility.ReflectionUtil.InstantiateAllProps(gpi, gpi.GetType().Assembly);
            SelectedObjectFrm frm = new SelectedObjectFrm();
            frm.DataSource = gpi;
            frm.Title = "Нова особа";
            bool? dlgRes = frm.ShowDialog();
            if (dlgRes == null || (bool)dlgRes != true)
                return;

            GenericPersonInfo newGpi = (GenericPersonInfo)frm.DataSource;
            DataModule.CurrentMentionedIdentities.Add(newGpi);
            cbx.Items.Refresh();
            SetSelectedValue(newGpi.ID);
            //cbx.SetCurrentValue(SelectedValueProperty, newGpi.ID); //doesn't work either
            //this.SelectedValue = newGpi.ID;
            //cbx.Items.Refresh();
            //cbx.SelectedValue = newGpi.ID;
        }

    }
}
