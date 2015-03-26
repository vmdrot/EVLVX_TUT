using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.Spares;

namespace BGU.DRPL.SignificantOwnership.Core.Checks
{
    public class QuestionnaireCheckUtils
    {
        public static List<GenericPersonID> ExtractPhysicsOnly(List<OwnershipStructure> haystack)
        {
            return ExtractPersons(haystack, EntityType.Physical);
        }

        public static List<GenericPersonID> ExtractLegalsOnly(List<OwnershipStructure> haystack)
        {
            return ExtractPersons(haystack, EntityType.Legal);
        }
        
        public static List<GenericPersonID> ExtractPersons(List<OwnershipStructure> haystack, EntityType filterByType)
        {
            List<GenericPersonID> rslt = new List<GenericPersonID>();
            List<string> hashes = new List<string>();
            foreach (OwnershipStructure o in haystack)
            {
                if (filterByType != EntityType.None)
                {
                    if (((int)o.Owner.PersonType & (int)filterByType) == 0)
                        continue;
                }
                if (hashes.Contains(o.Owner.HashID))
                    continue;
                rslt.Add(o.Owner);
                hashes.Add(o.Owner.HashID);
            }
            return rslt;
        }

        public static GenericPersonInfo FindPersonByID(List<GenericPersonInfo> lst, GenericPersonID id)
        {
            foreach (GenericPersonInfo gpi in lst)
            {
                if (gpi.ID == id)
                    return gpi;
            }

            return null;
        }

    }
}
