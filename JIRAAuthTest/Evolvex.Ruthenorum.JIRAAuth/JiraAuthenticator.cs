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

namespace Evolvex.Ruthenorum.JIRAAuth
{
    public class JiraAuthenticator: IJIRAAuthenticator
    {
        #region const(s)
        public const string JIRA_ROOT_URL_CFG_KEY = "JIRARootUrl";
        private static readonly string JIRA_ROOT_URL;
        
        #endregion
        
        public String JIRARootUrl { get; set; }
        public const string RESOURCE_PATH = "/rest/api/2/user?expand=groups&username=";
        public HttpStatusCode? LastStatus { get; private set; }
        public string LastResponseText { get; private set; }
        public bool Authenticate(string usr, string pwd)
        {
            string url = string.Format("{0}{1}{2}", JIRARootUrl, RESOURCE_PATH, usr);


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
                    LastResponseText = result;
                }
            }
            catch (WebException exc)
            {
                LastStatus = WebExceptionParser.Parse(exc.Message);
                LastResponseText = string.Empty;
            }
            return (bool)(LastStatus != null && (HttpStatusCode)LastStatus == HttpStatusCode.OK);
        }

        static JiraAuthenticator()
        {
            JIRA_ROOT_URL = ConfigurationManager.AppSettings[JIRA_ROOT_URL_CFG_KEY];
        }

        private string GetEncodedCredentials(string usr, string pwd)
        {
            string mergedCredentials = string.Format("{0}:{1}", usr, pwd);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }


        public List<string> ListGroups()
        {
            throw new NotImplementedException();
        }

        public IJIRAUserInfo GetUser(string userName)
        {
            throw new NotImplementedException();
        
        }
    }
}
