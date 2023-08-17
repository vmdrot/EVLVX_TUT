namespace Nunit.TestResultsComparer.Lib.Data.Allure.Analysis
{
    public class FailingScenarioInfo
    {
        public string SourceFile { get; set; }
        public string uuid { get; set; }
        public string ScenarioFullName { get; set; }
        public string ScenarioName { get; set; }
        public string FailingStepName { get; set; }
        public string ErrorMessage { get; set; }
        public string ExTrace1stLn { get; set; }
    }
}
