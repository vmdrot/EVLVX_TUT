using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PersonsAssociation_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PersonsAssociation
    {
        [Description("Перша особа")]
        public GenericPersonID One { get; set; }
        [Description("Друга особа")]
        public GenericPersonID Two { get; set; }
        [Description("Тип зв'язку")]
        public OwnershipType AssociationType { get; set; }
        [Description("Назва, ким приходиться перша особа другій")]
        public string AssociationTitleOneVsTwo { get; set; }
        [Description("Назва, ким приходиться друга особа першій")]
        public string AssociationTitleTwoVsOne { get; set; }

        public override string ToString()
        {
            return string.Format("{0} vs {1}, {2} ({3}, {4})", One, Two, AssociationType, AssociationTitleOneVsTwo, AssociationTitleTwoVsOne);
        }
    }
}
