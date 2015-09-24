using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.VKUtilLib.BGU.Data;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Evolvex.VKUtilLib.BGU
{
    public class BGUSiteReader : WebBrowserReaderBase
    {
        public List<BGUBankOwnStructInfo> BankStructs { get; private set; }
        private string _startUrl { get; set; }
        protected override bool ReadWorker()
        {
            this.BankStructs = new List<BGUBankOwnStructInfo>();
            HtmlElementCollection links =_wc.Document.GetElementsByTagName("a");
            foreach (HtmlElement lnk in links)
            {
                string currHref = lnk.GetAttribute("href");
                if (currHref.ToLower().Trim().IndexOf("files/Shareholders/".ToLower()) == -1)
                    continue;
                BankStructs.Add(new BGUBankOwnStructInfo() { BankName = lnk.InnerText.Trim(), BankOwnStruPgUrl = currHref.Trim(), OwnershipStructureVersions = new List<BankOwnStructVersionInfo>() });
            }
            //int i = 0;
            foreach (BGUBankOwnStructInfo osi in this.BankStructs)
            {
                //if (i > 3)
                //    break;
                if (!Navigate(osi.BankOwnStruPgUrl))
                    continue;
                osi.OwnershipStructureVersions = ParseCurrentOwnershipStructureVersions();
                //i++;
            }
            return true;
        }

        private List<BankOwnStructVersionInfo> ParseCurrentOwnershipStructureVersions()
        {
            List<BankOwnStructVersionInfo> rslt = new List<BankOwnStructVersionInfo>();
            HtmlElementCollection anchors = _wc.Document.GetElementsByTagName("a");

            foreach (HtmlElement anchor in anchors)
            {
                string currHref = anchor.GetAttribute("href");
                string currInnerTxt = anchor.InnerText.Trim();
                if (Path.GetExtension(currHref).ToLower() != ".pdf")
                    continue;
                DateTime dt;
                if (!DateTime.TryParse(currInnerTxt, out dt))
                    continue;
                long fsz = -1;
                //try
                //{
                //    fsz = GetFileSize(currHref);
                //}
                //catch (Exception exc)
                //{ }
                rslt.Add(new BankOwnStructVersionInfo() { AsOf = dt, Url = currHref, FileSize = fsz });
            }
            return rslt;


        }
    }
}
