using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Evolvex.GoogleDriveLib
{
    public class GoogleDriveDownloader
    {
        private const int KB = 0x400;
        private const int DownloadChunkSize = 256 * KB;
        /// <summary>The Drive API scopes.</summary>
        private static readonly string[] Scopes = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };

        private string _saveAs;
        public void Download(string url, string saveAs)
        {
            _saveAs = saveAs;
            //GoogleWebAuthorizationBroker.Folder = "Drive.Sample";
            GoogleWebAuthorizationBroker.Folder = "0B3mqM0LVG6dpUHFKclJ3dnFZazQ";

            UserCredential credential;
            using (var stream = new System.IO.FileStream("client_secrets.json",
                System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, Scopes, "valeriy.drotenko@gmail.com", CancellationToken.None);
            }
            //UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(null, Scopes, "valeriy.drotenko@gmail.com", 

            // Create the service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API Sample",
            });

            //await UploadFileAsync(service);

            // uploaded succeeded
            //Console.WriteLine("\"{0}\" was uploaded successfully", uploadedFile.Title);
            //await DownloadFile(service, uploadedFile.DownloadUrl);

            //await DownloadFile(service, @"https://docs.google.com/spreadsheet/ccc?key=0AnmqM0LVG6dpdEVSUW9id1Z3d2xNS1JkTjF3NWJpblE&usp=drive_web#gid=0");
            await DownloadFile(service, @"https://docs.google.com/spreadsheet/fm?id=tERQobwVwwlMKRdN1w5binQ.12825659819162080025.5925074579462148892&fmcmd=420");
            //await DeleteFile(service, uploadedFile);
        }

        private async Task DownloadFile(DriveService service, string url)
        {
            //var downloader = new MediaDownloader(service);
            var downloader = new MediaDownloader(service);
            downloader.ChunkSize = DownloadChunkSize;
            // add a delegate for the progress changed event for writing to console on changes
            downloader.ProgressChanged += Download_ProgressChanged;

            // figure out the right file type base on UploadFileName extension
            using (var fileStream = new System.IO.FileStream(_saveAs,
                System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                var progress = await downloader.DownloadAsync(url, fileStream);
                if (progress.Status == DownloadStatus.Completed)
                {
                    Console.WriteLine(_saveAs + " was downloaded successfully");
                }
                else
                {
                    Console.WriteLine("Download {0} was interpreted in the middle. Only {1} were downloaded. ",
                        _saveAs, progress.BytesDownloaded);
                }
            }
        }

        private static void Download_ProgressChanged(IDownloadProgress progress)
        {
            Console.WriteLine(progress.Status + " " + progress.BytesDownloaded);
        }

    }
}
