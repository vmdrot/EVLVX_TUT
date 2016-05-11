using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.VKUtilLib.Rexton.Spares;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

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
            List<string> objUrls = PrepareObjLinksList(pgsUrls);
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
                _wc.Navigate(objUrl);
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
            //rslt.Px = ReadPx(); //todo
            //todo - the rest of fields
            return rslt;
        }

        private RextonObjectPx ReadPx()
        {
            throw new NotImplementedException();
        }

        private string GetPhoneNr()
        {
            HtmlElement el = base.FindElementByTagAttribValue("div", "class", "ico-phone");
            if (el == null)
                return string.Empty;
            return el.InnerText.Trim();
        }

        private string GetNYM()
        {
            HtmlElement el = base.FindElementByTagAttribValue("span", "id", "model_name");
            if (el == null)
                return string.Empty;
            return el.InnerText.Trim();
        }

        private string GetID()
        {
            HtmlElement el = base.FindElementByTagAttribValue("span", "id", "model_id");
            if (el == null)
                return string.Empty;
            return el.InnerText.Trim();
        }

        private List<string> PrepareObjLinksList(List<string> pgsUrls)
        {
            List<string> rslt = new List<string>();
            foreach (string url in pgsUrls)
            {
                _wc.Navigate(url);
                //todo - catch / wait events;
                List<string> curr = ReadObjLinksFromPage();
                foreach(string objUrl in curr)
                {
                    if (rslt.Contains(objUrl))
                        continue;
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
                string currHref = ReadDivAttribValue(anc, "href");
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
