using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// https://osvita.net/ua/checkdoc/
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EducationRecordInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class EducationRecordInfo
    {
        [DisplayName("ВНЗ")]
        [Description("ВНЗ - університет, інститут, коледж, тощо")]
        public UniversityOrCollegeInfo UniOrCollege { get; set; }
        [DisplayName("Дата закінчення")]
        public DateTime GraduationDate { get; set; }
        [DisplayName("Тип диплома")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public HigherEducationDegreeType DegreeType { get; set; }
        [DisplayName("Тип відзнаки")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public DegreeHonourType HonourType { get; set; }
        [DisplayName("Серія диплома")]
        public string DegreeSeries { get; set; }
        [DisplayName("№ диплома")]
        public string DegreeID { get; set; }
        [DisplayName("Спеціальність/фах")]
        public string Trade { get; set; }
        [DisplayName("Спеціалізація")]
        public string Qualification { get; set; }
    }
}
