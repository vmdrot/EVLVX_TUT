﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication2
{
    public class MyCommands
    {
        private static readonly RoutedUICommand _doSomethingCommand = new RoutedUICommand("description", "DoSomethingCommand", typeof(MyCommands));
        private static readonly RoutedUICommand _addNewRowCommand = new RoutedUICommand("description", "AddNewRowCommand", typeof(MyCommands));
        private static readonly RoutedUICommand _editRowCommand = new RoutedUICommand("description", "EditRowCommand", typeof(MyCommands));
        private static readonly RoutedUICommand _deleteRowCommand = new RoutedUICommand("description", "DeleteRowCommand", typeof(MyCommands));
        
        public static RoutedUICommand DoSomethingCommand
        {
            get
            {
                return _doSomethingCommand;
            }
        }
        public static RoutedUICommand AddNewRowCommand
        {
            get
            {
                return _addNewRowCommand;
            }
        }
        public static RoutedUICommand EditRowCommand
        {
            get
            {
                return _editRowCommand;
            }
        }
        public static RoutedUICommand DeleteRowCommand
        {
            get
            {
                return _deleteRowCommand;
            }
        }

    }
}
