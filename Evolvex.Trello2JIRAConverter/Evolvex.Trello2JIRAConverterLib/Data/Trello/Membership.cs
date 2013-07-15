
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class Membership
    {
        public string id { get; set; }
        public string idMember { get; set; }
        public string memberType { get; set; }
        public string deactivated { get; set; }
        public string unconfirmed { get; set; }
    }
}
