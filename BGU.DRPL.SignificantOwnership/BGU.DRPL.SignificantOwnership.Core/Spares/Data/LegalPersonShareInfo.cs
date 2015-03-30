using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.LegalPersonShareInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class LegalPersonShareInfo
    {
        [Description("Юр.особа-власник")]
        public LegalPersonInfo Person { get; set; }
        [Description("Частка власності, %")]
        public decimal SharePct { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}%", Person, SharePct);
        }
    }
}
