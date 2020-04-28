using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSUsagesAnalysisHelperLib;

namespace VSUsagesInsight.CLI
{
    public class VSUsagesFinder
    {
        public VSUsagesFinder()
        {
            this.SkipClasses = new List<string>();
        }

        public List<string> SkipClasses { get; set; }
        public List<VSUsageRec> FindUsages(Solution sln, string projectName, string typeName, string memberName, string defFile)
        {
            List<VSUsageRec> rslt = new List<VSUsageRec>();
            var proj = sln.Projects.FirstOrDefault(p => p.Name == projectName);
            if (proj == null)
                return rslt;
            //var semModel = proj.Documents.First().GetSemanticModelAsync().Result;
            //var syntRoot = semModel.SyntaxTree.GetRoot();
            //var method = syntRoot.DescendantNodes().OfType<MethodDeclarationSyntax>().First();
            var compilation = proj.GetCompilationAsync().Result;
            ISymbol symbol2Find = null;
            var classSymbol = compilation.GetTypeByMetadataName(typeName);
            if (classSymbol == null)
            {
                //Console.WriteLine("Unable to get {0} for '{1}'", nameof(classSymbol), typeName);
                return rslt;
            }
            string defType = classSymbol.Name;
            if (!string.IsNullOrWhiteSpace(memberName))
            {
                var methSymbols = classSymbol.GetMembers(memberName);
                if (methSymbols != null)
                    symbol2Find = methSymbols.FirstOrDefault();
            }
            else
                symbol2Find = classSymbol;
            if (symbol2Find == null)
                return rslt;

            var refs = SymbolFinder.FindReferencesAsync(symbol2Find, sln).Result;
            if (refs == null)
                return rslt;
            foreach (var oRef in refs)
            {
                //Console.WriteLine("Locations = {0}", oRef.Locations.Count());
                foreach (var oLoc in oRef.Locations)
                {
                    //Console.WriteLine("Location = {0}", oLoc.ToString(), oLoc);
                    //Console.WriteLine("oLoc.Location = {0}", oLoc.Location.ToString(), oLoc);
                    //Console.WriteLine("SourceSpan: {{Start: {0}, End:{1}, Length:{2} }}", oLoc.Location.SourceSpan.Start, oLoc.Location.SourceSpan.End, oLoc.Location.SourceSpan.Length);
                    var tree = oLoc.Location.SourceTree;
                    var treeRoot = tree.GetRootAsync().Result;
                    SyntaxNode node = treeRoot.FindNode(oLoc.Location.SourceSpan);

                    SourceText srcTxt;
                    if (tree.TryGetText(out srcTxt))
                    {
                        string srcLine = GetSourceTextBySpan(tree, oLoc.Location.SourceSpan, srcTxt);
                        MethodDeclarationSyntax containingMember = GetContainingMember(node);
                        ClassDeclarationSyntax containingType = GetContainingType(containingMember);
                        NamespaceDeclarationSyntax containingNs = GetContainingNameSpace(containingType);
                        string parentNodeSrc = string.Empty;
                        string classSrc = string.Empty;
                        string nsSrc = string.Empty;
                        if (containingMember != null)
                        {
                            parentNodeSrc = GetSourceTextBySpan(tree, containingMember.Span, srcTxt);

                        }
                        if (containingType != null)
                        {
                            classSrc = GetSourceTextBySpan(tree, containingType.Span, srcTxt);
                        }

                        if (nsSrc != null)
                        {
                            nsSrc = GetSourceTextBySpan(tree, containingNs.Span, srcTxt);
                        }

                        VSUsageRec vsu = new VSUsageRec();
                        vsu.Code = srcLine;
                        var srcSpan = tree.GetLineSpan(oLoc.Location.SourceSpan);
                        vsu.Line = srcSpan.StartLinePosition.Line;
                        vsu.Column = srcSpan.StartLinePosition.Character;
                        vsu.ContainingMember = ParseMethodName(parentNodeSrc);
                        //Console.WriteLine("{0}('{1}') = '{2}'", nameof(ParseMethodName), parentNodeSrc, vsu.ContainingMember);
                        vsu.ContainingType = ParseClassName(classSrc);
                        //Console.WriteLine("{0}('{1}') = '{2}'", nameof(ParseClassName), classSrc, vsu.ContainingType);
                        vsu.ContainingNamespace = ParseNameSpaceName(nsSrc);
                        vsu.File = oLoc.Document.FilePath;
                        vsu.Project = oLoc.Document.Project.Name;
                        vsu.Kind = oLoc.Location.Kind.ToString();
                        defType = symbol2Find?.ContainingType?.Name;
                        if (!string.IsNullOrWhiteSpace(defType))
                            vsu.DefType = defType;
                        else
                            vsu.DefType = classSymbol.Name;
                        vsu.DefMember = symbol2Find?.Name;
                        vsu.DefFile = defFile;
                        rslt.Add(vsu);
                        //Console.WriteLine(new string('-', 20));
                        //Console.WriteLine("{0} = {1}", nameof(srcLine), srcLine);
                        //Console.WriteLine(new string('-', 20));
                        //Console.WriteLine("{0} = {1}", nameof(parentNodeSrc), parentNodeSrc);
                        //Console.WriteLine(new string('-', 20));
                        //Console.WriteLine("{0} = {1}", nameof(classSrc), classSrc);
                        //Console.WriteLine(new string('=', 20));
                    }
                    //oLoc.Location.GetLineSpan()
                }
            }

            return rslt;
        }

        private string ParseNameSpaceName(string defText)
        {
            //Console.WriteLine("{0}: {1} = '{2}'", nameof(ParseNameSpaceName), nameof(defText), defText);
            const string NSToken = "namespace ";
            int ctPos = defText.IndexOf(NSToken);
            string rslt = defText.Substring(ctPos + NSToken.Length).Trim();
            int bracketPos = rslt.IndexOf('{');
            if (bracketPos == -1)
                return rslt;
            return rslt.Substring(0, bracketPos).Trim();
        }

        private string ParseClassName(string defText)
        {
            //Console.WriteLine("{0}: {1} = '{2}'", nameof(ParseClassName), nameof(defText), defText);
            const string ClassToken = " class ";
            int ctPos = defText.IndexOf(ClassToken);
            string rslt = defText.Substring(ctPos + ClassToken.Length).Trim();
            int colonPos = rslt.IndexOf(':');
            if (colonPos == -1)
                return rslt;
            return rslt.Substring(0, colonPos).Trim();
        }

        private string ParseMethodName(string nodeText)
        {
            //Console.WriteLine("{0}: {1} = '{2}'", nameof(ParseMethodName), nameof(nodeText), nodeText);
            if (string.IsNullOrWhiteSpace(nodeText))
                return string.Empty;
            int bracketPos = nodeText.IndexOf('(');
            if (bracketPos == -1)
                return nodeText;
            string rslt = nodeText.Substring(0, bracketPos).Trim();
            int spacePos = rslt.LastIndexOf(' ');
            if (spacePos == -1)
                return rslt;
            if (spacePos > bracketPos)
                return rslt;
            rslt = rslt.Substring(spacePos + 1);
            int ltPos = rslt.IndexOf('<');
            if (ltPos != -1)
                rslt = rslt.Substring(0, ltPos);
            return rslt;
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

        private NamespaceDeclarationSyntax GetContainingNameSpace(SyntaxNode node)
        {
            SyntaxNode parentNode = node;
            while (!(parentNode is NamespaceDeclarationSyntax))
            {
                if (parentNode == null)
                    return null;
                parentNode = parentNode.Parent;
            }
            return parentNode as NamespaceDeclarationSyntax;
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
            {
                string rslt = null;
                int currLnOffset = 0;
                do
                {
                    rslt = srcTxt.Lines[srcSpan.StartLinePosition.Line + currLnOffset].ToString();
                    if (!string.IsNullOrWhiteSpace(rslt) &&  IsAttributeLine(rslt))
                        currLnOffset++;
                    else
                        break;
                } while (currLnOffset <= srcSpan.EndLinePosition.Line -  srcSpan.StartLinePosition.Line);
                return rslt;
            }
        }

        private static bool IsAttributeLine(string rslt)
        {
            if (string.IsNullOrWhiteSpace(rslt))
                return false;
            return rslt.Trim()[0] == '[';
        }

        private List<MissingUsageRec> ReCalcMissing(List<VSUsageRec> vsusHive)
        {
            var all = vsusHive.Where(u => !u.IsNoReferences).Distinct(new VSUsageRecFileContTypeMethodEqComparer()).ToList();
            var missing = all.Where(r => !SkipClasses.Contains(r.ContainingType) && vsusHive.FirstOrDefault(q => q.DefFile == r.File && q.DefType == r.ContainingType && q.DefMember == r.ContainingMember) == null).Distinct(new VSUsageRecFileContTypeMethodEqComparer()).ToList();
            var ds = from m in missing
                     select new MissingUsageRec() { ContainingMember = m.ContainingMember, ContainingType = m.ContainingType, File = m.File, ContainingNamespace = m.ContainingNamespace, Project = m.Project};
            return ds.ToList();
        }

        public bool FindUsagesRecursive(Solution sln, string projectName, string typeName, string memberName, List<VSUsageRec> vsusHive, out List<MissingUsageRec> notFound)
        {
            List<VSUsageRec> rslt = FindUsages(sln, projectName, typeName, memberName, string.Empty);
            notFound = new List<MissingUsageRec>();
            vsusHive.AddRange(rslt);
            if (rslt == null || rslt.Count == 0)
                return false;
            List<int> last10MissingCounts = new List<int>();
            List<MissingUsageRec> missing = ReCalcMissing(vsusHive);
            last10MissingCounts.Add(missing != null ? missing.Count : 0);
            do
            {
                if (missing == null)
                    break;

                foreach (var msng in missing)
                {
                    List<VSUsageRec> currRslt = FindUsages(sln, msng.Project, string.Format("{0}.{1}", msng.ContainingNamespace, msng.ContainingType), msng.ContainingMember, msng.File);
                    if (currRslt != null && currRslt.Count > 0)
                        AddUnique(vsusHive, currRslt);
                    else
                        vsusHive.Add(new VSUsageRec() { IsNoReferences = true, Project = msng.Project, ContainingNamespace = msng.ContainingNamespace, DefType = msng.ContainingType, DefMember = msng.ContainingMember, DefFile = msng.File });
                }

                missing = ReCalcMissing(vsusHive);

                if (last10MissingCounts.Count < 10)
                    last10MissingCounts.Add(missing != null ? missing.Count : 0);
                else
                {
                    if (last10MissingCounts.Distinct().Count() == 1)
                        break;
                    else
                    { 
                        last10MissingCounts.RemoveAt(0);
                        last10MissingCounts.Add(missing != null ? missing.Count : 0);
                    }
                }
            } while (missing != null && missing.Count > 0);
            if (missing != null && missing.Count > 0)
                notFound.AddRange(missing);
            return true;
        }

        private void AddUnique(List<VSUsageRec> vsusHive, List<VSUsageRec> currRslt)
        {
            foreach (var vsu in currRslt)
            {
                if (vsusHive.FindIndex(m => m.Project == vsu.Project
                                         && vsu.ContainingMember == m.ContainingMember
                                         && vsu.ContainingNamespace == m.ContainingNamespace
                                         && vsu.Line == m.Line
                                         && vsu.Column == m.Column
                                         && vsu.File == m.File) == -1)
                    vsusHive.Add(vsu);
            }
        }
    }
}
