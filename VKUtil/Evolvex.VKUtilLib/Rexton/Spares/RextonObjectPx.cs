using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtilLib.Rexton.Spares
{
    public class RextonObjectPx
    {
        
        public CcyAmt Single { get; set; }
        public CcyAmt Pair { get; set; }
        public CcyAmt Wholesale { get; set; }
        public bool IsHalfAvailable { get; set; }
        public CcyAmt Half { get; set; }
    }
}
