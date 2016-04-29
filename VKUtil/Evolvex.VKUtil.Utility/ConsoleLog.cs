using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtil.Utility
{
    public class ConsoleLog : ILog
    {

        private static ILog _instance;
        public static ILog Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new ConsoleLog();
                return _instance;
            }
        }
        public void Debug(string msg)
        {
            System.Console.WriteLine(msg);
        }

        public void Debug(string fmt, params object[] args)
        {
            System.Console.WriteLine(fmt, args);
        }

        public void Info(string msg)
        {
            System.Console.WriteLine(msg);
        }

        public void Info(string fmt, params object[] args)
        {
            System.Console.WriteLine(fmt, args);
        }

        public void Warn(string msg)
        {
            System.Console.WriteLine(msg);
        }

        public void Warn(string fmt, params object[] args)
        {
            System.Console.WriteLine(fmt, args);
        }

        public void Error(string msg)
        {
            System.Console.WriteLine(msg);
        }

        public void Error(string fmt, params object[] args)
        {
            System.Console.WriteLine(fmt, args);
        }

        public void Fatal(string msg)
        {
            System.Console.WriteLine(msg);
        }

        public void Fatal(string fmt, params object[] args)
        {
            System.Console.WriteLine(fmt, args);
        }
    }
}
