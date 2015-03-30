using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.AttachmentInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class AttachmentInfo
    {
        [Description("Файл")]
        public string FileName { get; set; }
        [Description("Назва")]
        public string Title { get; set; }
        [Description("Опис файлу")]
        public string Description { get; set; }
        [Description("Формат")]
        public string ContentType { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
