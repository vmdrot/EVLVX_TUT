using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2.Controls
{
    /// <summary>
    /// Логика взаимодействия для SimpleDropDownControl2.xaml
    /// </summary>
    public partial class SimpleDropDownControl2 : UserControl
    {
        public SimpleDropDownControl2()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        public string DisplayName
        {
            get { return lblDisplayName.Content as string; }
            set { lblDisplayName.Content = value; }
        }

        [Browsable(true)]
        public string Description 
        {
            get { return lblDescription.Content as string; }
            set { lblDescription.Content = value; }
        }

        public IEnumerable ListSource 
        {
            get { return cbx.ItemsSource; }
            set { cbx.ItemsSource = value; }
        }

        [Browsable(true)]
        public String DisplayMember { get { return cbx.DisplayMemberPath; } set { cbx.DisplayMemberPath = value; } }
        
        [Browsable(true)]
        public String ValueMember { get { return cbx.SelectedValuePath; } set { cbx.SelectedValuePath = value; } }

        public object SelectedValue 
        {
            get { return cbx.SelectedValue; }
            set { cbx.SelectedValue = value; }
        }
    }
}
