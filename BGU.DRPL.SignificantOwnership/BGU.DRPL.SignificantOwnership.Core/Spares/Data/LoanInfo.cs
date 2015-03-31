﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class LoanInfo
    {
        public GenericPersonID Lender { get; set; }
        public GenericPersonID Borrower { get; set; }
        public string AgreementNr { get; set; }
        public DateTime AgreementDt { get; set; }
        public CurrencyAmount Principal { get; set; }
        public DateTime RepaymentDueDate { get; set; }
        public CurrencyAmount OutstandingPricipal { get; set; }
        public CurrencyAmount PrincipalOverdue { get; set; }
        public DateTime? OverdueSince { get; set; }
        public string OverdueDetails { get; set; }
        public string OverdueReasons { get; set; }
    }
}
