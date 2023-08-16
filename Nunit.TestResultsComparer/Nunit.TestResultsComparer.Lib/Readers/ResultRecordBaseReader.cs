using Nunit.TestResultsComparer.Lib.Data.TestResultsXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nunit.TestResultsComparer.Lib.Readers
{
    public class ResultRecordBaseReader
    {
        public void Read(XmlNode src, NunitTestResultRecordBase target)
        {
            try
            {

                target.id = GetAttrStr(src, nameof(target.id));
                target.runstate = (RunState)GetAttrEnum<RunState>(src, nameof(target.runstate));
                target.result = (TestResult)GetAttrEnum<TestResult>(src, nameof(target.result));
                target.duration = GetAttrDec(src, nameof(target.duration));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error reading node {src.OuterXml}", ex);
            }
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

        protected long? GetAttrLong(XmlNode src, string attrName)
        {
            string strAttr = GetAttrStr(src, attrName);
            if (string.IsNullOrWhiteSpace(strAttr))
                return null;
            long rslt;
            if (!long.TryParse(strAttr, out rslt))
                return null;
            return rslt;
        }
    }
}
