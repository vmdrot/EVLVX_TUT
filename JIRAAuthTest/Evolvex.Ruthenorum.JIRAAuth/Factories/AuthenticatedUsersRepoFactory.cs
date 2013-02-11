using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using Evolvex.Ruthenorum.JIRAAuth.Data;

namespace Evolvex.Ruthenorum.JIRAAuth.Factories
{
    public class AuthenticatedUsersRepoFactory
    {

        #region singleton
        private AuthenticatedUsersRepoFactory() { }

        private static AuthenticatedUsersRepoFactory _instance;
        public static AuthenticatedUsersRepoFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AuthenticatedUsersRepoFactory();
                }
                return _instance;

            }
        }
        #endregion

        private IAuthenticatedUsersRepo _authenticatedUsers;

        public IAuthenticatedUsersRepo AuthenticatedUsers
        {
            get
            {
                if (_authenticatedUsers == null)
                    _authenticatedUsers = new AuthenticatedUsersRepo();
                return _authenticatedUsers;
            }
        }

    }
}
