using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib
{
    public abstract class NunitTestResultNodeBase : NunitTestResultRecordBase
    {
        public int testcasecount { get; set; }
        public int total { get; set; }
        public int passed { get; set; }
        public int failed { get; set; }
        public int warnings { get; set; }
        public int inconclusive { get; set; }
        public int skipped { get; set; }
        public int asserts { get; set; }
        

    }
}
