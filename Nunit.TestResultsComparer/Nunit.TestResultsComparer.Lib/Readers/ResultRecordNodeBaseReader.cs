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
            target.testcasecount = (int)GetAttrInt(src, nameof(target.testcasecount));
            target.passed = (int)GetAttrInt(src, nameof(target.passed));
            target.failed = (int)GetAttrInt(src, nameof(target.failed));
            target.total = (int)GetAttrInt(src, nameof(target.total));
            target.asserts = (int)GetAttrInt(src, nameof(target.asserts));
            target.skipped = (int)GetAttrInt(src, nameof(target.skipped));
            target.warnings = (int)GetAttrInt(src, nameof(target.warnings));
            target.inconclusive = (int)GetAttrInt(src, nameof(target.inconclusive));
        }

        protected string GetAttrStr(XmlNode src, string attrName)
        {
            var attr = src.Attributes[attrName];
            if (attr == null)
                return null;
            return attr.Value;
        }
        protected int? GetAttrInt(XmlNode src, string attrName)
        {
            string strAttr = GetAttrStr(src, attrName);
            if (string.IsNullOrWhiteSpace(strAttr))
                return null;
            int rslt;
            if (!int.TryParse(strAttr, out rslt))
                return null;
            return rslt;
        }
        protected decimal? GetAttrDec(XmlNode src, string attrName)
        {
            string strAttr = GetAttrStr(src, attrName);
            if (string.IsNullOrWhiteSpace(strAttr))
                return null;
            decimal rslt;
            if (!decimal.TryParse(strAttr, out rslt))
                return null;
            return rslt;
        }

        protected TEnum GetAttrEnum<TEnum>(XmlNode src, string attrName) where TEnum : struct
        {
            var strAttr = GetAttrStr(src, attrName);
            if (string.IsNullOrWhiteSpace(strAttr))
                return default(TEnum);
            TEnum rslt = default(TEnum);
            if(!Enum.TryParse<TEnum>(strAttr, out rslt))
                return default(TEnum);
            return rslt;
        }
    }
}
