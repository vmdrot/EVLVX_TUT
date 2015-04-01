using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PaymentModeInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PaymentModeInfo
    {
        public CurrencyAmount Amount { get; set; }
        public PaymentType PaymentMode { get; set; }
    }
}
