using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.UniversityOrCollegeInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class UniversityOrCollegeInfo
    {
        [DisplayName("Назва ВНЗ")]
        [Description("Назва вищого навчального закладу оригінальною мовою")]
        public string UniversityName { get; set; }
        [DisplayName("Назва ВНЗ (українською)")]
        [Description("Назва вищого навчального закладу оригінальною мовою українською (якщо оригінальна мова інша")]
        public string UniversityNameUkr { get; set; }
        [DisplayName("Адреса ВНЗ")]
        public LocationInfo Address { get; set; }
        [DisplayName("Ідентифікатор ВНЗ")]
        [Description("Ідентифікатор ВНЗ - якщо є/передбачений")]
        public string UniversityID { get; set; }
    }
}
