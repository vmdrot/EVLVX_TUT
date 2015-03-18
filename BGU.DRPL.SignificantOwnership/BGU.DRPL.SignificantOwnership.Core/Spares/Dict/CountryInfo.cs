using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    public class CountryInfo
    {
        /// <summary>
        /// this and other fields as per http://userpage.chemie.fu-berlin.de/diverse/doc/ISO_3166.html
        /// </summary>
        public string CountryISO2Code { get; set; }
        public string CountryISO3Code { get; set; }
        public string CountryISONr { get; set; }
        public string CountryNameEng { get; set; }
        public string CountryNameUkr{ get; set; }
    }
}
