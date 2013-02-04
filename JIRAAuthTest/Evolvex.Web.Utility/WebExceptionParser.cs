using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace Evolvex.Web.Utility
{
    public class WebExceptionParser
    {
        public static HttpStatusCode? Parse(string excMsg)
        {
            Regex r = new Regex("\\([0-9]{3}\\)");
            Match m = r.Match(excMsg);
            if(m == null || m.Groups == null || m.Groups.Count ==0 || m.Groups[0].Captures == null || m.Groups[0].Captures.Count == 0)
                return null;
            string match = m.Groups[0].Captures[0].Value;
            int iStatus = int.Parse(match.Substring(1, 3));
            return (HttpStatusCode)iStatus;
        }
    }
}
