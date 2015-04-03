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
        public string www { get; set; }

        public override string ToString()
        {
            StringBuilder rslt = new StringBuilder();
            int i = 0;
            if (Person != null)
            {
                rslt.Append(Person.ToString());
                i++;
            }
            if (Phones != null && Phones.Count > 0)
            {
                if (i > 0)
                    rslt.Append(", ");
                rslt.Append(Phones[0].ToString());
                i++;
            }
            if (Emails != null && Emails.Count > 0)
            {
                if (i > 0)
                    rslt.Append(", ");
                rslt.Append(Emails[0].ToString());
                i++;
            }
            //if (www != null && string.IsNullOrEmpty(www.ToString()))
            //{
            //    if (i > 0)
            //        rslt.Append(", ");
            //    rslt.Append(www.ToString());
            //    i++;
            //}
            if (string.IsNullOrEmpty(www))
            {
                if (i > 0)
                    rslt.Append(", ");
                rslt.Append(www);
                i++;
            }
            return rslt.ToString();
        }
    }
}
