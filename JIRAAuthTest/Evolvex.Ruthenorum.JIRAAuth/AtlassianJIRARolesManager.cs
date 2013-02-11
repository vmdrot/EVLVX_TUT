using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Evolvex.Ruthenorum.JIRAAuth.Factories;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;

namespace Evolvex.Ruthenorum.JIRAAuth
{
    public class AtlassianJIRARolesManager : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            //throw new NotImplementedException(); // n/a
        }

        private string _applicationName;
        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override void CreateRole(string roleName)
        {
            //throw new NotImplementedException(); // n/a
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return false;// n/a
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            //http://pm.ruthenorum.info/jira/rest/api/2/user/search?username=v
            //see userSearch_v.txt for the responses
            return new string[] { };//todo
        }

        public override string[] GetAllRoles()
        {
            return JIRAAuthenticatorFactory.Instance.Authenticator.ListGroups().ToArray(); //todo
        }

        public override string[] GetRolesForUser(string username)
        {
            return JIRAAuthenticatorFactory.Instance.Authenticator.GetUser(username).groups.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            //closest to what we're after is D:\home\vmdrot\DEV\_tut\JIRAAuthTest\SampleResponses\multiProjectSearch.txt
            //obtained by http://pm.ruthenorum.info/jira/rest/api/2/user/assignable/multiProjectSearch?projectKeys=PSMETHDLGY,JIRATRNSL
            //throw new NotImplementedException();
            return ExtractUsersByGroup(AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users.Values, roleName);
        }

        private string[] ExtractUsersByGroup(Dictionary<string, IJIRAUserInfo>.ValueCollection users, string roleName)
        {
            List<String> rslt = new List<string>();
            foreach (IJIRAUserInfo usr in users)
            {
                if (usr.groups.Contains(roleName))
                    rslt.Add(usr.name);
            }
            return rslt.ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return JIRAAuthenticatorFactory.Instance.Authenticator.GetUser(username).groups.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            //throw new NotImplementedException(); // n/a
        }

        public override bool RoleExists(string roleName)
        {
            return JIRAAuthenticatorFactory.Instance.Authenticator.ListGroups().Contains(roleName);
        }
    }
}
