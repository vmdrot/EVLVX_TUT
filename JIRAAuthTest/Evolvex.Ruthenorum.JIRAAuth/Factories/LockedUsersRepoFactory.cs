using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using Evolvex.Ruthenorum.JIRAAuth.Data;

namespace Evolvex.Ruthenorum.JIRAAuth.Factories
{
    public class LockedUsersRepoFactory
    {

        #region singleton
        private LockedUsersRepoFactory()
        { }

        private static LockedUsersRepoFactory _instance;
        public static LockedUsersRepoFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LockedUsersRepoFactory();
                return _instance;
            }
        }
        #endregion

        private ILockedUsersRepo _lockedUsers;
        public ILockedUsersRepo LockedUsers
        {
            get 
            {
                if (_lockedUsers == null)
                    _lockedUsers = new LockedUsersRepo();
                return _lockedUsers;
            }
        }

    }
}
