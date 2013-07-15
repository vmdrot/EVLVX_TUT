using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class Board
    {
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string closed { get; set; }
        public string idOrganization { get; set; }
        public string invited { get; set; }
        public string pinned { get; set; }
        public string url { get; set; }
        public Preference prefs { get; set; }
        public List<string> invitations { get; set; }
        public List<Membership> memberships { get; set; }
        public string shortUrl { get; set; }
        public string subscribed { get; set; }
        public List<string> labelNames { get; set; }
        public List<string> powerUps { get; set; }
        public string dateLastActivity { get; set; }
        public string dateLastView { get; set; }
        public List<CardsList> lists { get; set; }
        public List<Card> cards { get; set; }
        public List<Member> members { get; set; }
        public List<Checklist> checklists { get; set; }
        public List<Action> actions { get; set; }
    }
}
