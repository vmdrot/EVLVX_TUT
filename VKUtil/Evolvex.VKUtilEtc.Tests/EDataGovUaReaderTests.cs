using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.VKUtilLib.EDataGovUA;
using Newtonsoft.Json;
using System.IO;

namespace Evolvex.VKUtilEtc.Tests
{
    
    [TestFixture]
    public class EDataGovUaReaderTests
    {
        [Test]
        [STAThread]
        public void GetToSearchTest() 
        {
            DateTime ds = DateTime.Now;
            string[] aYeDRPOUs = File.ReadAllLines(@"D:\home\vmdrot\DEV\_tut\VKUtil\Evolvex.VKUtilLib\EDataGovUA\test_yedrpous.txt");
            List<EDataGovUaDisposerInfo> rslts = new List<EDataGovUaDisposerInfo>();
            using (EDataGovUaReader reader = new EDataGovUaReader() { LogDebugEvents = false})
            {

                foreach (string yedrpou in aYeDRPOUs)
                {
                    try
                    {
                        reader.SearchForYeDRPOU = yedrpou;
                        if (reader.Read(EDataGovUaReader.START_DISPOSERS_SEARCH_URL))
                        {
                            Console.WriteLine("reader.Read() succeeded");
                        }
                        else
                            Console.WriteLine("reader.Read() failed");
                        //Console.WriteLine(reader.HTML);
                        //Console.WriteLine("--------------------------------------------------------");

                        Console.WriteLine("reader.Result= {0}", JsonConvert.SerializeObject(reader.Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                        rslts.Add(reader.Result);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Reading '{0}': exception: {1}", yedrpou, exc);
                    }
                }
                File.WriteAllText(@"D:\home\vmdrot\DEV\_tut\VKUtil\Evolvex.VKUtilLib\EDataGovUA\search_res.json", JsonConvert.SerializeObject(rslts, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode);
                DateTime df = DateTime.Now;
                Console.WriteLine("Started: {0}", ds);
                Console.WriteLine("Finished: {0}", df);
                Console.WriteLine("Completed in: {0}", (TimeSpan)(df-ds));
                return;
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
