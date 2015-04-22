using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    /// <summary>
    /// Ліцензійна організація
    /// (напр. проф.комісія, що надає ліцензії для заняття 
    /// тим чи іншим видом професійної діяльності, чи ж видає сертифікат - галузевий чи професійний)
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.ProfessionLicensingBodyInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class ProfessionLicensingBodyInfo
    {
        [DisplayName("Назва ліцензіатора")]
        public string Name { get; set; }
        [DisplayName("Реквізити юрособи (ліцензіатора)")]
        public LegalPersonInfo LegalPerson { get; set; }
    }
}
