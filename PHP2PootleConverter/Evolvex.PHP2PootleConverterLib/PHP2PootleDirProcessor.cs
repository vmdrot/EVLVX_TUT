using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Evolvex.PHP2PootleConverterLib
{
    public class PHP2PootleDirProcessor
    {
        public const string PHP_FILE_EXT = "php";
        public const string PO_FILE_EXT = "po";
        public void Process(string root, ConvertDirection dir)
        {
            string mask = String.Format("*.{0}", GetFileExtByDirection(dir));
            string[] files = Directory.GetFiles(root, mask, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                ProcessFile(file, dir);
            }
        }

        public static string GetFileExtByDirection(ConvertDirection dir)
        {
            return (dir == ConvertDirection.PHP2Pootle ? PHP_FILE_EXT : PO_FILE_EXT);
        }
        public static ConvertDirection InvertDirection(ConvertDirection dir)
        {
            return dir == ConvertDirection.PHP2Pootle ? ConvertDirection.Pootle2PHP : ConvertDirection.PHP2Pootle;
        }
        private void ProcessFile(string file, ConvertDirection dir)
        {
            string targetDir = Path.GetDirectoryName(file);
            string targetFn = Path.GetFileNameWithoutExtension(file);
            string targetPath = Path.Combine(targetDir, string.Format("{0}.{1}", targetFn, GetFileExtByDirection(InvertDirection( dir))));
            PHP2PootleConverter c = new PHP2PootleConverter();
            c.ProcessFile(file, targetPath, dir);
        }
    }
}
