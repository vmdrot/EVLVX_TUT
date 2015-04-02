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
        [DisplayName("Контактна особа")]
        [Description("Контактна особа (фізособа)")]
        public PhysicalPersonInfo Person { get; set; }
        [DisplayName("Телефони")]
        [Description("Перелік телефонів")]
        public List<PhoneInfo> Phones { get; set; }
        [DisplayName("Факс")]
        public string Fax { get; set; }
        [DisplayName("E-mail-и")]
        [Description("Перелік адрес електронної пошти")]
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
