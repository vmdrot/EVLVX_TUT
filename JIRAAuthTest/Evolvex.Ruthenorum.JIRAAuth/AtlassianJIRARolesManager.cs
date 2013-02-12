using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Evolvex.Ruthenorum.JIRAAuth.Factories;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using System.IO;

namespace Evolvex.Ruthenorum.JIRAAuth
{
    public class AtlassianJIRARolesManager : RoleProvider
    {
        private static readonly Evolvex.Ruthenorum.Core.Interfaces.ILog log = Evolvex.Ruthenorum.Core.Logging.GetLogger(typeof(AtlassianJIRARolesManager));
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            log.Debug("AddUsersToRoles({0}, {1})", usernames.Length, roleNames.Length);
            LogDebug("AddUsersToRoles({0}, {1})", usernames.Length, roleNames.Length);
            //throw new NotImplementedException(); // n/a
        }

        private string _applicationName;
        public override string ApplicationName
        {
            get
            {
                log.Debug("get_ApplicationName");
                LogDebug("get_ApplicationName");
                return _applicationName;
            }
            set
            {
                log.Debug("set_ApplicationName('{0}')", value);
                LogDebug("set_ApplicationName('{0}')", value);
                _applicationName = value;
            }
        }

        public override void CreateRole(string roleName)
        {
            log.Debug("CreateRole('{0}')", roleName);
            LogDebug("CreateRole('{0}')", roleName);
            //throw new NotImplementedException(); // n/a
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            log.Debug("DeleteRole('{0}', {1})", roleName, throwOnPopulatedRole);
            LogDebug("DeleteRole('{0}', {1})", roleName, throwOnPopulatedRole);
            return false;// n/a
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            log.Debug("FindUsersInRole('{0}', '{1}')", roleName, usernameToMatch);
            LogDebug("FindUsersInRole('{0}', '{1}')", roleName, usernameToMatch);
            //http://pm.ruthenorum.info/jira/rest/api/2/user/search?username=v
            //see userSearch_v.txt for the responses
            return new string[] { };//todo
        }

        public override string[] GetAllRoles()
        {
            log.Debug("GetAllRoles()");
            LogDebug("GetAllRoles()");
            return JIRAAuthenticatorFactory.Instance.Authenticator.ListGroups().ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            log.Debug("GetRolesForUser('{0}')", username);
            LogDebug("GetRolesForUser('{0}')", username);
            return JIRAAuthenticatorFactory.Instance.Authenticator.GetUser(username).groups.ToArray(); //todo - more safety checks
        }

        public override string[] GetUsersInRole(string roleName)
        {
            log.Debug("GetUsersInRole('{0}')", roleName);
            LogDebug("GetUsersInRole('{0}')", roleName);
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
            log.Debug("IsUserInRole('{0}', '{1}')", username, roleName);
            LogDebug("IsUserInRole('{0}', '{1}')", username, roleName);
            return JIRAAuthenticatorFactory.Instance.Authenticator.GetUser(username).groups.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            log.Debug("RemoveUsersFromRoles({0}, {1})", usernames.Length, roleNames.Length);
            LogDebug("RemoveUsersFromRoles({0}, {1})", usernames.Length, roleNames.Length);
            //throw new NotImplementedException(); // n/a
        }

        public override bool RoleExists(string roleName)
        {
            log.Debug("RoleExists('{0}')", roleName);
            LogDebug("RoleExists('{0}')", roleName);
            return JIRAAuthenticatorFactory.Instance.Authenticator.ListGroups().Contains(roleName);
        }

        private void LogDebug(string fmt, params object[] args)
        {
            File.WriteAllText("jiraauth.log", string.Format(fmt + "\n\r", args));
        }

    }
}
