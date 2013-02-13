using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Evolvex.Ruthenorum.JIRAAuth.Data;
using System.Net;
using System.Configuration;
using Evolvex.Ruthenorum.JIRAAuth.Factories;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using System.IO;

namespace Evolvex.Ruthenorum.JIRAAuth
{
    public class AtlassianJIRAMembershipProvider : System.Web.Security.MembershipProvider
    {
        private static readonly Evolvex.Ruthenorum.Core.Interfaces.ILog log = Evolvex.Ruthenorum.Core.Logging.GetLogger(typeof(AtlassianJIRAMembershipProvider));
        #region inner type(s)
        #endregion


        #region field(s)
        private object _jiraAuthenticatorLocObj = new object();
        private string _applicationName;
        //private Dictionary<string, JIRAUserInfo> _authenticatedUsers;
        //private List<string> _lockedUsers;
        #endregion

        #region cctor(s)
        static AtlassianJIRAMembershipProvider()
        {
            
        }

        public AtlassianJIRAMembershipProvider()
        {
            //_authenticatedUsers = new Dictionary<string, JIRAUserInfo>();
            //_lockedUsers = new List<string>();
        }
        #endregion

        #region MembershipProvider member(s)
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            //log.Debug("ChangePassword('{0}', '{1}', '{2}')", username, oldPassword, newPassword);
            //LogDebug("ChangePassword('{0}', '{1}', '{2}')", username, oldPassword, newPassword);
            //throw new NotImplementedException();
            return false;
        }

        private void LogDebug(string fmt, params object[] args)
        {
            File.WriteAllText("jiraauth.log", string.Format(fmt+"\n\r", args));
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            //log.Debug("ChangePasswordQuestionAndAnswer('{0}', '{1}', '{2}', '{3}')", username, password, newPasswordQuestion, newPasswordAnswer);
            //LogDebug("ChangePasswordQuestionAndAnswer('{0}', '{1}', '{2}', '{3}')", username, password, newPasswordQuestion, newPasswordAnswer);
            //throw new NotImplementedException();
            return false;
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            //log.Debug("CreateUser('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}')", username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey);
            //LogDebug("CreateUser('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}')", username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey);
            //throw new NotImplementedException();
            status = System.Web.Security.MembershipCreateStatus.ProviderError;
            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            //log.Debug("DeleteUser('{0}', '{1}')", username, deleteAllRelatedData);
            //LogDebug("DeleteUser('{0}', '{1}')", username, deleteAllRelatedData);
            //throw new NotImplementedException();
            return false;
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            //log.Debug("FindUsersByEmail('{0}', {1}, {2})", emailToMatch, pageIndex, pageSize);
            //LogDebug("FindUsersByEmail('{0}', {1}, {2})", emailToMatch, pageIndex, pageSize);
            //throw new NotImplementedException();
            totalRecords = 0;
            return null; //todo
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            //log.Debug("FindUsersByName('{0}', {1}, {2})", usernameToMatch, pageIndex, pageSize);
            //LogDebug("FindUsersByName('{0}', {1}, {2})", usernameToMatch, pageIndex, pageSize);
            totalRecords = 0;
            MembershipUserCollection rslt = new MembershipUserCollection();
            MembershipUser usr = this.GetUser(usernameToMatch, false);
            if (usr != null)
            {
                rslt.Add(usr);
                totalRecords++;
            }
            return rslt;
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            //log.Debug("GetAllUsers({0}, {1})",  pageIndex, pageSize);
            //LogDebug("GetAllUsers({0}, {1})", pageIndex, pageSize);

            totalRecords = 0;
            return null; //hardly
        }

        public override int GetNumberOfUsersOnline()
        {
            //log.Debug("GetNumberOfUsersOnline()");
            //LogDebug("GetNumberOfUsersOnline()");
            //throw new NotImplementedException();
            return AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users.Count; //hardly
        }

        public override string GetPassword(string username, string answer)
        {
            //log.Debug("GetAllUsers('{0}', '{1}')", username, answer);
            //LogDebug("GetAllUsers('{0}', '{1}')", username, answer);
            //throw new NotImplementedException();
            return null; //hardly
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            //log.Debug("GetUser('{0}', {1})", username, userIsOnline);
            //LogDebug("GetUser('{0}', {1})", username, userIsOnline);
            if (AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users.ContainsKey(username))
            {
                IJIRAUserInfo jui = AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users[username];
                return GetMembershipUserFromJIRAUserInfo(jui);
            }
            else
            {
                IJIRAUserInfo jui = JIRAAuthenticatorFactory.Instance.Authenticator.GetUser(username);
                if (jui != null)
                {
                    AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users.Add(jui.name, jui);
                    return GetMembershipUserFromJIRAUserInfo(jui);
                }
            }
            return null;
        }

        private MembershipUser GetMembershipUserFromJIRAUserInfo(IJIRAUserInfo jui)
        {
            MembershipUser rslt = new MembershipUser(this.Name, jui.name, jui.self, jui.email, string.Empty, jui.displayName, jui.active, LockedUsersRepoFactory.Instance.LockedUsers.Users.Contains(jui.name.ToLower()), DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            return rslt;
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            //log.Debug("GetUser('{0}', {1})", providerUserKey, userIsOnline);
            //LogDebug("GetUser('{0}', {1})", providerUserKey, userIsOnline);
            IJIRAUserInfo jui = FindJIRAUserByKey(providerUserKey as string);
            if(jui != null)
                return GetMembershipUserFromJIRAUserInfo(jui);
            jui = JIRAAuthenticatorFactory.Instance.Authenticator.GetUser(JIRAUserInfo.ParseUserNameFromKey(providerUserKey as string));
            if (jui != null)
            {
                AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users.Add(jui.name, jui);
                return GetMembershipUserFromJIRAUserInfo(jui);
            }
            return null;
        }

        private JIRAUserInfo FindJIRAUserByKey(string self)
        {
            foreach (JIRAUserInfo jui in AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users.Values)
            {
                if (jui.self == self)
                    return jui;
            }
            return null;
        }

        public override string GetUserNameByEmail(string email)
        {
            //log.Debug("GetUserNameByEmail('{0}')", email);
            //LogDebug("GetUserNameByEmail('{0}')", email);
            //throw new NotImplementedException();
            return null; //hardly
        }

        public override int MaxInvalidPasswordAttempts
        {
            get 
            {
                //log.Debug("get_MaxInvalidPasswordAttempts");
                //throw new NotImplementedException(); 
                return 2; //todo - http://pm.ruthenorum.info/jira/browse/TV-33
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get 
            {
                //log.Debug("get_MinRequiredNonAlphanumericCharacters");
                return 0;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get 
            {
                //log.Debug("get_MinRequiredPasswordLength");
                return 4; //not sure
            }
        }

        public override int PasswordAttemptWindow
        {
            get 
            {
                //log.Debug("get_PasswordAttemptWindow");
                return 1;
            }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get 
            {
                //log.Debug("get_PasswordFormat");
                return MembershipPasswordFormat.Clear; //not sure
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get 
            {
                //log.Debug("get_PasswordStrengthRegularExpression");
                return ".+";
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get 
            {
                //log.Debug("get_RequiresQuestionAndAnswer");
                return false; 
            }
        }

        public override bool RequiresUniqueEmail
        {
            get 
            {
                //log.Debug("get_RequiresUniqueEmail");
                return true; 
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            //log.Debug("ResetPassword('{0}', '{1}')", username, answer);
            //LogDebug("ResetPassword('{0}', '{1}')", username, answer);
            //throw new NotImplementedException();
            return null; //hardly
        }

        public override bool UnlockUser(string userName)
        {
            //log.Debug("UnlockUser('{0}')", userName);
            //LogDebug("UnlockUser('{0}')", userName);
            //throw new NotImplementedException();
            return false; //only manually
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            //log.Debug("UpdateUser('{0}')", user.ProviderUserKey);
            //LogDebug("UpdateUser('{0}')", user.ProviderUserKey);
            //throw new NotImplementedException();
            return;//only manually
        }

        public override bool ValidateUser(string username, string password)
        {
            //log.Debug("ValidateUser('{0}', '{1}')", username, password);
            //LogDebug("ValidateUser('{0}', '{1}')", username, password);
            bool rslt = false;
            string json = null;
            HttpStatusCode? httpStatus = null;
            lock (this._jiraAuthenticatorLocObj)
            {
                rslt = JIRAAuthenticatorFactory.Instance.Authenticator.Authenticate(username, password);
                if (rslt)
                {
                    json = JIRAAuthenticatorFactory.Instance.Authenticator.LastResponseText;
                }
                httpStatus = JIRAAuthenticatorFactory.Instance.Authenticator.LastStatus;
            }
            if (rslt)
            {
                if (LockedUsersRepoFactory.Instance.LockedUsers.Users.Contains(username.ToLower()))
                    LockedUsersRepoFactory.Instance.LockedUsers.Users.Remove(username.ToLower());

                JIRAUserInfo jui = JIRAUserInfo.Parse(json);
                if (AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users.ContainsKey(jui.name))
                    AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users[jui.name] = jui;
                else
                    AuthenticatedUsersRepoFactory.Instance.AuthenticatedUsers.Users.Add(jui.name, jui);
            }
            else
            {
                if (httpStatus != null && (HttpStatusCode)httpStatus == HttpStatusCode.Forbidden)
                {
                    if (!LockedUsersRepoFactory.Instance.LockedUsers.Users.Contains(username.ToLower()))
                        LockedUsersRepoFactory.Instance.LockedUsers.Users.Add(username.ToLower());
                }
            }
            return rslt;

        }
        #endregion
    }
}
