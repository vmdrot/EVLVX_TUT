using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces
{
    public interface IJIRAAuthenticator
    {
        bool Authenticate(string usr, string pwd);
        List<string> ListGroups();
        IJIRAUserInfo GetUser(string userName);
        String JIRARootUrl { get; set; }
        HttpStatusCode? LastStatus { get; }
        string LastResponseText { get; }
        List<string> ListAllUserNames();

    }
}
