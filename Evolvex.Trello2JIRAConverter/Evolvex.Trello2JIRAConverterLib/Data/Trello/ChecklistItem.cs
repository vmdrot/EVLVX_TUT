using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class ChecklistItem
    {
        public string state { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string pos { get; set; }
    }
}
