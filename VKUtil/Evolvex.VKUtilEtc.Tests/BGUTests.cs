using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.VKUtilLib.BGU;
using Newtonsoft.Json;
using System.IO;

namespace Evolvex.VKUtilEtc.Tests
{
    [TestFixture]
    public class BGUTests
    {
        [Test]
        [STAThread]
        public void ReadOwnershipStructBanksListTest()
        {
            using (BGUSiteReader r = new BGUSiteReader())
            {
                if (!r.Read("http://www.bank.gov.ua/control/uk/publish/article?art_id=6738234&cat_id=51342"))
                {
                    Console.WriteLine("Failed to read start url");
                    return;
                }

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Formatting = Formatting.Indented;
                string json = JsonConvert.SerializeObject(r.BankStructs, settings);
                Console.WriteLine(json);
                File.WriteAllText(@"D:\home\vmdrot\BGU\Var\VisaLiberalization\BankStructs_Hist.json", json);
            }
        }
    }
}
