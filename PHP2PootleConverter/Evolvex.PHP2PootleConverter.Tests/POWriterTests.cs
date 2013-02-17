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
    public class POWriterTests
    {
        [Test]
        public void WriteAllTest()
        {
            PHPReader r = new PHPReader();
            List<TranslationEntry> rslt = r.ReadAll(File.ReadAllLines(@"D:\home\vmdrot\DEV\_tut\PHP2PootleConverter\Samples\attribute.php"));
            StringBuilder sb = new StringBuilder();
            POWriter pow = new POWriter();
            pow.WriteAll(rslt, sb);
            Console.WriteLine(sb.ToString());
        }
    }
}
