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
        public HttpStatusCode? LastStatus { get; private set; }
        public bool Authenticate(string usr, string pwd, out string responseText)
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
