using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.Ruthenorum.JIRAAuth.Facade;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;

namespace Evolvex.Ruthenorum.JIRAAuth.Tests
{
    [TestFixture]
    public class JiraAuthFacadeTests
    {
        [Test]
        public void ListNewUsers()
        {

            DateTime started = DateTime.Now;
            List<IJIRAUserInfo> juis = JiraAuthFacade.ListNewUsers();
            Console.WriteLine("juis.Count = {0}", juis.Count);
            foreach(IJIRAUserInfo jui in juis)
            {
                Console.WriteLine(jui);
            }
            DateTime ended = DateTime.Now;
            Console.WriteLine("Fetching took {0}", (TimeSpan)(ended - started));
        }
    }
}
