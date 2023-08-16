using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib
{
    /// <summary>
    /// <see cref="https://docs.nunit.org/articles/nunit/technical-notes/usage/Test-Result-XML-Format.html#test-run"/>
    /// </summary>
    public class NunitTestRun : NunitTestResultNodeBase
    {
        public NunitTestSuite TestSuite { get; set; }
    }
}
