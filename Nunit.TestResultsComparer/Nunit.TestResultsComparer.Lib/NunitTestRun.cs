using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib
{
    public class NunitTestRun : NunitTestResultNodeBase
    {
        public NunitTestSuite TestSuite { get; set; }
    }
}
