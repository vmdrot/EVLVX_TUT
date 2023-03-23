using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib
{
    public class NunitTestCase : NunitTestResultRecordBase
    {
        public string name { get; set; }
        public string fullname { get; set; }
        public string classname { get; set; }
        public string methodname { get; set; }
        public long seed { get; set; }
        public string label { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FailureSite site { get; set; }
    }
}
