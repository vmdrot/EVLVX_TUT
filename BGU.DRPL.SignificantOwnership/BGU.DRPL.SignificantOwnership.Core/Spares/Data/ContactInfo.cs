using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.ContactInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class ContactInfo
    {
        public ContactInfo()
        {
            Phones = new List<PhoneInfo>();
            Emails = new List<string>();
        }
        public PhysicalPersonInfo Person { get; set; }
        public List<PhoneInfo> Phones { get; set; }
        public string Fax { get; set; }
        public List<string> Emails { get; set; }

        public override string ToString()
        {
            return Person.ToString();
        }
    }
}
