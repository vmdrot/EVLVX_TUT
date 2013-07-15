using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class ActionMemberCreator
    {
        public string id { get; set; }
        public string avatarHash { get; set; }
        public string fullName { get; set; }
        public string initials { get; set; }
        public string username { get; set; }
    }
}
