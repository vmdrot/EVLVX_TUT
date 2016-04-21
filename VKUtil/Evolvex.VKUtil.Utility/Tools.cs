using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace Evolvex.VKUtil.Utility
{
    public class Tools
    {
        public static void WriteXML<T>(T obj, string saveAs)
        {
            using (FileStream fs = File.Create(saveAs))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fs, obj);
            }
        }

        public static T ReadXML<T>(string fromFile)
        {
            using (FileStream fs = new FileStream(fromFile, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                object o = serializer.Deserialize(fs);
                return (T)o;
            }
        }

        public static T ReadXML2<T>(string fromFile)
        {
            using (FileStream fs = new FileStream(fromFile, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), "http://opendata.org/defaultns/");
                object o = serializer.Deserialize(fs);
                return (T)o;
            }
        }

        public static void WriteXML(object obj, string saveAs)
        {
            using (FileStream fs = File.Create(saveAs))
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
        }

        public static object ReadXML(string fromFile, Type targetType)
        {
            using (FileStream fs = new FileStream(fromFile, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(targetType);
                object o = serializer.Deserialize(fs);
                return o;
            }
        }



        public static bool DataTableToCSV(DataTable dtSource, string saveAsPath, bool includeHeader)
        {

            using (StreamWriter sw = new StreamWriter(saveAsPath, false, Encoding.Unicode))
            {
                return DataTableToCSV(dtSource, sw, includeHeader);
            }
        }
        public static bool DataTableToCSV(DataTable dtSource, StreamWriter writer, bool includeHeader)
        {
            if (dtSource == null || writer == null) return false;

            if (includeHeader)
            {
                //string[] columnNames = dtSource.Columns.Cast<DataColumn>().Select(column => "\"" + column.ColumnName.Replace("\"", "\"\"") + "\"").ToArray<string>();
                string[] columnNames = dtSource.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray<string>();
                writer.WriteLine(String.Join("\t", columnNames));
                writer.Flush();
            }

            foreach (DataRow row in dtSource.Rows)
            {
                //string[] fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"").ToArray<string>();
                string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray<string>();
                for (int i = 0; i < fields.Length; i++)
                {
                    //if (fields[i].IndexOf('\r') == -1 && fields[i].IndexOf('\n') == -1 && fields[i].IndexOf('"') == -1)
                    //    continue;
                    //fields[i] = string.Format("\"{0}\"", fields[i].Replace("\"", "\\\""));
                    if (fields[i].IndexOf('\r') == -1 && fields[i].IndexOf('\n') == -1)
                        continue;
                    fields[i] = fields[i].Replace('\r', ' ').Replace('\n', ' ');
                }
                writer.WriteLine(String.Join("\t", fields));
                writer.Flush();
            }

            return true;
        }
    }
}
