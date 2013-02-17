using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.PHP2PootleConverterLib.Readers;
using Evolvex.PHP2PootleConverterLib.Data;
using System.IO;

namespace Evolvex.PHP2PootleConverter.Tests
{
    [TestFixture]
    public class PHPReaderTests
    {
        [Test]
        public void TryExtractKeyValTest()
        {
            string key;
            string val;
            PHPReader r = new PHPReader();
            bool b = r.TryExtractKeyVal(@"$_['heading_title']          = 'Attributes';", out key, out val);
            Console.WriteLine("key = {0}, val = {1}", key, val);
        }

        [Test]
        public void ReadAllTest()
        {
            PHPReader r = new PHPReader();
            List<TranslationEntry> rslt = r.ReadAll(File.ReadAllLines(@"D:\home\vmdrot\DEV\_tut\PHP2PootleConverter\Samples\attribute.php"));
            Print(rslt);
        }

        private void Print(List<TranslationEntry> rslt)
        {
            foreach (TranslationEntry te in rslt)
            {
                Console.WriteLine("Comment = '{0}', MsgId = '{1}', MsgStr = '{2}'", te.Comment, te.MsgId, te.MsgStr);
            }
        }
    }
}
