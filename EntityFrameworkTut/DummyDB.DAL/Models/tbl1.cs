using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DummyDB.DAL.Models
{
    public class tbl1
    {
        public int id { get; set; }
        public string nm { get; set; }
        public bool is_valid { get; set; }
        public decimal price { get; set; }
        public string descr { get; set; }
        public int cnt { get; set; }
    }
}
