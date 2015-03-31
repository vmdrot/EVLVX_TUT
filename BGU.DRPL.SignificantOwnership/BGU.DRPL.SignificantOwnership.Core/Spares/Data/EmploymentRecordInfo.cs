using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class EmploymentRecordInfo
    {
        public GenericPersonID Employer { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateQuit { get; set; }
        public EmploymentState State { get; set; }
        public EmploymentTerminationType TerminationType { get; set; }
        public string DismissalOrUnemployedReason { get; set; }
        public ContactInfo EmployerContact { get; set; }
    }
}
