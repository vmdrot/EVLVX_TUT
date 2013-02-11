using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;

namespace Evolvex.Ruthenorum.JIRAAuth.Factories
{
    public class JIRAAuthenticatorFactory
    {
        #region singleton
        private static JIRAAuthenticatorFactory _instance;

        public static JIRAAuthenticatorFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JIRAAuthenticatorFactory();
                }
                return _instance;
            }
        }
        #endregion

        private JiraAuthenticator _authenticator;
        public IJIRAAuthenticator Authenticator
        {
            get
            {
                if (_authenticator == null)
                {
                    _authenticator = new JiraAuthenticator();
                    _authenticator.JIRARootUrl = "todo";
                }
                return _authenticator;
            }
        }


        private JIRAAuthenticatorFactory()
        {
        }
    }
}
