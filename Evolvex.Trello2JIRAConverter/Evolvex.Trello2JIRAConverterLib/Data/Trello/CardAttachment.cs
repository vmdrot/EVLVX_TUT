using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class CardAttachment
    {
        public string id { get; set; }
        public string bytes { get; set; }
        public string date { get; set; }
        public string idMember { get; set; }
        public string isUpload { get; set; }
        public string mimeType { get; set; }
        public string name { get; set; }
        public string previews { get; set; }
        public string url { get; set; }
    }
}
