using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net;

namespace Evolvex.VKUtilEtc.Tests
{
    [TestFixture]
    public class MiscResearch
    {
        [Test]
        public void NullHttpStatusTest()
        {

            HttpStatusCode?[] codes = new HttpStatusCode?[] { HttpStatusCode.Forbidden, null };
            foreach (HttpStatusCode? curr in codes)
                Console.WriteLine("The request for current YeDRPOU {0} will be repeated because of error, last HTTP status = {1} ({2})", string.Empty, curr, (int?)curr);

        }
    }
}
