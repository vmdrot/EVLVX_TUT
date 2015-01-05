using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace Evolvex.VKUtilLib.Misc
{
    public class PostTitleReader
    {
        public string Read_v1(string url)
        {
            using (WebClient wc = new WebClient())
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(wc.DownloadString(url));
                List<HtmlNode> divs = GetAllElements(doc, "div");
                foreach (HtmlNode div in divs)
                {
                    Console.WriteLine(div.OuterHtml);
                    string currClass = GetAttributeSafe(div, "class");
                    if (string.IsNullOrEmpty(currClass))
                        continue;
                    if (currClass == "page_media_caption" || currClass == "wall_post_text")
                        return div.InnerText;
                }
            }
            return null;
        }

        public string Read(string url)
        {
            using (WebClient wc = new WebClient())
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(wc.DownloadString(url));
                
                List<HtmlNode> tables = GetAllElements2(doc, "table");
                foreach (HtmlNode table in tables)
                {
                    Console.WriteLine(table.OuterHtml);
                    string currTableClass = GetAttributeSafe(table, "class");
                    if (string.IsNullOrEmpty(currTableClass))
                        continue;
                    if (currTableClass != "fw_post_table")
                        continue;

                    List<HtmlNode> divs = new List<HtmlNode>(table.Descendants("div"));
                    foreach (HtmlNode div in divs)
                    {
                        Console.WriteLine(div.OuterHtml);
                        string currClass = GetAttributeSafe(div, "class");
                        if (string.IsNullOrEmpty(currClass))
                            continue;
                        if (currClass == "page_media_caption" || currClass == "wall_post_text")
                            return div.InnerText;
                    }
                }
            }
            return null;
        }

        public string Read_v2(string url)
        {
            using (WebClient wc = new WebClient())
            {
                string html = wc.DownloadString(url);
                File.WriteAllText(@"D:\home\vmdrot\DEV\_tut\VKUtil\VKUtil\bin\Debug\Read_v2.html", html);
                HtmlDocument doc = new HtmlDocument();
                Console.WriteLine(doc.DetectEncodingHtml(html));
                doc.LoadHtml(html);
                Console.WriteLine(doc.Encoding);

                List<HtmlNode> divs = new List<HtmlNode>(doc.DocumentNode.Descendants("div"));
                //foreach (HtmlNode table in tables)
                //{
                //    Console.WriteLine(table.OuterHtml);
                //    string currTableClass = GetAttributeSafe(table, "class");
                //    if (string.IsNullOrEmpty(currTableClass))
                //        continue;
                //    if (currTableClass != "fw_post_table")
                //        continue;

                //    List<HtmlNode> divs = new List<HtmlNode>(table.Descendants("div"));
                    foreach (HtmlNode div in divs)
                    {
                        Console.WriteLine(div.OuterHtml);
                        string currClass = GetAttributeSafe(div, "class");
                        if (string.IsNullOrEmpty(currClass))
                            continue;
                        if (currClass == "page_media_caption" || currClass == "wall_post_text")
                            return div.InnerText;
                    }
                //}
            }
            return null;
        }

        private List<HtmlNode> GetAllElements(HtmlDocument doc, string tagName)
        {
            List<HtmlNode> all = new List<HtmlNode>(doc.DocumentNode.Elements(tagName));
            List<HtmlNode> rslt = new List<HtmlNode>();
            foreach (HtmlNode el in all)
            {
                if (el.Name.ToLower() != tagName.ToLower())
                    continue;
                rslt.Add(el);
            }
            return rslt;
        }

        private List<HtmlNode> GetAllElements2(HtmlDocument doc, string tagName)
        {
            List<HtmlNode> all = new List<HtmlNode>(doc.DocumentNode.Descendants(tagName));
            List<HtmlNode> rslt = new List<HtmlNode>();
            foreach (HtmlNode el in all)
            {
                if (el.Name.ToLower() != tagName.ToLower())
                    continue;
                rslt.Add(el);
            }
            return rslt;
        }

        private static string GetAttributeSafe(HtmlNode tag, string attrName)
        {
            if (tag == null)
                return null;
            HtmlAttribute attr = tag.Attributes[attrName];
            if (attr == null)
                return null;
            return attr.Value;
        }

    }
}
