using System.Collections.Generic;

namespace Nunit.TestResultsComparer.Lib.Data.Allure
{
    public class ScenarioRunResult : IAllureStartStopItem
    {
        public string uuid { get; set; }
        public string historyId { get; set; }
        public string fullName { get; set; }
        public List<Label> labels { get; set; }
        public List<string> links { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public StatusDetails statusDetails { get; set; }
        public string stage { get; set; }
        public List<Step> steps { get; set; }
        public List<string> attachments { get; set; }
        public List<Parameter> parameters { get; set; }
        public long start { get; set; }
        public long stop { get; set; }
        public string resultFileName { get; set; }
    }

    public class StatusDetails
    {
        public bool known { get; set; }
        public bool muted { get; set; }
        public bool flaky { get; set; }
        public string message { get; set; }
        public string trace { get; set; }

    }

    public class Step
    {
        public string name { get; set; }
        public string status { get; set; }
        public StatusDetails statusDetails { get; set; }
        public string stage { get; set; }
        public List<Step> steps { get; set; }
        public List<AttachmentInfo> attachments { get; set; }
        public List<string> parameters { get; set; }
        public long start { get; set; }
        public long stop { get; set; }
    }
    public class Label
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Parameter
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class AttachmentInfo
    {
        public string name { get; set; }
        public string source { get; set; }
        public string type { get; set; }
    }
}
