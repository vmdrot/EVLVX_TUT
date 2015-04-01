using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

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
        [Description("Контактна особа")]
        public PhysicalPersonInfo Person { get; set; }
        [Description("Телефони")]
        public List<PhoneInfo> Phones { get; set; }
        [Description("Факс")]
        public string Fax { get; set; }
        [Description("E-mail-и")]
        public List<string> Emails { get; set; }
        [DisplayName("www")]
        [Description("Веб-сайт")]
        public Uri wwww { get; set; }

        public override string ToString()
        {
            return Person.ToString();
        }
    }
}
