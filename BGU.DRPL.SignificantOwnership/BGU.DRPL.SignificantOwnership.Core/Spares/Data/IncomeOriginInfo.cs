using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.IncomeOriginInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class IncomeOriginInfo
    {
        public FundsOriginType Origin { get; set; }
        public CurrencyAmount Income { get; set; }
        public string IncomeOriginNotes { get; set; }
    }
}
