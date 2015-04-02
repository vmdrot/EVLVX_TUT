using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PhoneInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PhoneInfo
    {
        [DisplayName("Тип/назва телефону")]
        [Description("Тип/назва телефону")]
        public string PhoneName { get; set; }
        [DisplayName("№ телефону")]
        [Description("№ телефону")]
        public string PhoneNr { get; set; }
        [DisplayName("Примітки щодо телефону")]
        [Description("Примітки щодо телефону")]
        public string PhoneNotes { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2})", PhoneName, PhoneNr, PhoneNotes);
        }
    }
}
