using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BreachOfLawRecordInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BreachOfLawRecordInfo
    {

        public BreachOfLawRecordInfo()
        {
            JurisdictionCountry = CountryInfo.UKRAINE;
        }
        [DisplayName("Тип правопорушення")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public BreachOfLawType BreachType { get; set; }
        [DisplayName("Суд")]
        public string CourtName { get; set; }
        [DisplayName("Країна підсудності")]
        public CountryInfo JurisdictionCountry { get; set; }
        [DisplayName("Дата вироку")]
        public DateTime SentenceDate { get; set; }
        [DisplayName("Закон/кодекс")]
        public string CodeOrLaw { get; set; }
        [DisplayName("Стаття(-і)")]
        public string Articles { get; set; }
        [DisplayName("Тип вироку")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.EnumLookupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public SentenceType Sentence { get; set; }
        [DisplayName("Інші санкції")]
        public string OtherSanctionDetails { get; set; }
        [DisplayName("Судимість погашена?")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public bool IsConvictionSettled { get; set; }
        [DisplayName("Дата погашення судимості")]
        public DateTime? SettledDate { get; set; }
    }
}
