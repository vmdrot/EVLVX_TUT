using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PaymentModeInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PaymentModeInfo
    {
        [DisplayName("Сума")]
        public CurrencyAmount Amount { get; set; }
        [DisplayName("Спосіб сплати")]
        public PaymentType PaymentMode { get; set; }
    }
}
