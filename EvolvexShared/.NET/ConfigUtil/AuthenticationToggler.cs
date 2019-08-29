using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace ConfigUtil
{
    public class AuthenticationToggler
    {
        private string _configPath;
        private bool? _isNone;
        private static readonly string AUTHENT_NONE_XPATH = "/configuration/system.web/authentication[@mode = 'None']";
        private static readonly string AUTHENT_ANY_XPATH = "/configuration/system.web/authentication";
        private static readonly string AUTHORZ_DENY_ANON_XPATH = "/configuration/system.web/authorization/deny[@users = '?']";
        private static readonly string AUTHORZ_ALLOW_ALL_XPATH = "/configuration/system.web/authorization/allow[@users = '*']";

        private XmlDocument _doc = null;

        public AuthenticationToggler()
        {
            _isNone = null;
        }
        public AuthenticationToggler(string cfgPath)
        {
            _isNone = null;
            _configPath = cfgPath;
        }


        private XmlDocument Doc
        {
            get
            {
                if (_doc == null)
                {
                    _doc = OpenDocCommon(_configPath);
                }
                return _doc;
            }
        }

        public string ConfigPath
        {
            get { return _configPath; }
            set
            {
                if (_configPath != value)
                {
                    _isNone = null;
                    _doc = null;
                }

                _configPath = value; 
            }
        }
        public void CommentNone()
        {
            XmlNode authNone = Doc.SelectSingleNode(AUTHENT_NONE_XPATH);
            if (authNone == null)
                return;
            XmlComment cmntAuthOther = (XmlComment)authNone.NextSibling;
            string sAuthOthXmlCommented = cmntAuthOther.OuterXml;
            string sAuthOthXmlUnCommented = cmntAuthOther.InnerText;
            XmlComment cmntAuthNone = Doc.CreateComment(authNone.OuterXml);
            authNone.ParentNode.ReplaceChild(cmntAuthNone, authNone);
            XmlNode authrzDenyAnon = Doc.SelectSingleNode(AUTHORZ_DENY_ANON_XPATH);
            string sAuthrzDenyAnonCommented = string.Empty;
            string sAuthrzDenyAnonUnCommented = string.Empty;
            if (authrzDenyAnon == null)
            {
                XmlNode authrzAllowAll = Doc.SelectSingleNode(AUTHORZ_ALLOW_ALL_XPATH);
                if (authrzAllowAll != null)
                {
                    XmlComment cmtAuthrzDenyAnon = (XmlComment)authrzAllowAll.PreviousSibling;
                    if (cmtAuthrzDenyAnon != null)
                    {
                        sAuthrzDenyAnonCommented = cmtAuthrzDenyAnon.OuterXml;
                        sAuthrzDenyAnonUnCommented = cmtAuthrzDenyAnon.InnerText;
                    }
                }
            }

            Doc.Save(ConfigPath);
            string sCfgContents = File.ReadAllText(ConfigPath);
            sCfgContents = sCfgContents.Replace(sAuthOthXmlCommented, sAuthOthXmlUnCommented);
            sCfgContents = sCfgContents.Replace(sAuthrzDenyAnonCommented, sAuthrzDenyAnonUnCommented);
            File.WriteAllText(ConfigPath, sCfgContents);
            _doc = null;
        }

        public void UnCommentNone()
        {
            XmlNode authNone = Doc.SelectSingleNode(AUTHENT_NONE_XPATH);
            if (authNone != null)
                return;
            XmlNode authOther = Doc.SelectSingleNode(AUTHENT_ANY_XPATH);
            XmlComment cmntAuthNone = (XmlComment)authOther.PreviousSibling;
            string sAuthNoneXmlCommented = cmntAuthNone.OuterXml;
            string sAuthNoneXmlUnCommented = cmntAuthNone.InnerText;
            XmlComment cmntAuthOther = Doc.CreateComment(authOther.OuterXml);
            authOther.ParentNode.ReplaceChild(cmntAuthOther, authOther);

            XmlNode authDenyAnon = Doc.SelectSingleNode(AUTHORZ_DENY_ANON_XPATH);
            if (authDenyAnon != null)
            {
                XmlComment cmntAuthorzDenyAnon = Doc.CreateComment(authDenyAnon.OuterXml);
                authDenyAnon.ParentNode.ReplaceChild(cmntAuthorzDenyAnon, authDenyAnon);
            }
            Doc.Save(ConfigPath);
            string sCfgContents = File.ReadAllText(ConfigPath);
            File.WriteAllText(ConfigPath, sCfgContents.Replace(sAuthNoneXmlCommented, sAuthNoneXmlUnCommented));
            _doc = null;
        }

        public bool IsNone
        {
            get 
            {
                if (!_isNone.HasValue)
                {
                    _isNone = IsAuthenticationNone(Doc);
                }
                return _isNone.Value;
            }
        }

        private static XmlDocument OpenDocCommon(string configPath)
        {
            if (string.IsNullOrEmpty(configPath) || !File.Exists(configPath))
                throw new System.ArgumentException(string.Format("The path doesn't exist - \"{0}\"", configPath));
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            return doc;
        }

        private static bool IsAuthenticationNone(XmlDocument doc)
        {
            XmlNode authNone = doc.SelectSingleNode(AUTHENT_NONE_XPATH);
            if (authNone == null)
                return false;
            else
                return true;
        }

        public static bool IsAuthenticationNone(string configPath)
        {
            return IsAuthenticationNone(OpenDocCommon(configPath));
        }

        
        public void Toggle()
        {
            if (IsNone)
                CommentNone();
            else
                UnCommentNone();
            
        }
    }
}
