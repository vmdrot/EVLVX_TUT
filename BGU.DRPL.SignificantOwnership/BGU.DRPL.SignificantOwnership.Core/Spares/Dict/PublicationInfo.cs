using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PublicationInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PublicationInfo
    {
        public PublishingHouseInfo Publisher { get; set; }
        // http://www.ukrbook.net/agentstvo.html
        public string ISBN { get; set; }
        public string MediaName { get; set; }
        public DateTime PubDate { get; set; }
        public string IssueNr { get; set; }
        public Uri PubUrl { get; set; }
    }
}
