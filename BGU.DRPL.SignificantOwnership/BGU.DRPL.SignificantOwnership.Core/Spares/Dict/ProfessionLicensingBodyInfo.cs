using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.ProfessionLicensingBodyInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class ProfessionLicensingBodyInfo
    {
        [DisplayName("Назва ліцензіатора")]
        public string Name { get; set; }
        [DisplayName("Реквізити юрособи (ліцензіатора)")]
        public LegalPersonInfo LegalPerson { get; set; }
    }
}
