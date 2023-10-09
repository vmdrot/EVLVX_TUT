using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Nunit.TestResultsComparer.Lib;
using Nunit.TestResultsComparer.Lib.Comparer;
using Nunit.TestResultsComparer.Lib.Comparers.Allure;
using Nunit.TestResultsComparer.Lib.Data.Allure.Analysis;
using Nunit.TestResultsComparer.Lib.Data.TestResultsXml;
using Nunit.TestResultsComparer.Lib.Readers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nunit.TestResultsComparer.CLI
{
    internal class Program
    {
        #region field(s)
        private static Dictionary<string, CmdHandler> _cmdHandlers;
        private static bool _noBuzz = false;
        #endregion

        #region inner type(s)
        private delegate int CmdHandler(string[] args);
        #endregion

        #region Init CMD handlers
        private static Dictionary<string, CmdHandler> AutoDetectCMDHandlers()
        {
            Dictionary<string, CmdHandler> rslt = new Dictionary<string, CmdHandler>();
            Type thisType = typeof(Program);
            MethodInfo[] methods = thisType.GetMethods();
            foreach (MethodInfo mi in methods)
            {
                if (mi.Name == nameof(Main))
                    continue;
                if (mi.ReturnType != typeof(int))
                    continue;
                ParameterInfo[] args = mi.GetParameters();
                if (args == null || args.Length != 1)
                    continue;
                if (args[0].ToString() != "System.String[] args")
                    continue;
                rslt.Add(mi.Name.ToLower(), (CmdHandler)mi.CreateDelegate(typeof(CmdHandler)));
            }
            return rslt;
        }
        private static void FillCmdHandlers()
        {
            #region auto-detect
            _cmdHandlers = AutoDetectCMDHandlers();
            #endregion

            #region fill necessary CMD aliases (if any)
            _cmdHandlers.Add(string.Empty, ShowUsage);
            #endregion
        }
        #endregion

        #region the sacred Main()
        public static int Main(string[] args)
        {
            FillCmdHandlers();
            string keyArg = args.Length > 0 ? args[0].ToLower() : null;
            if (!string.IsNullOrWhiteSpace(keyArg) && _cmdHandlers.ContainsKey(keyArg))
            {
                List<string> origArgs = new List<string>(args);
                origArgs.RemoveAt(0);
                bool isDebug = false;
                int debugSwitchPos = DetectSpecialSwitch(origArgs,"-Debug");
                if (debugSwitchPos != -1)
                {
                    isDebug = true;
                    origArgs.RemoveAt(debugSwitchPos);
                }

                if (isDebug)
                    Console.Read();

                int noBuzzPos = DetectSpecialSwitch(origArgs, "-SkipLogo");
                if (noBuzzPos != -1)
                {
                    _noBuzz = true;
                    origArgs.RemoveAt(noBuzzPos);
                }

                if (!_noBuzz) LogCmdArgs(_cmdHandlers[keyArg].Method.Name, origArgs.ToArray());
                return _cmdHandlers[keyArg](origArgs.ToArray());
            }
            else
                return _cmdHandlers[string.Empty](args);
        }

        private static int DetectSpecialSwitch(List<string> args, string switchName)
        {
            //const string DebugSwitch = "-Debug";
            return args.FindIndex(a => a == switchName || a.ToLower() == switchName.ToLower());
        }
        #endregion

        #region usage & help
        public static int ShowUsage(string[] args)
        {
            Console.WriteLine("A valid command key must be supplied. See below the list of available commands:");
            foreach (string key in _cmdHandlers.Keys)
                Console.WriteLine("  {0}", key);
            return 1;
        }
        #endregion

        #region applied CMD handlers
        public static int ReadTest(string[] args)
        {
            string xmlPath = args[0];
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            NunitTestRun ntr = (new TestRunReader()).Read(doc.DocumentElement.SelectSingleNode("/test-run"));
            Console.WriteLine(JsonConvert.SerializeObject(ntr, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            return 0;
        }

        public static int CompareTest(string[] args)
        {
            string path1 = args[0];
            string path2 = args[1];
            DetailedComparisonResult detailed;
            int cmpRes = (new NunitTestRunComparer()).Compare(ReadTestRunWorker(path1), ReadTestRunWorker(path2), out detailed);
            if (!_noBuzz) Console.WriteLine($"{nameof(cmpRes)}:{cmpRes}");
            return cmpRes;
        }

        private static NunitTestRun ReadTestRunWorker(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            return (new TestRunReader()).Read(doc.DocumentElement.SelectSingleNode("/test-run"));
        }

        private static void PWSHExpore()
        {
            //System.IO.File.WriteAllText()
        }

        public static int CompareWebHealthTabbedFiles(string[] args)
        {
            string path1 = args[0];
            string path2 = args[1];
            string keyColNm = args[2];
            string resColNm = args[3];
            var dt1 = TextDelimitedFileReader.Read(path1, '\t', 2);
            var dt2 = TextDelimitedFileReader.Read(path2, '\t', 2);
            if (dt1 == null && dt2 == null) return 0;
            if (dt1 == null || dt2 == null) return int.MinValue;
            if (dt1.Rows.Count != dt2.Rows.Count) return 10*(dt1.Rows.Count - dt2.Rows.Count);
            DataTable merged = dt1.AsEnumerable()
                .Where(ra => dt2.AsEnumerable()
                .Any(rb => rb.Field<string>(keyColNm) == ra.Field<string>(keyColNm) && int.Parse(rb.Field<string>(resColNm).Trim()) == int.Parse(ra.Field<string>(resColNm).Trim()))).CopyToDataTable();
            return 100 * (dt1.Rows.Count - merged.Rows.Count);
        }

        public static int ReadAllureFolder(string[] args)
        {
            string path = args[0];
            bool skipAttachs = args.Length > 1 ? bool.Parse(args[1]) : false;
            var hive = AllureScenariosRunHiveReader.Read(folderPath: path, skipAttachments: skipAttachs);
            Console.WriteLine(hive.Containers?.Count);
            Console.WriteLine(hive.Results?.Count);
            Console.WriteLine(hive.Attachements?.Count);
            Console.WriteLine(new string('=', 33));
            Console.WriteLine(JsonConvert.SerializeObject(hive, Newtonsoft.Json.Formatting.Indented));
            return 0;
        }

        public static int AllureExtractFailed(string[] args)
        {
            string path = args[0];
            bool withUuidAndFileName = args.Length > 1 ? bool.Parse(args[1]) : false;
            var hive = AllureScenariosRunHiveReader.Read(path);
            var rslt = hive.Results.Where(r => r.Value.status == "failed").ToList();
            rslt.ForEach(r => 
                {
                    var pfx = withUuidAndFileName ? $"{r.Key}\t{r.Value?.resultFileName}\t" : string.Empty;
                    Console.WriteLine($"{pfx}{r.Value?.fullName}\t{r.Value?.steps?.FirstOrDefault(s => s.status == "failed")?.name}");
                });
            return 0;
        }

        public static int AllureExtractFailed2(string[] args)
        {
            string path = args[0];
            Console.WriteLine(JsonConvert.SerializeObject(AllureScenariosRunHiveReader.ExtractFailingOnes(AllureScenariosRunHiveReader.Read(path)), Newtonsoft.Json.Formatting.Indented));
            return 0;
        }

        public static int CompareMultipleRuns(string[] args)
        {
            string inputFile = args[0];
            Dictionary<string,string> inputs = new Dictionary<string,string>();
            #region Read inputs
            var inputLns = new List<string>(File.ReadAllLines(inputFile));
            inputLns.ForEach(ln => 
                {
                    if (!string.IsNullOrWhiteSpace(ln))
                    {
                        var flds = ln.Split('\t');
                        if (flds.Length == 2) 
                        {
                            inputs.Add(flds[0]?.Trim(), flds[1]?.Trim());
                        }
                    }
                });
            #endregion

            var failsHives = new Dictionary<string, List<FailingScenarioInfo>>();
            var runsDates = new Dictionary<string, DateTime>();
            foreach (var runNr in inputs.Keys)
            {
                var hive = AllureScenariosRunHiveReader.Read(folderPath: inputs[runNr], skipAttachments: true);
                failsHives.Add(runNr, AllureScenariosRunHiveReader.ExtractFailingOnes(hive));
                runsDates.Add(runNr, hive.RunDate);
            }
            var cmprr = new AllureMultipleRunsComparer();
            Console.WriteLine(cmprr.Group(failsHives, runsDates:runsDates));
            return 0;
        }

        #endregion
        #region aux
        public static int ExitWithComplaints(string msg, int ret)
        {
            return ExitWithComplaints(msg, ret, false);
        }
        public static int ExitWithComplaints(string msg, int ret, bool beep)
        {
            Console.WriteLine(msg);
            if (beep)
                Console.Beep();
            return ret;
        }

        private static void LogCmdArgs(string methodName, string[] args)
        {
            StringBuilder sbArgs = new StringBuilder();
            sbArgs.AppendFormat("routed to method {0}(", methodName);
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                    sbArgs.AppendLine(string.Format("[{0}]: '{1}'", i, args[i]));
            }
            sbArgs.AppendLine(")");
            Console.WriteLine(sbArgs.ToString());
        }
        #endregion
    }
}
