using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;

namespace Evolvex.Ruthenorum.JIRAAuth.Data
{
    public class LockedUsersRepo : ILockedUsersRepo
    {
        public LockedUsersRepo()
        {
            this.Users = new List<string>();
        }
        #region ILockedUsersRepo Members


        public List<string> Users
        {
            get;
            private set;
        }

        #endregion
    }
}
