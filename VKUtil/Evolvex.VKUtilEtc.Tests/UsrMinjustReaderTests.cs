using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.VKUtilLib.YeDR;

namespace Evolvex.VKUtilEtc.Tests
{
    [TestFixture]
    public class UsrMinjustReaderTests
    {
        [Test]
        [STAThread]
        public void ResearchTest()
        {
            using (UsrMinjustGovUaReader reader = new UsrMinjustGovUaReader())
            {
                reader.SearchForYeDRPOU = "39492190";
                bool res = reader.Read(UsrMinjustGovUaReader.FREE_SEARCH_TRUE_URL);
                Console.WriteLine("res = {0}", res);
                Form1 frm = new Form1();
                reader.ShowBrowserIn(frm.IEContainer);
                frm.ShowDialog();
            }
        }
    }
}
