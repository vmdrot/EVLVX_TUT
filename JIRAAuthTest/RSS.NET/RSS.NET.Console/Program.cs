using System;
using System.Collections.Generic;
using System.Text;
using Rss;

namespace RSS.NET.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RssFeed feed = RssFeed.Read("http://aktualne.centrum.sk/export/rss-hp.phtml");
            System.Console.WriteLine("Items.Count = {0}", feed.Channels[0].Items.Count);
            System.Console.Read();
        }
    }
}
