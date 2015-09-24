using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Evolvex.VKUtilLib
{
    public abstract class WebBrowserReaderBase : IDisposable
    {
        protected WebBrowser _wc;
        private WebClient _wcLight;
        private bool _disposed = false;
        private volatile bool _navigateCompleted = false;
        private static int _maxDiscoverTimeOutSec = 120; //i.e. two minutes

        protected string _url;
        public static int MaxDiscoverTimeOut
        {
            get { return _maxDiscoverTimeOutSec; }
            set { _maxDiscoverTimeOutSec = value; }
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
            _wc = new WebBrowser();
            _wc.ScriptErrorsSuppressed = true;
            _wc.NewWindow += new System.ComponentModel.CancelEventHandler(_wc_NewWindow);
            //_wc.Navigated += new WebBrowserNavigatedEventHandler(_wc_Navigated);
            _wc.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(_wc_DocumentCompleted);


        }

        protected bool Navigate(string url)
        {
            _navigateCompleted = false;
            //_syncContext.Post(
            _wc.Navigate(new Uri(url));
            //Console.WriteLine("just _wc.Navigate -ed");
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
            e.Cancel = true;
        }

        private void _wc_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _navigateCompleted = true;
        }

        private void _wc_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            _navigateCompleted = true;
        }


        protected long GetFileSize(string url)
        {
            WCLight.OpenRead(url);
            long rslt = (long)Convert.ToInt64(WCLight.ResponseHeaders["Content-Length"]);
            
            return rslt;
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
            if (_wcLight != null) 
            {
                _wcLight.Dispose();
                _wcLight = null;
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
