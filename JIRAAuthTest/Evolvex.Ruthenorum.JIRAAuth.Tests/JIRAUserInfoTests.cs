using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.Ruthenorum.JIRAAuth.Data;
using System.IO;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;

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

        [Test]
        public void ParseMany()
        {
            List<IJIRAUserInfo> juis = JIRAUserInfo.ParseMany(File.ReadAllText(@"D:\home\vmdrot\DEV\_tut\JIRAAuthTest\SampleResponses\userSearch_r.txt"));
            foreach (IJIRAUserInfo jui in juis)
                Console.WriteLine(jui);
        }

        [Test]
        public void ParseUserNames()
        {
            List<string> usrs = JIRAUserInfo.ParseUserNames(File.ReadAllText(@"D:\home\vmdrot\DEV\_tut\JIRAAuthTest\SampleResponses\userSearch_r.txt"));
            foreach (string jui in usrs)
                Console.WriteLine(jui);
        }
    }
}
