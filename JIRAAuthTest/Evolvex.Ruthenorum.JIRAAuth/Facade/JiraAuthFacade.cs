using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using Evolvex.Ruthenorum.JIRAAuth.Factories;

namespace Evolvex.Ruthenorum.JIRAAuth.Facade
{
    public class JiraAuthFacade
    {
        public static List<IJIRAUserInfo> ListNewUsers()
        {
            List<IJIRAUserInfo> rslt = new List<IJIRAUserInfo>();
            List<string> allUsrs = JIRAAuthenticatorFactory.Instance.Authenticator.ListAllUserNames();
            foreach (String usr in allUsrs)
            {
                IJIRAUserInfo jui = JIRAAuthenticatorFactory.Instance.Authenticator.GetUser(usr);

                if (jui.groups == null || jui.groups.Count < 2)
                    rslt.Add(jui);
            }
            return rslt;
        }
    }
}
