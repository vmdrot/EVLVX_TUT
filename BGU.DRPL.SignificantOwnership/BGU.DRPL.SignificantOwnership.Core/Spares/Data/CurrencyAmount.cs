using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.CurrencyAmount_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class CurrencyAmount
    {
        public string CCY { get; set; }
        public decimal Amt { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", CCY, Amt.ToString("N0"));
        }
    }
}
