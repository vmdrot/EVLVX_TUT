using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Xml;

namespace BGU.DRPL.SignificantOwnership.Utility
{
    public class XSDReflectionUtil
    {
        public static Dictionary<string, PropDispDescr> FetchPropDispsDescrs(Type typ)
        {
            Dictionary<string, PropDispDescr> rslt = new Dictionary<string,PropDispDescr>();
            
            System.ComponentModel.PropertyDescriptorCollection pdc = System.ComponentModel.TypeDescriptor.GetProperties(typ);
            foreach (PropertyDescriptor pd in pdc)
            {
                string dispName = Tools.GetPropDisplayName(pd);
                string descr = Tools.GetPropDescription(pd);
                if (string.IsNullOrEmpty(dispName) && string.IsNullOrEmpty(descr))
                    continue;
                rslt.Add(pd.Name, new PropDispDescr() { Description = descr, DisplayName = dispName });
            }
            return rslt;
        }

        public static List<string> GetXSDComplexTypes(XmlDocument doc)
        {
            XmlNodeList nodes = doc.DocumentElement.FirstChild.SelectNodes("//xs:complexType");
            List<string> rslt = new List<string>();
            foreach (XmlNode node in nodes)
            {
                string typeName = node.Attributes["name"].Value;
                if (string.IsNullOrEmpty(typeName) || rslt.Contains(typeName))
                    continue;
                rslt.Add(typeName);
             }
            return rslt; 
        }

        public static List<string> GetXSDEnums(XmlDocument doc)
        {
            XmlNodeList nodes = doc.SelectNodes("//xs:simpleType");
            List<string> rslt = new List<string>();
            foreach (XmlNode node in nodes)
            {
                string typeName = node.Attributes["name"].Value;
                if (string.IsNullOrEmpty(typeName) || rslt.Contains(typeName))
                    continue;
                rslt.Add(typeName);
            }
            return rslt;
        }

        public static void ProcessXSD(string xsdPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xsdPath);
        }

        #region inner type(s)

        public class PropDispDescr
        {
            public string DisplayName { get; set; }
            public string Description { get; set; }
        }
        #endregion
    }
}
