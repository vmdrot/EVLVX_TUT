using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class SignatoryInfo
    {
        public string SignatoryPosition { get; set; }
        public string SurnameInitials { get; set; }
        public DateTime DateSigned { get; set; }
    }
}
