using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.GenericPersonInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class GenericPersonInfo
    {
        public EntityType PersonType { get; set; }
        public PhysicalPersonInfo PhysicalPerson { get; set; }
        public LegalPersonInfo LegalPerson { get; set; }

        [Browsable(false)]
        public GenericPersonID ID 
        { 
            get 
            { 
                object o = PersonType == EntityType.Physical ? (object)PhysicalPerson : (object)LegalPerson;
                if(o == null)
                    return null;
                return PersonType == EntityType.Physical ? PhysicalPerson.GenericID : LegalPerson.GenericID;
            } 
        }
        
        public string DisplayName
        {
            get
            {
                object o = PersonType == EntityType.Physical ? (object)PhysicalPerson : (object)LegalPerson;
                if (o == null)
                    return null;
                string rslt = PersonType == EntityType.Physical ? PhysicalPerson.FullName : LegalPerson.Name;
                if (!string.IsNullOrEmpty(rslt))
                    return rslt;
                return this.ID.HashID;
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
