using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    public class ProfessionLicensingBodyInfo
    {
        public string Name { get; set; }
        public LegalPersonInfo LegalPerson { get; set; }
        public LocationInfo Address { get; set; }
    }
}
