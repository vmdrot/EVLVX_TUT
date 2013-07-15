using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class Preference
    {
        public string permissionLevel { get; set; }
        public string voting { get; set; }
        public string comments { get; set; }
        public string invitations { get; set; }
        public string selfJoin { get; set; }
        public string cardCovers { get; set; }
        public string canBePublic { get; set; }
        public string canBeOrg { get; set; }
        public string canBePrivate { get; set; }
        public string canInvite { get; set; }
    }
}
