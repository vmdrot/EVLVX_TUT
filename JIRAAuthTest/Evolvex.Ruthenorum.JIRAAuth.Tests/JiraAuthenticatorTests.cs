using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Evolvex.Ruthenorum.JIRAAuth.Tests
{
    [TestFixture]
    public class JiraAuthenticatorTests
    {
        [Test]
        public void ListAllUserNames()
        {
            JiraAuthenticator ja = new JiraAuthenticator();

            List<string> usrs = ja.ListAllUserNames();
            Console.WriteLine("usrs.Count = {0}", usrs.Count);
            foreach (string jui in usrs)
                Console.WriteLine(jui);
        }
    }
}
