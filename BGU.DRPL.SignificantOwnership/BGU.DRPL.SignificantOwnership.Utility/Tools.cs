using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;

namespace BGU.DRPL.SignificantOwnership.Utility
{
    public class Tools
    {
        public static string GetTypeClassDescription(Type typ)
        {
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])typ.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return string.Empty;
        }
        public static string GetObjectClassDescription(object classInstance)
        {
            return GetTypeClassDescription(classInstance.GetType());

        }

        public static string GetPropDescription(PropertyDescriptor prop)
        {
            foreach (Attribute attr in prop.Attributes)
            {
                if (attr is DescriptionAttribute)
                    return ((DescriptionAttribute)attr).Description;
            }
            return string.Empty;
        }

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
    }
}
