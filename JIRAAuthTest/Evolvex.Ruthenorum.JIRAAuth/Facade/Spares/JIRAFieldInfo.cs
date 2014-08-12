using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Ruthenorum.JIRAAuth.Facade.Spares
{
    public class JIRAFieldInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool custom { get; set; }
        public bool orderable { get; set; }
        public bool navigable { get; set; }
        public bool searchable { get; set; }
        public JIFAFieldSchema schema { get; set; }
        public class JIFAFieldSchema
        {
            public string type { get; set; }
            public string custom { get; set; }
            public int customId { get; set; }
        }
    }
}
