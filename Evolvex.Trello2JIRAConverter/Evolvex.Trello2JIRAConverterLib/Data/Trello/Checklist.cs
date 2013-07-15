using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class Checklist
    {
        public string id { get; set; }
        public string name { get; set; }
        public string idBoard { get; set; }
        public string idCard { get; set; }
        public string pos { get; set; }
        public List<ChecklistItem> checkItems { get; set; }
    }
}
