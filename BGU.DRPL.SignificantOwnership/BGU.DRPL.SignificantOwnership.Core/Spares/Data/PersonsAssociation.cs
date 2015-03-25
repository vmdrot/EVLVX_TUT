using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class PersonsAssociation
    {
        public GenericPersonID One { get; set; }
        public GenericPersonID Two { get; set; }
        public OwnershipType AssociationType { get; set; }
        public string AssociationTitleOneVsTwo { get; set; }
        public string AssociationTitleTwoVsOne { get; set; }
    }
}
