using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace JIRAAuthTest
{
    public class JiraAuthenticator
    {
        public String JIRARootUrl { get; set; }
        public const string RESOURCE_PATH = "/rest/api/2/user?expand=groups&username=";
        public const string GROUPS_LIST_PATH = "/rest/api/2/groups/picker";
        public HttpStatusCode? LastStatus { get; private set; }

        #region field(s)
        private string _serviceAcctUsrName;
        private string _serviceAcctPwd;
        #endregion

        public void SetSvcUser(String usr, string pwd)
        {
            this._serviceAcctUsrName = usr;
            this._serviceAcctPwd = pwd;
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


        private string GetEncodedCredentials(string usr, string pwd)
        {
            string mergedCredentials = string.Format("{0}:{1}", usr, pwd);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }

    }
}
