using Nunit.TestResultsComparer.Lib.Data.TestResultsXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nunit.TestResultsComparer.Lib.Readers
{
    public class TestSuiteReader : ResultRecordNodeBaseReader
    {
        public NunitTestSuite Read(XmlNode src)
        {
            NunitTestSuite rslt = new NunitTestSuite();
            base.Read(src, rslt);
            rslt.type = base.GetAttrEnum<TestSuiteType>(src, nameof(rslt.type));
            rslt.name = base.GetAttrStr(src, nameof(rslt.name));
            rslt.fullname = base.GetAttrStr(src, nameof(rslt.fullname));
            rslt.classname = base.GetAttrStr(src, nameof(rslt.classname));
            rslt.label = base.GetAttrStr(src, nameof(rslt.label));
            rslt.site = base.GetAttrEnum<FailureSite>(src, nameof(rslt.site));

            XmlNodeList testSuites = src.SelectNodes("./test-suite");
            if (testSuites != null && testSuites.Count > 0)
            {
                rslt.TestSuites = new List<NunitTestSuite>();
                foreach(XmlNode testSuite in testSuites)
                    rslt.TestSuites.Add((new TestSuiteReader()).Read(testSuite));
            }
            
            XmlNodeList tcNodes = src.SelectNodes("./test-case");
            if (tcNodes != null && tcNodes.Count > 0)
            {
                rslt.TestCases = new List<NunitTestCase>();
                foreach(XmlNode tcn in tcNodes)
                {
                    var rdr = new TestCaseReader();
                    rslt.TestCases.Add(rdr.Read(tcn));
                }
            }

            return rslt;
        }
    }
}
