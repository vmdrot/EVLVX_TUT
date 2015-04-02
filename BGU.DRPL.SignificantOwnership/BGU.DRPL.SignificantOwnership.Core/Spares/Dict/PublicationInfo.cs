using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PublicationInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PublicationInfo
    {
        [DisplayName("Видавництво")]
        public PublishingHouseInfo Publisher { get; set; }
        // http://www.ukrbook.net/agentstvo.html
        [DisplayName("ІSBN ідентифікатор")]
        [Description("ІSBN ідентифікатор видання (якщо є)")]
        public string ISBN { get; set; }
        [DisplayName("Назва ЗМІ")]
        public string MediaName { get; set; }
        [DisplayName("Дата публікації")]
        public DateTime PubDate { get; set; }
        [DisplayName("Число публікації")]
        [Description("Число/№ публікації (випуск газети, журналу, тощо)")]
        public string IssueNr { get; set; }
        [DisplayName("URL публікації")]
        [Description("Ланка на публікацію (адреса публікації в інтернет)")]
        public Uri PubUrl { get; set; }
    }
}
