using Nunit.TestResultsComparer.Lib.Data.Allure;
using Nunit.TestResultsComparer.Lib.Data.Allure.Analysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nunit.TestResultsComparer.Lib.Tools;

namespace Nunit.TestResultsComparer.Lib.Readers
{
    public static class AllureScenariosRunHiveReader
    {
        public static ScenariosContainersResultsHive Read(string folderPath, bool suppressErrors = true, bool skipAttachments = false)
        {
            List<string> containerFilePaths = ListFilesByMask(folderPath, "*-container.json");
            List<string> resultFilePaths = ListFilesByMask(folderPath, "*-result.json");
            List<string> attachmentsPaths = skipAttachments ? null : ListFilesByMask(folderPath, "*-attachment.txt");
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
            if (!skipAttachments && attachmentsPaths != null && attachmentsPaths.Any())
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

            rslt.RunDate = ExtractRunDate(rslt);
            //CalcScenariosPerStep(rslt);
            return rslt;
        }

        private static void CalcScenariosPerStep(ScenariosContainersResultsHive rslt)
        {
            var subStepNames = new List<string>();
            var allSteps = rslt.Results.Values.Select(r => r.steps).ToList();
            allSteps.ForEach(ll => {
                ll.ForEach(s => {
                    //todo
                });
            });
            
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
                        ErrorMessage = ExtractFirstLine(r.Value?.statusDetails?.message),
                        ExcTrace1stLn = ExtractFirstLine(r.Value?.statusDetails?.trace)
                    });
            });
            CalcScenariosPerStep(rslt);
            return rslt;
        }

        private static void CalcScenariosPerStep(List<FailingScenarioInfo> rslt)
        {
            var stepNames = rslt.Select(s => s.FailingStepName).Distinct().ToList();
            var dict = new Dictionary<string, int>();
            stepNames.ForEach(s => {
                var cnt = rslt.Where(i => i.FailingStepName == s).Count();
                dict.Add(s, cnt);
            });
            rslt.ForEach(i => {
                i.ScenariosPerStep = dict[i.FailingStepName];
            });
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

        private static DateTime ExtractRunDate(ScenariosContainersResultsHive hive)
        {
            List<long> candidates = new List<long>();
            candidates.Add(ExtractEarliestStartDt(hive.Containers.Values));
            candidates.Add(ExtractEarliestStartDt(hive.Results.Values));
            long rslt = candidates.Min();
            /*
            Console.WriteLine($"{nameof(DateTime.FromBinary)}({rslt}):{DateTime.FromBinary(rslt)}");
            Console.WriteLine($"{nameof(DateTime.FromFileTime)}({rslt}):{DateTime.FromFileTime(rslt)}");
            Console.WriteLine($"{nameof(DateTime.FromFileTimeUtc)}({rslt}):{DateTime.FromFileTimeUtc(rslt)}");
            Console.WriteLine($"new DateTime({rslt}):{new DateTime(rslt)}");
            //Console.WriteLine($"{nameof(UnixTimeStampToDateTime)}({rslt}):{UnixTimeStampToDateTime(rslt)}");
            try {
                Console.WriteLine($"{nameof(EpochTimeExtensions.ToDateTimeFromEpoch)}({rslt}):{rslt.ToDateTimeFromEpoch()}");
            } catch { }
            try
            {
                Console.WriteLine($"{nameof(EpochTimeExtensions.ToDateTimeFromEpoch)}({rslt/1000}):{(rslt/1000).ToDateTimeFromEpoch()}");
            }
            catch { }
            try
            {
                var str = rslt.ToString();
                double trueTime = double.Parse($"{str.Substring(0, str.Length - 3)}.{str.Substring(str.Length - 3)}");
                Console.WriteLine($"{nameof(EpochTimeExtensions.ToDateTimeFromEpoch)}({trueTime}):{trueTime.ToDateTimeFromEpoch()}");
            }
            catch { }

            return DateTime.FromBinary(rslt);
            */
            var str = rslt.ToString();
            double trueTime = double.Parse($"{str.Substring(0, str.Length - 3)}.{str.Substring(str.Length - 3)}");
            return trueTime.ToDateTimeFromEpoch();
        }

        private static long ExtractEarliestStartDt<T>(Dictionary<string,T>.ValueCollection src) where T : IAllureStartStopItem
        {
            return src.Select(v => v.start).Min();
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            /*
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
            */
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp - dateTime.Ticks).DateTime;//UtcDateTime;
        }
    }
}
