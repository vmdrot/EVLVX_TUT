using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib
{
    /// <summary>
    /// <see cref="https://docs.nunit.org/articles/nunit/technical-notes/usage/Test-Result-XML-Format.html#test-suite"/>
    /// </summary>
    public class NunitTestSuite : NunitTestSuiteCaseBase
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TestSuiteType type { get; set; }
        public List<NunitTestSuite> TestSuites {get;set;}
        public NunitTestSuite TestSuite { get { return TestSuites?.FirstOrDefault(); } }
        public List<NunitTestCase> TestCases { get; set; }
        public string label { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FailureSite site { get; set; }
    }
}
