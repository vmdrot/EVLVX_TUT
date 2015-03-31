using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class IncomeOriginInfo
    {
        public FundsOriginType Origin { get; set; }
        public CurrencyAmount Income { get; set; }
        public string IncomeOriginNotes { get; set; }
    }
}
