using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// https://osvita.net/ua/checkdoc/
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EducationRecordInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class EducationRecordInfo
    {
        public UniversityOrCollegeInfo UniOrCollege { get; set; }
        public DateTime GraduationDate { get; set; }
        public HigherEducationDegreeType DegreeType { get; set; }
        public DegreeHonourType HonourType { get; set; }
        public string DegreeSeries { get; set; }
        public string DegreeID { get; set; }
        public string Trade { get; set; }
        public string Qualification { get; set; }
    }
}
