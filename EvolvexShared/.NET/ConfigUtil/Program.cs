using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigUtil
{
    delegate int CmdOptionHandler(string[] args);
    class Program
    {
        private static Dictionary<string, CmdOptionHandler> _cmdOptionHandlers;


        static Program()
        {
            _cmdOptionHandlers = new Dictionary<string, CmdOptionHandler>();
            InitCmdOptions();
        }

        private static void InitCmdOptions()
        {
            _cmdOptionHandlers.Add("checkisnoneauthentication", CheckIsNoneAuthentication);
            _cmdOptionHandlers.Add("toggleconfigauthenticationmode", ToggleConfigAuthenticationMode);
            
        }

        static int Main(string[] args)
        {
            //Console.Read();
            if (args.Length == 0)
                return ShowUsage();
            string sCmdLc = args[0].ToLower();
            if (!_cmdOptionHandlers.ContainsKey(sCmdLc))
                return ShowUsage();
            return _cmdOptionHandlers[sCmdLc](args);
        }

        private static int ShowUsage()
        {
            Console.WriteLine("Please provide command name to invoke, + arguments, if any required.\n See the available commands below:");
            foreach (string key in _cmdOptionHandlers.Keys)
                Console.WriteLine(key);
            Console.WriteLine();
            return 1;
        }

        #region Cmd option handlers
        private static int CheckIsNoneAuthentication(string[] args)
        {
            Console.WriteLine(AuthenticationToggler.IsAuthenticationNone(args[1]));
            return 0;
        }

        private static int ToggleConfigAuthenticationMode(string[] args)
        {
            AuthenticationToggler toggler = new AuthenticationToggler(args[1]);
            toggler.Toggle();
            return 0;
        }
        #endregion
    }
}
