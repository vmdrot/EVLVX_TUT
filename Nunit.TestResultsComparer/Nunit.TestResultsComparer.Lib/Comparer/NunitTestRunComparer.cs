using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib.Comparer
{
    public class NunitTestRunComparer
    {
        public int Compare(NunitTestRun one, NunitTestRun two, out DetailedComparisonResult details)
        {
            if (!CheckCompatible(one, two))
            {
                details = DetailedComparisonResult.Empty;
                return (int)CompareResult.IncompatiblePair;
            }
            if (AggregatesEqual(one, two))
            {
                details = DetailedComparisonResult.Empty;
                return (int)CompareResult.Equal;
            }
            details = DetailedCompare(one, two);
            return (int)CompareResult.Different;
        }

        private DetailedComparisonResult DetailedCompare(NunitTestRun one, NunitTestRun two)
        {
            return DetailedComparisonResult.Empty; //todo
        }

        private bool CheckCompatible(NunitTestRun one, NunitTestRun two)
        {
            NunitTestSuite assemblyTS1 = FindAssemblyTestSuite(one);
            NunitTestSuite assemblyTS2 = FindAssemblyTestSuite(two);
            if (assemblyTS1 == null || assemblyTS2 == null)
                return false;
            return assemblyTS1.name.Equals(assemblyTS2.name) && assemblyTS1.fullname.Equals(assemblyTS2.fullname, StringComparison.OrdinalIgnoreCase);
        }

        private NunitTestSuite FindAssemblyTestSuite(NunitTestRun subj)
        {
            if (subj.TestSuite?.type == TestSuiteType.Assembly) return subj.TestSuite;
            return null;//todo
        }

        private bool AggregatesEqual(NunitTestRun one, NunitTestRun two)
        {
            if (one.asserts != two.asserts) return false;
            if (one.passed!= two.passed) return false;
            if (one.warnings!= two.warnings) return false;
            if (one.skipped!= two.skipped) return false;
            if (one.failed!= two.failed) return false;
            if (one.inconclusive!= two.inconclusive) return false;
            if (one.result != two.result) return false;
            return true;
        }
    }
}
