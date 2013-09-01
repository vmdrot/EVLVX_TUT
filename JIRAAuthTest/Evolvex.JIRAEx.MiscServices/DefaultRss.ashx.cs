using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Evolvex.Ruthenorum.JIRAAuth.Facade;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using System.Text;

namespace Evolvex.JIRAEx.MiscServices
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DefaultRss : RssFeedsPublisherBase
    {
        const string QU_PRM_ACTION_KEY = "action";
        const string ACTION_NAME_LIST_NEW_USERS = "list_new_users";
        private static readonly Dictionary<string, string> CHANNEL_TITLES;
        private static readonly Dictionary<string, string> CHANNEL_DESCRIPTIONS;

        static DefaultRss()
        {
            CHANNEL_TITLES = new Dictionary<string, string>();
            CHANNEL_DESCRIPTIONS = new Dictionary<string, string>();
            CHANNEL_TITLES.Add(ACTION_NAME_LIST_NEW_USERS, "JIRA new users");
            CHANNEL_DESCRIPTIONS.Add(ACTION_NAME_LIST_NEW_USERS, "Lists JIRA new registered users (and not yet assigned to any project).");
        }

        private string _actionKey;
        private string ActionKey
        {
            get
            { 
                if(string.IsNullOrEmpty(_actionKey))
                {
                    if (_httpContext == null)
                        return null;
                    _actionKey = _httpContext.Request.Params[QU_PRM_ACTION_KEY];
                }
                return _actionKey;
            }
        }


        protected override List<Rss.RssItem> GenerateResultItems(HttpContext context)
        {
            if (_httpContext == null)
                _httpContext = context;
            List<IJIRAUserInfo> juis = JiraAuthFacade.ListNewUsers();

            if (juis.Count == 0)
                return new List<Rss.RssItem>();
            Rss.RssItem rslt = NewUsersToRssItem(juis, context);
            return new List<Rss.RssItem>() { rslt };

        }

        protected override string GetChannelTitle(HttpContext context)
        {
            if (_httpContext == null)
                _httpContext = context;
            if (String.IsNullOrEmpty(ActionKey) || !CHANNEL_TITLES.ContainsKey(ActionKey))
                return "Unknown Channel title";
            return CHANNEL_TITLES[ActionKey];
        }

        protected override string GetChannelDescription(HttpContext context)
        {
            if (_httpContext == null)
                _httpContext = context;
            if (String.IsNullOrEmpty(ActionKey) || !CHANNEL_DESCRIPTIONS.ContainsKey(ActionKey))
                return "Unknown Channel title";
            return CHANNEL_DESCRIPTIONS[ActionKey];
        }

        public static Rss.RssItem NewUsersToRssItem(List<IJIRAUserInfo> rix, HttpContext context)
        {
            Rss.RssItem rslt = new Rss.RssItem();

            String sTitle = String.Format("New JIRA users registered - {0}", JoinUserNames(rix));
            rslt.Title = sTitle;
            rslt.Description = JoinUserInfos(rix);
            rslt.PubDate = DateTime.Now;
            Rss.RssGuid guid = new Rss.RssGuid();
            guid.Name = JoinUserNamesAsGuid(rix);
            guid.PermaLink = Rss.DBBool.False;
            rslt.Guid = guid;
            rslt.Link = new Uri("http://pm.ruthenorum.info/jira/secure/admin/user/UserBrowser.jspa");
            return rslt;
        }

        private static string JoinUserNamesAsGuid(List<IJIRAUserInfo> rix)
        {
            List<string> names = new List<string>();
            foreach (IJIRAUserInfo jui in rix)
                names.Add(jui.name);
            return String.Join("_", names.ToArray());
        }

        private static string JoinUserInfos(List<IJIRAUserInfo> rix)
        {
            List<string> lines = new List<string>();
            foreach (IJIRAUserInfo jui in rix)
                lines.Add(jui.ToString());
            return String.Join("<br/>\n", lines.ToArray());
        }

        private static string JoinUserNames(List<IJIRAUserInfo> rix)
        {
            List<string> names = new List<string>();
            foreach (IJIRAUserInfo jui in rix)
                names.Add(jui.name);
            return String.Join(", ", names.ToArray());
        }


    }
}
