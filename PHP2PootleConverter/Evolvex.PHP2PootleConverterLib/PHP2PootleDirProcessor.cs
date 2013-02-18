using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Evolvex.PHP2PootleConverterLib.Data;

namespace Evolvex.PHP2PootleConverterLib
{
    public class PHP2PootleDirProcessor
    {
        #region const(s)
        public const string PHP_FILE_EXT = "php";
        public const string PO_FILE_EXT = "po";
        #endregion

        #region prop(s)
        public ConversionSettings Settings { get; private set; }
        #endregion

        #region method(s)
        [Obsolete]
        public void Process(string root, ConvertDirection dir)
        {
            string mask = String.Format("*.{0}", GetFileExtByDirection(dir));
            string[] files = Directory.GetFiles(root, mask, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                ProcessFile(file, dir);
            }

        }

        private void CheckRenameDirs()
        {
            if (String.IsNullOrEmpty(Settings.SourceLanguageName) ||
                String.IsNullOrEmpty(Settings.TargetLanguageName) ||
                Settings.SourceLanguageName.ToLower() == Settings.TargetLanguageName.ToLower())
                return;
            string[] dirs = Directory.GetDirectories(Settings.TargetDir, Settings.SourceLanguageName, SearchOption.AllDirectories);
            if (dirs.Length == 0)
                return;
            foreach(string dir in dirs)
            {
                if (!Directory.Exists(dir))
                    continue;
                string parentPath = Path.GetDirectoryName(dir);
                String newTargetNm = Path.Combine(parentPath, Settings.TargetLanguageName);
                Directory.Move(dir, newTargetNm);
            }
        }

        public void Process(ConversionSettings settings)
        {
            Settings = settings;
            string mask = String.Format("*.{0}", GetFileExtByDirection(Settings.Direction));
            string[] files = Directory.GetFiles(Settings.SourceDir, mask, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                ProcessFile(file);
            }
            CheckRenameDirs();
        }

        private void ProcessFile(string file)
        {
            string targetDir = Path.GetDirectoryName(file).Replace(Settings.SourceDir, Settings.TargetDir);
            string sourceFn = Path.GetFileNameWithoutExtension(file);
            string targetFn = (!String.IsNullOrEmpty(Settings.SourceLanguageName) && !String.IsNullOrEmpty(Settings.TargetLanguageName)&& sourceFn.ToLower() == Settings.SourceLanguageName.ToLower()) ? Settings.TargetLanguageName : sourceFn;
            string targetPath = Path.Combine(targetDir, string.Format("{0}.{1}", targetFn, GetFileExtByDirection(InvertDirection(Settings.Direction))));
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);
            PHP2PootleConverter c = new PHP2PootleConverter();
            c.ProcessFile(file, targetPath, Settings.Direction, Settings.SaveAsEncoding);
            if (Settings.SourceDir.ToLower() == Settings.TargetDir.ToLower() && Settings.DeleteSourceFiles)
                File.Delete(file);
        }

        public static string GetFileExtByDirection(ConvertDirection dir)
        {
            return (dir == ConvertDirection.PHP2Pootle ? PHP_FILE_EXT : PO_FILE_EXT);
        }
        public static ConvertDirection InvertDirection(ConvertDirection dir)
        {
            return dir == ConvertDirection.PHP2Pootle ? ConvertDirection.Pootle2PHP : ConvertDirection.PHP2Pootle;
        }
        [Obsolete]
        private void ProcessFile(string file, ConvertDirection dir)
        {
            string targetDir = Path.GetDirectoryName(file);
            string targetFn = Path.GetFileNameWithoutExtension(file);
            string targetPath = Path.Combine(targetDir, string.Format("{0}.{1}", targetFn, GetFileExtByDirection(InvertDirection( dir))));
            PHP2PootleConverter c = new PHP2PootleConverter();
            c.ProcessFile(file, targetPath, dir);
        }
        #endregion
    }
}
