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
    public class POReaderTests
    {
        [Test]
        public void TryExtractKeyValTest()
        {
            string key;
            string val;
            POReader r = new POReader();


            bool b = r.TryExtractKey(@"msgid ""heading_title""", out key);
            bool b1 = r.TryExtractVal(@"msgstr ""Attributes""", out val);
            Console.WriteLine("key = {0}, val = {1}", key, val);
        }

        [Test]
        public void ReadAllTest()
        {
            POReader r = new POReader();
            List<TranslationEntry> rslt = r.ReadAll(File.ReadAllLines(@"D:\home\vmdrot\DEV\_tut\PHP2PootleConverter\Samples\attribute[correct].po"));
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
