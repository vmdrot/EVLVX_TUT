using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.VKUtilLib.Spares.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Globalization;

namespace Evolvex.VKUtilLib.Misc
{
    public class VKWallTranslationFormatter
    {
        
        #region Formats for English
        private static readonly string POST_TRANSL_FMT = @"<p style=""text-align: center;""><strong>{0}</strong></p>
<p style=""text-align: center;"">{1}</p>
<em>{2}Original VK post - <a href=""{3}"" target=""_blank"">here</a>.</em>
<p style=""text-align: center;""><strong>*   *   *</strong></p>
";
        private static readonly string IN_REALITY_FMT = @"<strong>In reality</strong>: {0}.";
        private static readonly string BOTTOM_LEGEND_FMT = @"({0}{1}). ";
        private static readonly string IMG_FMT = @"<a href=""{0}""><img class=""aligncenter size-medium wp-image-{1}"" src=""{0}"" alt=""{2}"" width=""{3}"" height=""{4}"" /></a>";
        private static readonly string SEE_ALSO_FMT = @" See also: <a href=""{0}"">{1}</a> ";
        #endregion

        #region Formats for Italian
        private static readonly string POST_TRANSL_FMT_IT = @"<p style=""text-align: center;""><strong>{0}</strong></p>
<p style=""text-align: center;"">{1}</p>
<em>{2}Il link sul post VK.com - <a href=""{3}"" target=""_blank"">qui</a>.</em>
<p style=""text-align: center;""><strong>*   *   *</strong></p>
";
        private static readonly string IN_REALITY_FMT_IT = @"<strong>In realtà</strong>: {0}.";
        private static readonly string BOTTOM_LEGEND_FMT_IT = @"({0}{1}). ";
        private static readonly string IMG_FMT_IT = @"<a href=""{0}""><img class=""aligncenter size-medium wp-image-{1}"" src=""{0}"" alt=""{2}"" width=""{3}"" height=""{4}"" /></a>";
        private static readonly string SEE_ALSO_FMT_IT = @" Vedi anche: <a href=""{0}"">{1}</a> ";
        #endregion
        private static readonly Dictionary<int, Dictionary<Format, string>> FORMATS;

        static VKWallTranslationFormatter()
        {
            FORMATS = new Dictionary<int, Dictionary<Format, string>>();

            #region English
            Dictionary<Format, string> englishFormats = new Dictionary<Format, string>();
            englishFormats.Add(Format.POST_TRANSL, POST_TRANSL_FMT);
            englishFormats.Add(Format.IN_REALITY, IN_REALITY_FMT);
            englishFormats.Add(Format.BOTTOM_LEGEND, BOTTOM_LEGEND_FMT);
            englishFormats.Add(Format.IMG, IMG_FMT);
            englishFormats.Add(Format.SEE_ALSO, SEE_ALSO_FMT);
            FORMATS.Add(CultureInfo.GetCultureInfoByIetfLanguageTag("en").LCID, englishFormats);
            #endregion 

            #region Italian
            Dictionary<Format, string> italianFormats = new Dictionary<Format, string>();
            italianFormats.Add(Format.POST_TRANSL, POST_TRANSL_FMT_IT);
            italianFormats.Add(Format.IN_REALITY, IN_REALITY_FMT_IT);
            italianFormats.Add(Format.BOTTOM_LEGEND, BOTTOM_LEGEND_FMT_IT);
            italianFormats.Add(Format.IMG, IMG_FMT_IT);
            italianFormats.Add(Format.SEE_ALSO, SEE_ALSO_FMT_IT);
            FORMATS.Add(CultureInfo.GetCultureInfoByIetfLanguageTag("it").LCID, italianFormats);
            #endregion
        }

        public VKWallTranslationFormatter()
        {
            this.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("en");
        }

        public CultureInfo Culture { get; set; }

        public int LCID
        {
            get
            {
                return Culture.LCID;
            }
        }
        
        public string ComposeHTML(List<VKWallPostTranslationInfo> infos)
        {
            StringBuilder rslt = new StringBuilder();
            for (int i = 0; i < infos.Count; i++)
            {
                if(i>0)
                    rslt.Append("\r\n");
                rslt.Append(ComposeSingleEntryHTML(infos[i]));
            }
            return rslt.ToString();
        }

        public string ComposeSingleEntryHTML(VKWallPostTranslationInfo info)
        {
            if(string.IsNullOrEmpty(info.LegendTranslated) || string.IsNullOrEmpty(info.VKPostUrl))
                return string.Empty;
            string inReality = string.Empty;
            string seeAlso = string.Empty;
            string imgPart = string.Empty;
            string bottomLegend = string.Empty;
            if (!String.IsNullOrEmpty(info.RealLegend))
                inReality = string.Format(FORMATS[LCID][Format.IN_REALITY], info.RealLegend);
            if (!string.IsNullOrEmpty(info.RealPlaceText) && !string.IsNullOrEmpty(info.RealPlaceLink))
                seeAlso = string.Format(FORMATS[LCID][Format.SEE_ALSO], info.RealPlaceLink, info.RealPlaceText);

            if (!string.IsNullOrEmpty(info.UploadedImgUrl))
                imgPart = string.Format(FORMATS[LCID][Format.IMG], info.UploadedImgUrl, info.UploadedImgId, info.ImgFileName, info.ImgW, info.ImgH);
            if(!string.IsNullOrEmpty(inReality) || !string.IsNullOrEmpty(seeAlso))
                bottomLegend = string.Format(FORMATS[LCID][Format.BOTTOM_LEGEND], inReality, seeAlso);
            return string.Format(FORMATS[LCID][Format.POST_TRANSL], info.LegendTranslated, imgPart, bottomLegend, info.VKPostUrl);
        }


        public static void FillImageSize(VKWallPostTranslationInfo info, string localImgsDir)
        {

            Size sz = GetImageSize(Path.Combine(localImgsDir, info.ImgFileName));
            info.ImgW = sz.Width;
            info.ImgH = sz.Height;
        }

        public static Size GetImageSize(String imgPath)
        {
            using (Image img = Image.FromFile(imgPath))
            {
                return img.Size;
            }
        }

        private enum Format
        {
            None = 0,
            POST_TRANSL,
            IN_REALITY,
            BOTTOM_LEGEND,
            IMG,
            SEE_ALSO
        }

    }

}
