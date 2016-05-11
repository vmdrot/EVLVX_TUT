using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtilLib.Rexton.Spares
{
    public class RextonRecordInfo
    {
        public string RextonID { get; set; }
        public string NYM { get; set; }
        public string PhoneNr { get; set; }
        public RextonObjectPx Px { get; set; }
        public int A1 { get; set; }
        public int H1 { get; set; }
        public int B1 { get; set; }
        public int NW { get; set; }
        public string[] TTags { get; set; }
        public string[] TJNSTR { get; set; }
        public int[] RelIDs { get; set; }
        public RextonFBInfo[] FB { get; set; }
    }
}
