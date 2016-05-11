using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using mshtml;

namespace Evolvex.VKUtilLib.YeDR
{
    public class UsrMinjustGovUaReader : WebBrowserReaderBase
    {
        public static readonly string FREE_SEARCH_URL = "https://usr.minjust.gov.ua/ua/freesearch";
        public static readonly string FREE_SEARCH_TRUE_URL = "https://usrinfo.irc.gov.ua/edr.html";
        public string SearchForYeDRPOU { get; set; }
        public UsrMinjustSearchResult Result { get; private set; }


        protected override bool ReadWorker()
        {
            if (!string.IsNullOrEmpty(SearchForYeDRPOU))
            {
                if (!DoYeDRPOUSearch())
                {
                    this.Result = UsrMinjustSearchResult.Empty;
                    return false;
                }
            }

            return true;
    
        }


        private bool DoYeDRPOUSearch()
        {
            const int sleepInt = 3000;
            // Search criteria html:
            //<div class="wFormRow"> <input type="tel" value="" name="senderCode" placeholder="код ЄДРПОУ платника" class="wInput" data-rule-number="true" data-rule-minlength="8"> </div>
            //<div class="wFormRow row_submit"> <button type="submit" class="wSubmit button"><span>Пошук</span></button> </div>
            HtmlElement chkbxLP = FindElementByTagAttribValue("input", "id", "yurcheck");
            if (chkbxLP == null)
                return false;
            chkbxLP.SetAttribute("checked", "true");
            Thread.Sleep(sleepInt);

            HtmlElement radioByYeDRPOU = FindElementByTagAttribValue("input", "id","shortname");
            if (radioByYeDRPOU == null)
                return false;
            radioByYeDRPOU.SetAttribute("checked", "true");
            Thread.Sleep(sleepInt);
            HtmlElement edYeDRPOU = FindElementByTagAttribValue("input", "id", "query");
            if (edYeDRPOU == null)
                return false;
            edYeDRPOU.SetAttribute("value", SearchForYeDRPOU);
            Thread.Sleep(sleepInt);
            HtmlElement elSubmit = FindElementByTagAttribValues("input", new string[] { "type", "value" }, new string[] { "submit", "Шукати" });
            if (elSubmit == null)
                return false;
            
            //IHTMLButtonElement btnSubmit = (IHTMLButtonElement)(elSubmit.DomElement);
            //if (btnSubmit == null)
            //    return false;

            //elSubmit.RaiseEvent("click");
            elSubmit.InvokeMember("Click");
            //elSubmit.RaiseEvent("onclick");



            return true;
        }
    }
}
