using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class PaymentModeInfo
    {
        public CurrencyAmount Amount { get; set; }
        public PaymentType PaymentMode { get; set; }
    }
}
