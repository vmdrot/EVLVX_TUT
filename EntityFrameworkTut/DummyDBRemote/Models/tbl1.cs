using System;
using System.Collections.Generic;

namespace DummyDBRemote.Models
{
    public class tbl1
    {
        public int id { get; set; }
        public string nm { get; set; }
        public Nullable<bool> is_valid { get; set; }
        public Nullable<decimal> price { get; set; }
        public string descr { get; set; }
        public Nullable<int> cnt { get; set; }
    }
}
