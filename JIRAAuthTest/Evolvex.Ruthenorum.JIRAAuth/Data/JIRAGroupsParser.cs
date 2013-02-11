using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Evolvex.Ruthenorum.JIRAAuth.Data
{
    public class JIRAGroupsParser
    {
        private const string GROUPS_PROP_NM = "groups";
        public static List<string> Parse(string json)
        {
            List<string> rslt = new List<string>();

            string lastPropNm = string.Empty;
            Dictionary<string, object> props = new Dictionary<string, object>();
            using (Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {

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
                            if (lastPropNm == GROUPS_PROP_NM)
                                props.Add(lastPropNm, JIRAUserInfo.ReadGroups(reader));
                            break;
                        case JsonToken.Undefined:
                            break;
                        default:
                            break;
                    }
                }
                #endregion
            }
            if (props.ContainsKey(GROUPS_PROP_NM) && props[GROUPS_PROP_NM] != null)
            {
                rslt.AddRange((List<string>)props[GROUPS_PROP_NM]);
            }
            return rslt;
        }
    }
}
