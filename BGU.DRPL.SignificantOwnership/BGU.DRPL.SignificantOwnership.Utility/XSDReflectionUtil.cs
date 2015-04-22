﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Xml;
using System.IO;

namespace BGU.DRPL.SignificantOwnership.Utility
{
    public class XSDReflectionUtil
    {
        private static readonly string AssemblyName = "BGU.DRPL.SignificantOwnership.Core";
        private static readonly string[] SearchNamespaces = new string[] { "BGU.DRPL.SignificantOwnership.Core.Questionnaires", "BGU.DRPL.SignificantOwnership.Core.Spares.Data", "BGU.DRPL.SignificantOwnership.Core.Spares.Dict", "BGU.DRPL.SignificantOwnership.Core.Spares" };

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

        public static XmlDocument LoadAnnotationXml(Assembly asbmly)
        {
            string dir = Path.GetDirectoryName(asbmly.CodeBase).Replace("file:\\",string.Empty);;
            string fnm = Path.GetFileNameWithoutExtension(asbmly.CodeBase);
            string xmlPath = Path.Combine(dir, string.Format("{0}.xml", fnm));
            if (!File.Exists(xmlPath))
                return null;
            XmlDocument rslt = new XmlDocument();
            rslt.Load(xmlPath);
            return rslt;
        }


        #region inner type(s)

        public class PropDispDescr
        {
            public string DisplayName { get; set; }
            public string Description { get; set; }
        }
        #endregion
        public static void InjectDispProps(XmlDocument doc)
        {
            InjectDispProps(doc, null);
        }
        public static void InjectDispProps(XmlDocument doc, Dictionary<string, bool> alreadyProcessedTypes)
        {
            InjectDispProps(doc, alreadyProcessedTypes, null);
        }
        public static void InjectDispProps(XmlDocument doc, Dictionary<string,bool> alreadyProcessedTypes, XmlDocument assemblySummariesXml)
        {
            List<XmlNode> toBeDel = new List<XmlNode>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "xs:complexType")
                {
                    if (alreadyProcessedTypes != null && alreadyProcessedTypes.ContainsKey(node.Attributes["name"].Value))
                        toBeDel.Add(node);
                    //node.ParentNode.RemoveChild(node);//doc.RemoveChild(node);
                    //doc.DocumentElement.RemoveChild(node);
                    else
                    {
                        ProcessClass(node, assemblySummariesXml);
                        if (alreadyProcessedTypes != null)
                            alreadyProcessedTypes.Add(node.Attributes["name"].Value, true);
                    }
                }
                else if (node.Name == "xs:simpleType")
                {
                    if (alreadyProcessedTypes != null && alreadyProcessedTypes.ContainsKey(node.Attributes["name"].Value))
                        toBeDel.Add(node);//node.ParentNode.RemoveChild(node);//doc.RemoveChild(node);
                    else
                    {
                        ProcessEnum(node, assemblySummariesXml);
                        if (alreadyProcessedTypes != null)
                            alreadyProcessedTypes.Add(node.Attributes["name"].Value, true);
                    }
                }
            }
            for(int i = 0;i<toBeDel.Count;i++)
                doc.DocumentElement.RemoveChild(toBeDel[i]);
        }

        public static string GetSummaryFromAssemblyXmlForAType(XmlDocument doc, Type typ)
        {
            XmlNode node = doc.SelectSingleNode(string.Format("/doc/members/member[@name='T:{0}']/summary", typ.FullName));
            if (node == null)
                return null;
            return node.InnerText;
        }

        public static string GetSummaryFromAssemblyXmlForATypeProperty(XmlDocument doc, Type typ, string propName)
        {
            XmlNode node = doc.SelectSingleNode(string.Format("/doc/members/member[@name='P:{0}.{1}']/summary", typ.FullName, propName));
            if (node == null)
                return null;
            return node.InnerText;
        }

        private static string GetSeeAlsoCref(XmlDocument doc, string xPath)
        { 
            XmlNode node = doc.SelectSingleNode(xPath);
            if (node == null)
                return null;
            string cref = node.Attributes["cref"].Value;
            if(cref.IndexOf("!:")==0)
                cref = cref.Substring(2);
            return cref;
        }
        public static string GetSeeAlsoFromAssemblyXmlForAType(XmlDocument doc, Type typ)
        {
            return GetSeeAlsoCref(doc, string.Format("/doc/members/member[@name='T:{0}']/seealso", typ.FullName));
        }

        public static string GetSeeAlsoFromAssemblyXmlForATypeProperty(XmlDocument doc, Type typ, string propName)
        {
            return GetSeeAlsoCref(doc, string.Format("/doc/members/member[@name='P:{0}.{1}']/seealso", typ.FullName, propName));
        }

        private static void FindAddAnnotation(XmlNode target, Type typ, XmlDocument asmblyDoc)
        {
            string comment = GetSummaryFromAssemblyXmlForAType(asmblyDoc, typ);
            string seeAlso = GetSeeAlsoFromAssemblyXmlForAType(asmblyDoc, typ);
            if (string.IsNullOrEmpty(comment) && string.IsNullOrEmpty(seeAlso))
                return;
            AddAnnotation(target, comment,seeAlso);
        }

        private static void FindAddAnnotation(XmlNode target, Type typ, string prop, XmlDocument asmblyDoc)
        {
            string comment = GetSummaryFromAssemblyXmlForATypeProperty(asmblyDoc, typ, prop);
            string seeAlso = GetSeeAlsoFromAssemblyXmlForATypeProperty(asmblyDoc, typ,prop);
            if (string.IsNullOrEmpty(comment) && string.IsNullOrEmpty(seeAlso))
                return;
            AddAnnotation(target, comment, seeAlso);
        }

        private static void AddAnnotation(XmlNode target, string comment, string seeAlso)
        {
            XmlNode annotNode = target.OwnerDocument.CreateNode(XmlNodeType.Element, "xs:annotation", target.OwnerDocument.DocumentElement.Attributes["xmlns:xs"].Value);
            XmlNode docNode = target.OwnerDocument.CreateNode(XmlNodeType.Element, "xs:documentation", target.OwnerDocument.DocumentElement.Attributes["xmlns:xs"].Value);
            StringBuilder sbInnerTxt = new StringBuilder();
            int i = 0;
            if(!string.IsNullOrEmpty(comment))
            {
                sbInnerTxt.Append(comment); i++;
            }

            if (!string.IsNullOrEmpty(seeAlso))
            {
                if (i > 0)
                    sbInnerTxt.AppendLine();
                sbInnerTxt.AppendFormat("Див.також: {0}", seeAlso); i++;
            }

            docNode.InnerText = sbInnerTxt.ToString();
            
            annotNode.AppendChild(docNode);
            if (target.ChildNodes == null || target.ChildNodes.Count == 0)
                target.AppendChild(annotNode);
            else
                target.InsertBefore(annotNode, target.ChildNodes[0]);
        }
        private static void ProcessEnum(XmlNode node, XmlDocument assemblySummariesXml)
        {
            string typNm = node.Attributes["name"].Value;
            Type typ = FindType(typNm);
            if (typ == null)
                return;

            XmlNode seqNode = FindRestrictionChild(node);
            if (seqNode == null)
                return;
            if(assemblySummariesXml != null)
                FindAddAnnotation(node, typ, assemblySummariesXml);
            List<EnumType> enmTyps = EnumType.GetEnumList(typ, false);
            Dictionary<string, string> enms2Descrs = new Dictionary<string, string>();
            foreach (EnumType enmTyp in enmTyps)
            {
                if (!enms2Descrs.ContainsKey(enmTyp.EnumValue.ToString()))
                    enms2Descrs.Add(enmTyp.EnumValue.ToString(), enmTyp.Value);
            }
            XmlNodeList propNodes = seqNode.ChildNodes;
            foreach (XmlNode propNode in propNodes)
            {
                if (propNode.Name != "xs:enumeration")
                    continue;
                string propNm = propNode.Attributes["value"].Value;

                if (!enms2Descrs.ContainsKey(propNm))
                    continue;
                if (!string.IsNullOrEmpty(enms2Descrs[propNm]))
                    WriteAttribute(propNode, "description", enms2Descrs[propNm]);
            }
        }

        private static void ProcessClass(XmlNode node, XmlDocument assemblySummariesXml)
        {
            string typNm = node.Attributes["name"].Value;
            Type typ = FindType(typNm);
            if (typ == null)
                return;
            Dictionary<string, PropDispDescr> dispDescrs = FetchPropDispsDescrs(typ);
            XmlNode seqNode = FindSequenseChild(node);
            if (seqNode == null)
                return;

            if (assemblySummariesXml != null)
                FindAddAnnotation(node, typ, assemblySummariesXml);

            XmlNodeList propNodes = seqNode.ChildNodes;
            foreach (XmlNode propNode in propNodes)
            {
                string propNm = propNode.Attributes["name"].Value;
                if (!dispDescrs.ContainsKey(propNm))
                    continue;
                
                string propTypeNmEng = propNode.Attributes["type"].Value;
                string propTypeNmUkr = TranslateTypeNm(propTypeNmEng);
                if (propTypeNmEng != propTypeNmUkr)
                    WriteAttribute(propNode, "type_ukr", propTypeNmUkr);

                if (!string.IsNullOrEmpty(dispDescrs[propNm].DisplayName))
                    WriteAttribute(propNode, "displayName", dispDescrs[propNm].DisplayName);
                if (!string.IsNullOrEmpty(dispDescrs[propNm].Description))
                    WriteAttribute(propNode, "description", dispDescrs[propNm].Description);
                if (assemblySummariesXml != null)
                    FindAddAnnotation(propNode, typ, propNm,assemblySummariesXml);

            }
        }

        private static string TranslateTypeNm(string propTypeNmEng)
        {
            switch (propTypeNmEng)
            {
                case "xs:string": return "текст";
                case "xs:hexBinary": return "гексадвоїчний";
                case "xs:base64Binary": return "двоїчний у 64-му кодуванні";
                case "xs:anyURI": return "URL";
                case "xs:QName": return "кваліфіковане ім'я";
                case "xs:NOTATION": return "нотація";
                case "xs:normalizedString": return "нормалізований текст";
                case "xs:token": return "токен";
                case "xs:language": return "мова";
                case "xs:NMTOKEN": return "ім'я-токен";
                case "xs:Name": return "ім'я";
                case "xs:NCName": return "ім'я";
                case "xs:ID": return "ідентифікатор";
                case "xs:IDREF": return "посилання на ідентифікатор";
                case "xs:ENTITY": return "сутність";
                case "xs:integer": return "ціле число";
                case "xs:nonPositiveInteger": return "недодатнє ціле число";
                case "xs:negativeInteger": return "від'ємне ціле число";
                case "xs:nonNegativeInteger": return "невід'ємне ціле число";
                case "xs:positiveInteger": return "додатнє ціле число";
                case "xs:long": return "довге ціле число";
                case "xs:int": return "ціле число";
                case "xs:short": return "коротке ціле число";
                case "xs:byte": return "байт";
                case "xs:unsignedLong": return "беззнакове довге ціле";
                case "xs:unsignedInt": return "беззнакове ціле";
                case "xs:unsignedShort": return "беззнакове коротке ціле";
                case "xs:unsignedByte": return "беззнаковий байт";
                case "xs:float": return "число з плаваючою комою";
                case "xs:double": return "подвійне з плаваючою комою";
                case "xs:dateTime": return "дата і час";
                case "xs:date": return "дата";
                case "xs:gYearMonth": return "рік і місяць";
                case "xs:gYear": return "рік";
                case "xs:time": return "час";
                case "xs:gDay": return "день";
                case "xs:gMonth": return "місяць";
                case "xs:gMonthDay": return "місяць і день";
                case "xs:duration": return "тривалість (по часу)";
                case "xs:NMTOKENS": return "масив імен-токенів";
                case "xs:IDREFS": return "масив посилань на ідентифікатори";
                case "xs:ENTITIES": return "масив сутностей";
                case "xs:boolean": return "логічний тип(так/ні)";
                case "xs:decimal": return "десяткове число, у т.ч. дрібне";
                default: break;
            }
            return propTypeNmEng;
        }

        private static XmlNode FindSequenseChild(XmlNode node)
        {
            return FindTagNameChild(node, "xs:sequence");
        }

        private static XmlNode FindRestrictionChild(XmlNode node)
        {
            return FindTagNameChild(node, "xs:restriction");
        }

        private static XmlNode FindTagNameChild(XmlNode node, string tagName)
        {
            XmlNode rslt = node;
            while (rslt != null && rslt.Name != tagName)
            {
                rslt = rslt.FirstChild;
            }
            if (rslt != null && rslt.Name == tagName)
                return rslt;
            return null;
        }

        private static Type FindType(string typNm)
        {
            Type rslt = null;
            foreach (string namespaceNm in SearchNamespaces)
            {
                rslt = Type.GetType(string.Format("{0}.{1}, {2}", namespaceNm, typNm, AssemblyName), false);
                if (rslt != null)
                    break;
            }
            return rslt;
        }

        private static void WriteAttribute(XmlNode node, string attrName, string attrVal)
        {
            XmlAttribute attr = node.OwnerDocument.CreateAttribute(attrName);
            attr.Value = attrVal;
            node.Attributes.SetNamedItem(attr);
        }
    }
}
