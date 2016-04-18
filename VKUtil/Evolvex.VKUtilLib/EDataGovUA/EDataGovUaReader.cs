using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Threading;

namespace Evolvex.VKUtilLib.EDataGovUA
{
    public class EDataGovUaReader : WebBrowserReaderBase
    {
        public static readonly string START_DISPOSERS_SEARCH_URL = "http://spending.gov.ua/web/guest/disposers";

        public string SearchForYeDRPOU { get; set; }
        

        protected override bool ReadWorker()
        {
            
            if(!string.IsNullOrEmpty(SearchForYeDRPOU))
            {
                if(!DoYeDRPOUSearch())
                    return false;
                //<div class="disposer_table_col">статус</div> </div> <div class="disposer_table_row" style="cursor: pointer;" id="10149200" senderName="ПУБЛІЧНЕ АКЦІОНЕРНЕ ТОВАРИСТВО «ДЕРЖАВНИЙ ЕКСПОРТНО-ІМПОРТНИЙ БАНК УКРАЇНИ»" senderCode="00032112">
                HtmlElement divDisposerRow = FindElementByTagAttribValue("div", "senderCode", SearchForYeDRPOU);
                if (divDisposerRow != null)
                    Console.WriteLine("divDisposerRow: {0}", divDisposerRow.OuterHtml);
                else
                    Console.WriteLine("divDisposerRow is null");
            }
            
            return true; //todo
        }

        private bool DoYeDRPOUSearch()
        {
            // Search criteria html:
            //<div class="wFormRow"> <input type="tel" value="" name="senderCode" placeholder="код ЄДРПОУ платника" class="wInput" data-rule-number="true" data-rule-minlength="8"> </div>
            //<div class="wFormRow row_submit"> <button type="submit" class="wSubmit button"><span>Пошук</span></button> </div>
            HtmlElement edYeDRPOU = FindElementByTagAttribValue("input", "placeholder", "код ЄДРПОУ платника");
            if(edYeDRPOU == null)
                return false;
            edYeDRPOU.SetAttribute("value", SearchForYeDRPOU);
            HtmlElement spanSubmit = FindElementByTagInnerText("span", "Пошук");
            if(spanSubmit == null)
                return false;
            HtmlElement elemSubmit = spanSubmit.Parent;
            IHTMLButtonElement btnSubmit = (IHTMLButtonElement)(elemSubmit.DomElement);
            base._navigateCompleted = false;
            if (btnSubmit != null)
                btnSubmit.form.submit();
            Console.WriteLine("Before Ready - {0}", DateTime.Now);
            bool bReady = WaitUntilBrowserReady();
            Console.WriteLine("After Ready - {0}", DateTime.Now);
            //while (!base._navigateCompleted)
            //{
            //    Thread.Sleep(100);
            //}
            Console.WriteLine("Thread.Sleep(...) - {0}", DateTime.Now);
            return WaitUntilBrowserReady();
        }

        
    }
}
