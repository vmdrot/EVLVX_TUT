using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class CouncilBodyInfo
    {
        public List<CouncilMemberInfo> Members { get; set; }
        public int HeadMemberIndex { get; set; }
    }
}
