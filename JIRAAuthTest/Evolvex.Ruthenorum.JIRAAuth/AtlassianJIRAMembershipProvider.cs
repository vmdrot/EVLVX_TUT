using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Evolvex.Ruthenorum.JIRAAuth.Data;
using System.Net;
using System.Configuration;

namespace Evolvex.Ruthenorum.JIRAAuth
{
    public class AtlassianJIRAMembershipProvider : System.Web.Security.MembershipProvider
    {
        #region inner type(s)
        #endregion

        #region const(s)
        public const string JIRA_ROOT_URL_CFG_KEY = "JIRARootUrl";
        private static readonly string JIRA_ROOT_URL;
        
        #endregion

        #region field(s)
        private JiraAuthenticator _jiraAuthenticator;
        private string _applicationName;
        private Dictionary<string, JIRAUserInfo> _authenticatedUsers;
        #endregion

        #region cctor(s)
        static AtlassianJIRAMembershipProvider()
        {
            JIRA_ROOT_URL = ConfigurationManager.AppSettings[JIRA_ROOT_URL_CFG_KEY];
        }

        public AtlassianJIRAMembershipProvider()
        {
             
        }
        #endregion

        #region prop(s)
        protected JiraAuthenticator Authenticator
        {
            get
            {
                if (_jiraAuthenticator == null)
                {
                    _jiraAuthenticator = new JiraAuthenticator();
                    _jiraAuthenticator.JIRARootUrl = JIRA_ROOT_URL;
                }
                return _jiraAuthenticator;
            }
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
            //throw new NotImplementedException();
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            //throw new NotImplementedException();
            return false;
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            //throw new NotImplementedException();
            status = System.Web.Security.MembershipCreateStatus.ProviderError;
            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
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
            //throw new NotImplementedException();
            totalRecords = 0;
            return null; //todo
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            //throw new NotImplementedException();
            totalRecords = 0;
            return null; //todo
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            //throw new NotImplementedException();
            totalRecords = 0;
            return null; //hardly
        }

        public override int GetNumberOfUsersOnline()
        {
            //throw new NotImplementedException();
            return 0; //hardly
        }

        public override string GetPassword(string username, string answer)
        {
            //throw new NotImplementedException();
            return null; //hardly
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            //throw new NotImplementedException();
            return null; //todo
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            //throw new NotImplementedException();
            return null; //todo
        }

        public override string GetUserNameByEmail(string email)
        {
            //throw new NotImplementedException();
            return null; //hardly
        }

        public override int MaxInvalidPasswordAttempts
        {
            get 
            { 
                //throw new NotImplementedException(); 
                return 2; //todo - http://pm.ruthenorum.info/jira/browse/TV-33
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get 
            {
                return 0;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get 
            {
                return 4; //not sure
            }
        }

        public override int PasswordAttemptWindow
        {
            get 
            {
                return 1;
            }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get 
            {
                return MembershipPasswordFormat.Clear; //not sure
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get 
            {
                return ".+";
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            //throw new NotImplementedException();
            return null; //hardly
        }

        public override bool UnlockUser(string userName)
        {
            //throw new NotImplementedException();
            return false; //only manually
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            //throw new NotImplementedException();
            return;//only manually
        }

        public override bool ValidateUser(string username, string password)
        {
            bool rslt = false;
            string json = null;
            HttpStatusCode? httpStatus = null;
            lock(this._jiraAuthenticator)
            {
                rslt = Authenticator.Authenticate(username, password);
                if(rslt)
                {
                    json = Authenticator.LastResponseText;
                }
            }
            if (rslt)
            {
                JIRAUserInfo jui = JIRAUserInfo.Parse(json);
            }
            return rslt;

        }
        #endregion
    }
}
