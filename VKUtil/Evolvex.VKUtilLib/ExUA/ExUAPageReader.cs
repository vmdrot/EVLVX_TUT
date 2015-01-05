using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Evolvex.VKUtilLib.ExUA.Data;
using System.IO;

namespace Evolvex.VKUtilLib.ExUA
{
    public class ExUAPageReader: WebBrowserReaderBase
    {
        public static readonly string[] DefaultFileExts = new string[] { ".mkv", ".avi", ".mov", ".mp4", ".mpeg", ".mpg", ".srt", ".mp3", ".ogg", ".acc", ".amr", ".zip", ".7z", ".rar", ".bzip", ".bz2" };

        private List<string> _mediaList;
        public ExUAPageReader() : base()
        {
            FileExts = DefaultFileExts;
        }

        public string[] FileExts { get; set; }

        public List<string> MediaList
        {
            get
            {
                return _mediaList;
            }
        }


        protected override bool ReadWorker()
        {
            _mediaList = new List<string>();

            List<ExUALinkInfo> links = new List<ExUALinkInfo>();
            HtmlElementCollection trs = _wc.Document.GetElementsByTagName("tr");
            //Console.WriteLine("trs.Count = {0}", trs.Count);
            foreach (HtmlElement el in trs)
            {
                HtmlElementCollection tds = el.GetElementsByTagName("td");
                //Console.WriteLine("tds.Count = {0}", tds.Count);
                if (tds.Count < 4)
                    continue;
                ExUALinkInfo currLink = new ExUALinkInfo();

                HtmlElementCollection imgs = tds[0].GetElementsByTagName("img");
                //Console.WriteLine("imgs.Count = {0}", imgs.Count);
                if (imgs.Count > 0)
                {
                    currLink.ImgSrc1stCol = imgs[0].GetAttribute("src");
                }


                HtmlElementCollection anchors = tds[1].GetElementsByTagName("a");
                //Console.WriteLine("anchors.Count = {0}", anchors.Count);
                if (anchors.Count > 0)
                {
                    currLink.GetHRef = anchors[0].GetAttribute("href");
                    currLink.FileName = anchors[0].GetAttribute("title");
                }

                HtmlElementCollection actionAnchors = tds[2].GetElementsByTagName("a");
                //Console.WriteLine("actionAnchors.Count = {0}", actionAnchors.Count);
                if (actionAnchors.Count > 0)
                {
                    currLink.ActionLinkOnClick = actionAnchors[0].GetAttribute("onclick");
                }

                links.Add(currLink);
            }

            //Console.WriteLine("links.Count = {0}", links.Count);

            foreach (ExUALinkInfo lnk in links)
            {
                string currExt = Path.GetExtension(lnk.FileName);
                //Console.WriteLine("currExt = '{0}' , lnk.FileName = '{1}', lnk.ImgSrc1stCol = '{2}', lnk.GetHRef = '{3}', lnk.ActionLinkOnClick = '{4}'", currExt, lnk.FileName, lnk.ImgSrc1stCol, lnk.GetHRef, lnk.ActionLinkOnClick);
                if (!FileExts.Contains(currExt))
                    continue;
                //if (lnk.ActionLinkOnClick.ToLower().IndexOf("play") == -1)
                //    continue;
                _mediaList.Add(lnk.GetHRef);
            }
            return true;
        }
    }
}
