using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Evolvex.PHP2PootleConverterLib.Readers;
using Evolvex.PHP2PootleConverterLib.Data;
using Evolvex.PHP2PootleConverterLib.Writers;

namespace Evolvex.PHP2PootleConverter.Tests
{
    [TestFixture]
    public class PHWriterTests
    {
        [Test]
        public void WriteAllTest()
        {
            POReader r = new POReader();
            List<TranslationEntry> rslt = r.ReadAll(File.ReadAllLines(@"D:\home\vmdrot\DEV\_tut\PHP2PootleConverter\Samples\attribute[correct].po"));
            StringBuilder sb = new StringBuilder();
            PHPWriter pow = new PHPWriter();
            pow.WriteAll(rslt, sb);
            Console.WriteLine(sb.ToString());
        }
    }
}
