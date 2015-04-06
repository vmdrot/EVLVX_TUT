using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class TotalOwnershipDetailsInfoEx : TotalOwnershipDetailsInfo
    {
        public TotalOwnershipDetailsInfoEx() :base()
        { 
        }

        public TotalOwnershipDetailsInfoEx(TotalOwnershipDetailsInfo src)
        {
            this.DirectOwnership = src.DirectOwnership;
            this.ImplicitOwnership = src.ImplicitOwnership;
            this.AcquiredVotes = src.AcquiredVotes;
            this.TotalCapitalSharePct = src.TotalCapitalSharePct;
            this.TotalVotes = src.TotalVotes;
        }

        public TotalOwnershipDetailsInfoEx(TotalOwnershipDetailsInfo src, GenericPersonID id, string dispName)
        {
            this.DirectOwnership = src.DirectOwnership;
            this.ImplicitOwnership = src.ImplicitOwnership;
            this.AcquiredVotes = src.AcquiredVotes;
            this.TotalCapitalSharePct = src.TotalCapitalSharePct;
            this.TotalVotes = src.TotalVotes;
            this.OwnerID = id;
            this.OwnerDisplayName = dispName;
        }

        [DisplayName("ID Власника")]
        public GenericPersonID OwnerID { get; set; }
        [DisplayName("Власник")]
        public string OwnerDisplayName { get; set; }
    }
}
