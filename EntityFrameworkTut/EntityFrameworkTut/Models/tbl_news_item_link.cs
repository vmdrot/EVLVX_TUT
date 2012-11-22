using System;
using System.Collections.Generic;

namespace EntityFrameworkTut.Models
{
    public class tbl_news_item_link
    {
        public int news_item_link_id { get; set; }
        public int news_item_id { get; set; }
        public int portal_id { get; set; }
        public string link { get; set; }
    }
}
