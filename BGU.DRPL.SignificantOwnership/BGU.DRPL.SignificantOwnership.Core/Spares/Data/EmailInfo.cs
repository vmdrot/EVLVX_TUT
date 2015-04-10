using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class EmailInfo
    {
        [DisplayName("Адреса ел.пошти")]
        public string Email { get; set; }
        [DisplayName("Примітки (необов'язково)")]
        public string EmailDescription { get; set; }

        public override string ToString()
        {
            return Email;
        }
    }
}
