using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BankingLicensedActivityInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BankingLicensedActivityInfo
    {
        [DisplayName("Вид діяльності")]
        [Description("Назва виду діяльності")]
        public string ActivityName { get; set; }
        //todo

        public override string ToString()
        {
            return ActivityName;
        }
    }
}
