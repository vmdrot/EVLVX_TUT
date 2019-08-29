using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAdBrowser
{
    public class ADConnectParameters
    {
        public string Domain { get; set; }
        public string Root { get; set; }
        public string Usr { get; set; }
        public string Pwd { get; set; }

        
        public static ADConnectParameters Default
        {
            get
            {
                return new ADConnectParameters() { Root = "rootDSE" };
            }
        }
    }
}
