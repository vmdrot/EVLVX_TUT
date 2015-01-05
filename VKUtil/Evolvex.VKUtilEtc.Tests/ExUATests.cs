using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.VKUtilLib.ExUA;

namespace Evolvex.VKUtilEtc.Tests
{
    [TestFixture]
    public class ExUATests
    {

        [Test]
        [STAThread]
        public void ReadBigBangS04()
        {
            using (ExUAPageReader reader = new ExUAPageReader())
            {
                if (reader.Read("http://www.ex.ua/53452843"))
                {
                    //Console.WriteLine("reader.MediaList.Count = {0}", reader.MediaList.Count);
                    foreach (string url in reader.MediaList)
                        Console.WriteLine(url);
                }
            }
            //
        }
    }
}
