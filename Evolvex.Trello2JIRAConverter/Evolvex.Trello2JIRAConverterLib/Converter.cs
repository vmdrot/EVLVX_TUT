using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Xml;
using Evolvex.Trello2JIRAConverterLib.Data.Trello;

namespace Evolvex.Trello2JIRAConverterLib
{
    public class Converter
    {
        public string Convert(string srcJson)
        {

            //using (Newtonsoft.Json.JsonReader r = new JsonReader(new StringReader(srcJson)))
            //{
            //    while((r.Read())
            //    {
            //        switch(r.TokenType
            //    }
            //}
            JToken jsonObj = JObject.Parse(srcJson);
            return string.Empty;//todo
        }

        public void ConvertFile(string srcFilePath, string destFilePath)
        { 

        }

        public void ConvertToXml(string srcFilePath, string destFilePath, string rootNodeName)
        {
            JToken jsonObj = JObject.Parse(File.ReadAllText(srcFilePath));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Auto;
            settings.Encoding = Encoding.Unicode;
            settings.Indent = true;
            using(XmlWriter w = XmlWriter.Create(destFilePath,settings))
            {
                //w.WriteStartElement(rootNodeName);
                Export(jsonObj, w, rootNodeName);
                //w.WriteEndElement();
            }
        }

        private void Export(JToken jsonObj, XmlWriter w, string embedIntoNodeName)
        {
            w.WriteStartElement(embedIntoNodeName);
            foreach (JToken child in jsonObj.Children())
            {
                if (child.Type == JTokenType.Property)
                {
                    JProperty prop = (JProperty)child;
                    if (prop.Value.HasValues)
                        Export(prop.Value, w, prop.Name.ToString());
                    else
                        ExportScalarValue(prop, w);
                }
                else if (child.Type == JTokenType.Object)
                {
                    if (child.Parent.Type == JTokenType.Array)
                        Export(child, w, "add");
                }
            }
            w.WriteEndElement();
        }

        private void ExportScalarValue(JProperty val, XmlWriter w)
        {
            w.WriteStartElement(val.Name.ToString());
            w.WriteString(val.Value.ToString());
            w.WriteEndElement();
        }

        #region parser(s)

        public Board Parse(string boardJson)
        {
            JToken jsonObj = JObject.Parse(boardJson);
            Board rslt = new Board();
            ParseRecursive(jsonObj, rslt);

            return null;
        }

        private void ParseRecursive(JToken jsonObj, object target)
        {
            
            foreach (JToken child in jsonObj.Children())
            {
                if (child.Type == JTokenType.Property)
                {
                    JProperty prop = (JProperty)child;
                    if (prop.Value.HasValues)
                        ParseRecursive(prop.Value, SpawnInstance(prop.Name));
                    else
                        ExportScalarValue(prop, w);
                }
                else if (child.Type == JTokenType.Object)
                {
                    if (child.Parent.Type == JTokenType.Array)
                        Export(child, w, "add");
                }
            }
            
        }

        private object SpawnInstance(string propName)
        {
            switch (propName)
            { 
                case "": return new 
            }
        }

        private void ParseScalarValue(JProperty val, object target)
        {
            target.GetType().GetProperty(val.Name).SetValue(target, val.Value.ToString(), null);
        }

        #endregion

    }
}
