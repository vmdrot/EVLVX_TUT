using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class CardsList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string closed { get; set; }
        public string idBoard { get; set; }
        public string pos { get; set; }
        public string subscribed { get; set; }
    }
}
