using Nunit.TestResultsComparer.Lib.Data.TestResultsXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nunit.TestResultsComparer.Lib.Readers
{
    public class TestRunReader : ResultRecordNodeBaseReader
    {
        public NunitTestRun Read(XmlNode src)
        {
            NunitTestRun rslt = new NunitTestRun();
            base.Read(src, rslt);
            rslt.id = GetAttrStr(src, nameof(rslt.id));

            rslt.testcasecount = (int)GetAttrInt(src, nameof(rslt.testcasecount));
            XmlNode testSuite = src.SelectSingleNode("./test-suite");
            if (testSuite != null)
                rslt.TestSuite = (new TestSuiteReader()).Read(testSuite);
            return rslt;
        }
    }
}
