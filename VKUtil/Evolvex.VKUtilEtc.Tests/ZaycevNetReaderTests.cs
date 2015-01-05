using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.VKUtilLib.Zaycev;

namespace Evolvex.VKUtilEtc.Tests
{
    [TestFixture]
    public class ZaycevNetReaderTests
    {
        [Test]
        [STAThread]
        public void ReadDoloresORiordan()
        {
            using (ZaycevNetReader reader = new ZaycevNetReader())
            {
                if (reader.Read("http://zaycev.net/artist/37539#works"))
                {
                    //Console.WriteLine("reader.MediaList.Count = {0}", reader.MediaList.Count);
                    foreach (string url in reader.MediaList)
                        Console.WriteLine(url);
                }
            }
            //
        }

        [Test]
        [STAThread]
        public void ReadJohnnyCash()
        {
            using (ZaycevNetReader reader = new ZaycevNetReader())
            {
                if (reader.Read("http://zaycev.net/artist/55352#works"))
                {
                    //Console.WriteLine("reader.MediaList.Count = {0}", reader.MediaList.Count);
                    foreach (string url in reader.MediaList)
                        Console.WriteLine(url);
                }
            }
            //
        }

        [Test]
        public void ZaycevPlayUrlResearch()
        {
            Uri uri = new Uri("http://dl.zaycev.net/37539/171490/dolores_o_riordan_-_accept_things.mp3?dlKind=play&format=json");
            Console.WriteLine(string.Join(",", uri.Segments));
        }

        [Test]
        [STAThread]
        public void ReadAndDownloadMarkForster()
        {
            using (ZaycevNetReader reader = new ZaycevNetReader())
            {
                if (reader.Read("http://zaycev.net/artist/555747#works"))
                {
                    reader.DownloadAll(reader.MediaList, @"D:\home\Zenet\20150105\MarkForster");
                }
            }
            //
        }

        [Test]
        [STAThread]
        public void ReadAndDownloadDoloresORiordan()
        {
            using (ZaycevNetReader reader = new ZaycevNetReader())
            {
                if (reader.Read("http://zaycev.net/artist/37539#works"))
                {
                    reader.DownloadAll(reader.MediaList, @"D:\home\Zenet\20150105\DoloresORiordan");
                }
            }
            //
        }
    }
}
