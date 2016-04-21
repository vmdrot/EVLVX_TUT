using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.VKUtilLib.EDataGovUA;
using Newtonsoft.Json;
using System.IO;
using Evolvex.VKUtil.Utility;

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

        [Test]
        public void DisposersJSONToTabDelim()
        {
            List<EDataGovUaDisposerInfo> lst = JsonConvert.DeserializeObject<List<EDataGovUaDisposerInfo>>(File.ReadAllText(@"D:\home\vmdrot\DEV\_tut\VKUtil\Evolvex.VKUtilLib\EDataGovUA\search_res.json"));
            using (StreamWriter sw = new StreamWriter(@"D:\home\vmdrot\DEV\_tut\VKUtil\Evolvex.VKUtilLib\EDataGovUA\search_res.tab"))
            {
                foreach(EDataGovUaDisposerInfo di in lst)
                {
                    if(di.IsFound)
                        continue;
                    sw.WriteLine("{0}\t{1}\t{2}\t{3}", di.YeDRPOU, di.InternalID, di.CabinetStatus,  di.DisposerName);
                }
            }
        }

        [Test]
        public void DisposersJSONToTabDelim2()
        {
            List<EDataGovUaDisposerInfo> lst = JsonConvert.DeserializeObject<List<EDataGovUaDisposerInfo>>(File.ReadAllText(@"D:\home\vmdrot\DEV\_tut\VKUtil\Evolvex.VKUtilLib\EDataGovUA\search_res20160420_2016.json"));
            using (StreamWriter sw = new StreamWriter(@"D:\home\vmdrot\DEV\_tut\VKUtil\Evolvex.VKUtilLib\EDataGovUA\search_res20160420_2016.tab"))
            {
                foreach(EDataGovUaDisposerInfo di in lst)
                {
                    if(di.IsFound)
                        continue;
                    sw.WriteLine("{0}\t{1}\t{2}\t{3}", di.YeDRPOU, di.InternalID, di.CabinetStatus,  di.DisposerName);
                }
            }
        }


        [Test]
        public void PathMisc()
        {
            string path = @"D:\home\vmdrot\DEV\_tut\VKUtil\Evolvex.VKUtilLib\EDataGovUA\search_res.json";
            Console.WriteLine(Path.GetExtension(path));
        }

        [Test]
        public void ConvertJson2XML_SoEsCabinets()
        {
            ConvertJson2XMLWorker<EDataGovUaDisposerInfo>(@"D:\home\vmdrot\Testing\OpenData\Output\DSAPIStuff\spending.gov.ua\out\SoEs_edata_status_20160421_1908.json", @"D:\home\vmdrot\Testing\OpenData\Output\DSAPIStuff\Insights\Aggregation");
        }

        private void ConvertJson2XMLWorker<T>(string jsonPath, string xmlSave2Dir)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonPath));
            Tools.WriteXML<List<T>>(list, Path.Combine(xmlSave2Dir, string.Format("{0}.xml", Path.GetFileNameWithoutExtension(jsonPath))));
        }
    }
}
