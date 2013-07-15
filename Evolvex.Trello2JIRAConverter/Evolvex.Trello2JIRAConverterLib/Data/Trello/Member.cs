using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class Member
    {
        public string id { get; set; }
        public string avatarHash { get; set; }
        public string bio { get; set; }
        public string confirmed { get; set; }
        public string fullName { get; set; }
        public List<string> idPremOrgsAdmin { get; set; }
        public string initials { get; set; }
        public string memberType { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public string username { get; set; }
    }
}
