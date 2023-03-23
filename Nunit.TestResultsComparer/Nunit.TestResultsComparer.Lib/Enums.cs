namespace Nunit.TestResultsComparer.Lib
{
    public enum TestResult
    {
        None = 0,
        Passed,
        Failed
    }

    public enum RunState
    {
        None = 0,
        NotRunnable,
        Runnable,
        Explicit,
        Skipped,
        Ignored
    }

    public enum TestSuiteType
    {
        None = 0,Project, Assembly, TestSuite, TestFixture, GenericFixture, ParameterizedFixture, SetUpFixture, GenericMethod, ParameterizedMethod, Theory
    }
    public enum FailureSite
    {
        None = 0, Test, SetUp, TearDown, Parent, Child
    }

    public enum CompareResult
    {
        Equal = 0,
        IncompatiblePair,
        Different,
        Improved,
        Worsened
    }
}