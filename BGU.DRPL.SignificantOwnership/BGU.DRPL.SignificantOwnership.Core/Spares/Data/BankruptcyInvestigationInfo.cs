using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class BankruptcyInvestigationInfo
    {
        public DateTime CaseFiledDt { get; set; }
        public BankruptcyCaseResolutionType Resolution { get; set; }
        public CourtDecisionInfo CourtDecision { get; set; }
    }
}
