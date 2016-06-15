using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Threading;
using System.Configuration;

namespace Evolvex.VKUtilLib.EDataGovUA
{
    public class EDataGovUaReader : WebBrowserReaderBase
    {
        public static readonly string START_DISPOSERS_SEARCH_URL = "http://spending.gov.ua/web/guest/disposers";
        public static readonly bool MakePausesBetweenBatches;
        public static readonly int PauseBatchSize;
        public static readonly int PauseLengthMs;
        public static readonly bool MakePausesBetweenBatches_Default = false;
        public static readonly int PauseBatchSize_Default = 100;
        public static readonly int PauseLengthMs_Default = 10*1000;
        public static readonly int PauseOnHttpErrorMs_Default = 60 * 1000;
        public static readonly int PauseOnHttpErrorMs;
        public static readonly int? PauseAfterEveryEntryMs;

        static EDataGovUaReader()
        {
            #region read config settings/substitute with defaults
            string MakePausesBetweenBatchesStr = ConfigurationManager.AppSettings["edataMakePauses"];
            {
                bool tmp;
                if (string.IsNullOrEmpty(MakePausesBetweenBatchesStr) || !bool.TryParse(MakePausesBetweenBatchesStr, out tmp))
                    MakePausesBetweenBatches = MakePausesBetweenBatches_Default;
                else
                    MakePausesBetweenBatches = tmp;
            }

            string PauseBatchSizeStr = ConfigurationManager.AppSettings["edataPauseBatchSize"];
            {
                int tmp;
                if (string.IsNullOrEmpty(PauseBatchSizeStr) || !int.TryParse(PauseBatchSizeStr, out tmp))
                    PauseBatchSize = PauseBatchSize_Default;
                else
                    PauseBatchSize = tmp;
            }
            string PauseLengthSecsStr = ConfigurationManager.AppSettings["edataPauseLengthSecs"];
            {
                int tmp;
                if (string.IsNullOrEmpty(PauseLengthSecsStr) || !int.TryParse(PauseLengthSecsStr, out tmp))
                    PauseLengthMs = PauseLengthMs_Default;
                else
                    PauseLengthMs = tmp*1000;
            }
            string PauseOnHttpErrorSecsStr = ConfigurationManager.AppSettings["edataPauseOnHttpErrorSecs"];
            {
                int tmp;
                if (string.IsNullOrEmpty(PauseOnHttpErrorSecsStr) || !int.TryParse(PauseOnHttpErrorSecsStr, out tmp))
                    PauseOnHttpErrorMs = PauseLengthMs_Default;
                else
                    PauseOnHttpErrorMs = tmp * 1000;
            }
            string PauseAfterEveryEntryMSecsStr = ConfigurationManager.AppSettings["edataPauseAfterEveryEntryMSecs"];
            {
                int tmp;
                if (string.IsNullOrEmpty(PauseAfterEveryEntryMSecsStr) || !int.TryParse(PauseAfterEveryEntryMSecsStr, out tmp))
                    PauseAfterEveryEntryMs = null;
                else
                    PauseAfterEveryEntryMs = tmp;
            }
            
            #endregion
        }

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
                    Console.WriteLine("divDisposerRow is null");
                    HtmlElement divNothingFound = FindElementByTagInnerText("div", "Нажаль, Нічого не знайдено.");
                    HtmlElement divTryWidenSearchCrit = FindElementByTagInnerText("div", "Спробуйте повторити пошук з більш розширенними параметрами.");
                    if (divNothingFound != null || divTryWidenSearchCrit != null)
                    {
                        Console.WriteLine("divNothingFound = {0}", divNothingFound != null ? divNothingFound.OuterHtml : "null");
                        Console.WriteLine("divTryWidenSearchCrit = {0}", divTryWidenSearchCrit != null ? divTryWidenSearchCrit.OuterHtml : "null");
                        string cabStatus = divNothingFound != null ? divNothingFound.InnerText : divTryWidenSearchCrit.InnerText;
                        this.Result = ConstructNotFoundDisposerInfo();
                        this.Result.CabinetStatus = cabStatus;
                    }
                    else
                    {
                        Console.WriteLine("divDisposerRow, divNothingFound and divTryWidenSearchCrit are null's");
                        this.Result = ConstructNotFoundDisposerInfo();
                        this.Result.HttpStatus = LastHttpStatus;
                    }
                    
                    
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
            
            // disposer rows can be broken, e.g.:
            //<div class="disposer_table_row" style="cursor: pointer;" id="136307085" sendername="" Державний="" науково="" -="" дослідний,="" проектно="" конструкторський="" і="" проектний="" інститут="" вугільної="" промисловості="" "УКРНДІПРОЕКТ""="" sendercode="00174125"> <div class="disposer_table_col">"Державний науково - дослідний, проектно - конструкторський і проектний інститут вугільної промисловості "УКРНДІПРОЕКТ"</div> <div class="disposer_table_col"><span class="disposer_table_mobile">код ЄДРПОУ: </span>00174125</div> <div class="disposer_table_col">зареєстровано</div> </div>

            //rslt.YeDRPOU = ReadDivAttribValue(divDisposerRow, "sendercode");
            //rslt.DisposerName = ReadDivAttribValue(divDisposerRow, "sendername").Replace((char)34,(char)39);
            HtmlElementCollection innerDivs = divDisposerRow.GetElementsByTagName("div");
            if (innerDivs.Count == 3)
            {
                //since the disposer rows can be broken (see previous comment), reading the name and YeDRPOU from the columns' inner texts, rather than from the div's attributes:
                rslt.YeDRPOU = innerDivs[0].InnerText.Trim();
                rslt.DisposerName = innerDivs[1].InnerText.Trim().Replace((char)34, (char)39);

                //cabinet status can only be retrieved from inner text of the rightmost column:
                rslt.CabinetStatus = innerDivs[2].InnerText.Trim();
            }
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
            if (spanSubmit == null)
            {
                if (LogDebugEvents) Console.WriteLine("spanSubmit == null", DateTime.Now);
                return false;
            }
            HtmlElement elemSubmit = spanSubmit.Parent;
            IHTMLButtonElement btnSubmit = (IHTMLButtonElement)(elemSubmit.DomElement);
            base._navigateCompleted = false;
            _yeDRPOUSearchNavigated = false;
            _yeDRPOUSearchStatusTextDone = false;
            base.WC_Navigated += new WebBrowserNavigatedEventHandler(YeDRPOUSearch_WC_Navigated);
            base.WC_StatusTextChanged += new EventHandler(YeDRPOUSearch_WC_StatusTextChanged);
            if (btnSubmit != null)
            {
                if (LogDebugEvents) Console.WriteLine("btnSubmit != null", DateTime.Now);
                btnSubmit.form.submit();
            }
            else
            {
                if (LogDebugEvents) Console.WriteLine("btnSubmit != null", DateTime.Now);
                return false;
            }
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
