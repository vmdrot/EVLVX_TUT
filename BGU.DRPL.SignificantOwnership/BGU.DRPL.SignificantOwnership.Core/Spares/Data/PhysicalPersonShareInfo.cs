using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PhysicalPersonShareInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PhysicalPersonShareInfo
    {
        public PhysicalPersonInfo Person { get; set; }
        public decimal SharePct { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}%", Person, SharePct);
        }
    }
}
