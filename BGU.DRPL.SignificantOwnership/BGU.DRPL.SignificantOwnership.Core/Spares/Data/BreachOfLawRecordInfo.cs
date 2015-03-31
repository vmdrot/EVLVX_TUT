using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class BreachOfLawRecordInfo
    {
        public BreachOfLawType BreachType { get; set; }
        public string CourtName { get; set; }
        public CountryInfo JurisdictionCountry { get; set; }
        public DateTime SentenceDate { get; set; }
        public string CodeOrLaw { get; set; }
        public string Articles { get; set; }
        public SentenceType Sentence { get; set; }
        public string OtherSanctionDetails { get; set; }
        public bool IsConvictionSettled { get; set; }
        public DateTime? SettledDate { get; set; }
    }
}
