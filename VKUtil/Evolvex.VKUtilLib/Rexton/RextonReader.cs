using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.VKUtilLib.Rexton.Spares;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace Evolvex.VKUtilLib.Rexton
{
    public class RextonReader : WebBrowserReaderBase
    {
        private static JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        public string StartPgUrl { get; set; }
        public List<RextonRecordInfo> Result { get; set; }

        protected override bool ReadWorker()
        {
            List<string> pgsUrls = DetectPages();
            Console.WriteLine("Detected {0} page urls\r\n{1}", pgsUrls.Count, JsonConvert.SerializeObject(pgsUrls, JSON_SETTINGS));
            List<string> objUrls = PrepareObjLinksList(pgsUrls);
            Console.WriteLine("Detected {0} object urls\r\n{1}", objUrls.Count, JsonConvert.SerializeObject(objUrls, JSON_SETTINGS));
            //List<RextonRecordInfo> existingObjs = null;
            //if(File.Exists(TargetPath)) 
            //    existingObjs = JsonConvert.DeserializeObject<List<RextonRecordInfo>>(File.ReadAllText(TargetPath), JSON_SETTINGS);
            //else
            //    existingObjs = new List<RextonRecordInfo>();
            Result = ReadAllRecs(objUrls);
            //File.WriteAllText(TargetPath, JsonConvert.SerializeObject(recs, JSON_SETTINGS)
            return true;
        }

        private List<RextonRecordInfo> ReadAllRecs(List<string> objUrls)
        {
            List<RextonRecordInfo> rslt = new List<RextonRecordInfo>();
            foreach (string objUrl in objUrls)
            {
                base.Navigate(objUrl);
                //todo - wait events, etc.
                RextonRecordInfo curr = ReadRecord();
                rslt.Add(curr);
            }
            return rslt;
        }

        private RextonRecordInfo ReadRecord()
        {
            RextonRecordInfo rslt = new RextonRecordInfo();
            rslt.RextonID = GetID();
            rslt.NYM = GetNYM();
            rslt.PhoneNr = GetPhoneNr();
            rslt.A1 = GetA1();
            rslt.TTags = GetTTags();
            rslt.Px = ReadPx(); //todo
            //todo - the rest of fields
            return rslt;
        }

        private HtmlElement GetCurrObjectDiv()
        {
            return FindElementByTagAttribValue("div", "class", "product-view");
        }
        private string[] GetTTags()
        {
            return null; //todo
        }

        private int GetA1()
        {
            HtmlElement lblSpan = FindElementByTagInnerText(GetCurrObjectDiv(), "span", "Возраст :");
            if (lblSpan == null || lblSpan.Parent == null || lblSpan.Parent.TagName.ToLower() != "div")
                return 0;
            string a1String = lblSpan.Parent.InnerHtml.Replace(lblSpan.OuterHtml, "");
            int tmp;
            if (int.TryParse(a1String, out tmp))
                return tmp;
            return 0; 
        }

        private RextonObjectPx ReadPx()
        {

            HtmlElement pxTitleDiv = FindElementByTagInnerText(GetCurrObjectDiv(), "div", "Стоимость услуг");
            if (pxTitleDiv == null)
                return null;
            HtmlElement pxContDiv = pxTitleDiv.NextSibling;
            if (pxContDiv.TagName.ToLower() != "div")
                return null;
            string pxContDivCls = ReadDivAttribValue(pxContDiv, "class");
            if (string.IsNullOrEmpty(pxContDivCls) || pxContDivCls.ToLower().Trim() != "line")
                return null;
            
            HtmlElementCollection spans = pxContDiv.GetElementsByTagName("span");
            if (spans == null || spans.Count == 0)
                return null;

            RextonObjectPx rslt = new RextonObjectPx();
            string lastNr = null;
            string lastCcy = null;
            foreach (HtmlElement span in spans)
            {
                string currCls = ReadDivAttribValue(span, "class");
                string currTxt = span.InnerText;
                if (currCls.ToLower().Trim() == "label")
                    lastNr = currTxt;
                else if (currCls.ToLower().Trim() == "p5") ;
                {
                    lastCcy = currTxt;
                    if(lastNr.IndexOf("1 ") != -1)
                    {
                        rslt.Single = new CcyAmt(lastCcy, lastNr);
                    }
                    else if (lastNr.IndexOf("2 ") != -1)
                    {
                        rslt.Pair = new CcyAmt(lastCcy, lastNr);
                    }
                    else
                    {
                        rslt.Wholesale = new CcyAmt(lastCcy, lastNr);
                    }
                }


            }
            return rslt;

        }

        private string GetPhoneNr()
        {
            HtmlElement el = base.FindElementByTagAttribValue(GetCurrObjectDiv(), "div", "class", "ico-phone", false);
            if (el == null)
                return string.Empty;
            return el.InnerText.Trim().Replace("(", "").Replace(")","").Replace(" ", "").Replace("-", "").Replace("+", "");
        }

        private string GetNYM()
        {
            HtmlElement el = base.FindElementByTagAttribValue(GetCurrObjectDiv(), "span", "id", "model_name", false);
            if (el == null)
                return string.Empty;
            return el.InnerText.Trim();
        }

        private string GetID()
        {
            HtmlElement el = base.FindElementByTagAttribValue(GetCurrObjectDiv(), "span", "id", "model_id", false);
            if (el == null)
                return string.Empty;
            return el.InnerText.Trim();
        }

        private List<string> PrepareObjLinksList(List<string> pgsUrls)
        {
            List<string> rslt = new List<string>();
            foreach (string url in pgsUrls)
            {
                base.Navigate(url);
                //Thread.Sleep(3000); //workaround
                Console.WriteLine("PrepareObjLinksList: Navigated page {0}", url);
                //todo - catch / wait events;
                List<string> curr = ReadObjLinksFromPage();
                Console.WriteLine("PrepareObjLinksList: ReadObjLinksFromPage() = \r\n {0}", JsonConvert.SerializeObject(curr));
                foreach(string objUrl in curr)
                {
                    if (rslt.Contains(objUrl))
                    {
                        Console.WriteLine("An object url already exists - {0}", objUrl);
                        continue;
                    }
                    rslt.Add(objUrl);
                }
            }
            return rslt;
        }

        private List<string> ReadObjLinksFromPage()
        {
            const string marker = "/modeli/";
            List<string> rslt = new List<string>();
            HtmlElementCollection allAnchors = _wc.Document.GetElementsByTagName("a");
            foreach (HtmlElement anc in allAnchors)
            {
                string currHref = anc.GetAttribute("href");
                int markerPos = currHref.IndexOf(marker);
                if (markerPos == -1)
                    continue;
                string objIdStr = currHref.Substring(markerPos + marker.Length);
                int tmp;
                if (!int.TryParse(objIdStr, out tmp))
                    continue;
                rslt.Add(currHref);
            }
            return rslt;
        }



        private List<string> DetectPages()
        {
            List<string> rslt = new List<string>();
            HtmlElementCollection anchors = _wc.Document.GetElementsByTagName("a");
            int maxPageNr = -1;
            foreach (HtmlElement anc in anchors)
            {
                string currHref = ReadDivAttribValue(anc, "href");
                if (currHref.IndexOf("?page=") == -1)
                    continue;
                string innerTxt = anc.InnerText;
                if (string.IsNullOrEmpty(innerTxt))
                    continue;
                if (innerTxt.Trim() != "Последняя")
                    continue;

                int eqSgnPos = currHref.IndexOf('=');
                if (eqSgnPos == -1)
                    continue;
                string maxPgNrStr = currHref.Substring(eqSgnPos + 1);
                int tmp;
                if (!int.TryParse(maxPgNrStr, out tmp))
                    continue;
                maxPageNr = tmp;
                break;
            }
            rslt.Add(StartPgUrl);
            for(int i = 2; i <= maxPageNr; i++)
                rslt.Add(string.Format("{0}?pg={1}", StartPgUrl, i));
            return rslt;

        }
    }
}
