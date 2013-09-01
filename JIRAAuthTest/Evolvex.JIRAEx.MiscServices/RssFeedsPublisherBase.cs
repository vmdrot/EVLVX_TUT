using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
//using Evolvex.Ruthenorum.PR360.WebPortal.Framework;
using System.Globalization;

namespace Evolvex.JIRAEx.MiscServices
{
    public abstract class RssFeedsPublisherBase : IHttpHandler
    {
        protected const int MAX_ITEMS_COUNT = 25;
        protected const string QU_PARAMS_KEYS_ITEMS_COUNT = "pg_sz";
        private static readonly CultureInfo DEFAULT_CULTURE = CultureInfo.GetCultureInfoByIetfLanguageTag("uk-UA");
        protected HttpContext _httpContext;

        protected int GetPageSize(HttpContext context)
        {
            if (context.Request.QueryString[QU_PARAMS_KEYS_ITEMS_COUNT] == null)
                return MAX_ITEMS_COUNT;
            else
            {
                int rslt;
                if (!int.TryParse(context.Request.QueryString[QU_PARAMS_KEYS_ITEMS_COUNT], out rslt))
                    return MAX_ITEMS_COUNT;
                else
                    return rslt;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            _httpContext = context;
            System.Threading.Thread.CurrentThread.CurrentUICulture = DEFAULT_CULTURE;
            System.Threading.Thread.CurrentThread.CurrentCulture = DEFAULT_CULTURE;

            Rss.RssChannel channel = new Rss.RssChannel();
            channel.Title = GetChannelTitle(context);
            channel.Description = GetChannelDescription(context);
            channel.Link = context.Request.Url;
            List<Rss.RssItem> items = GenerateResultItems(context);
            foreach (Rss.RssItem item in items)
                channel.Items.Add(item);

            Rss.RssFeed feed = new Rss.RssFeed();
            feed.Encoding = System.Text.Encoding.Unicode;
            feed.Channels.Add(channel);
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.Unicode;
            feed.Write(context.Response.OutputStream);
            context.Response.End();
        }

        protected abstract List<Rss.RssItem> GenerateResultItems(HttpContext context);

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected abstract String GetChannelTitle(HttpContext context);
        protected abstract String GetChannelDescription(HttpContext context);

        private const String EMPTY_ITEM_GUID = "BB0F93B8-1417-4e5e-993E-3108F0372E1D";
        protected Rss.RssItem MakeValidEmptyRssItem()
        {
            Rss.RssItem rslt = new Rss.RssItem();
            rslt.Title = "... Стрічка пуста ...";
            rslt.Description = "Нічого не знайдено за Вашими критеріями пошуку";
            Rss.RssGuid guid = new Rss.RssGuid();
            guid.Name = EMPTY_ITEM_GUID;
            guid.PermaLink = Rss.DBBool.False;
            rslt.Guid = guid;
            //rslt.Link = 
            rslt.Guid = guid;
            rslt.PubDate = DateTime.Now;
            return rslt;
        }

    }
}