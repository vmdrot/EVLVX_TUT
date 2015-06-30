﻿using BGU.DRPL.SignificantOwnership.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApplication2.Forms;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            var binding = new CommandBinding(MyCommands.DoSomethingCommand, DoSomething, CanDoSomething);
            var addRowBinding = new CommandBinding(MyCommands.AddNewRowCommand, AddNewRow, CanAddNewRow);
            var editRowBinding = new CommandBinding(MyCommands.EditRowCommand, EditRow, CanEditRow);
            var deleteRowBinding = new CommandBinding(MyCommands.DeleteRowCommand, DeleteRow, CanDeleteRow);
            var addMentionedPersonBinding = new CommandBinding(MyCommands.AddMentionedPersonCommand, AddMentionedPerson, CanAddMentionedPerson);

            // Register CommandBinding for all windows.
            CommandManager.RegisterClassCommandBinding(typeof(Window), binding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), addRowBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), editRowBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), deleteRowBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), addMentionedPersonBinding);

        }

        #region DeleteRow
        private void DeleteRow(object sender, ExecutedRoutedEventArgs e)
        {
            object[] prms = (object[])e.Parameter;
            if (prms == null || prms.Length == 0)
            {
                e.Handled = true;
                return;
            }
            object di = prms[0];
            object diIdx = prms.Length >= 2 ? prms[1] : null;
            object dg = prms.Length >= 3 ? prms[2] : null;

            if (System.Windows.MessageBox.Show(String.Format("Do you really want to delete row #{0} ({1})", (int)diIdx, di), "Confirm delete", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;
            if (dg != null && dg is System.Windows.Controls.DataGrid)
            {
                object ds = ((System.Windows.Controls.DataGrid)dg).ItemsSource;
                if (ds.GetType().IsGenericType)
                {
                    ds.GetType().GetMethod("Remove").Invoke(ds, new[] { di });
                    ((System.Windows.Controls.DataGrid)dg).Items.Refresh();

                }
            }

            e.Handled = true;
        }

        private void CanDeleteRow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
            return;
        }
        #endregion

        #region EditRow
        private void EditRow(object sender, ExecutedRoutedEventArgs e)
        {
            object[] prms = (object[])e.Parameter;
            if (prms == null || prms.Length == 0)
            {
                e.Handled = true;
                return;
            }
            object di = prms[0];
            object dg = prms.Length >= 2 ? prms[1] : null;

            SelectedObjectFrm frm = new SelectedObjectFrm();
            frm.DataSource = di;
            bool? hr = frm.ShowDialog();
            if (hr != null && (bool)hr)
            {
                //if (dg != null && dg is System.Windows.Controls.DataGrid)
                //    ((System.Windows.Controls.DataGrid)dg).Items;
            }

            e.Handled = true;
        }

        private void CanEditRow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
            return;
        }

        #endregion

        #region AddNewRow
        private void CanAddNewRow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddNewRow(object sender, ExecutedRoutedEventArgs e)
        {
            object[] prms = (object[])e.Parameter;
            if (prms == null || prms.Length == 0)
            {
                e.Handled = true;
                return;
            }
            object ds = prms[0];
            object dataContext = prms.Length >= 2 ? prms[1] : null;
            object list = prms.Length >= 3 ? prms[2] : null;
            object dg = prms.Length >= 4 ? prms[3] : null;

            if (!list.GetType().IsGenericType)
            {
                e.Handled = true;
                return;
            }

            object newItem = null;
            Type[] gnrcArgs = list.GetType().GetGenericArguments();
            if (gnrcArgs != null && gnrcArgs.Length > 0)
            {
                newItem = ReflectionUtil.InstantiateObject(gnrcArgs[0]);
                ReflectionUtil.InstantiateAllProps(newItem, newItem.GetType().Assembly);
            }
            if (newItem == null)
            {
                e.Handled = true;
                return;
            }
            SelectedObjectFrm frm = new SelectedObjectFrm();
            frm.DataSource = newItem;
            bool? hr = frm.ShowDialog();
            if (hr != null && (bool)hr)
            {
                list.GetType().GetMethod("Add").Invoke(list, new[] { frm.DataSource });
                //;// e.Parameter = frm.DataSource; //todo
                if (dg != null && dg is System.Windows.Controls.DataGrid)
                    ((System.Windows.Controls.DataGrid)dg).Items.Refresh();
            }

            e.Handled = true;
        }

        #endregion

        #region
        private void CanAddMentionedPerson(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddMentionedPerson(object sender, ExecutedRoutedEventArgs e)
        {
            object[] prms = (object[])e.Parameter;
            if (prms == null || prms.Length == 0)
            {
                e.Handled = true;
                return;
            }
            object dataContext = prms[0];

            e.Handled = true;
        }
        #endregion


        #region DoSomething
        private void DoSomething(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show(string.Format("Doing something with {0}", e.Parameter));

            SelectedObjectFrm frm = new SelectedObjectFrm();
            if (frm.DialogResult != null && (bool)frm.DialogResult)
                ;// e.Parameter = frm.DataSource; //todo
            e.Handled = true;
        }

        private void CanDoSomething(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion
    }
}
