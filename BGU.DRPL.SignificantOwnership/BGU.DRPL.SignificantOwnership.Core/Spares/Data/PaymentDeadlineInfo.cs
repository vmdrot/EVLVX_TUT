using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class PaymentDeadlineInfo
    {
        public CurrencyAmount Amount { get; set; }
        public DateTime Deadline { get; set; }
    }
}
