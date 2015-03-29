using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.SignatoryInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class SignatoryInfo
    {
        public string SignatoryPosition { get; set; }
        public string SurnameInitials { get; set; }
        public DateTime DateSigned { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2}", SignatoryPosition, SurnameInitials, DateSigned);
        }
    }
}
