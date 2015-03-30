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
        [Description("Тип особи")]
        public EntityType PersonType { get; set; }
        [Description("Фізична особа")]
        public PhysicalPersonInfo PhysicalPerson { get; set; }
        [Description("Юридична особа")]
        public LegalPersonInfo LegalPerson { get; set; }

        public GenericPersonInfo()
        { }

        public GenericPersonInfo(LegalPersonInfo lp)
        {
            this.PersonType = EntityType.Legal;
            this.LegalPerson = lp;
        }
        
        public GenericPersonInfo(PhysicalPersonInfo pp)
        {
            this.PersonType = EntityType.Physical;
            this.PhysicalPerson = pp;
        }

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


        [Browsable(false)]
        public LocationInfo Address
        {
            get
            {
                if (PersonType == EntityType.Legal && LegalPerson != null)
                    return LegalPerson.Address;
                if (PersonType == EntityType.Physical && PhysicalPerson != null)
                    return PhysicalPerson.Address;
                return null;
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
