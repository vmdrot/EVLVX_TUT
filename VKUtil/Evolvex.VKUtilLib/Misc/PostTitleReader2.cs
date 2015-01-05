using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using Evolvex.VKUtilLib.Spares.Data;


namespace Evolvex.VKUtilLib.Misc
{
    public class PostTitleReader2 : IDisposable
    {
        private WebBrowser _wc;
        private bool _disposed = false;
        private volatile bool _navigateCompleted = false;
        private static int _maxDiscoverTimeOutSec = 120; //i.e. two minutes
        public static int MaxDiscoverTimeOut
        {
            get { return _maxDiscoverTimeOutSec; }
            set { _maxDiscoverTimeOutSec = value; }
        }

        public PostTitleReader2()
        {
            _wc = new WebBrowser();
            _wc.ScriptErrorsSuppressed = true;
            _wc.NewWindow += new System.ComponentModel.CancelEventHandler(_wc_NewWindow);
            //_wc.Navigated += new WebBrowserNavigatedEventHandler(_wc_Navigated);
            _wc.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(_wc_DocumentCompleted);


        }
        public VKPostInfo Read(string url)
        {
            _navigateCompleted = false;
            //_syncContext.Post(
            _wc.Navigate(new Uri(url));
            DateTime dtStart = DateTime.Now;
            while (_wc.ReadyState != WebBrowserReadyState.Complete)
            {
                //System.Threading.Thread.Sleep(300);
                Application.DoEvents();
                if (((TimeSpan)(DateTime.Now - dtStart)).TotalSeconds > _maxDiscoverTimeOutSec)
                {

                    return null;
                }
            }

            //while (!_navigateCompleted)
            //{
            //    System.Threading.Thread.Sleep(300);
            //}

            VKPostInfo rslt = new VKPostInfo();
            HtmlElementCollection divs = _wc.Document.GetElementsByTagName("div");
            foreach (HtmlElement el in divs)
            {
                string currClass = el.GetAttribute("class");
                if (string.IsNullOrEmpty(currClass))
                {

                    //mshtml.IHTMLElement2 divObj2 = (mshtml.IHTMLElement2)el.DomElement;
                    //mshtml.IHTMLElement3 divObj3 = (mshtml.IHTMLElement3)el.DomElement;
                    mshtml.IHTMLElement4 divObj4 = (mshtml.IHTMLElement4)el.DomElement;
                    IHTMLDOMAttribute attr = divObj4.getAttributeNode("class");
                    //currClass = ((mshtml.HTMLDivElementClass)el.DomElement).attributes["class"].Value;
                    if (attr != null)
                    {
                        currClass = attr.nodeValue;
                    }
                }

                if (string.IsNullOrEmpty(currClass))
                    continue;
                if (currClass == "page_media_caption" || currClass == "wall_post_text")
                {
                    rslt.Title = el.InnerText;
                    break;
                }
            }

            //"page_post_thumb_sized_photo"
            HtmlElementCollection imgs = _wc.Document.GetElementsByTagName("img");
            foreach (HtmlElement img in imgs)
            {
                string currClass = img.GetAttribute("class");
                string currSrc = img.GetAttribute("src");
                if (string.IsNullOrEmpty(currClass))
                {

                    //mshtml.IHTMLElement2 divObj2 = (mshtml.IHTMLElement2)el.DomElement;
                    //mshtml.IHTMLElement3 divObj3 = (mshtml.IHTMLElement3)el.DomElement;
                    mshtml.IHTMLElement4 imgObj = (mshtml.IHTMLElement4)img.DomElement;
                    IHTMLDOMAttribute attr = imgObj.getAttributeNode("class");
                    //currClass = ((mshtml.HTMLDivElementClass)el.DomElement).attributes["class"].Value;
                    if (attr != null)
                    {
                        currClass = attr.nodeValue;
                    }
                    IHTMLDOMAttribute attrSrc = imgObj.getAttributeNode("src");
                    if(attrSrc != null)
                        currSrc = attrSrc.nodeValue;
                }

                if (string.IsNullOrEmpty(currClass))
                    continue;
                if (currClass == "page_post_thumb_sized_photo")
                {
                    
                    rslt.Img = currSrc;
                    break;
                }
            }

            if (rslt.IsEmpty)
                return null;
            return rslt;
        }

        void _wc_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        void _wc_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _navigateCompleted = true;
        }

        void _wc_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            _navigateCompleted = true;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void DisposeManagedResources()
        {
            if (_wc != null)
            {
                _wc.Dispose();
                _wc = null;
            }
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
