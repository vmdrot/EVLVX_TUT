using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares
{
    [Flags]
    public enum  EntityType
    {
        None = 0,
        Physical,
        Legal
    }

    public enum CommonOwnershipType
    {
        None = 0,
        Direct,
        Implicit
    }

    [Flags]
    public enum SexType
    {
        None = 0,
        Male,
        Female
    }
}
