using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtilLib.Spares.Data
{
    public class VKWallPostTranslationInfo
    {
        public string VKPostUrl { get; set; }
        public string ImgFileName { get; set; }
        public string LegendTranslated { get; set; }
        public string RealPlaceLink { get; set; }
        public string RealPlaceText { get; set; }
        public string RealLegend { get; set; }
        public string UploadedImgUrl { get; set; }
        public string UploadedImgId { get; set; }
        public int ImgH { get; set; }
        public int ImgW { get; set; }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(VKPostUrl) && string.IsNullOrEmpty(ImgFileName) && string.IsNullOrEmpty(LegendTranslated);
            }
        }

        public static VKWallPostTranslationInfo Parse(string line)
        {
            VKWallPostTranslationInfo rslt = new VKWallPostTranslationInfo();
            string[] flds = line.Split('\t');
            if (flds.Length > 0)
                rslt.VKPostUrl = flds[0];
            if (flds.Length > 3)
                rslt.ImgFileName = flds[3];
            if (flds.Length > 4)
                rslt.LegendTranslated = flds[4];
            if (flds.Length > 5)
                rslt.RealPlaceLink = flds[5];
            if (flds.Length > 6)
                rslt.RealPlaceText = flds[6];
            if (flds.Length > 7)
                rslt.RealLegend = flds[7];
            //if (flds.Length > 8)
            //    rslt.UploadedImgUrl = flds[8];
            //if (flds.Length > 9)
            //    rslt.RealPlaceLink = flds[9];
            //if (flds.Length > 10)
            //    rslt.RealPlaceLink = flds[10];
            //if (flds.Length > 5)
            //    rslt.RealPlaceLink = flds[5];
            //if (flds.Length > 5)
            //    rslt.RealPlaceLink = flds[5];
            return rslt;
        }
    }
}
