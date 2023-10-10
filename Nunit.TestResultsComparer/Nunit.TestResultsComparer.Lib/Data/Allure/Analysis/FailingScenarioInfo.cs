namespace Nunit.TestResultsComparer.Lib.Data.Allure.Analysis
{
    public class FailingScenarioInfo : ScenarioRunInfoBase
    {
        public string SourceFile { get; set; }
        public string uuid { get; set; }
        public string ScenarioFullName { get; set; }
        public string ScenarioName { get; set; }
        public string FailingStepName { get; set; }
        public string ErrorMessage { get; set; }
        public string ExcTrace1stLn { get; set; }
        public int ScenariosPerStep { get; set; }
        public static bool AreTheSame(FailingScenarioInfo one, FailingScenarioInfo two)
        {
            return one?.ScenarioFullName == two?.ScenarioFullName
                && one?.FailingStepName == two?.FailingStepName
                && one?.ErrorMessage == two?.ErrorMessage
                && one?.ExcTrace1stLn == two?.ExcTrace1stLn;
        }

        public string LogicalKey
        {
            get => $"{ScenarioFullName}_{FailingStepName}_{ErrorMessage}_{ExcTrace1stLn}";
        }
    }
}
