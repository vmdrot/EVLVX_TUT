using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Ruthenorum.JIRAAuth.Utilities
{
    public class Tools
    {
        public static string EncodeJQL(string bareJQL)
        {
            return bareJQL.Replace("=","%3D").Replace(" ", "%20");
        }
    }
}
