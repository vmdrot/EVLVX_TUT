using Nunit.TestResultsComparer.Lib.Data.Allure.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nunit.TestResultsComparer.Lib.Comparers.Allure
{
    public class AllureMultipleRunsComparer
    {

        private string _delim;
        public AllureMultipleRunsComparer() { }
        public string Group(Dictionary<string, List<FailingScenarioInfo>> src, string delimiter = "\t", Dictionary<string, DateTime> runsDates = default)
        {
            _delim = delimiter;
            Dictionary<string,FailingScenarioInfo> failuresDist = new Dictionary<string,FailingScenarioInfo>();
            foreach(var key in src.Keys)
            {
                var currLst = src[key];
                currLst.ForEach(f => {
                    if (!failuresDist.ContainsKey(f.LogicalKey)) failuresDist.Add(f.LogicalKey, f);
                });
            }
            var failuresPresence = new Dictionary<string, List<string>>();
            foreach(var currFailureKey in failuresDist.Keys)
            {
                List<string> currRunNrs = new List<string>();
                foreach (var runNr in src.Keys)
                {
                    if (src[runNr].Any(f => f.LogicalKey == currFailureKey)) currRunNrs.Add(runNr);
                }
                failuresPresence.Add(currFailureKey, currRunNrs);
            }

            StringBuilder rslt = new StringBuilder();
            AppendHeader(rslt, src.Keys, runsDates);
            var sortedKeys = failuresPresence.OrderByDescending(p => p.Value.Count).Select(p => p.Key).ToList();
            foreach(var failKey in sortedKeys)
                AppendRow(rslt, src.Keys, failuresDist[failKey], failuresPresence[failKey]);
            return rslt.ToString();
        }


        private void AppendHeader(StringBuilder target, Dictionary<string, List<FailingScenarioInfo>>.KeyCollection keys, Dictionary<string, DateTime> runsDates)
        {
            StringBuilder rslt = new StringBuilder($"{nameof(FailingScenarioInfo.ScenarioFullName)}{_delim}{nameof(FailingScenarioInfo.FailingStepName)}{_delim}{nameof(FailingScenarioInfo.ErrorMessage)}{_delim}{nameof(FailingScenarioInfo.ExcTrace1stLn)}{_delim}{nameof(FailingScenarioInfo.ScenariosPerStep)}{_delim}Occurrences{_delim}Pct");
            foreach (var key in keys)
            {
                var currDtHdrPfx = string.Empty;
                if (runsDates != null && runsDates.Any())
                {
                    var currHasDate = runsDates.TryGetValue(key, out var runDt);
                    //currDtHdrPfx = currHasDate ? runDt.ToString("yyyyMMdd_HHmmss") : string.Empty;
                    currDtHdrPfx = currHasDate ? runDt.ToString("s") : string.Empty;
                }
                var currColHdr = string.IsNullOrWhiteSpace(currDtHdrPfx) ? $"{_delim}{key}" : $"{_delim}{key}({currDtHdrPfx})";
                rslt.Append(currColHdr);
            }
            target.AppendLine(rslt.ToString());
        }

        private void AppendRow(StringBuilder target, Dictionary<string, List<FailingScenarioInfo>>.KeyCollection keys, FailingScenarioInfo failingScenarioInfo, List<string> list)
        {
            decimal pct = Math.Round(100M * (decimal)list.Count / (decimal)keys.Count, 2);
            StringBuilder rslt = new StringBuilder($"{failingScenarioInfo.ScenarioFullName}{_delim}{failingScenarioInfo.FailingStepName}{_delim}{failingScenarioInfo.ErrorMessage}{_delim}{failingScenarioInfo.ExcTrace1stLn}{_delim}{failingScenarioInfo.ScenariosPerStep}{_delim}{list.Count}{_delim}{pct}%");
            foreach (var key in keys)
            {
                string currCellVal = list.Contains(key) ? "y" : "";
                rslt.Append($"{_delim}{currCellVal}");
            }
            target.AppendLine(rslt.ToString());
        }

    }
}
