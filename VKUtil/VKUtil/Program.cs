using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Evolvex.VKUtilLib.Misc;
using System.Net;
using Evolvex.VKUtilLib.Spares.Data;
using System.Globalization;
using Evolvex.VKUtilLib.ExUA;
using Evolvex.VKUtilLib.Zaycev;
using Evolvex.VKUtilLib.EDataGovUA;
using Newtonsoft.Json;
using System.Configuration;

namespace Evolvex.VKUtil
{
    class Program
    {
        private delegate void CmdHandler(string[] args);

        private static readonly Dictionary<string, CmdHandler> _cmdHandlers;
        private static readonly bool EdataLogDebugEvents;
        static Program()
        {
            _cmdHandlers = new Dictionary<string, CmdHandler>();

            #region populate
            _cmdHandlers.Add("readposttitles", ReadPostTitles);
            _cmdHandlers.Add("readposttitles2", ReadPostTitles2);
            _cmdHandlers.Add("downloadsomething", DownloadSomething);
            _cmdHandlers.Add("downloadfiles", DownloadFiles);
            _cmdHandlers.Add("formatdecayingwesthtml", FormatDecayingWestHtml);
            _cmdHandlers.Add("readexualinks", ReadExUALinks);
            _cmdHandlers.Add("readanddownloadzaycevnet", ReadAndDownloadZaycevNet);
            _cmdHandlers.Add("edatagovuaget", EDataGovUaGet);
            _cmdHandlers.Add("disposersjsontotabdelim", DisposersJSONToTabDelim);
            #endregion
             string strEdataLogDebugEvents = ConfigurationManager.AppSettings["edataLogDebugEvents"];
            if(!string.IsNullOrEmpty(strEdataLogDebugEvents))
            {
                if(!bool.TryParse(strEdataLogDebugEvents, out EdataLogDebugEvents))
                    EdataLogDebugEvents = false;
            }
            
        }

        [STAThread]
        static void Main(string[] args)
        {
            //Console.Read();
            string cmdHandlerKey = string.Empty;
            if (args.Length > 0)
                cmdHandlerKey = args[0].ToLower();
            try
            {
                if (string.IsNullOrEmpty(cmdHandlerKey) || !_cmdHandlers.ContainsKey(cmdHandlerKey))
                    return;
                else
                    _cmdHandlers[cmdHandlerKey](args);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        private static void ReadPostTitles(string[] args)
        {
            string urlsFile = args[1];
            string[] lines = File.ReadAllLines(urlsFile);
            Console.WriteLine("'{0}'", (new PostTitleReader()).Read(lines[0]));
            Console.WriteLine("'{0}'", (new PostTitleReader()).Read_v1(lines[0]));
            Console.WriteLine("'{0}'", (new PostTitleReader()).Read_v2(lines[0]));
        }

        private static void ReadPostTitles2(string[] args)
        {
            string urlsFile = args[1];
            string[] lines = File.ReadAllLines(urlsFile);
            using (PostTitleReader2 reader = new PostTitleReader2())
            {
                foreach (string line in lines)
                {
                    VKPostInfo pi = reader.Read(line);
                    if(pi == null)
                        Console.WriteLine("{1}\t{0}", line, string.Empty);
                    else
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", line, pi.Title.Replace('\r', ' ').Replace('\n', ' '), pi.Img, pi.ImgFileName);
                    
                }
            }
        }

        private static void DownloadSomething(string[] args)
        {
            string url = args[1];
            string saveAs = args[2];

            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(url, saveAs);
            }
        }

        private static void DownloadFiles(string[] args)
        {
            string urlsListFile = args[1];
            string saveToDir = args[2];
            string[] urls = File.ReadAllLines(urlsListFile);            
            using (WebClient wc = new WebClient())
            {
                foreach (string url in urls)
                {
                    if (string.IsNullOrEmpty(url))
                        continue;
                    string fileName = Path.GetFileName(url);

                    wc.DownloadFile(url, Path.Combine(saveToDir, fileName));
                }
            }
        }

        private static void FormatDecayingWestHtml(string[] args)
        {
            string inputFile = args[1];
            string imgsSaved2Dir = args[2];
            string imgsUploaded2Url = args[3];
            string imgsUploadedId = args[4];
            string saveHTMLAs = args[5];
            string culture = args.Length > 6  ? args[6] : string.Empty;

            #region read input file
            List<VKWallPostTranslationInfo> infos = new List<VKWallPostTranslationInfo>();
            string[] lines = File.ReadAllLines(inputFile);
            foreach (string line in lines)
            {
                if(string.IsNullOrEmpty(line) || string.IsNullOrEmpty(line.Trim()))
                    continue;
                VKWallPostTranslationInfo info = VKWallPostTranslationInfo.Parse(line);
                if (info.IsEmpty)
                    continue;
                infos.Add(info);
            }
            #endregion

            #region fill auxiliary info

            foreach (VKWallPostTranslationInfo info in infos)
            {
                VKWallTranslationFormatter.FillImageSize(info, imgsSaved2Dir);
                string uploadedFileName = (info.ImgFileName[0] == '_' ? info.ImgFileName = info.ImgFileName.Substring(1) : info.ImgFileName);
                info.UploadedImgUrl = Path.Combine(imgsUploaded2Url, uploadedFileName);
                info.UploadedImgId = imgsUploadedId;
            }
            #endregion
            VKWallTranslationFormatter fmtr = new VKWallTranslationFormatter();
            if (!string.IsNullOrEmpty(culture))
                fmtr.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag(culture);

            File.WriteAllText(saveHTMLAs, fmtr.ComposeHTML(infos), Encoding.Unicode);
        }

        [STAThread]
        public static void ReadExUALinks(string[] args)
        {
            List<string> urls = new List<string>();
            for (int i = 1; i < args.Length; i++)
            {
                urls.Add(args[i]);
            }
            using (ExUAPageReader reader = new ExUAPageReader())
            {
                foreach (string url in urls)
                {
                    if (reader.Read(url))
                    {
                        //Console.WriteLine("reader.MediaList.Count = {0}", reader.MediaList.Count);
                        foreach (string murl in reader.MediaList)
                            Console.WriteLine(murl);
                    }
                }
            }
            
            //
        }

        [STAThread]
        public static void ReadAndDownloadZaycevNet(string[] args)
        {
            string url = args[1];
            string save2Dir = args[2];
            string sleepBtwDownloadsMs = args.Length > 3 ? args[3] : string.Empty;
            int? sleepMs = null;
            using (ZaycevNetReader reader = new ZaycevNetReader())
            {
                if (!string.IsNullOrEmpty(sleepBtwDownloadsMs))
                {
                    int tmp;
                    if (int.TryParse(sleepBtwDownloadsMs, out tmp))
                        sleepMs = tmp;
                }
                if(sleepMs != null)
                    reader.SleepBetweenDownloadsMs = (int)sleepMs;

                if (reader.Read(url))
                {
                    Console.WriteLine(string.Join("\n", reader.MediaList));
                    if(sleepMs != null)
                        System.Threading.Thread.Sleep((int)sleepMs);
                    reader.DownloadAll(reader.MediaList, save2Dir);
                }
            }

            //
        }

        [STAThread]
        public static void EDataGovUaGet(string[] args)
        {
            string yedrpous_input_file = args[1];
            string saveAsFormat = args[2];
            DateTime ds = DateTime.Now;
            string[] aYeDRPOUs = File.ReadAllLines(yedrpous_input_file);
            List<EDataGovUaDisposerInfo> rslts = new List<EDataGovUaDisposerInfo>();
            using (EDataGovUaReader reader = new EDataGovUaReader() { LogDebugEvents = EdataLogDebugEvents })
            {

                foreach (string yedrpou in aYeDRPOUs)
                {
                    try
                    {
                        reader.SearchForYeDRPOU = yedrpou;
                        if (reader.Read(EDataGovUaReader.START_DISPOSERS_SEARCH_URL))
                        {
                            Console.WriteLine("reader.Read({0}) succeeded", yedrpou);
                        }
                        else
                            Console.WriteLine("reader.Read({0}) failed", yedrpou);
                        //Console.WriteLine(reader.HTML);
                        //Console.WriteLine("--------------------------------------------------------");

                        Console.WriteLine("reader.Result= {0}", JsonConvert.SerializeObject(reader.Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                        rslts.Add(reader.Result);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Reading '{0}': exception: {1}", yedrpou, exc);
                    }
                }
                String saveAsFileNameFmt = Path.GetFileNameWithoutExtension(saveAsFormat);
                String saveAsFinalFileName = string.Format("{0}{1:yyyyMMdd_HHmm}{2}", saveAsFileNameFmt, DateTime.Now, Path.GetExtension(saveAsFormat));
                String saveAsFinalPath = Path.Combine(Path.GetDirectoryName(saveAsFormat), saveAsFinalFileName);
                File.WriteAllText(saveAsFinalPath, JsonConvert.SerializeObject(rslts, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode);
                DateTime df = DateTime.Now;
                Console.WriteLine("Started: {0}", ds);
                Console.WriteLine("Finished: {0}", df);
                Console.WriteLine("Completed in: {0}", (TimeSpan)(df - ds));
                Console.WriteLine("Saved to: {0}", saveAsFinalPath);
            }

        }

        private static void DisposersJSONToTabDelim(string[] args)
        {
            string inJson = args[1];
            List<EDataGovUaDisposerInfo> lst = JsonConvert.DeserializeObject<List<EDataGovUaDisposerInfo>>(File.ReadAllText(inJson));
            using (StreamWriter sw = new StreamWriter(Path.Combine( Path.GetDirectoryName(inJson), String.Format("{0}{1}", Path.GetFileNameWithoutExtension(inJson), ".tab")),false, Encoding.Unicode))
            {
                foreach (EDataGovUaDisposerInfo di in lst)
                {
                    if (!di.IsFound)
                        continue;
                    sw.WriteLine("{0}\t{1}\t{2}\t{3}", di.YeDRPOU, di.InternalID.ToString(), di.CabinetStatus, di.DisposerName);
                }
            }
        }


    }
}
