using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Evolvex.VKUtilLib.Spares.Other
{
    public class WebBrowserNavigationErrorArgs : EventArgs
    {
        public WebBrowserNavigationErrorArgs(HttpStatusCode status, string url) : base ()
        {
            this.HttpStatus = status;
            this.Url = url;
        }

        public HttpStatusCode HttpStatus { get; private set; }
        public string Url { get; private set; }
    }
}
