﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.LegalPersonShareInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class LegalPersonShareInfo
    {
        public LegalPersonInfo Person { get; set; }
        public decimal SharePct { get; set; }
    }
}
