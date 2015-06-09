using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication2.Commands
{
    public class StackPanelWindowCommand : System.Windows.Input.ICommand
    {
        public bool CanExecute(object parameter)
        {
            //e.CanExecute = true;
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            StackPanelWindow wnd = new StackPanelWindow();
            wnd.ShowDialog();
        }
    }
}
