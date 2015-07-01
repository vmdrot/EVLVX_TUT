using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using Evolvex.Utility.Core.Common;
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

        private static readonly ILog log = Logging.GetLogger(typeof(GenericPersonIDControl));

        //public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(GenericPersonID), typeof(GenericPersonIDControl));
        //public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(GenericPersonID), typeof(GenericPersonIDControl), null);
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void SetValueDp(DependencyProperty property, object value, 
        //    [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        //{
        //    SetValue(property, value);
        //    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(p));
        //}

        
        
        public GenericPersonIDControl()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            cbx.ItemsSource = DataModule.CurrentMentionedIdentities;
            cbx.DataContext = this.DataContext;
            log.Debug("cctor: DataContext = {0}", this.DataContext);
            
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
                log.Debug("get_SelectedValue: {0}", (GenericPersonID)cbx.SelectedValue);
                //if(cbx.SelectedItem != null && cbx.SelectedItem is GenericPersonInfo)
                    return ((GenericPersonInfo)cbx.SelectedItem).ID;
                //return GenericPersonID.Empty;
                //return (GenericPersonID)GetValue(SelectedValueProperty);
                //return (GenericPersonID)cbx.SelectedValue;
            }

            set
            {
                log.Debug("set_SelectedValue: old = {0}, new = {1}", (GenericPersonID)cbx.SelectedValue, value);
                //SetValueDp(SelectedValueProperty, value);
                cbx.SelectedValue = value;
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
            //SetSelectedValue(newGpi.ID);
            //cbx.SetCurrentValue(SelectedValueProperty, newGpi.ID); //doesn't work either
            this.SelectedValue = newGpi.ID;
            cbx.Items.Refresh();
            //cbx.SelectedValue = newGpi.ID;
        }

        private static void OnSelectedValuePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            log.Debug("OnSelectedValuePropertyChanged");
            ComboBox control = source as ComboBox;
            GenericPersonID val = (GenericPersonID)e.NewValue;
        }

    }
}
