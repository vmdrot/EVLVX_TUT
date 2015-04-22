using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Інформація про правопорушення/судимість (зокрема, у анкеті на кандидатів у керівники/члени наглядової ради)
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BreachOfLawRecordInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BreachOfLawRecordInfo
    {

        public BreachOfLawRecordInfo()
        {
            JurisdictionCountry = CountryInfo.UKRAINE;
        }
        /// <summary>
        /// Обрати з переліку, обов'язкове, хоча б Інше
        /// </summary>
        [DisplayName("Тип правопорушення")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public BreachOfLawType BreachType { get; set; }
        /// <summary>
        /// Суд, що виніс рішення, обов'язкове поле
        /// </summary>
        [DisplayName("Суд")]
        public string CourtName { get; set; }
        /// <summary>
        /// Суд, що виніс рішення - укр. (якщо іноземна юрисдикція)
        /// </summary>
        [DisplayName("Суд (укр.)")]
        public string CourtNameUkr { get; set; }
        /// <summary>
        /// Юрисдикція, за змовчанням - Україна
        /// </summary>
        [DisplayName("Країна підсудності")]
        public CountryInfo JurisdictionCountry { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Дата вироку")]
        public DateTime SentenceDate { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Закон/кодекс")]
        public string CodeOrLaw { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Стаття(-і)")]
        public string Articles { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Тип вироку")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public SentenceType Sentence { get; set; }
        /// <summary>
        /// якщо були (інші санкції)
        /// </summary>
        [DisplayName("Інші санкції")]
        public string OtherSanctionDetails { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Судимість погашена?")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public bool IsConvictionSettled { get; set; }
        /// <summary>
        /// якщо IsConvictionSettled == true, то обов'язкове
        /// </summary>
        [DisplayName("Дата погашення судимості")]
        public DateTime? SettledDate { get; set; }
    }
}
