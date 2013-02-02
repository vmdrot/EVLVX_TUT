using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIRAAuthTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Read();
            String jiraRootUrl = args[0];
            string usr = args[1];
            string pwd = args[2];
            string resp;
            JiraAuthenticator auth = new JiraAuthenticator();
            auth.JIRARootUrl = jiraRootUrl;
            bool rslt = auth.Authenticate(usr, pwd, out resp);
            Console.WriteLine("{0} ({1})\n{2}", rslt, auth.LastStatus, resp);
        }
    }
}
