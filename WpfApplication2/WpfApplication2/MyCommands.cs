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
        private static readonly RoutedUICommand _addMentionedPersonCommand = new RoutedUICommand("description", "AddMentionedPersonCommand", typeof(MyCommands));
        private static readonly RoutedUICommand _reOpenSelectedObjectFormCommand = new RoutedUICommand("description", "ReOpenSelectedObjectFormCommand", typeof(MyCommands));
        private static readonly RoutedUICommand _addBankCommand = new RoutedUICommand("description", "AddBankCommand", typeof(MyCommands));
        private static readonly RoutedUICommand _addStockExchangeCommand = new RoutedUICommand("description", "AddStockExchangeCommand", typeof(MyCommands));

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

        public static RoutedUICommand AddMentionedPersonCommand
        {
            get
            {
                return _addMentionedPersonCommand;
            }
        }


        public static RoutedUICommand ReOpenSelectedObjectFormCommand
        {
            get
            {
                return _reOpenSelectedObjectFormCommand;
            }
        }

        public static RoutedUICommand AddBankCommand
        {
            get
            {
                return _addBankCommand;
            }
        }

        public static RoutedUICommand AddStockExchangeCommand
        {
            get
            {
                return _addStockExchangeCommand;
            }
        }

    }
}
