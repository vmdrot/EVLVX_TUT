using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Trello2JIRAConverterLib.Data.Trello
{
    public class Card
    {
        public string id { get; set; }
        public Badges badges { get; set; }
        public List<string> checkItemStates { get; set; }
        public string closed { get; set; }
        public string dateLastActivity { get; set; }
        public string desc { get; set; }
        public string due { get; set; }
        public string idBoard { get; set; }
        public List<string> idChecklists { get; set; }
        public string idList { get; set; }
        public List<string> idMembers { get; set; }
        public string idMembersVoted { get; set; }
        public string idShort { get; set; }
        public string idAttachmentCover { get; set; }
        public string manualCoverAttachment { get; set; }
        public List<string> labels { get; set; }
        public string name { get; set; }
        public string pos { get; set; }
        public string shortUrl { get; set; }
        public string subscribed { get; set; }
        public string url { get; set; }
        public List<CardAttachment> attachments { get; set; }
    }
}
