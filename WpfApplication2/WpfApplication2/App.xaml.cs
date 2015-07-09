using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApplication2.Data;
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
            var reOpenSelectedObjectFormBinding = new CommandBinding(MyCommands.ReOpenSelectedObjectFormCommand, ReOpenSelectedObjectForm, CanReOpenSelectedObjectForm);
            var addBankBinding = new CommandBinding(MyCommands.AddBankCommand, AddBank, CanAddBank);
            var addStockExchangeBinding = new CommandBinding(MyCommands.AddStockExchangeCommand, AddStockExchange, CanAddStockExchange);
            

            // Register CommandBinding for all windows.
            CommandManager.RegisterClassCommandBinding(typeof(Window), binding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), addRowBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), editRowBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), deleteRowBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), addMentionedPersonBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), reOpenSelectedObjectFormBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), addBankBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), addStockExchangeBinding);
        }

        private void CanAddStockExchange(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddStockExchange(object sender, ExecutedRoutedEventArgs e)
        {
            object[] prms = (object[])e.Parameter;
            if (prms == null || prms.Length == 0)
            {
                e.Handled = true;
                return;
            }
            object oCbx = prms[0];
            if (oCbx == null || !(oCbx is System.Windows.Controls.ComboBox))
            {
                e.Handled = true;
                return;
            }

            {
                System.Windows.Controls.ComboBox cbx = (System.Windows.Controls.ComboBox)oCbx;
                StockExchangeInfo bi = new StockExchangeInfo();

                BGU.DRPL.SignificantOwnership.Utility.ReflectionUtil.InstantiateAllProps(bi, bi.GetType().Assembly);
                SelectedObjectFrm frm = new SelectedObjectFrm();
                frm.DataSource = bi;
                frm.Title = "Нова фондова біржа";
                bool? dlgRes = frm.ShowDialog();
                if (dlgRes == null || (bool)dlgRes != true)
                    return;

                StockExchangeInfo newBourse = (StockExchangeInfo)frm.DataSource;
                StockExchangeInfo.AllWFExchanges.Add(newBourse);
                cbx.Items.Refresh();
                //SetSelectedValue(newGpi.ID);
                //cbx.SetCurrentValue(SelectedValueProperty, newGpi.ID); //doesn't work either
                cbx.SelectedItem = newBourse;
                cbx.Items.Refresh();
            }
            e.Handled = true;
        }

        private void CanAddBank(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddBank(object sender, ExecutedRoutedEventArgs e)
        {
            object[] prms = (object[])e.Parameter;
            if (prms == null || prms.Length == 0)
            {
                e.Handled = true;
                return;
            }
            object oCbx = prms[0];
            if (oCbx == null || !(oCbx is System.Windows.Controls.ComboBox))
            {
                e.Handled = true;
                return;
            }

            {
                System.Windows.Controls.ComboBox cbx = (System.Windows.Controls.ComboBox)oCbx;
                BankInfo bi = new BankInfo();

                BGU.DRPL.SignificantOwnership.Utility.ReflectionUtil.InstantiateAllProps(bi, bi.GetType().Assembly);
                SelectedObjectFrm frm = new SelectedObjectFrm();
                frm.DataSource = bi;
                frm.Title = "Новий банк";
                bool? dlgRes = frm.ShowDialog();
                if (dlgRes == null || (bool)dlgRes != true)
                    return;

                BankInfo newBk = (BankInfo)frm.DataSource;
                DataModule.СurrentBanks.Add(newBk);
                cbx.Items.Refresh();
                //SetSelectedValue(newGpi.ID);
                //cbx.SetCurrentValue(SelectedValueProperty, newGpi.ID); //doesn't work either
                cbx.SelectedItem = newBk;
                cbx.Items.Refresh();
            }
            e.Handled = true;
        }

        private void CanReOpenSelectedObjectForm(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ReOpenSelectedObjectForm(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
            {
                e.Handled = true;
                return;
            }

            object[] args = (object[])e.Parameter;
            if (args == null || args.Length < 1)
            {
                e.Handled = true;
                return;
            }
            string frmCaption = string.Empty;
            if(args.Length > 1 && args[1] is Window)
            {
                frmCaption = ((Window)args[1]).Title;
                ((Window)args[1]).Close();
            }
            System.Windows.Forms.Cursor prevCursors = System.Windows.Forms.Cursor.Current;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            ShowQuestionnaireEditForm(frmCaption, args[0]);
            e.Handled = true;
            System.Windows.Forms.Cursor.Current = prevCursors;
            return;
        }


        private void ShowQuestionnaireEditForm(string frmCaption, object questio)
        {
            BGU.DRPL.SignificantOwnership.Utility.ReflectionUtil.InstantiateAllProps(questio, questio.GetType().Assembly);

            if (questio is IQuestionnaire)
                DataModule.CurrentQuestionnare = (IQuestionnaire)questio;
            SelectedObjectFrm frm = new SelectedObjectFrm();
            frm.DataSource = questio;
            frm.Title = frmCaption;
            frm.Focus();
            frm.ShowDialog();
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

            if (System.Windows.MessageBox.Show(String.Format("Ви певні, що хочете видалити рядок № {0} ({1})", (int)diIdx, di), "Підтвердіть видалення рядка", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) != MessageBoxResult.Yes)
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
            object propDispNm = prms.Length >= 3 ? prms[2] : null;
            object propDescr = prms.Length >= 4 ? prms[3] : null;

            SelectedObjectFrm frm = new SelectedObjectFrm();
            frm.DataSource = di;
            frm.Title = propDispNm as string;
            //frm.ToolTip = propDescr as string;
            bool? hr = frm.ShowDialog();
            if (hr != null && (bool)hr)
            {
                if (dg != null && dg is System.Windows.Controls.DataGrid)
                {
                    ((System.Windows.Controls.DataGrid)dg).Items.Refresh();
                }

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
            object propDispNm = prms.Length >= 5 ? prms[4] : null;
            object propDescr = prms.Length >= 6 ? prms[5] : null;

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
            frm.Title = propDispNm as string;
            //frm.ToolTip = propDescr as string;
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
            object oCbx = prms[0];
            if (oCbx == null || !(oCbx is System.Windows.Controls.ComboBox))
            {
                e.Handled = true;
                return;
            }

            {
                System.Windows.Controls.ComboBox cbx = (System.Windows.Controls.ComboBox)oCbx;
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
                cbx.SelectedValue = newGpi.ID;
                cbx.Items.Refresh();
            }
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
