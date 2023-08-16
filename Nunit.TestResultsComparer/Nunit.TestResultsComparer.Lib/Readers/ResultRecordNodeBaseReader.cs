using Nunit.TestResultsComparer.Lib.Data.TestResultsXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nunit.TestResultsComparer.Lib.Readers
{
    public class ResultRecordNodeBaseReader : ResultRecordBaseReader
    {
        public void Read(XmlNode src, NunitTestResultNodeBase target)
        {
            base.Read(src, target);
            target.testcasecount = GetAttrInt(src, nameof(target.testcasecount));
            target.passed = GetAttrInt(src, nameof(target.passed));
            target.failed = GetAttrInt(src, nameof(target.failed));
            target.total = GetAttrInt(src, nameof(target.total));
            target.asserts = GetAttrInt(src, nameof(target.asserts));
            target.skipped = GetAttrInt(src, nameof(target.skipped));
            target.warnings = GetAttrInt(src, nameof(target.warnings));
            target.inconclusive = GetAttrInt(src, nameof(target.inconclusive));
        }
    }
}
