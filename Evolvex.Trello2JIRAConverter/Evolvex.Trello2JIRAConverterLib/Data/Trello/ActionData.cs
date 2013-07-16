using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class ActionData
    {
        public ActionDataBoard board { get; set; }
        public ActionDataCard card { get; set; }
        public string idMember { get; set; }
        public string text { get; set; }
    }
}
