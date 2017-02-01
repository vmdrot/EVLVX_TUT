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
using Evolvex.VKUtil.Utility;
using Evolvex.VKUtilLib.Rexton.Spares;
using Evolvex.VKUtilLib.Rexton;
using System.Threading;
using Evolvex.VKUtilLib.DataGovUa;

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
            _cmdHandlers.Add("convertjson2xml", ConvertJson2XML);
            _cmdHandlers.Add("readrexton", ReadRexton);
            _cmdHandlers.Add("convertrextontoxml", ConvertRextonToXML);
            _cmdHandlers.Add("readdatagovuadslist", ReadDataGovUaDSList);
            

            #endregion
            string strEdataLogDebugEvents = ConfigurationManager.AppSettings["edataLogDebugEvents"];
            if (!string.IsNullOrEmpty(strEdataLogDebugEvents))
            {
                if (!bool.TryParse(strEdataLogDebugEvents, out EdataLogDebugEvents))
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
                    if (pi == null)
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
            string culture = args.Length > 6 ? args[6] : string.Empty;

            #region read input file
            List<VKWallPostTranslationInfo> infos = new List<VKWallPostTranslationInfo>();
            string[] lines = File.ReadAllLines(inputFile);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(line.Trim()))
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
                if (sleepMs != null)
                    reader.SleepBetweenDownloadsMs = (int)sleepMs;

                if (reader.Read(url))
                {
                    //Console.WriteLine(string.Join("\n", reader.MediaList));
                    //foreach (Evolvex.VKUtilLib.Zaycev.ZaycevNetReader.TrackInfo ti in reader.TrackList)
                    //    Console.WriteLine(ti.Url);
                    Console.WriteLine(JsonConvert.SerializeObject(reader.TrackList, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
                    if (sleepMs != null)
                        System.Threading.Thread.Sleep((int)sleepMs);
                    reader.DownloadAll(reader.TrackList, save2Dir);
                }
            }

            //
        }

        [STAThread]
        public static void EDataGovUaGet_old(string[] args)
        {
            LogCmdArgs("EDataGovUaGet", args);
            string yedrpous_input_file = args[1];
            string saveAsFormat = args[2];
            string startIdx = args.Length >= 4 ? args[3] : string.Empty;
            string procLen = args.Length >= 5 ? args[4] : string.Empty;

            DateTime ds = DateTime.Now;
            List<string> aYeDRPOUs = ReadListOfYeDRPOUs(yedrpous_input_file, startIdx, procLen);
            Console.WriteLine("Processing {0} YeDRPOU's...", aYeDRPOUs.Count);
            if (aYeDRPOUs.Count > 0)
                Console.WriteLine("YeDRPOU's from '{0}' to '{1}'...", aYeDRPOUs[0], aYeDRPOUs[aYeDRPOUs.Count - 1]);
            List<EDataGovUaDisposerInfo> rslts = new List<EDataGovUaDisposerInfo>();
            IProgressTracker progressTracker = new ProgressTrackerBase(); //todo
            using (EDataGovUaReader reader = new EDataGovUaReader() { LogDebugEvents = EdataLogDebugEvents })
            {

                progressTracker.Start(Guid.NewGuid().ToString(), aYeDRPOUs.Count, ConsoleLog.Instance, ds);
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
                        progressTracker.ItemComplete();
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Reading '{0}': exception: {1}", yedrpou, exc);
                    }
                }
                String saveAsFileNameFmt = Path.GetFileNameWithoutExtension(saveAsFormat);
                String saveAsFinalFileName = string.Format("{0}{1:yyyyMMdd_HHmmss}{2}", saveAsFileNameFmt, DateTime.Now, Path.GetExtension(saveAsFormat));
                String saveAsFinalPath = Path.Combine(Path.GetDirectoryName(saveAsFormat), saveAsFinalFileName);
                File.WriteAllText(saveAsFinalPath, JsonConvert.SerializeObject(rslts, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode);
                DateTime df = DateTime.Now;
                Console.WriteLine("Started: {0}", ds);
                Console.WriteLine("Finished: {0}", df);
                Console.WriteLine("Completed in: {0}", (TimeSpan)(df - ds));
                Console.WriteLine("Saved to: {0}", saveAsFinalPath);
            }

        }

        [STAThread]
        public static void EDataGovUaGet(string[] args)
        {
            LogCmdArgs("EDataGovUaGet", args);
            string yedrpous_input_file = args[1];
            string saveAsFormat = args[2];
            string startIdx = args.Length >= 4 ? args[3] : string.Empty;
            string procLen = args.Length >= 5 ? args[4] : string.Empty;

            DateTime ds = DateTime.Now;
            List<string> aYeDRPOUs = ReadListOfYeDRPOUs(yedrpous_input_file, startIdx, procLen);
            Console.WriteLine("Processing {0} YeDRPOU's...", aYeDRPOUs.Count);
            if (aYeDRPOUs.Count > 0)
                Console.WriteLine("YeDRPOU's from '{0}' to '{1}'...", aYeDRPOUs[0], aYeDRPOUs[aYeDRPOUs.Count - 1]);
            List<EDataGovUaDisposerInfo> rslts = new List<EDataGovUaDisposerInfo>();
            IProgressTracker progressTracker = new ProgressTrackerBase(); //todo

            progressTracker.Start(Guid.NewGuid().ToString(), aYeDRPOUs.Count, ConsoleLog.Instance, ds);
            int currbatchIdx = 0;
            foreach (string yedrpou in aYeDRPOUs)
            {
                bool bRepeatCurrentBecauseOfError = false;
                HttpStatusCode? lastHttpStatus = null;
                do
                {
                    try
                    {
                        using (EDataGovUaReader reader = new EDataGovUaReader() { LogDebugEvents = EdataLogDebugEvents })
                        {

                            reader.SearchForYeDRPOU = yedrpou;
                            if (reader.Read(EDataGovUaReader.START_DISPOSERS_SEARCH_URL))
                            {
                                Console.WriteLine("reader.Read({0}) succeeded", yedrpou);
                                bRepeatCurrentBecauseOfError = false;
                            }
                            else if (reader.LastHttpStatus != null)
                            {
                                Console.WriteLine("reader.Read({0}) failed, HTTP error status = {1}", yedrpou, (HttpStatusCode)(int)reader.LastHttpStatus);
                                lastHttpStatus = (HttpStatusCode)(int)reader.LastHttpStatus;
                            }
                            else
                                Console.WriteLine("reader.Read({0}) failed", yedrpou);
                            //Console.WriteLine(reader.HTML);
                            //Console.WriteLine("--------------------------------------------------------");
                            if (reader.LastHttpStatus == null)
                            {
                                Console.WriteLine("reader.Result= {0}", JsonConvert.SerializeObject(reader.Result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                                rslts.Add(reader.Result);
                                progressTracker.ItemComplete();
                                bRepeatCurrentBecauseOfError = false;
                            }
                            else
                                bRepeatCurrentBecauseOfError = true;
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Reading '{0}': exception: {1}", yedrpou, exc);
                        bRepeatCurrentBecauseOfError = true;
                    }

                    if (bRepeatCurrentBecauseOfError)
                    {
                        Console.WriteLine("The request for current YeDRPOU {0} will be repeated because of error, last HTTP status = {1} ({2})", yedrpou, lastHttpStatus, (int?)lastHttpStatus);
                        System.Threading.Thread.Sleep(EDataGovUaReader.PauseOnHttpErrorMs);
                    }
                } while (bRepeatCurrentBecauseOfError);
                currbatchIdx++;
                if (EDataGovUaReader.PauseAfterEveryEntryMs != null)
                    Thread.Sleep((int)EDataGovUaReader.PauseAfterEveryEntryMs);
                else if (EDataGovUaReader.MakePausesBetweenBatches)
                {
                    if (currbatchIdx == EDataGovUaReader.PauseBatchSize)
                    {
                        currbatchIdx = 0;
                        System.Threading.Thread.Sleep(EDataGovUaReader.PauseLengthMs);
                    }
                }
            }
            String saveAsFileNameFmt = Path.GetFileNameWithoutExtension(saveAsFormat);
            String saveAsFinalFileName = string.Format("{0}{1:yyyyMMdd_HHmmss}{2}", saveAsFileNameFmt, DateTime.Now, Path.GetExtension(saveAsFormat));
            String saveAsFinalPath = Path.Combine(Path.GetDirectoryName(saveAsFormat), saveAsFinalFileName);
            File.WriteAllText(saveAsFinalPath, JsonConvert.SerializeObject(rslts, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode);
            DateTime df = DateTime.Now;
            Console.WriteLine("Started: {0}", ds);
            Console.WriteLine("Finished: {0}", df);
            Console.WriteLine("Completed in: {0}", (TimeSpan)(df - ds));
            Console.WriteLine("Saved to: {0}", saveAsFinalPath);

        }

        [STAThread]
        public static void ReadRexton(string[] args)
        {
            LogCmdArgs("ReadRexton", args);
            string startUrl = args[1];
            string saveAs = args[2];

            DateTime ds = DateTime.Now;
            List<RextonRecordInfo> rslts = new List<RextonRecordInfo>();

            using (RextonReader reader = new RextonReader() { StartPgUrl = startUrl })
            {

                try
                {
                    if (reader.Read(startUrl))
                    {
                        Console.WriteLine("reader.Read({0}) succeeded", startUrl);
                    }
                    else
                        Console.WriteLine("reader.Read({0}) failed", startUrl);
                    //Console.WriteLine(reader.HTML);
                    //Console.WriteLine("--------------------------------------------------------");

                    rslts.AddRange(reader.Result);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Reading '{0}': exception: {1}", startUrl, exc);
                }
                if (File.Exists(saveAs))
                {
                    rslts = RextonRecordInfo.Merge(rslts, JsonConvert.DeserializeObject<List<RextonRecordInfo>>(File.ReadAllText(saveAs)));
                }
                File.WriteAllText(saveAs, JsonConvert.SerializeObject(rslts, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode);
                DateTime df = DateTime.Now;
                Console.WriteLine("Started: {0}", ds);
                Console.WriteLine("Finished: {0}", df);
                Console.WriteLine("Completed in: {0}", (TimeSpan)(df - ds));
                Console.WriteLine("Saved to: {0}", saveAs);
            }

        }

        
        [STAThread]
        public static void ReadDataGovUaDSList(string[] args)
        {
            DateTime ds = DateTime.Now;
            LogCmdArgs("ReadDataGovUaDSList", args);
            Console.WriteLine("Started: {0}", ds);
            string startFromPageStr = args.Length > 1 ? args[1] : string.Empty;
            string stopAfterPageStr = args.Length > 2 ? args[2] : string.Empty;
            string saveRsltsTo = args.Length > 3 ? args[3] : string.Empty;
            bool saveContinously = false;
            using (DataGovUaReader reader = new DataGovUaReader())
            {
                if (!string.IsNullOrWhiteSpace(startFromPageStr))
                {
                    int startFromPg;
                    if (int.TryParse(startFromPageStr, out startFromPg))
                        reader.StartFromPage = startFromPg;
                }

                if (!string.IsNullOrWhiteSpace(stopAfterPageStr))
                {
                    int stopAfterPg;
                    if (int.TryParse(stopAfterPageStr, out stopAfterPg))
                        reader.StopAfterPage = stopAfterPg;
                }

                if (!string.IsNullOrWhiteSpace(saveRsltsTo) && Directory.Exists(Path.GetDirectoryName(saveRsltsTo)))
                {
                    saveContinously = true;
                    reader.SaveResultAs = saveRsltsTo;
                }

                if (reader.Read(""))
                {
                    Console.WriteLine("PagesCount = {0}", reader.PagesCount);
                }
                if (reader.PassportUrls != null && reader.PassportUrls.Count > 0)
                {

                    if (!string.IsNullOrWhiteSpace(saveRsltsTo) && !saveContinously)
                    {
                        try
                        {
                            File.WriteAllLines(saveRsltsTo, reader.PassportUrls.ToArray());
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine("Error saving passport urls to file '{0}', details: {1}", saveRsltsTo, exc);
                            PrintAllLines(reader.PassportUrls);
                        }
                    }
                    else
                    {
                        PrintAllLines(reader.PassportUrls);
                    }
                }
            }
            DateTime df = DateTime.Now;
            Console.WriteLine("Finished: {0}", df);
            Console.WriteLine("Took (time): {0}", (TimeSpan)(df-ds));

        }

        private static void PrintAllLines(List<string> lines)
        {
            Console.WriteLine(string.Join("\n", lines.ToArray()));
        }

        private static List<string> ReadListOfYeDRPOUs(string yedrpous_input_file, string startIdxStr, string procLenStr)
        {
            List<string> rslt = new List<string>();
            string[] allLines = File.ReadAllLines(yedrpous_input_file);
            if (string.IsNullOrEmpty(startIdxStr) && string.IsNullOrEmpty(procLenStr))
                rslt.AddRange(allLines);
            int iStart = 0;
            int iUntil = allLines.Length;
            int? startIdx = null; int? procLen = null;
            { int tmp; if (int.TryParse(startIdxStr, out tmp)) startIdx = (int?)tmp; }
            { int tmp; if (int.TryParse(procLenStr, out tmp)) procLen = (int?)tmp; }

            if (startIdx != null)
                iStart = (int)startIdx;
            if (procLen != null)
                iUntil = iStart + (int)procLen;
            if (iUntil > allLines.Length)
                iUntil = allLines.Length;
            for (int i = iStart; i < iUntil; i++)
                rslt.Add(allLines[i]);
            return rslt;
        }

        private static void DisposersJSONToTabDelim(string[] args)
        {
            //LogCmdArgs("DisposersJSONToTabDelim", args);
            string inJson = args[1];
            List<EDataGovUaDisposerInfo> lst = JsonConvert.DeserializeObject<List<EDataGovUaDisposerInfo>>(File.ReadAllText(inJson));
            using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(inJson), String.Format("{0}{1}", Path.GetFileNameWithoutExtension(inJson), ".tab")), false, Encoding.Unicode))
            {
                foreach (EDataGovUaDisposerInfo di in lst)
                {
                    if (!di.IsFound)
                        continue;
                    sw.WriteLine("{0}\t{1}\t{2}\t{3}", di.YeDRPOU, di.InternalID.ToString(), di.CabinetStatus, di.DisposerName);
                }
            }
        }

        private static void ConvertJson2XML(string[] args)
        {
            LogCmdArgs("ConvertJson2XML", args);
            string jsonIn = args[1];
            string xmlSave2Dir = args[2];
            ConvertJson2XMLWorker<EDataGovUaDisposerInfo>(jsonIn, xmlSave2Dir);
        }


        private static void ConvertRextonToXML(string[] args)
        {
            LogCmdArgs("ConvertRextonToXML", args);
            string jsonIn = args[1];
            ConvertJson2XMLWorker<RextonRecordInfo>(jsonIn, Path.GetDirectoryName(jsonIn));
        }

        private static void ConvertJson2XMLWorker<T>(string jsonPath, string xmlSave2Dir)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonPath));
            Tools.WriteXML<List<T>>(list, Path.Combine(xmlSave2Dir, string.Format("{0}.xml", Path.GetFileNameWithoutExtension(jsonPath))));
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
            System.Diagnostics.Process currProcess = System.Diagnostics.Process.GetCurrentProcess();
            Console.WriteLine(@"Process info : {{
ProcessId: {0}, MainWindowTitle: '{1}', ProcessName: '{2}', StartInfo.FileName: '{3}', StartInfo.Arguments: '{4}', StartTime: {5}, StartInfo.RedirectStandardOutput: {6}, StartInfo.RedirectStandardError: {7} }}", currProcess.Id, currProcess.MainWindowTitle, currProcess.ProcessName, currProcess.StartInfo.FileName, currProcess.StartInfo.Arguments, currProcess.StartTime, currProcess.StartInfo.RedirectStandardOutput, currProcess.StartInfo.RedirectStandardError);

        }

    }
}
