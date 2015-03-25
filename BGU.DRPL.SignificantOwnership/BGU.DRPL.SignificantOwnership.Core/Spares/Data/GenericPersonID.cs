using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Misc;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class GenericPersonID
    {
        public string CountryISO3Code { get; set; }
        public EntityType PersonType { get; set; }
        public string PersonCode { get; set; }
        private string _hashId;
        public string HashID
        {
            get
            {
                if (_hashId == null)
                    _hashId = string.Format("{0}-{1}-{2}", CountryISO3Code, PersonType, PersonCode);
                return _hashId;
            }
        }

        public static readonly GenericPersonID Empty = new GenericPersonID() { CountryISO3Code = string.Empty, PersonType = EntityType.None, PersonCode = string.Empty };

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(this.CountryISO3Code) && this.PersonType == EntityType.None && string.IsNullOrEmpty(this.PersonCode);
            }
        }

        public static bool operator ==(GenericPersonID one, GenericPersonID two)
        {
            if (!Utils.AreStringsEqual(one.CountryISO3Code, two.CountryISO3Code)
                || one.PersonType != two.PersonType
                || !Utils.AreStringsEqual(one.PersonCode, two.PersonCode))
                return false;
            return true;
        }

        public static bool operator !=(GenericPersonID one, GenericPersonID two)
        {
            if (!Utils.AreStringsEqual(one.CountryISO3Code, two.CountryISO3Code)
                || one.PersonType != two.PersonType
                || !Utils.AreStringsEqual(one.PersonCode, two.PersonCode))
                return true;
            return false;
        }
    }
}
