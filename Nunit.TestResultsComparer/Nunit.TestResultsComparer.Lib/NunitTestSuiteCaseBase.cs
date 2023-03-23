using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib
{
    public class NunitTestSuiteCaseBase : NunitTestResultNodeBase
    {
        public string name { get; set; }
        public string fullname { get; set; }
        public string classname { get; set; }
    }
}
