using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.IO;

namespace Evolvex.VKUtilLib.Zaycev
{
    public class ZaycevNetReader: WebBrowserReaderBase
    {

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookieEx(
            string url,
            string cookieName,
            StringBuilder cookieData,
            ref int size,
            Int32 dwFlags,
            IntPtr lpReserved);
        private const Int32 InternetCookieHttponly = 0x2000;


        #region field(s)
        private List<string> _mediaList;
        #endregion

        #region cctor(s)
        public ZaycevNetReader()
            : base()
        {
            SleepBetweenDownloadsMs = 100;
        }
        #endregion

        public List<string> MediaList
        {
            get
            {
                return _mediaList;
            }
        }

        public int SleepBetweenDownloadsMs { get; set; }

        protected override bool ReadWorker()
        {
            _mediaList = new List<string>();
            List<string> dataUrls = new List<string>();

            string nextPageUrl = string.Empty;
            do
            {
                if(!string.IsNullOrEmpty(nextPageUrl))
                    Navigate(nextPageUrl);
                HtmlElementCollection allDivs = _wc.Document.GetElementsByTagName("div");
                PrintElems(allDivs);
                List<HtmlElement> parentDiv = FilterDivsContainingClassName(allDivs, "musicset-track-list__items");
                if (parentDiv == null || parentDiv.Count == 0)
                    return false;
                List<HtmlElement> trackDivs = FilterDivsContainingClassName(parentDiv[0].Children, "musicset-track");

                
                foreach (HtmlElement div in trackDivs)
                {
                    //http://zaycev.net/musicset/play/e595f279f2af0c55727544b2eceb3641/171495.json

                    //data-url
                    string dataUrl = ReadDivAttribValue(div, "data-url");
                    if (string.IsNullOrEmpty(dataUrl))
                        continue;
                    UriBuilder ub = new UriBuilder();
                    Uri baseUri = new Uri(base._url);
                    ub.Scheme = baseUri.Scheme;
                    ub.Host = baseUri.Host;
                    ub.Path = dataUrl;
                    dataUrls.Add(ub.Uri.ToString());

                    //<div data-duration="297" data-url="/musicset/play/e595f279f2af0c55727544b2eceb3641/171495.json" data-mini-url="" data-id="171495" class="musicset-track musicset-track_new clearfix"><div class="musicset-track__title"><span class="musicset-track__control musicset__button"><i class="musicset-player__icon"></i></span><a target="_blank" class="musicset-track__artist musicset-track__link" href="/artist/37539">Dolores O`riordan</a><div class="musicset-track__track-dash">–</div><div class="musicset-track__track-name"><a href="/pages/1714/171495.shtml" class='musicset-track__link' target="_blank" >Black Widow</a></div></div><div class="musicset-track__duration">04:57</div><div class="musicset-track__download"><a href="/pages/1714/171495.shtml" class='musicset-track__download-link' target="_blank" ><span class="musicset__button musicset__button_download"><i class="musicset-icon musicset-icon_download"></i></span></a></div></div>

                    //sample json: '{"url":"http://dl.zaycev.net/37539/171495/dolores_o_riordan_-_black_widow.mp3?dlKind=play&format=json"}'
                }

                nextPageUrl = DetectNextPageUrl();
                
            } while (!string.IsNullOrEmpty(nextPageUrl));
            _mediaList.AddRange(ResolveData2DLUrls(dataUrls));
            return true;
        }

        private string DetectNextPageUrl()
        {
            const string nextPageClasses = "pager__item_last";

            List<HtmlElement> anchors = FilterDivsContainingClassName(_wc.Document.GetElementsByTagName("a"), nextPageClasses);
            if (anchors.Count == 0)
                return string.Empty;
            return ReadDivAttribValue(anchors[0], "href");
        }

        private void SetWCHeaders(WebClient wc)
        {
            wc.Headers.Add("Accept-Language: uk");
            wc.Headers.Add("Accept: */*");
            wc.Headers.Add("UA-CPU: AMD64");
            wc.Headers.Add("User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Win64; x64; Trident/7.0; .NET CLR 2.0.50727; SLCC2; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E)");
            wc.Headers.Add("Host: zaycev.net");
            //wc.Headers.Add("Connection: Keep-Alive"); //forbidden
            //setting cookies does the trick
            //wc.Headers.Add("Cookie: znbdc=\"H4sIAAAAAAAAAGNgYGAIWTUxnAGVPg8ALnt5GxgAAAA=\"; mm_cookie=1; __utma=84938386.796024452.1420458234.1420458234.1420458234.1; __utmb=84938386.16.10.1420458234; __utmz=84938386.1420458234.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); __utmt=1; __utmc=84938386; route=4769b160a9e78cefbdf5bc771bfccf1e; dlses=znOokN66nF5_upYYXRQs8mRlQiUJ6KxdZyby0rjRFPiAaSfOJoD31s7_dXPvM8qiJ8OsL-ZtZcq9z3S1Tcyu4qAalWqJ0jqF");
            Uri referrerUri = new Uri(_url);
            //Console.WriteLine(string.Format("Cookies from container: '{0}'", GetUriCookieContainer(referrerUri).GetCookieHeader(referrerUri)));
            wc.Headers.Add(String.Format("Cookie: {0}", GetUriCookieContainer(referrerUri).GetCookieHeader(referrerUri)));
            wc.Headers.Add(string.Format("Referer: {0}", _url));
        }

        private List<string> ResolveData2DLUrls(List<string> dataUrls)
        {
            List<string> rslt = new List<string>();
            using (WebClient wc = new WebClient())
            {
                SetWCHeaders(wc);
                foreach(string dataUrl in dataUrls)
                {
                    string currJson = wc.DownloadString(dataUrl);
                    //Console.WriteLine("currJson = '{0}'", currJson);
                    DataUrl du = JsonConvert.DeserializeObject<DataUrl>(currJson);
                    if (du == null || string.IsNullOrEmpty(du.url))
                    {
                        //Console.WriteLine("du is null or empty");
                        continue;
                    }
                    //Console.WriteLine("du.url = '{0}'", du.url);
                    rslt.Add(du.url);
                }
            }
            return rslt;
        }

        private List<string> ResolveData2DLUrls_IE(List<string> dataUrls)
        {
            List<string> rslt = new List<string>();
            
            {
                foreach (string dataUrl in dataUrls)
                {
                    _wc.NewWindow += new System.ComponentModel.CancelEventHandler(_wc_NewWindow);
                    _wc.FileDownload += new EventHandler(_wc_FileDownload);
                    //_wc.Navigating += new WebBrowserNavigatingEventHandler(_wc_Navigating);
                    //Console.WriteLine("dataUrl = '{0}'", dataUrl);
                    if (!Navigate(dataUrl))
                        continue;
                    string currJson = _wc.DocumentStream.ToString();
                    ////Console.WriteLine("currJson = '{0}'", currJson);
                    ////Console.WriteLine("_wc.DocumentText = '{0}'", _wc.DocumentText);
                    ////Console.WriteLine("_wc.Document.Body = '{0}'", _wc.Document.Body);
                    
                    continue;
                    DataUrl du = JsonConvert.DeserializeObject<DataUrl>(currJson);
                    if (du == null || string.IsNullOrEmpty(du.url))
                    {
                        //Console.WriteLine("du is null or empty");
                        continue;
                    }
                    //Console.WriteLine("du.url = '{0}'", du.url);
                    rslt.Add(du.url);
                }
            }
            return rslt;
        }

        void _wc_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //Console.WriteLine("_wc_Navigating(..., '{0}')", e.Url.ToString());
            if (e.Url.ToString().IndexOf(".json") != -1)
            {
                e.Cancel = true;
            }
        }

        void _wc_FileDownload(object sender, EventArgs e)
        {
            //Console.WriteLine("file download!");
            //Console.WriteLine("file download: e.type = {0}, e = {1}", e.GetType(), e.ToString());
        }

        void _wc_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Console.WriteLine("new window!");
            e.Cancel = true;
            
        }

        private void PrintElems(HtmlElementCollection allDivs)
        {
            foreach (HtmlElement el in allDivs)
            {
                ////Console.WriteLine(el.OuterHtml);
                ////Console.WriteLine("------------------------------------------------------------");
            }
        }

        private List<HtmlElement> FilterDivsContainingClassName(HtmlElementCollection divs, string className)
        {
            List<HtmlElement> rslt = new List<HtmlElement>();
            foreach (HtmlElement div in divs)
            {
                string classAttrRaw = ReadDivAttribValue(div, "class");
                ////Console.WriteLine("class = '{0}", classAttrRaw);
                if (string.IsNullOrEmpty(classAttrRaw))
                    continue;
                List<string> classNames = new List<string>(classAttrRaw.Split(' '));
                ////Console.WriteLine("class names array = [{0}]", String.Join<string>(",", classNames));
                if (classNames.Contains(className))
                    rslt.Add(div);

            }

            return rslt;
        }

        private static string ReadDivAttribValue(HtmlElement div, string attribName)
        {
            string rslt = div.GetAttribute(attribName);
            if (string.IsNullOrEmpty(rslt))
            {
                mshtml.IHTMLElement4 divObj4 = (mshtml.IHTMLElement4)div.DomElement;
                IHTMLDOMAttribute attr = divObj4.getAttributeNode(attribName);
                if (attr != null)
                {
                    rslt = attr.nodeValue;
                }
            }
            return rslt;
        }

        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            CookieContainer cookies = null;
            // Determine the size of the cookie
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;
                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(
                    uri.ToString(),
                    null, cookieData,
                    ref datasize,
                    InternetCookieHttponly,
                    IntPtr.Zero))
                    return null;
            }
            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }


        public void DownloadAll(List<string> urls, string save2Dir)
        {
            if(!Directory.Exists(save2Dir))
                Directory.CreateDirectory(save2Dir);
            using (WebClient wc = new WebClient())
            {
                SetWCHeaders(wc);
                bool sleep = false;
                foreach(string url in urls)
                {
                    if (sleep)
                        System.Threading.Thread.Sleep(SleepBetweenDownloadsMs);
                    sleep = false;
                    Uri uri = new Uri(url);
                    String saveAs = Path.Combine(save2Dir, uri.Segments[uri.Segments.Length - 1]);
                    if (!File.Exists(saveAs))
                    {
                        while (true)
                        {
                            try
                            {
                                wc.DownloadFile(url, saveAs);
                                sleep = true;
                                break;
                            }
                            catch (System.Net.WebException exc)
                            {
                                if (exc.Message.IndexOf("(404)") != -1)
                                    System.Threading.Thread.Sleep(2 * 60 * 1000);
                            }
                            catch (System.Exception)
                            {
                                break;
                            }
                        }
                    }
                }

            }
        }

        #region inner type(s)
        public class DataUrl
        {
            public string url { get; set; }
        }
        #endregion
    }
}
