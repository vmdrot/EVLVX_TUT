using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class ContactInfo
    {
        public PhysicalPersonInfo Person { get; set; }
        public List<PhoneInfo> Phones { get; set; }
        public string Fax { get; set; }
        public List<string> Emails { get; set; }
    }
}
