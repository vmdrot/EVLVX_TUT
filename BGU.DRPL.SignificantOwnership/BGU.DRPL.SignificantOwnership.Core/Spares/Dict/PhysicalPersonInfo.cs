using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    public class PhysicalPersonInfo
    {
        public string SurnameUkr { get; set; }
        public string NameUkr { get; set; }
        public string MiddleNameUkr { get; set; }
        public string FullNameUkr { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public SexType Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string PassportID { get; set; }
        public DateTime PassIssuedDate { get; set; }
        public RegistrarAuthority PassIssueAuthority { get; set; }
        public string TaxOrSocSecID { get; set; }
        public LocationInfo Address { get; set; }
        public CountryInfo CitizenshipCountry { get; set; }

        public GenericPersonID GenericID { get { return new GenericPersonID() { CountryISO3Code = CitizenshipCountry.CountryISONr, PersonCode = TaxOrSocSecID ?? PassportID , PersonType = EntityType.Physical }; } }
    }
}
