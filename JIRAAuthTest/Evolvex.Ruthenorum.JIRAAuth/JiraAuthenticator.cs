using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Evolvex.Web.Utility;
using System.Web.Security;
using Evolvex.Ruthenorum.JIRAAuth.Data;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using System.Configuration;
using Newtonsoft.Json;
using NLog;

namespace Evolvex.Ruthenorum.JIRAAuth
{
    public class JiraAuthenticator: IJIRAAuthenticator
    {
        private static readonly Evolvex.Ruthenorum.Core.Interfaces.ILog log = Evolvex.Ruthenorum.Core.Logging.GetLogger(typeof(JiraAuthenticator));

        #region const(s)
        public const string JIRA_ROOT_URL_CFG_KEY = "JIRARootUrl";
        private static readonly string JIRA_ROOT_URL;
        private const string JIRA_SVC_USR_CFG_KEY = "jiraSvcAcct";
        private static readonly string JIRA_SVC_USR;
        private const string JIRA_SVC_PWD_CFG_KEY = "jiraSvcAccp";
        private static readonly string JIRA_SVC_PWD;
        #endregion

        #region field(s)
        private string _serviceAcctUsrName;
        private string _serviceAcctPwd;
        #endregion
        
        public String JIRARootUrl { get; set; }
        public string JIRAUserName 
        {
            get { return this._serviceAcctUsrName; }
            set { this._serviceAcctUsrName = value; }
        }

        public string JIRAUserPwd
        {
            get { return this._serviceAcctPwd; }
            set { this._serviceAcctPwd = value; }
        }

        public const string RESOURCE_PATH = "/rest/api/2/user?expand=groups&username=";
        public const string GROUPS_LIST_PATH = "/rest/api/2/groups/picker";
        public const string USER_SEARCH_PATH = "/rest/api/2/user/search?username={0}&startAt={1}&maxResults={2}";
        public HttpStatusCode? LastStatus { get; private set; }
        public string LastResponseText { get; private set; }

        static JiraAuthenticator()
        {
            JIRA_ROOT_URL = ConfigurationManager.AppSettings[JIRA_ROOT_URL_CFG_KEY];
            JIRA_SVC_USR = ConfigurationManager.AppSettings[JIRA_SVC_USR_CFG_KEY];
            JIRA_SVC_PWD = ConfigurationManager.AppSettings[JIRA_SVC_PWD_CFG_KEY];
            //log.Debug("JIRA_ROOT_URL = '{0}', JIRA_SVC_USR = '{1}', JIRA_SVC_PWD = '{2}'", JIRA_ROOT_URL, JIRA_SVC_USR, JIRA_SVC_PWD);
        }
        public JiraAuthenticator(string jiraUsr, string jiraPwd) : this(JIRA_ROOT_URL, jiraUsr, jiraPwd)
        {
        }


        public JiraAuthenticator(string jiraRootUrl, string jiraUsr, string jiraPwd)
        {
            JIRARootUrl = jiraRootUrl;
            this._serviceAcctUsrName = jiraUsr;
            this._serviceAcctPwd = jiraPwd;
            //log.Debug("JIRARootUrl = '{0}', _serviceAcctUsrName = '{1}', _serviceAcctPwd = '{2}'", JIRARootUrl, _serviceAcctUsrName, _serviceAcctPwd);
        }

        public JiraAuthenticator(): this(JIRA_ROOT_URL, JIRA_SVC_USR, JIRA_SVC_PWD)
        {
            //log.Debug("JIRARootUrl = '{0}', _serviceAcctUsrName = '{1}', _serviceAcctPwd = '{2}'", JIRARootUrl, _serviceAcctUsrName, _serviceAcctPwd);
        }

        private string GetEncodedCredentials(string usr, string pwd)
        {
            string mergedCredentials = string.Format("{0}:{1}", usr, pwd);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }

        public bool Authenticate(string usr, string pwd)
        {
            LastResponseText = string.Empty;
            string usrJson;
            bool rslt = Authenticate(usr, pwd, out usrJson);
            if (rslt)
                LastResponseText = usrJson;
            return rslt;
        }

        public bool Authenticate(string usr, string pwd, out string responseText)
        {
            string url = string.Format("{0}{1}{2}", JIRARootUrl, RESOURCE_PATH, usr);
            return AuthenticateWorker(usr, pwd, out responseText, url);
        }

        public bool GetUserInfo(string usr, out string responseJson)
        {
            string url = string.Format("{0}{1}{2}", JIRARootUrl, RESOURCE_PATH, usr);
            return AuthenticateWorker(this._serviceAcctUsrName, this._serviceAcctPwd, out responseJson, url);
        }

        public bool ListGroups(string usr, string pwd, out string responseText, int limitTo)
        {
            string url = string.Format("{0}{1}?maxResults={2}", JIRARootUrl, GROUPS_LIST_PATH, limitTo);
            return AuthenticateWorker(usr, pwd, out responseText, url);
        }

        private bool AuthenticateWorker(string usr, string pwd, out string responseText, string url)
        {
            //log.Debug("AuthenticateWorker(..., url = '{0}') - entering...", url);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "GET";

            string base64Credentials = GetEncodedCredentials(usr, pwd);
            request.Headers.Add("Authorization", "Basic " + base64Credentials);

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    string result = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                    }
                    LastStatus = response.StatusCode;
                    responseText = result;
                }
            }
            catch (WebException exc)
            {
                if (exc.Message.IndexOf("(401)") != -1)
                    LastStatus = HttpStatusCode.Unauthorized;
                else if (exc.Message.IndexOf("(403)") != -1)
                    LastStatus = HttpStatusCode.Forbidden;
                else
                    LastStatus = HttpStatusCode.ServiceUnavailable;
                responseText = string.Empty;
            }
            return (bool)(LastStatus != null && (HttpStatusCode)LastStatus == HttpStatusCode.OK);
        }

        private bool PostWorker(string usr, string pwd, out string responseText, string url, string sData)
        {
            //log.Debug("AuthenticateWorker(..., url = '{0}') - entering...", url);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";
            UnicodeEncoding enc = new UnicodeEncoding();
            byte[] data = enc.GetBytes(sData);

            string base64Credentials = GetEncodedCredentials(usr, pwd);
            request.Headers.Add("Authorization", "Basic " + base64Credentials);
            request.ContentLength = data.Length;
            using(Stream strm = request.GetRequestStream())
            {
                strm.Write(data, 0, data.Length);
            }
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    string result = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                    }
                    LastStatus = response.StatusCode;
                    responseText = result;
                }
            }
            catch (WebException exc)
            {
                if (exc.Message.IndexOf("(401)") != -1)
                    LastStatus = HttpStatusCode.Unauthorized;
                else if (exc.Message.IndexOf("(403)") != -1)
                    LastStatus = HttpStatusCode.Forbidden;
                else
                    LastStatus = HttpStatusCode.ServiceUnavailable;
                responseText = string.Empty;
            }
            return (bool)(LastStatus != null && (HttpStatusCode)LastStatus == HttpStatusCode.OK);
        }

        public void SetSvcUser(String usr, string pwd)
        {
            this._serviceAcctUsrName = usr;
            this._serviceAcctPwd = pwd;
        }

        public List<string> ListGroups()
        {
            const int maxGroups = 1000;
            string groupsJson;
            if (!ListGroups(this._serviceAcctUsrName, this._serviceAcctPwd, out groupsJson, maxGroups))
                return new List<string>();
            else
                return JIRAGroupsParser.Parse(groupsJson);
        }

        public IJIRAUserInfo GetUser(string userName)
        {
            string url = string.Format("{0}{1}{2}", JIRARootUrl, RESOURCE_PATH, userName);
            string usrJson;
            if (!AuthenticateWorker(this._serviceAcctUsrName, this._serviceAcctPwd, out usrJson, url))
                return null;
            return JIRAUserInfo.Parse(usrJson);
        }

        public List<string> ListAllUserNames()
        {
            List<string> rslt = new List<string>();
            for (int i = 97; i <= 122; i++)
            {
                string currUrl = string.Format("{0}{1}", JIRA_ROOT_URL, String.Format(USER_SEARCH_PATH, (char)i, 0, 20));
                string jsonRslt;
                if (!AuthenticateWorker(this._serviceAcctUsrName, this._serviceAcctPwd, out jsonRslt, currUrl))
                    continue;
                List<string> currUsrs = JIRAUserInfo.ParseUserNames(jsonRslt);
                MergeUserNames(rslt, currUsrs);
            }
            return rslt;
        }

        private void MergeUserNames(List<string> rslt, List<string> currUsrs)
        {
            foreach (string usr in currUsrs)
            { 
                if(rslt.Contains(usr)) 
                    continue;
                rslt.Add(usr);
            }
        }


        #region IJIRAAuthenticator Members


        public string GenericQuery(string restApiPath, string queryString)
        {
            string rslt;
            if (AuthenticateWorker(this._serviceAcctUsrName, this._serviceAcctPwd, out rslt, string.Format("{0}{1}?{2}", this.JIRARootUrl, restApiPath, queryString)))
                return rslt;
            return string.Empty;
        }

        public string GenericPost(string restApiUrl, string jsonData)
        { 
            string rslt;
            if(PostWorker(this._serviceAcctUsrName, this._serviceAcctPwd, out rslt, string.Format("{0}{1}", this.JIRARootUrl, restApiUrl), jsonData))
                return rslt;
            return string.Empty;
        }

        #endregion

    }
}
