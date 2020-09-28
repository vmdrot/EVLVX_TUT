using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VSUsages.Utilities;
using VSUsagesAnalysisHelperLib;
using VSUsagesAnalysisHelperLib.Spares;
using VSUsagesInsight.CLI.Spares;
using Project = Microsoft.CodeAnalysis.Project;

namespace VSUsagesInsight.CLI
{
    class Program
    {
        #region field(s)
        private static Dictionary<string, CmdHandler> _cmdHandlers;
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
            _cmdHandlers.Add(string.Empty, MainHandler);
            _cmdHandlers.Add("help", ShowUsage);
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
                LogCmdArgs(_cmdHandlers[keyArg].Method.Name, origArgs.ToArray());
                return _cmdHandlers[keyArg](origArgs.ToArray());
            }
            else
                return _cmdHandlers[string.Empty](args);
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
        //private static MSBuildWorkspace _workspace;
        //private static Solution _solution;
        public static int MainHandler(string[] args)
        {
            //Console.Read();
            DateTime ds = DateTime.Now;
            Console.WriteLine("Started: {0}", ds.ToString("s"));
            string slnPath = args[0];
            string projNm = args[1];
            string className = args[2];
            string methodName = args.Length > 3 ? args[3] : string.Empty;
            bool bFullTree;
            string sbFullTree = args.Length > 4 ? args[4] : string.Empty;
            if (!bool.TryParse(sbFullTree, out bFullTree))
                bFullTree = false;
            string skipClassesStr = args.Length > 5 ? args[5] : string.Empty;
            string outPath4All = args.Length > 6 ? args[6] : string.Empty;
            string outPath4NotFound = args.Length > 7 ? args[7] : string.Empty;
            string outPathSession = args.Length > 8 ? args[8] : string.Empty;
            string udfStr = args.Length > 9 ? args[9] : string.Empty;
            List<VSUsageRec> vsus;
            List<MissingUsageRec> notFound = null;
            VSUsagesFinder finder = new VSUsagesFinder();
            if (!string.IsNullOrWhiteSpace(skipClassesStr))
                finder.SkipClasses = new List<string>(skipClassesStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            if (!string.IsNullOrWhiteSpace(udfStr))
                finder.SearchForUDFs = new List<string>(udfStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            using (MSBuildWorkspace _workspace = MSBuildWorkspace.Create())
            {
                Solution sln = _workspace.OpenSolutionAsync(slnPath).Result;

                if (bFullTree)
                {
                    vsus = new List<VSUsageRec>();
                    if (!finder.FindUsagesRecursive(sln, projNm, className, methodName, vsus, out notFound))
                        Console.WriteLine("{0} failed", nameof(finder.FindUsagesRecursive));
                }
                else
                    vsus = finder.FindUsages(sln, projNm, className, methodName, string.Empty);
            }
            DateTime de1 = DateTime.Now;
            Console.WriteLine("Finished searching: {0}", de1.ToString("s"));
            if (string.IsNullOrWhiteSpace(outPath4All))
                Console.WriteLine(JsonConvert.SerializeObject(vsus, Newtonsoft.Json.Formatting.Indented));
            else
                File.WriteAllText(outPath4All, JsonConvert.SerializeObject(vsus, Newtonsoft.Json.Formatting.Indented), Encoding.UTF8);
            if (notFound != null && notFound.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(outPath4NotFound))
                {
                    Console.WriteLine(new string('-', 25));
                    Console.WriteLine("Not Found references come here:");
                    Console.WriteLine(new string('-', 25));
                    Console.WriteLine(JsonConvert.SerializeObject(notFound, Newtonsoft.Json.Formatting.Indented));
                }
                else
                    File.WriteAllText(outPath4NotFound, JsonConvert.SerializeObject(notFound, Newtonsoft.Json.Formatting.Indented), Encoding.UTF8);

            }
            if (!string.IsNullOrWhiteSpace(outPathSession))
                File.WriteAllText(outPathSession, JsonConvert.SerializeObject(new VSUsageRecSession() { Usages = vsus, SkipClasses = finder.SkipClasses }, Newtonsoft.Json.Formatting.None), Encoding.UTF8);
            DateTime de = DateTime.Now;
            Console.WriteLine("Finished output: {0}", de.ToString("s"));
            Console.Beep();
            return 0;
        }


        public static int ScanSlns(string[] args)
        {
            //Console.Read();
            string srcRoot = args[0];
            string typeName = args[1];
            string memberName = args.Length > 2 ? args[2] : string.Empty;
            string outPath = args.Length > 3 ? args[3] : string.Empty;
            string sTryScanRecursively = args.Length > 4 ? args[4] : string.Empty;
            string outMissing = args.Length > 5 ? args[5] : string.Empty;
            string outSession = args.Length > 6 ? args[6] : string.Empty;
            string ignoreSlnsListPath = args.Length > 7 ? args[7] : string.Empty;
            bool bTryScanRecursively;
            List<string> ignoreSlns = new List<string>();
            if (!string.IsNullOrWhiteSpace(ignoreSlnsListPath) && File.Exists(ignoreSlnsListPath))
                ignoreSlns.AddRange(File.ReadAllLines(ignoreSlnsListPath).Where(s => s != null && s.Trim().Length > 0));
            if (!string.IsNullOrWhiteSpace(sTryScanRecursively))
            {
                if (!bool.TryParse(sTryScanRecursively, out bTryScanRecursively))
                    bTryScanRecursively = false;
            }
            else
                bTryScanRecursively = false;
            string[] slns = Directory.GetFiles(srcRoot, "*.sln", SearchOption.AllDirectories);
            List<VSUsageRec> all = new List<VSUsageRec>();
            List<MissingUsageRec> allMissing = new List<MissingUsageRec>();
            Dictionary<string, bool> slnRslts = new Dictionary<string, bool>();
            foreach (string slnPath in slns)
            {
                if (ignoreSlns.Any(s => s.ToLower() == slnPath.ToLower()))
                    continue;
                try
                {
                    using (MSBuildWorkspace _workspace = MSBuildWorkspace.Create())
                    {
                        Solution sln = _workspace.OpenSolutionAsync(slnPath).ConfigureAwait(false).GetAwaiter().GetResult();
                        VSUsagesFinder finder = new VSUsagesFinder();
                        List<VSUsageRec> vsus = null;
                        if (!bTryScanRecursively)
                        {
                            vsus = finder.FindUsages(sln, string.Empty, typeName, memberName, string.Empty, true);
                            if (vsus != null && vsus.Count > 0)
                                all.AddRange(vsus);
                        }
                        else
                        {
                            vsus = new List<VSUsageRec>();
                            List<MissingUsageRec> currMissing;
                            finder.FindUsagesRecursive(sln, string.Empty, typeName, memberName, vsus, out currMissing);
                            if (currMissing != null && currMissing.Count > 0)
                                allMissing.AddRange(currMissing);

                        }
                        slnRslts.Add(slnPath, true);
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine("An error occurred scanning sln '{0}': {1}", slnPath, exc);
                    slnRslts.Add(slnPath, false);
                }
            }
            Console.WriteLine("{0} = {1}", nameof(all.Count), all.Count);
            if (!string.IsNullOrWhiteSpace(outPath))
                File.WriteAllText(outPath, JsonConvert.SerializeObject(all, Newtonsoft.Json.Formatting.Indented), Encoding.UTF8);
            else
                Console.WriteLine(JsonConvert.SerializeObject(all, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine(new string('=', 25));
            Console.WriteLine("suceeded: {0} of {1}", slnRslts.Count(d => d.Value), slnRslts.Count);
            Console.WriteLine(new string('-', 25));
            Console.WriteLine(JsonConvert.SerializeObject(slnRslts, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine(new string('-', 25));
            Console.WriteLine("failed: {0} of {1}", slnRslts.Count(d => !d.Value), slnRslts.Count);
            Console.WriteLine(string.Join("\n", slnRslts.Where(r => !r.Value).Select(r => r.Key).ToArray()));
            if (bTryScanRecursively)
            {
                if (!string.IsNullOrWhiteSpace(outMissing))
                    File.WriteAllText(outMissing, JsonConvert.SerializeObject(allMissing), Encoding.UTF8);
                else
                {
                    Console.WriteLine(new string('-', 25));
                    Console.WriteLine("Missing references({0}):", allMissing.Count);
                    Console.WriteLine(new string('-', 25));
                    Console.WriteLine(JsonConvert.SerializeObject(allMissing, Newtonsoft.Json.Formatting.Indented));
                }
            }
            if (!string.IsNullOrWhiteSpace(outSession))
                File.WriteAllText(outSession, JsonConvert.SerializeObject(new VSUsageRecSession() { Usages = all }), Encoding.UTF8);
            Console.Beep();
            return 0;
        }

        public static int SolutionProjectsStats(string[] args)
        {
            //Console.Read();
            string slnPath = args[0];

            using (MSBuildWorkspace _workspace = MSBuildWorkspace.Create())
            {
                Solution sln = _workspace.OpenSolutionAsync(slnPath).ConfigureAwait(false).GetAwaiter().GetResult();
                foreach (var proj in sln.Projects)
                {
                    Console.WriteLine(proj.CompilationOptions.OutputKind.ToString());
                }
            }
            return 0;
        }
        public static int Json2Csv(string[] args)
        {

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(File.ReadAllText(args[0]));
            if (Tools.DataTableToCSV(dt, args[1], true))
                return 0;
            else
                return 1;
        }

        public static int JoinVSUS(string[] args)
        {
            List<VSUsageRec> all = new List<VSUsageRec>();
            for (int i = 0; i < args.Length - 1; i++)
            {
                string file = args[i];
                if (string.IsNullOrWhiteSpace(file))
                    continue;
                if (!File.Exists(file))
                    continue;
                List<VSUsageRec> curr = JsonConvert.DeserializeObject<List<VSUsageRec>>(File.ReadAllText(file, Encoding.UTF8));
                if (curr != null && curr.Count > 0)
                    all.AddRange(curr);
            }
            File.WriteAllText(args[args.Length - 1], JsonConvert.SerializeObject(all, Newtonsoft.Json.Formatting.Indented), Encoding.UTF8);
            return 0;
        }


        public static int ListAllSlnsNugetPackages(string[] args)
        {
            //Console.Read();
            string slnsPathsStr = args[0];
            string detectLatestStableStr = args.Length > 1 ? args[1] : string.Empty;
            bool detectLatestStable = !string.IsNullOrWhiteSpace(detectLatestStableStr) ? bool.Parse(detectLatestStableStr) : false;

            string[] slnPaths = slnsPathsStr.Split(';');
            List<NugetPackageRec> allPckgRecs = new List<NugetPackageRec>();
            foreach (string slnPathRoot in slnPaths)
            {
                string[] allSlns = Directory.GetFiles(slnPathRoot, "*.sln", SearchOption.AllDirectories);
                foreach (string slnPath in allSlns)
                {
                    try
                    {
                        using (MSBuildWorkspace _workspace = MSBuildWorkspace.Create())
                        {
                            Solution sln = _workspace.OpenSolutionAsync(slnPath).ConfigureAwait(false).GetAwaiter().GetResult();
                            var currPckgs = NugetPackagesHelper.ListAllSlnNugetPackages(sln, detectLatestStable);
                            if (currPckgs != null && currPckgs.Any())
                            {
                                string slnFileName = Path.GetFileName(slnPath);
                                currPckgs.ForEach(p => { p.SolutionPath = slnPath; p.SolutionName = slnFileName; });
                                allPckgRecs.AddRange(currPckgs);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine("Error processing sln '{0}': {1}", slnPath, ex);
                    }
                }
            }
            Console.WriteLine(JsonConvert.SerializeObject(allPckgRecs, Newtonsoft.Json.Formatting.Indented));
            return 0;
        }

        public static int ListAllSlnNugetPackages(string[] args)
        {
            //Console.Read();
            string slnPath = args[0];
            string detectLatestStableStr = args.Length > 1 ? args[1] : string.Empty;
            bool detectLatestStable = !string.IsNullOrWhiteSpace(detectLatestStableStr) ? bool.Parse(detectLatestStableStr) : false;



            using (MSBuildWorkspace _workspace = MSBuildWorkspace.Create())
            {
                Solution sln = _workspace.OpenSolutionAsync(slnPath).ConfigureAwait(false).GetAwaiter().GetResult();
                Console.WriteLine(JsonConvert.SerializeObject(NugetPackagesHelper.ListAllSlnNugetPackages(sln, detectLatestStable), Newtonsoft.Json.Formatting.Indented));
            }
            return 0;
        }

        public static int GetLatestStablePackage(string[] args)
        {
            Console.Read();
            Console.WriteLine("{0}\t{1}", args[0], NugetPackagesHelper.DetectLatestStable(args[0]));

            return 0;
        }
        //public static int FindReferences(string[] args)
        //{
        //    string slnPath = args[0];
        //    string proj = args[1];
        //    string symbolType = args[2];
        //    string
        //    return 0;
        //}

        public static int ListAllAssemblySymbols(string[] args)
        {
            string assemblyPath = args[0];
            //Assembly asm = Assembly.LoadFile(assemblyPath);
            Assembly asm = Assembly.LoadFrom(assemblyPath);
            List<string> allPublicTypeNames = new List<string>();
            foreach (var ti in asm.DefinedTypes)
            {
                if (!ti.IsPublic)
                    continue;
                allPublicTypeNames.Add(ti.FullName);
            }

            Console.WriteLine($"Count = {allPublicTypeNames.Count}");
            Console.WriteLine(JsonConvert.SerializeObject(allPublicTypeNames, Newtonsoft.Json.Formatting.Indented));
            return 0;
        }
        public static int ListAllAssembliesByNugetPackage(string[] args)
        {
            Console.Read();
            string slnPath = args[0];
            string packageId = args[1];
            string pkgVersion = args.Length > 2 ? args[2] : string.Empty;
            using (MSBuildWorkspace _workspace = MSBuildWorkspace.Create())
            {
                Solution sln = _workspace.OpenSolutionAsync(slnPath).ConfigureAwait(false).GetAwaiter().GetResult();
                Console.WriteLine(JsonConvert.SerializeObject(NugetPackagesHelper.ListAllAssembliesByNugetPackage(sln, packageId, pkgVersion), Newtonsoft.Json.Formatting.Indented));
            }
            return 0;
        }
        public static int GetPackagesRoot(string[] args)
        {
            Console.WriteLine(NugetPackagesHelper.GetPackagesRoot(args[0]));
            return 0;
        }


        public static int FindAssemblyUsages(string[] args)
        {
            
            return 0;
        }

        public static int ListTargetFrameworks(string[] args)
        {
            //Console.Read();
            string slnPath = args[0];
            List<VSProjectInfo> rslt = new List<VSProjectInfo>();
            using (MSBuildWorkspace _workspace = MSBuildWorkspace.Create())
            {
                Solution sln = _workspace.OpenSolutionAsync(slnPath).ConfigureAwait(false).GetAwaiter().GetResult();
                Dictionary<string, int> buildOrders = GetSlnBuildOrder(sln.Projects);
                foreach (var proj in sln.Projects)
                {
                    VSProjectInfo pi = new VSProjectInfo() { Name = proj.Name, BuildOrder = buildOrders[proj.Name] };
                    XmlDocument projXml = new XmlDocument();
                    projXml.Load(proj.FilePath);
                    bool bFound = false;
                    for (int i = 0; i < projXml.DocumentElement.ChildNodes.Count; i++)
                    {
                        var currChild = projXml.DocumentElement.ChildNodes[i];
                        if (currChild.Name != "PropertyGroup")
                            continue;
                        for (int j = 0; j < currChild.ChildNodes.Count; j++)
                        {
                            var currPGChild = currChild.ChildNodes[j];
                            if (currPGChild.Name == "TargetFrameworkVersion")
                            {
                                pi.TargetFramework = currPGChild.InnerText;
                                bFound = true;
                            }
                        }
                        if (bFound)
                            break;
                    }
                    
                    //rslt.Add(new VSUsagesAnalysisHelperLib.Spares.ProjectInfo() { Name = proj.Name, TargetFramework = "" });
                    //var tfvNode = projXml.SelectSingleNode("/Project/PropertyGroup/TargetFrameworkVersion");
                    //if (tfvNode != null)
                    //    pi.TargetFramework = tfvNode.InnerText;
                    rslt.Add(pi);
                }
                Console.WriteLine(JsonConvert.SerializeObject(rslt, Newtonsoft.Json.Formatting.Indented));
            }
            return 0;
        }

        private static Dictionary<string, int> GetSlnBuildOrder(IEnumerable<Microsoft.CodeAnalysis.Project> projects)
        {
            List<Project> lstProjs = new List<Microsoft.CodeAnalysis.Project>(projects);
            Dictionary<string, int> rslt = new Dictionary<string, int>();
            Dictionary<string, List<string>> dirDeps = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> fullDeps = new Dictionary<string, List<string>>();
            //get direct dependencies first
            foreach(var proj in lstProjs)
            {
                List<string> currDeps = new List<string>();
                foreach (var prjRef in proj.AllProjectReferences)
                {
                    string refNm = lstProjs.Where(p => p.Id.Equals(prjRef.ProjectId))?.FirstOrDefault()?.Name;
                    if (!string.IsNullOrWhiteSpace(refNm))
                        currDeps.Add(refNm);
                }
                dirDeps.Add(proj.Name, currDeps);
            }
            //then, build a full dependencies graph
            foreach (string key in dirDeps.Keys)
            {
                fullDeps.Add(key, GetAllTransitiveDependencies(key, dirDeps));
            }
            var orderedByDeps = fullDeps.OrderBy(p => p.Value.Count);
            int currOrder = 0;
            foreach (var orderedDepcy in orderedByDeps)
            {
                rslt.Add(orderedDepcy.Key, currOrder++);
            }

            return rslt;
        }

        private static List<string> GetAllTransitiveDependencies(string key, Dictionary<string, List<string>> dirDeps)
        {
            List<string> rslt = new List<string>(dirDeps[key]);
            foreach (string dirDepcy in dirDeps[key])
                IncrementDependencies(dirDepcy, dirDeps, rslt);
            return rslt;
        }

        private static void IncrementDependencies(string topDepcy, Dictionary<string, List<string>> dirDeps, List<string> rslt)
        {
            foreach(string dirDepcy in dirDeps[topDepcy])
            {
                foreach (string trnDepcy in dirDeps[dirDepcy])
                {
                    if (!rslt.Contains(trnDepcy))
                        rslt.Add(trnDepcy);
                    IncrementDependencies(trnDepcy, dirDeps, rslt);
                }
            }
        }

        private 

        //private static List<string> GetAllTransitiveDependencies()

        #endregion

        #region FFR
        static void Main_old(string[] args)
        {
            using (MSBuildWorkspace _workspace = MSBuildWorkspace.Create())
            {
                Solution sln = _workspace.OpenSolutionAsync(@"D:\git\VRTF\QQNG\QQCatalystMain\QQSolutions.NextGen.Web\CatalystWebsiteWithAPI.sln").Result;
                Console.WriteLine("{0} = {1}", nameof(sln.Projects), sln?.Projects?.Count());
                foreach (var proj in sln.Projects)
                {
                    //if (proj.Name != "QQSolutions.NextGen.WebApi")
                    if (proj.Name != "QQSolutions.NextGen.Data")
                        continue;
                    var semModel = proj.Documents.First().GetSemanticModelAsync().Result;
                    var syntRoot = semModel.SyntaxTree.GetRoot();
                    var method = syntRoot.DescendantNodes().OfType<MethodDeclarationSyntax>().First();
                    var compilation = proj.GetCompilationAsync().Result;
                    var classSymbol = compilation.GetTypeByMetadataName("QQSolutions.NextGen.Data.AzureBlob.BlobStorage");
                    //var methSymbols = compilation.GetSymbolsWithName()

                    //ISymbol methSymbol = (ISymbol)method.GetReference();
                    //if (methSymbol != null)
                    //{
                    var refs = SymbolFinder.FindReferencesAsync(classSymbol, sln).Result;
                    if (refs != null)
                    {
                        Console.WriteLine("{0} = {1}", nameof(refs), refs.Count());
                        //Console.Read();
                        foreach (var oRef in refs)
                        {
                            Console.WriteLine("Locations = {0}", oRef.Locations.Count());
                            foreach (var oLoc in oRef.Locations)
                            {
                                Console.WriteLine("Location = {0}", oLoc.ToString(), oLoc);
                                Console.WriteLine("oLoc.Location = {0}", oLoc.Location.ToString(), oLoc);
                                Console.WriteLine("SourceSpan: {{Start: {0}, End:{1}, Length:{2} }}", oLoc.Location.SourceSpan.Start, oLoc.Location.SourceSpan.End, oLoc.Location.SourceSpan.Length);
                                var tree = oLoc.Location.SourceTree;
                                var treeRoot = tree.GetRootAsync().Result;
                                SyntaxNode node = treeRoot.FindNode(oLoc.Location.SourceSpan);

                                SourceText srcTxt;
                                if (tree.TryGetText(out srcTxt))
                                {
                                    string srcLine = GetSourceTextBySpan(tree, oLoc.Location.SourceSpan, srcTxt);
                                    MethodDeclarationSyntax containingMember = GetContainingMember(node);
                                    ClassDeclarationSyntax containingType = GetContainingType(containingMember);
                                    string parentNodeSrc = string.Empty;
                                    string classSrc = string.Empty;
                                    if (containingMember != null)
                                    {
                                        parentNodeSrc = GetSourceTextBySpan(tree, containingMember.Span, srcTxt);

                                    }
                                    if (containingType != null)
                                    {
                                        classSrc = GetSourceTextBySpan(tree, containingType.Span, srcTxt);
                                    }
                                    Console.WriteLine(new string('-', 20));
                                    Console.WriteLine("{0} = {1}", nameof(srcLine), srcLine);
                                    Console.WriteLine(new string('-', 20));
                                    Console.WriteLine("{0} = {1}", nameof(parentNodeSrc), parentNodeSrc);
                                    Console.WriteLine(new string('-', 20));
                                    Console.WriteLine("{0} = {1}", nameof(classSrc), classSrc);
                                    Console.WriteLine(new string('=', 20));
                                }
                                //oLoc.Location.GetLineSpan()
                            }
                        }
                    }
                    //}
                    //Console.Read();
                }
            }
            //Console.Read();
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

        private static MethodDeclarationSyntax GetContainingMember(SyntaxNode node)
        {
            SyntaxNode parentNode = node;
            while (!(parentNode is MethodDeclarationSyntax))
            {
                if (parentNode == null)
                    return null;
                parentNode = parentNode.Parent;
            }
            return parentNode as MethodDeclarationSyntax;
        }

        private static ClassDeclarationSyntax GetContainingType(SyntaxNode node)
        {
            SyntaxNode parentNode = node;
            while (!(parentNode is ClassDeclarationSyntax))
            {
                if (parentNode == null)
                    return null;
                parentNode = parentNode.Parent;
            }
            return parentNode as ClassDeclarationSyntax;
        }

        private static string GetSourceTextBySpan(SyntaxTree tree, TextSpan span, SourceText srcTxt)
        {
            var srcSpan = tree.GetLineSpan(span);
            if (srcSpan.StartLinePosition.Line == srcSpan.EndLinePosition.Line)
            {
                string srcLine = srcTxt.Lines[srcSpan.StartLinePosition.Line].ToString().Substring(srcSpan.StartLinePosition.Character, srcSpan.EndLinePosition.Character - srcSpan.StartLinePosition.Character);
                return srcLine;
            }
            else
                return srcTxt.Lines[srcSpan.StartLinePosition.Line].ToString();
        }
        #endregion

    }
}
