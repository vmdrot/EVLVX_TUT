using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class Action
    {
        public string id { get; set; }
        public string idMemberCreator { get; set; }
        public ActionData data { get; set; }
        public string type { get; set; }
        public string date { get; set; }
        public ActionMember member { get; set; }
        public ActionMemberCreator memberCreator { get; set; }
        public string text { get; set; }
        public string attachment { get; set; }
    }
}
