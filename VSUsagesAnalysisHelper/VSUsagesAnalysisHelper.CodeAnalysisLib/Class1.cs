using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelper.CodeAnalysisLib
{
    public class Class1
    {
        /*
        public List<string> FindUsages()
        {
            AdhocWorkspace wsp = new AdhocWorkspace();
            Solution sln = wsp.AddSolution(SolutionInfo.Create(SolutionId.CreateFromSerialized(Guid.Parse("1D65F6B3-278D-454A-9E3D-0401A659BC29")), VersionStamp.Create(), @"D:\git\VRTF\QQNG\QQCatalystMain\QQSolutions.NextGen.Web\CatalystWebsiteWithAPI.sln"));
            
            //ISymbol symbol;
            //Solution sln = new Solution(); 
            //sln.
            Project proj = sln.Projects.Where(p => p.FilePath == @"D:\git\VRTF\QQNG\QQCatalystMain\QQSolutions.NextGen.Web\QD.NextGen.BusinessLogic\QD.NextGen.BusinessLogic.csproj").FirstOrDefault();
            if (proj == null)
                return null;
            string asmNm = proj.AssemblyName;
            //Microsoft.CodeAnalysis.FindSymbols.SymbolFinder.FindReferencesAsync()
            return null; //todo
        }
        */
        public List<string> FindUsages()
        {
            var wsp = MSBuildWorkspace.Create();
            Solution sln = wsp.OpenSolutionAsync(@"D:\git\VRTF\QQNG\QQCatalystMain\QQSolutions.NextGen.Web\CatalystWebsiteWithAPI.sln").Result;
            return null;
        }
    }
}
