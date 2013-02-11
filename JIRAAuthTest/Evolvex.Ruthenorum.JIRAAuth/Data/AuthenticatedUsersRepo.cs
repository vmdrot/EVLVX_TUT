using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;

namespace Evolvex.Ruthenorum.JIRAAuth.Data
{
    public class AuthenticatedUsersRepo : IAuthenticatedUsersRepo
    {
        public AuthenticatedUsersRepo()
        {
            this.Users = new Dictionary<string, IJIRAUserInfo>();
        }
        
        #region IAuthenticatedUsersRepo Members

        public Dictionary<string, IJIRAUserInfo> Users
        {
            get;
            private set;
        }

        #endregion
    }
}
