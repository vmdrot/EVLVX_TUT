using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces
{
    public interface IJIRAAuthenticator
    {
        bool Authenticate(string usr, string pwd);
        List<string> ListGroups();
        IJIRAUserInfo GetUser(string userName);
    }
}
