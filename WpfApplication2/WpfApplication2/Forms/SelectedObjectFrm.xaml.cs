﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApplication2.Forms
{
    /// <summary>
    /// Логика взаимодействия для SelectedObjectFrm.xaml
    /// </summary>
    public partial class SelectedObjectFrm : Window
    {
        public SelectedObjectFrm()
        {
            InitializeComponent();
        }

        public object DataSource
        {
            get { return quCtrl.Content; }
            set { quCtrl.Content = value; }
        }

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
    }
}
