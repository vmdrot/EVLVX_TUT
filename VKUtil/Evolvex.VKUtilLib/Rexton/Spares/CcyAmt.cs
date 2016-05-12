using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtilLib.Rexton.Spares
{
    public class CcyAmt
    {

        public CcyAmt() { }
        public CcyAmt(string ccy, string amtStr)
        {
            this.Ccy = ccy;
            decimal tmp;
            if (decimal.TryParse(amtStr, out tmp))
                Amt = tmp;
        }
        public string Ccy { get; set; }
        public decimal Amt { get; set; }
    }
}
