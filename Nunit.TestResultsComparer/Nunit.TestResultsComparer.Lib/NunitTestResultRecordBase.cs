using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib
{
    public abstract class NunitTestResultRecordBase
    {
        public string id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RunState runstate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TestResult result { get; set; }
        public decimal duration { get; set; }

    }
}
