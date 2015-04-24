﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;
using Evolvex.Utility.Core.ComponentModelEx;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Інформація про документ про освіту (вищу, неповну, академічну, тощо)
    /// </summary>
    /// <seealso cref="https://osvita.net/ua/checkdoc/"/>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EducationRecordInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class EducationRecordInfo
    {
        /// <summary>
        /// Обов'язкове поле
        /// </summary>
        [DisplayName("ВНЗ")]
        [Description("ВНЗ - університет, інститут, коледж, тощо")]
        [Required]
        public UniversityOrCollegeInfo UniOrCollege { get; set; }
        /// <summary>
        /// Обов'язкове поле; достатньо рік, або рік і місяць
        /// </summary>
        [DisplayName("Дата закінчення")]
        [Required]
        public DateTime GraduationDate { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Тип диплома")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Required]
        public HigherEducationDegreeType DegreeType { get; set; }
        /// <summary>
        /// необов'язкове поле (подавачі самі зацікавлені)
        /// </summary>
        [DisplayName("Тип відзнаки")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public DegreeHonourType HonourType { get; set; }
        /// <summary>
        /// Якщо український диплом - обов'язкове; 
        /// іншої країни - на віру
        /// </summary>
        [DisplayName("Серія диплома")]
        [Required]
        public string DegreeSeries { get; set; }
        /// <summary>
        /// Якщо український диплом - обов'язкове;
        /// 
        /// </summary>
        /// <seealso cref="DegreeSeries"/>
        [DisplayName("№ диплома")]
        [Required]
        public string DegreeID { get; set; }
        /// <summary>
        /// Обов'язково
        /// </summary>
        [DisplayName("Спеціальність/фах")]
        [Required]
        public string Trade { get; set; }
        /// <summary>
        /// Якщо передбачено
        /// </summary>
        [DisplayName("Спеціалізація")]
        public string Qualification { get; set; }
    }
}
