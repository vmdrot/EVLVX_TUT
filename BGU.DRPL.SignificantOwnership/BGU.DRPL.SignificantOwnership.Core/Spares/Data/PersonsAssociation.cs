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
        [DisplayName("Перша особа")]
        [Description("Перша особа")]
        public GenericPersonID One { get; set; }
        [DisplayName("Друга особа")]
        [Description("Друга особа")]
        public GenericPersonID Two { get; set; }
        [DisplayName("Тип зв'язку")]
        [Description("Тип зв'язку")]
        public OwnershipType AssociationType { get; set; }
        [DisplayName("Ким приходиться перша особа другій")]
        [Description("Назва, ким приходиться перша особа другій")]
        public string AssociationTitleOneVsTwo { get; set; }
        [DisplayName("Ким приходиться друга особа першій")]
        [Description("Назва, ким приходиться друга особа першій")]
        public string AssociationTitleTwoVsOne { get; set; }

        public override string ToString()
        {
            return string.Format("{0} vs {1}, {2} ({3}, {4})", One, Two, AssociationType, AssociationTitleOneVsTwo, AssociationTitleTwoVsOne);
        }
    }
}
