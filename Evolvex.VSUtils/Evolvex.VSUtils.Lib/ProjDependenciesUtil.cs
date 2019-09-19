using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Evolvex.VSUtils.Lib
{
    public static class ProjDependenciesUtil
    {
        public static List<string> ListDependencies(string projPath, string rootItem)
        {
            List<string> rslt = new List<string>();
            rslt.Add(rootItem);
            XmlDocument doc = new XmlDocument();
            doc.Load(projPath);
            XmlNamespaceManager xnManager = new XmlNamespaceManager(doc.NameTable);
            xnManager.AddNamespace("",
             "http://schemas.microsoft.com/developer/msbuild/2003");
            XmlElement root = doc.DocumentElement;
            //rslt.AddRange( ListItemDependencies(root, xnManager, rootItem));
            rslt.AddRange(ListItemDependenciesV2(root, rootItem));
            return rslt;
        }

        private static List<string> ListItemDependencies(XmlElement root, XmlNamespaceManager xnManager, string item)
        {
            List<string> rslt = new List<string>();
            XmlNodeList nodes = root.SelectNodes(string.Format("//*/@Include/DependentUpon[text() = '{0}']", item), xnManager);
            if (nodes == null || nodes.Count == 0)
                return rslt;
            foreach (XmlNode node in nodes)
            {
                if (node.ParentNode == null || node.ParentNode["Include"] == null || string.IsNullOrWhiteSpace(node.ParentNode["Include"].Value))
                    continue;
                if (!rslt.Contains(node.ParentNode["Include"].Value))
                    rslt.Add(node.ParentNode["Include"].Value);
            }

            foreach (string directDep in rslt)
            {
                rslt.AddRange(ListItemDependencies(root, xnManager, directDep));
            }
            return rslt;
        }
        private static List<string> ListItemDependenciesV2(XmlElement root, string item)
        {
            List<string> rslt = new List<string>();
            XmlNodeList nodes = root.SelectNodes("//*");
            if (nodes == null || nodes.Count == 0)
                return rslt;
            foreach (XmlNode node in nodes)
            {
                if (!IsNodeDependentUpon(node, item))
                    continue;
                
                if (!rslt.Contains(GetAttrValue(node, "Include")))
                    rslt.Add(GetAttrValue(node, "Include"));
            }
            List<string> directDeps = new List<string>();
            directDeps.AddRange(rslt);

            foreach (string directDep in directDeps)
            {
                rslt.AddRange(ListItemDependenciesV2(root, directDep));
            }
            return rslt;
        }


        private static string GetAttrValue(XmlNode node, string attrName)
        {
            for (int i = 0; i < node.Attributes.Count; i++)
            {
                if (node.Attributes[i].Name == attrName)
                    return node.Attributes[i].Value;
            }
            return null;
        }

        private static bool IsNodeDependentUpon(XmlNode node, string item)
        {
            if (!node.HasChildNodes)
                return false;
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "DependentUpon" && child.InnerText == item)
                    return true;
            }
            return false;
        }

        public static List<string> CutOffItemsXmls(string projPath, string[] items)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(projPath);
            XmlElement root = doc.DocumentElement;
            return GetItemsXmls(root, items);
        }

        private static List<string> GetItemsXmls(XmlElement root, string[] items)
        {
            List<string> rslt = new List<string>();
            XmlNodeList nodes = root.SelectNodes("//*");
            if (nodes == null || nodes.Count == 0)
                return rslt;
            foreach (XmlNode node in nodes)
            {
                if (items.Contains(GetAttrValue(node, "Include")))
                    rslt.Add(node.OuterXml);
            }
            return rslt;
        }
    }
}
