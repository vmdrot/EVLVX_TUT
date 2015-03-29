using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PersonsAssociation_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PersonsAssociation
    {
        public GenericPersonID One { get; set; }
        public GenericPersonID Two { get; set; }
        public OwnershipType AssociationType { get; set; }
        public string AssociationTitleOneVsTwo { get; set; }
        public string AssociationTitleTwoVsOne { get; set; }

        public override string ToString()
        {
            return string.Format("{0} vs {1}, {2} ({3}, {4})", One, Two, AssociationType, AssociationTitleOneVsTwo, AssociationTitleTwoVsOne);
        }
    }
}
