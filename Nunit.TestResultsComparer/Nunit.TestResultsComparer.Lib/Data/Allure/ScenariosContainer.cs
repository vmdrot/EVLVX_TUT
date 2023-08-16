namespace Nunit.TestResultsComparer.Lib.Data.Allure
{
    public class ScenariosContainer
    {
        public string uuid { get; set; }
        public string[] children { get; set; }
        public Step[] befores { get; set; }
        public Step[] afters { get; set; }
        public string[] links { get; set; }
        public long start { get; set; }
        public long stop { get; set; }
    }
}
