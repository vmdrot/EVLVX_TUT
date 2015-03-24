using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class GenericPersonID
    {
        public string CountryISO3Code { get; set; }
        public EntityType PersonType { get; set; }
        public string PersonCode { get; set; }
    }
}
