using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Web.Security;

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
        public Dictionary<String, String> avatarUrls { get; private set; }

        private JIRAUserInfo(JsonTextReader reader)
        {
            string lastPropNm = string.Empty;
            Dictionary<string, object> props = new Dictionary<string, object>();
            #region read-out json
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
            #endregion

            #region fill the object props
            foreach (string prop in props.Keys)
            {
                switch (prop)
                { 
                    case "self":
                        this.self = props[prop] as string;
                        break;
                    case "name":
                        this.name = props[prop] as string;
                        break;
                    case "emailAddress":
                        this.email = props[prop] as string;
                        break;
                    case "displayName":
                        this.displayName = props[prop] as string;
                        break;
                    case "active":
                        this.active = (bool)props[prop];
                        break;
                    case "timeZone":
                        this.timeZone = props[prop] as string;
                        break;
                    case "groups":
                        this.groups = (List<String>)props[prop];
                        break;
                    case "avatarUrls":
                        this.avatarUrls = (Dictionary<string,string>)props[prop];
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }

        private Dictionary<string, string> ReadAvatars(JsonTextReader reader)
        {
            Dictionary<string, string> rslt = new Dictionary<string, string>();
            string lastPropName = string.Empty;
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                    break;
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    lastPropName = reader.Value as string;
                }
                if (reader.TokenType == JsonToken.String)
                    rslt.Add(lastPropName, reader.Value as string);
            }
            return rslt;
        }

        private static List<string> ReadGroups(JsonTextReader reader)
        {
            List<string> rslt = new List<string>();
            bool arrayStarted = false;
            string lastPropName = string.Empty;
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.StartArray)
                    arrayStarted = true;
                if (!arrayStarted)
                    continue;
                if (reader.TokenType == JsonToken.EndArray)
                    break;
                if(reader.TokenType == JsonToken.PropertyName)
                {
                    lastPropName = reader.Value as string;
                }
                if (reader.TokenType == JsonToken.String && lastPropName == "name")
                    rslt.Add(reader.Value as string);
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

        public override string ToString()
        {
            return string.Format("self = {0}, name = {1}, email = {2}, displayName = {3}, active = {4}, timeZone = {5}, groups.Count = {6}, avatarUrls.Count = {7}", self, name, email, displayName, active, timeZone, groups.Count, avatarUrls.Count);
        }

        //public static explicit operator MembershipUser(JIRAUserInfo src)
        //{
        //    return new MembershipUser("???!", src.name, src.self
        //}

    }
}
