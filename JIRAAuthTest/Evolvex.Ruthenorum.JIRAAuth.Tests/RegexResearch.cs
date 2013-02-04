using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Evolvex.Ruthenorum.JIRAAuth.Tests
{
    [TestFixture]
    public class RegexResearch
    {
        [Test]
        public void MatchEverything() {  MatchEverythingWorker("asdflkjwer81247nxvc"); }

        public void MatchEverythingWorker(string input)
        {
            Regex r = new Regex(".+");
            Match m = r.Match(input);
            Console.WriteLine(m.ToString());
        }

    }
}
