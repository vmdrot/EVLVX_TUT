using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares2;
using Evolvex.Utility.Core.Geo;

namespace BGU.DRPL.SignificantOwnership.Facade.Search
{
    public class GenericFinInstSearchCriteria : SearchCriteriaBase
    {
        public string InstitutionSearchText { get; set; }
        public string InstitutionTaxCode { get; set; }
        public FinancialInstitutionType InstType { get; set; }
        public FinancialInstitutionStatus InstStatus { get; set; }
        public CompanyOwnershipType OwnershipType { get; set; }
        public string ActivitiesSearchText { get; set; }
        public BasicGeoposition NearPoint { get; set; }
        public double? InRangeKm { get; set; }
    }
}
