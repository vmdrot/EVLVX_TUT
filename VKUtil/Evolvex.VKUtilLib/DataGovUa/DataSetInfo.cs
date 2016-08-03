using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtilLib.DataGovUa
{
    public class DataSetInfo
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Version { get; set; }
        public string Language { get; set; }
        public string Formats { get; set; }
        public string InformationOwner { get; set; }
        public string ResponsiblePerson { get; set; }
        public string ResponsiblePersonEmail { get; set; }
        public DateTime FirstPublished {get; set;}
        public DateTime LastChanged { get; set; }
        public string PlannedUpdatesFrequency { get; set; }
        public string ActualUpdateFrequency { get; set; }
        public string Keywords { get; set; }
        public List<VersionInfo> EarlierVersions { get; set; }

    }
}
