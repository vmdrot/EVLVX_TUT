using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIRAAuthTest
{
    class Program
    {
        
        public delegate void CmdHandler(string[] args);
        private static Dictionary<string, CmdHandler> _cmdHandlers;

        static Program()
        {
            _cmdHandlers = new Dictionary<string, CmdHandler>();
            _cmdHandlers.Add("authenticateuser", AuthenticateUser);
            _cmdHandlers.Add("listgroups", ListGroups);
            

        }
        static void Main(string[] args)
        {
            //Console.Read();
            CmdHandler hndlr;
            if (_cmdHandlers.TryGetValue(args[0].ToLower(), out hndlr))
                hndlr(args);
            else
                AuthenticateUser(args);
        }

        private static void AuthenticateUser(string[] args)
        {
            String jiraRootUrl = args[1];
            string usr = args[2];
            string pwd = args[3];
            string resp;
            JiraAuthenticatorProto auth = new JiraAuthenticatorProto();
            auth.JIRARootUrl = jiraRootUrl;
            bool rslt = auth.Authenticate(usr, pwd, out resp);
            Console.WriteLine("{0} ({1})\n{2}", rslt, auth.LastStatus, resp);
        }

        private static void ListGroups(string[] args)
        {
            String jiraRootUrl = args[1];
            string usr = args[2];
            string pwd = args[3];
            int limitTo = int.Parse(args[4]);
            string resp;
            JiraAuthenticatorProto auth = new JiraAuthenticatorProto();
            auth.JIRARootUrl = jiraRootUrl;
            bool rslt = auth.ListGroups(usr, pwd, out resp, limitTo);
            Console.WriteLine("{0} ({1})\n{2}", rslt, auth.LastStatus, resp);
        }
    }
}
