using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.SignatoryInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class SignatoryInfo
    {
        [Description("Посада (підписанта)")]
        public string SignatoryPosition { get; set; }
        [Description("Прізвище й ініціали (підписанта)")]
        public string SurnameInitials { get; set; }
        [Description("Дата підпису")]
        public DateTime DateSigned { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2}", SignatoryPosition, SurnameInitials, DateSigned);
        }
    }
}
