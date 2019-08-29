using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using mshtml;
using Evolvex.VKUtilLib.Spares.Other;

namespace Evolvex.VKUtilLib
{
    public abstract class WebBrowserReaderBase : IDisposable
    {
        protected WebBrowser _wc;
        private WebClient _wcLight;
        private bool _disposed = false;
        protected volatile bool _navigateCompleted = false;
        private static int _maxDiscoverTimeOutSec = 120; //i.e. two minutes
        protected static readonly string DELIM_LN = "----------------------------------------------------------------------";

        protected string _url;
        public static int MaxDiscoverTimeOut
        {
            get { return _maxDiscoverTimeOutSec; }
            set { _maxDiscoverTimeOutSec = value; }
        }

        public bool LogDebugEvents
        {
            get;
            set;
        }

        public int? LastHttpStatus
        {
            get;
            private set;
        }

        protected WebClient WCLight
        {
            get
            {
                if(_wcLight == null)
                    _wcLight = new WebClient();
                return _wcLight;
            }
        }
        public WebBrowserReaderBase()
        {
            LogDebugEvents = false;
            _wc = new WebBrowser();
            _wc.ScriptErrorsSuppressed = true;
            _wc.NewWindow += new System.ComponentModel.CancelEventHandler(_wc_NewWindow);
            _wc.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(_wc_DocumentCompleted);
            _wc.Navigated +=new WebBrowserNavigatedEventHandler(_wc_Navigated);
            _wc.FileDownload += new EventHandler(_wc_FileDownload);
            _wc.Navigating += new WebBrowserNavigatingEventHandler(_wc_Navigating);
            _wc.ProgressChanged += new WebBrowserProgressChangedEventHandler(_wc_ProgressChanged);
            _wc.StatusTextChanged += new EventHandler(_wc_StatusTextChanged);
            SHDocVw.WebBrowser shWc = (SHDocVw.WebBrowser)_wc.ActiveXInstance;
            if (shWc != null)
            {
                if (LogDebugEvents) Console.WriteLine("shWc != null");
                shWc.NavigateComplete2 += new SHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(shWc_NavigateComplete2);
                shWc.NavigateError += new SHDocVw.DWebBrowserEvents2_NavigateErrorEventHandler(shWc_NavigateError);
            }
        }

        private void shWc_NavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel)
        {
            int iStatus = (int)StatusCode;
            LastHttpStatus = iStatus;
            if (LogDebugEvents) 
            {
                
                Console.WriteLine("shWc_NavigateError(...,'{0}', {1})", URL as string, iStatus);
            }
            if (NavigationError != null)
                NavigationError(this, new WebBrowserNavigationErrorArgs((HttpStatusCode)iStatus, URL as string));
        }

        private void shWc_NavigateComplete2(object pDisp, ref object URL)
        {
            if (LogDebugEvents) Console.WriteLine("shWc_NavigateComplete2(..., {0})", URL as string);
        }

        #region event(s) propagation
        protected event System.ComponentModel.CancelEventHandler WC_NewWindow;
        protected event WebBrowserNavigatedEventHandler WC_Navigated;
        protected event WebBrowserDocumentCompletedEventHandler WC_DocumentCompleted;
        protected event EventHandler WC_FileDownload;
        protected event WebBrowserNavigatingEventHandler WC_Navigating;
        protected event WebBrowserProgressChangedEventHandler WC_ProgressChanged;
        protected event EventHandler WC_StatusTextChanged;
        public event EventHandler<WebBrowserNavigationErrorArgs> NavigationError; 
        #endregion


        void _wc_StatusTextChanged(object sender, EventArgs e)
        {
            if (LogDebugEvents)
            {
                Console.WriteLine("_wc_StatusTextChanged = '{0}'", _wc.StatusText);
                //Console.WriteLine(HTML);
                //Console.WriteLine(DELIM_LN);
            }
            if (WC_StatusTextChanged != null)
                WC_StatusTextChanged(sender, e);
        }

        void _wc_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (LogDebugEvents)
            {
                Console.WriteLine("_wc_ProgressChanged({0} of {1})", e.CurrentProgress, e.MaximumProgress);
                //Console.WriteLine(HTML);
                //Console.WriteLine(DELIM_LN);
            }
            if (WC_ProgressChanged != null)
                WC_ProgressChanged(sender, e);
        }

        void _wc_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            LastHttpStatus = null;
            if (LogDebugEvents)
            {
                Console.WriteLine("_wc_Navigating");
                //Console.WriteLine(HTML);
                //Console.WriteLine(DELIM_LN);
            }
            if (WC_Navigating != null)
                WC_Navigating(sender, e);
        }

        void _wc_FileDownload(object sender, EventArgs e)
        {
            if (LogDebugEvents)
            {
                Console.WriteLine("_wc_FileDownload");
                //Console.WriteLine(HTML);
                //Console.WriteLine(DELIM_LN);
            }
            if (WC_FileDownload != null)
                WC_FileDownload(sender, e);
        }

		protected HtmlElement FindChildElementByClass(HtmlElement parent, string tagName, string  classValue)
		{
			if (parent.Children == null || parent.Children.Count == 0)
				return null;
			foreach(HtmlElement child in parent.Children)
			{
				if (child.TagName.ToLower() != tagName.ToLower())
					continue;
				string cssClassName = ReadDivAttribValue(child, "class");
				if (string.IsNullOrWhiteSpace(cssClassName))
					continue;
				if (cssClassName == classValue)
					return child;
			}
			return null;
		}

		protected bool WaitUntilBrowserReady()
        {
            if(LogDebugEvents) Console.WriteLine("WaitUntilBrowserReady::_wc.ReadyState = {0}", _wc.ReadyState);
            DateTime dtStart = DateTime.Now;
            while (_wc.ReadyState != WebBrowserReadyState.Complete)
            {
                //System.Threading.Thread.Sleep(300);
                Application.DoEvents();
                if (((TimeSpan)(DateTime.Now - dtStart)).TotalSeconds > _maxDiscoverTimeOutSec)
                {

                    return false;
                }
            }
            return true;
        }
        protected bool Navigate(string url)
        {
            _navigateCompleted = false;
            //_syncContext.Post(
            _wc.Navigate(url);
            //if (LogDebugEvents) Console.WriteLine("WebBrowserReaderBase::Navigate('{0}').status = ", _wc.Document.);
            //Console.WriteLine("just _wc.Navigate -ed");
            return WaitUntilBrowserReady();
        }

        public bool Read(string url)
        {
            _url = url;
            if(Navigate(url))
                return ReadWorker();
            return false;
        }

        protected abstract bool ReadWorker();

        private void _wc_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (LogDebugEvents)
            {
                Console.WriteLine("_wc_NewWindow");
                //Console.WriteLine(HTML);
                //Console.WriteLine(DELIM_LN);
            }
            if (WC_NewWindow != null)
                WC_NewWindow(sender, e);
            else
                e.Cancel = true;
        }

        private void _wc_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _navigateCompleted = true;
            if (LogDebugEvents)
            {
                Console.WriteLine("_wc_DocumentCompleted({0})", e.Url);
                Console.WriteLine(HTML);
                Console.WriteLine(DELIM_LN);
            }
            if (WC_DocumentCompleted != null)
                WC_DocumentCompleted(sender, e);
        }

        private void _wc_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (LogDebugEvents)
            {
                Console.WriteLine("_wc_Navigated({0})", e.Url);
                Console.WriteLine(HTML);
                Console.WriteLine(DELIM_LN);
            }
            if (WC_Navigated != null)
                WC_Navigated(sender, e);
        }


        protected long GetFileSize(string url)
        {
            WCLight.OpenRead(url);
            long rslt = (long)Convert.ToInt64(WCLight.ResponseHeaders["Content-Length"]);
            
            return rslt;
        }

        protected static string ReadDivAttribValue(HtmlElement div, string attribName)
        {
            string rslt = div.GetAttribute(attribName);
            if (string.IsNullOrEmpty(rslt))
            {
                mshtml.IHTMLElement4 divObj4 = (mshtml.IHTMLElement4)div.DomElement;
                IHTMLDOMAttribute attr = divObj4.getAttributeNode(attribName);
                if (attr != null)
                {
                    if(attr.nodeValue.GetType() == typeof(System.DBNull))
                        return null;
                    rslt = attr.nodeValue;
                }
            }
            return rslt;
        }

        protected HtmlElement FindElementByTagAttribValue(string tagName, string attrName, string attrVal)
        {
            return FindElementByTagAttribValue(tagName, attrName, attrVal, true);
        }
        protected HtmlElement FindElementByTagAttribValue(string tagName, string attrName, string attrVal, bool isCaseSensitive)
        {
            return FindElementByTagAttribValue(this._wc.Document.Body, tagName, attrName, attrVal, isCaseSensitive);
        }
        protected HtmlElement FindElementByTagAttribValue(HtmlElement parent, string tagName, string attrName, string attrVal, bool isCaseSensitive)
        {
            HtmlElementCollection elems = parent.GetElementsByTagName(tagName);
            foreach (HtmlElement elem in elems)
            {
                try
                {
                    string currAttrVal = ReadDivAttribValue(elem, attrName);
                    int cmpRes = String.Compare(currAttrVal, attrVal, !isCaseSensitive);
                    if (cmpRes == 0)
                        return elem;
                }
                catch { continue; }
            }
            return null;
        }
        protected HtmlElement FindElementByTagAttribValues(string tagName, string[] attrNames, string[] attrVals)
        {
            return FindElementByTagAttribValues(this._wc.Document.Body, tagName, attrNames, attrVals);
        }

        protected HtmlElement FindElementByTagAttribValues(HtmlElement parent, string tagName, string[] attrNames, string[] attrVals)
        {
            HtmlElementCollection elems = parent.GetElementsByTagName(tagName);
            
            foreach (HtmlElement elem in elems)
            {
                try
                {
                    int matchedAttrValsNum = 0;
                    for (int ai = 0; ai < attrNames.Length; ai++)
                    {
                        string currAttrVal = ReadDivAttribValue(elem, attrNames[ai]);
                        if (currAttrVal == attrVals[ai])
                            matchedAttrValsNum++;
                    }
                    if (matchedAttrValsNum == attrNames.Length)
                        return elem;
                }
                catch { continue; }
            }
            return null;
        }

        protected HtmlElement FindElementByTagInnerText(string tagName, string innerTxt)
        {
            return FindElementByTagInnerText(this._wc.Document.Body, tagName, innerTxt);
        }

        protected HtmlElement FindElementByTagInnerText(HtmlElement parent, string tagName, string innerTxt)
        {
            HtmlElementCollection elems = parent.GetElementsByTagName(tagName);
            foreach (HtmlElement elem in elems)
            {
                string currTxt = elem.InnerText;
                if (currTxt == innerTxt)
                    return elem;
            }
            return null;
        }

        public static string ParseUrlGetParam(string url, string paramNm) 
        {
            Uri uri = new Uri(url);
            string query = uri.Query;
            if (query[0] == '?')
                query = query.Substring(1);
            string[] quPrms = query.Split('&');
            foreach (string quPrm in quPrms)
            {
                if(string.IsNullOrWhiteSpace(quPrm))
                    continue;
                string[] keyVal = quPrm.Split('=');
                if (keyVal == null || keyVal.Length == 0)
                    continue;
                if (keyVal[0] != paramNm)
                    continue;
                if (keyVal.Length > 1)
                    return keyVal[1];
                else
                    return null;
            }
            return null;
        }

        public void ShowBrowserIn(Control parent)
        {
            _wc.Parent = parent;
            _wc.Dock = DockStyle.Fill;
            _wc.Visible = true;
        }

        public string HTML
        {
            get
            {
                if(_wc != null && _wc.Document != null && _wc.Document.Body != null)
                    return _wc.Document.Body.OuterHtml;
                return string.Empty;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void DisposeCustomManagedResources() 
        { 
        }

        private void DisposeManagedResources()
        {
            if (_wc != null)
            {
                _wc.Dispose();
                _wc = null;
            }
            if (_wcLight != null) 
            {
                _wcLight.Dispose();
                _wcLight = null;
            }
            DisposeCustomManagedResources();
        }

        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {

                    DisposeManagedResources();
                }

                _disposed = true;

            }
        }

        #endregion
    }
}
