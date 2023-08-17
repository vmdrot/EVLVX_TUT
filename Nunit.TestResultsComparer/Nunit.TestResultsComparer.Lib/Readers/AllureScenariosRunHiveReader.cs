using Nunit.TestResultsComparer.Lib.Data.Allure;
using Nunit.TestResultsComparer.Lib.Data.Allure.Analysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nunit.TestResultsComparer.Lib.Readers
{
    public static class AllureScenariosRunHiveReader
    {
        public static ScenariosContainersResultsHive Read(string folderPath, bool suppressErrors = true)
        {
            List<string> containerFilePaths = ListFilesByMask(folderPath, "*-container.json");
            List<string> resultFilePaths = ListFilesByMask(folderPath, "*-result.json");
            List<string> attachmentsPaths = ListFilesByMask(folderPath, "*-attachment.txt");
            ScenariosContainersResultsHive rslt = new ScenariosContainersResultsHive();
            if (containerFilePaths != null && containerFilePaths.Any())
            {
                rslt.Containers = new Dictionary<string, ScenariosContainer>();
                containerFilePaths.ForEach(cp =>
                {
                    try
                    {
                        var currContainer = Newtonsoft.Json.JsonConvert.DeserializeObject<ScenariosContainer>(File.ReadAllText(cp));
                        rslt.Containers.Add(Path.GetFileNameWithoutExtension(cp), currContainer);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"{Path.GetFileName(cp)}:{ex}\r\n{new string('-',33)}");
                        if (!suppressErrors) throw;
                    }
                });
            }
            if (resultFilePaths != null && resultFilePaths.Any())
            {
                rslt.Results = new Dictionary<string, ScenarioRunResult>();
                resultFilePaths.ForEach(rp =>
                {
                    try
                    {
                        var currRslt = Newtonsoft.Json.JsonConvert.DeserializeObject<ScenarioRunResult>(File.ReadAllText(rp));
                        currRslt.resultFileName = Path.GetFileName(rp);
                        rslt.Results.Add(currRslt.uuid, currRslt);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"{Path.GetFileName(rp)}:{ex}\r\n{new string('-', 33)}");
                        if (!suppressErrors) throw;
                    }
                });
            }
            if (attachmentsPaths != null && attachmentsPaths.Any())
            {
                rslt.Attachements = new Dictionary<string, string>();
                attachmentsPaths.ForEach(ap =>
                {
                    try
                    {
                        rslt.Attachements.Add(Path.GetFileName(ap), File.ReadAllText(ap));
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"{Path.GetFileName(ap)}:{ex}\r\n{new string('-', 33)}");
                        if (!suppressErrors) throw;
                    }
                    
                });
            }
            return rslt;
        }

        public static List<FailingScenarioInfo> ExtractFailingOnes(ScenariosContainersResultsHive src)
        {
            var rslt = new List<FailingScenarioInfo>();
            var interim = src.Results.Where(r => r.Value.status == "failed").ToList();
            interim.ForEach(r =>
            {
                var failingStep = r.Value?.steps?.FirstOrDefault(s => s.status == "failed");
                rslt.Add(
                    new FailingScenarioInfo() 
                    {
                        uuid = r.Value?.uuid,
                        SourceFile = r.Value?.resultFileName,
                        ScenarioFullName = r.Value?.fullName,
                        ScenarioName = r.Value?.name,
                        FailingStepName = failingStep?.name,
                        ErrorMessage = r.Value?.statusDetails.message,
                        ExTrace1stLn = ExtractFirstLine(r.Value?.statusDetails.trace)
                    });
            });

            return rslt;
        }

        private static string ExtractFirstLine(string trace, int limit = 255)
        {
            if (string.IsNullOrWhiteSpace(trace)) return null;
            int nlPos = trace.IndexOf('\n');
            if (nlPos == -1) nlPos = trace.Length > limit ? limit : trace.Length;
            return trace.Substring(0, nlPos);
        }

        private static List<string> ListFilesByMask(string folderPath, string mask)
        {
            return new List<string>(Directory.GetFiles(folderPath, mask));
        }

    }
}
