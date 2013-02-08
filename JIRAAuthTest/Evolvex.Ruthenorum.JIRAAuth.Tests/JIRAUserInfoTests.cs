using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.Ruthenorum.JIRAAuth.Data;
using System.IO;

namespace Evolvex.Ruthenorum.JIRAAuth.Tests
{
    [TestFixture]
    public class JIRAUserInfoTests
    {
        private const string USER_INFO_ME_FN = @"D:\home\vmdrot\DEV\_tut\JIRAAuthTest\SampleResponses\valeriy.drotenko.txt";

        [Test]
        public void ParseUser()
        {
            JIRAUserInfo jui = JIRAUserInfo.Parse(File.ReadAllText(USER_INFO_ME_FN));
            Console.WriteLine(jui);
        }
    }
}
