using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtilLib.BGU.Data
{
    public class BGUBankOwnStructInfo
    {
        public string BankName { get; set; }
        public string BankOwnStruPgUrl { get; set; }
        public List<BankOwnStructVersionInfo> OwnershipStructureVersions { get; set; }
    }
}
