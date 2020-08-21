using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using VSUsagesAnalysisHelperLib;
using VSUsagesAnalysisHelperLib.NugetSimple;

namespace VSUsagesInsight.CLI.Spares
{
    public static class NugetPackagesHelper
    {

        public static readonly string PackageJSON = "package.json";
        public static readonly string PackagesConfig = "packages.config";

        public static string Version { get; private set; }

        public static List<NugetPackageRec> ListAllSlnNugetPackages(Solution sln)
        {
            return ListAllSlnNugetPackages(sln, true);
        }
        public static List<NugetPackageRec> ListAllSlnNugetPackages(Solution sln, bool detectLatestStable)
        {
            List<NugetPackageRec> rslt = new List<NugetPackageRec>();
            foreach (var proj in sln.Projects)
            {
                string currPgkCfg = IdentifyProjectPackageConfig(proj);
                if (string.IsNullOrEmpty(currPgkCfg)) continue;
                List<NugetPackageRec> currPkgs = ReadOutPackages(currPgkCfg);
                currPkgs.ForEach(p => p.ProjectName = proj.Name);
                if (detectLatestStable)
                    currPkgs.ForEach(p => p.LatestStableVersion = DetectLatestStable(p.Id));
                rslt.AddRange(currPkgs);
            }
            return rslt;
        }
        public static List<string> ListAllAssembliesByNugetPackage(Solution sln, string packageId, string version = null)
        {
            List<string> rslt = new List<string>();
            string slnPackagesRoot = GetPackagesRoot(sln.FilePath);
            string thePackageRoot;
            if (!string.IsNullOrWhiteSpace(version))
                thePackageRoot = Path.Combine(slnPackagesRoot, GetPackageFolderName(packageId, version));
            foreach (var proj in sln.Projects)
            {
                if (string.IsNullOrWhiteSpace(version))
                {
                    
                    string currPgkCfg = IdentifyProjectPackageConfig(proj);
                    if (string.IsNullOrEmpty(currPgkCfg)) continue;
                    List<NugetPackageRec> currPkgs = ReadOutPackages(currPgkCfg);
                    string currPkgVersion = currPkgs?.FirstOrDefault(p => p.Id == packageId)?.Version;
                    if (string.IsNullOrEmpty(currPkgVersion))
                        continue;
                    thePackageRoot = Path.Combine(slnPackagesRoot, GetPackageFolderName(packageId, currPkgVersion));
                }
                //todo
            }
            return rslt;
        }

        public static string GetPackagesRoot(string slnPath)
        {
            string dir = Path.GetDirectoryName(slnPath);
            return Path.Combine(dir, "packages");
        }

        public static string GetPackageFolderName(string packageId, string version)
        {
            return $"{packageId}.{version}";
        }

        public static string DetectLatestStable_(string pkgId)
        {
            NuGet.Protocol.PackageDetailsUriResourceV3Provider provider = new PackageDetailsUriResourceV3Provider();
            NuGet.Configuration.PackageSource ps = new NuGet.Configuration.PackageSource(null);
            HttpClient http = new HttpClient();
            HttpSource hts = new HttpSource(null,null, null);
            RegistrationResourceV3 rrv3 = new RegistrationResourceV3(hts, new Uri("https://api.nuget.org/v3/registration3/"));
            SourceCacheContext scc = new SourceCacheContext();
            CancellationTokenSource cts = new CancellationTokenSource();
            NuGet.Protocol.PackageMetadataResourceV3 mr = new PackageMetadataResourceV3(hts, rrv3, null, null);
            IEnumerable<IPackageSearchMetadata> md = mr.GetMetadataAsync(pkgId, false, false, scc, null, cts.Token).GetAwaiter().GetResult();
            Console.WriteLine("md.count = {0}", md.Count());
            return null;//todo
        }

        public static string DetectLatestStable(string pkgId)
        {
            const string pckUrlFmt = "https://api.nuget.org/v3/registration4/{0}/index.json";
            const string pckUrlFmt1 = "https://api.nuget.org/v3/registration3/{0}/index.json";
            string pckgUrl = string.Format(pckUrlFmt, pkgId.ToLower());
            PackageV3Info pckgInfo = null;
            using (WebClient wc = new WebClient())
            {
                string json = null;
                try
                {
                    json = wc.DownloadString(pckgUrl);
                }
                catch (WebException)
                {
                    try
                    {
                        pckgUrl = string.Format(pckUrlFmt1, pkgId.ToLower());
                        json = wc.DownloadString(pckgUrl);
                    }
                    catch (WebException)
                    { }
                }
                if (!string.IsNullOrEmpty(json))
                    pckgInfo = JsonConvert.DeserializeObject<PackageV3Info>(json, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore});
            }
            if (pckgInfo == null)
                return null;
            return pckgInfo.Items.LastOrDefault().Items.Where(i => i.CatalogEntry.Listed).LastOrDefault().CatalogEntry.Version;
        }

        public static string IdentifyProjectPackageConfig(Project proj)
        {
            string rslt = proj.Documents.FirstOrDefault(d => { string fnmlc = Path.GetFileName(d.FilePath).ToLower(); return fnmlc == PackageJSON || fnmlc == PackagesConfig; })?.FilePath;
            XmlDocument projXml = new XmlDocument();
            projXml.Load(proj.FilePath);
            var node = projXml.SelectSingleNode(string.Format("//*[@Include='{0}']", PackagesConfig));
            if (node == null)
                node = projXml.SelectSingleNode(string.Format("//*[@Include='{0}']", PackageJSON));
            string dir = Path.GetDirectoryName(proj.FilePath);
            if (node != null)
            {
                return Path.Combine(dir, node.Attributes["Include"].Value);
            }
            if (node == null)
            {
                rslt = Path.Combine(dir, PackageJSON);
                if (File.Exists(rslt))
                    return rslt;
                else
                {
                    rslt = Path.Combine(dir, PackagesConfig);
                    if (File.Exists(rslt))
                        return rslt;
                }
            }
            return null;
        }

        public static List<NugetPackageRec> ReadOutPackages(string filePath)
        {
            return Path.GetExtension(filePath) == "json" ? ReadOutPackageJson(filePath) : ReadOutPackagesConfig(filePath);
        }

        private static List<NugetPackageRec> ReadOutPackageJson(string filePath)
        {
            throw new NotImplementedException();
        }

        private static List<NugetPackageRec> ReadOutPackagesConfig(string filePath)
        {
            List<NugetPackageRec> rslt = new List<NugetPackageRec>();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(filePath);
            XmlNodeList pckgNodes = xdoc.SelectNodes("//package");
            foreach (XmlNode node in pckgNodes)
            {
                rslt.Add(new NugetPackageRec() { Id = GetAttr(node, "Id"), Version = GetAttr(node, "Version") });
            }
            return rslt;
        }

        private static string GetAttr(XmlNode node, string nm)
        {
            for (int i = 0; i < node.Attributes.Count; i++)
            {
                if (node.Attributes[i].Name.ToLower() == nm.ToLower())
                    return node.Attributes[i].Value;
            }
            return null;
        }
    }
}
