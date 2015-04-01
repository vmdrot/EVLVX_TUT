using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.UniversityOrCollegeInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class UniversityOrCollegeInfo
    {
        public string UniversityName { get; set; }
        public string UniversityNameUkr { get; set; }
        public LocationInfo Address { get; set; }
        public string UniversityID { get; set; }
    }
}
