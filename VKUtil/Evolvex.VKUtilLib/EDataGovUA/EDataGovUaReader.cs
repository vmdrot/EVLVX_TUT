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
        private volatile bool _yeDRPOUSearchNavigated = false;
        private volatile bool _yeDRPOUSearchStatusTextDone = false;
        public EDataGovUaDisposerInfo Result { get; private set; }
        protected override bool ReadWorker()
        {
            
            if(!string.IsNullOrEmpty(SearchForYeDRPOU))
            {
                if (!DoYeDRPOUSearch())
                {
                    this.Result = ConstructNotFoundDisposerInfo();
                    return false;
                }
                //<div class="disposer_table_col">статус</div> </div> <div class="disposer_table_row" style="cursor: pointer;" id="10149200" senderName="ПУБЛІЧНЕ АКЦІОНЕРНЕ ТОВАРИСТВО «ДЕРЖАВНИЙ ЕКСПОРТНО-ІМПОРТНИЙ БАНК УКРАЇНИ»" senderCode="00032112">
                HtmlElement divDisposerRow = FindElementByTagAttribValue("div", "senderCode", SearchForYeDRPOU, false);
                if (divDisposerRow != null)
                {
                    this.Result = ParseDisposerRow(divDisposerRow);
                    Console.WriteLine("divDisposerRow: {0}", divDisposerRow.OuterHtml);
                }
                else
                {
                    this.Result = ConstructNotFoundDisposerInfo();
                    Console.WriteLine("divDisposerRow is null");
                    return false;
                }
            }
            
            return true;
        }

        private EDataGovUaDisposerInfo ConstructNotFoundDisposerInfo()
        {
            return new EDataGovUaDisposerInfo() { IsFound = false, YeDRPOU = SearchForYeDRPOU, CheckedDttm = DateTime.Now };
        }

        private EDataGovUaDisposerInfo ParseDisposerRow(HtmlElement divDisposerRow)
        {
            if (LogDebugEvents) Console.WriteLine("ParseDisposerRow::divDisposerRow = '{0}'", divDisposerRow.OuterHtml);
            EDataGovUaDisposerInfo rslt = new EDataGovUaDisposerInfo();
            rslt.IsFound = true;
            rslt.CheckedDttm = DateTime.Now;
            rslt.InternalID = long.Parse(ReadDivAttribValue(divDisposerRow, "id"));
            rslt.YeDRPOU = ReadDivAttribValue(divDisposerRow, "sendercode");
            rslt.DisposerName = ReadDivAttribValue(divDisposerRow, "sendername");
            HtmlElementCollection innerDivs = divDisposerRow.GetElementsByTagName("div");
            if (innerDivs.Count == 3)
                rslt.CabinetStatus = innerDivs[2].InnerText;
            return rslt;
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
            _yeDRPOUSearchNavigated = false;
            _yeDRPOUSearchStatusTextDone = false;
            base.WC_Navigated += new WebBrowserNavigatedEventHandler(YeDRPOUSearch_WC_Navigated);
            base.WC_StatusTextChanged += new EventHandler(YeDRPOUSearch_WC_StatusTextChanged);
            if (btnSubmit != null)
                btnSubmit.form.submit();
            if (LogDebugEvents) Console.WriteLine("Before Ready - {0}", DateTime.Now);
            bool bReady = WaitUntilBrowserReady();
            if (LogDebugEvents) Console.WriteLine("After Ready - {0}", DateTime.Now);
            while (!_yeDRPOUSearchNavigated || !_yeDRPOUSearchStatusTextDone)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            if (LogDebugEvents) Console.WriteLine("Thread.Sleep(...) - {0}", DateTime.Now);
            base.WC_Navigated -= YeDRPOUSearch_WC_Navigated;
            base.WC_StatusTextChanged -= YeDRPOUSearch_WC_StatusTextChanged;
            return WaitUntilBrowserReady();
        }

        void YeDRPOUSearch_WC_StatusTextChanged(object sender, EventArgs e)
        {
            if (!_yeDRPOUSearchNavigated)
                return;
            if (_wc.StatusText != "Done")
                return;
            _yeDRPOUSearchStatusTextDone = true;
        }

        void YeDRPOUSearch_WC_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            _yeDRPOUSearchNavigated = true;
        }

        
    }
}
