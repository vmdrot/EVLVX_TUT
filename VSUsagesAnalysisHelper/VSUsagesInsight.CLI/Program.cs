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
using VSUsages.Utilities;
using VSUsagesAnalysisHelperLib;

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
            List<VSUsageRec> vsus;
            List<MissingUsageRec> notFound = null;
            VSUsagesFinder finder = new VSUsagesFinder();
            if (!string.IsNullOrWhiteSpace(skipClassesStr))
                finder.SkipClasses = new List<string>(skipClassesStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
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
                Console.WriteLine(JsonConvert.SerializeObject(vsus, Formatting.Indented));
            else
                File.WriteAllText(outPath4All, JsonConvert.SerializeObject(vsus, Formatting.Indented), Encoding.UTF8);
            if (notFound != null && notFound.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(outPath4NotFound))
                {
                    Console.WriteLine(new string('-', 25));
                    Console.WriteLine("Not Found references come here:");
                    Console.WriteLine(new string('-', 25));
                    Console.WriteLine(JsonConvert.SerializeObject(notFound, Formatting.Indented));
                }
                else
                    File.WriteAllText(outPath4NotFound, JsonConvert.SerializeObject(notFound, Formatting.Indented), Encoding.UTF8);

            }
            if (!string.IsNullOrWhiteSpace(outPathSession))
                File.WriteAllText(outPathSession, JsonConvert.SerializeObject(new VSUsageRecSession() { Usages = vsus, SkipClasses = finder.SkipClasses}, Formatting.None), Encoding.UTF8);
            DateTime de = DateTime.Now;
            Console.WriteLine("Finished output: {0}", de.ToString("s"));
            Console.Beep();
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

        //public static int FindReferences(string[] args)
        //{
        //    string slnPath = args[0];
        //    string proj = args[1];
        //    string symbolType = args[2];
        //    string
        //    return 0;
        //}
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
