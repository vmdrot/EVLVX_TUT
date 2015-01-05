using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Evolvex.VKUtilLib.Spares.Data
{
    public class VKPostInfo
    {
        public string Url { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }

        private string _imgFileName;
        public string ImgFileName
        {
            get
            {
                if (_imgFileName == null)
                {
                    _imgFileName = Path.GetFileName(Img);
                }
                return _imgFileName;
            }
        }
        public bool IsEmpty
        {
            get
            {
                if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Img))
                    return true;
                return false;
            }
        }
    }
}
