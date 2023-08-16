using Nunit.TestResultsComparer.Lib.Data.TestResultsXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nunit.TestResultsComparer.Lib.Readers
{
    public class TestCaseReader : ResultRecordBaseReader
    {
        public NunitTestCase Read(XmlNode src)
        {
            NunitTestCase rslt = new NunitTestCase();
            base.Read(src, rslt);
            rslt.name = base.GetAttrStr(src, nameof(rslt.name));
            rslt.fullname = base.GetAttrStr(src, nameof(rslt.fullname));
            rslt.classname = base.GetAttrStr(src, nameof(rslt.classname));
            rslt.methodname = base.GetAttrStr(src, nameof(rslt.methodname));
            rslt.seed = (long)base.GetAttrLong(src, nameof(rslt.seed));
            rslt.label = base.GetAttrStr(src, nameof(rslt.label));
            rslt.site = base.GetAttrEnum<FailureSite>(src, nameof(rslt.site));
            return rslt;
        }
    }
}
