using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    public class BankInfo
    {
        /// <summary>
        /// GLMFO
        /// </summary>
        public string HeadMFO { get; set; }
        /// <summary>
        /// REGN
        /// </summary>
        public string RegistryNr { get; set; }
        /// <summary>
        /// GLB
        /// </summary>
        public string Code { get; set; }
        public string Name { get; set; }
        public LegalPersonInfo LegalPerson { get; set; }

        public BankInfo() { }
        public BankInfo(LegalPersonInfo le)
        {
            LegalPerson = le;
            Name = le.Name;
        }
    }
}
