using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.Web.Utility;
using System.Net;

namespace Evolvex.Ruthenorum.JIRAAuth.Tests
{
    [TestFixture]
    public class UtilityTests
    {

        #region const's
        private const string WEB_EXC_401_MSG = @"The remote server returned an error: (401) Unauthorized.";
        private const string WEB_EXC_500_MSG = @"";
        private const string WEB_EXC_403_MSG = @"The remote server returned an error: (403) Forbidden.";
        private const string WEB_EXC_404_MSG = @"The remote server returned an error: (404) Not Found.";
        
        #endregion

        [Test]
        public void WebExceptionParserTest_401()
        {
            WebExceptionParserTestWorker(WEB_EXC_401_MSG);
        }

        [Test]
        public void WebExceptionParserTest_403()
        {
            WebExceptionParserTestWorker(WEB_EXC_403_MSG);
        }

        [Test]
        public void WebExceptionParserTest_404()
        {
            WebExceptionParserTestWorker(WEB_EXC_404_MSG);
        }
        
        private void WebExceptionParserTestWorker(string excMsg)
        {
            HttpStatusCode? res = WebExceptionParser.Parse(excMsg);
            Console.WriteLine("res = {0}", res);
        }

        [Test]
        public void ObtainSampleWebExc401()
        {
            ObtainSampleWebExcWorker("http://pm.ruthenorum.info/jira/rest/api/2/user?username=valeriy.drotenko");
        }

        [Test]
        public void ObtainSampleWebExc404()
        {
            ObtainSampleWebExcWorker("http://pm.ruthenorum.info/jira/nonexistentpage.html");
        }

        private void ObtainSampleWebExcWorker(String url)
        { 
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.DownloadString(url);
                }
                catch (WebException wexc)
                {
                    Console.WriteLine(wexc.Message);
                }

            }
        }
    }
}
