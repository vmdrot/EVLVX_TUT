using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Evolvex.Ruthenorum.JIRAAuth.Data
{
    public class JIRAUserInfo
    {
        public string self { get; private set; }
        public string name { get; private set; }
        public string email { get; private set; }
        public string displayName { get; private set; }
        public bool active { get; private set; }
        public string timeZone { get; private set; }
        public List<string> groups { get; private set; }

        private JIRAUserInfo(JsonTextReader reader)
        {
            string lastPropNm = string.Empty;
            Dictionary<string, object> props = new Dictionary<string, object>();
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Boolean:
                    case JsonToken.Bytes:
                    case JsonToken.Date:
                    case JsonToken.Float:
                    case JsonToken.Integer:
                    case JsonToken.Null:
                    case JsonToken.String:
                        props.Add(lastPropNm, reader.Value);
                        break;
                    case JsonToken.Comment:
                        break;
                    case JsonToken.EndArray:
                        break;
                    case JsonToken.EndConstructor:
                        break;
                    case JsonToken.EndObject:
                        break;
                    case JsonToken.None:
                        break;
                    case JsonToken.PropertyName:
                        lastPropNm = reader.Value as string;
                        break;
                    case JsonToken.Raw:
                        break;
                    case JsonToken.StartArray:
                        break;
                    case JsonToken.StartConstructor:
                        break;
                    case JsonToken.StartObject:
                        if (lastPropNm == "groups")
                            props.Add(lastPropNm, ReadGroups(reader));
                        else if (lastPropNm == "avatarUrls")
                            props.Add(lastPropNm, ReadAvatars(reader));
                        break;
                    case JsonToken.Undefined:
                        break;
                    default:
                        break;
                }
            }
        }

        private Dictionary<string, string> ReadAvatars(JsonTextReader reader)
        {
            throw new NotImplementedException();
        }

        private static List<string> ReadGroups(JsonTextReader reader)
        {
            List<string> rslt = new List<string>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                    break;
            }
            return rslt;
        }

        public static JIRAUserInfo Parse(string json)
        {
            using(Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                return new JIRAUserInfo(reader);
            }

        }
    }
}
