using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    public class BankAccountInfo
    {
        public GenericPersonID AccountOwner { get; set; }
        public BankInfo Bank { get; set; }
        public string AccountNr { get; set; }
        public string AccountCCY { get; set; }
        public string AccountDescription { get; set; }
    }
}
