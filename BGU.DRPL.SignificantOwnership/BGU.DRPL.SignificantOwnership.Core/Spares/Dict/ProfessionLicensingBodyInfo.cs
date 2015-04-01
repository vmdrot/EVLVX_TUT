using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.ProfessionLicensingBodyInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class ProfessionLicensingBodyInfo
    {
        public string Name { get; set; }
        public LegalPersonInfo LegalPerson { get; set; }
        public LocationInfo Address { get; set; }
    }
}
