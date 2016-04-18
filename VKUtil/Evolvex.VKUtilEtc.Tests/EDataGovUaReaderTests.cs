using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.VKUtilLib.EDataGovUA;

namespace Evolvex.VKUtilEtc.Tests
{
    
    [TestFixture]
    public class EDataGovUaReaderTests
    {
        [Test]
        [STAThread]
        public void GetToSearchTest() 
        {
            using (EDataGovUaReader reader = new EDataGovUaReader())
            {
                reader.SearchForYeDRPOU = "00032112";
                if (reader.Read(EDataGovUaReader.START_DISPOSERS_SEARCH_URL))
                {
                    Console.WriteLine("reader.Read() succeeded");

                }
                else
                    Console.WriteLine("reader.Read() failed");
                Console.WriteLine(reader.HTML);
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Before Form show");
                Form1 frm = new Form1();
                reader.ShowBrowserIn(frm.IEContainer);
                frm.ShowDialog();
                Console.WriteLine("After Form show");
                Console.WriteLine("--------------------------------------------------------");
            }
        }
    }
}
