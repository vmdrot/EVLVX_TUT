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
        [DisplayName("Файл")]
        public string FileName { get; set; }
        [DisplayName("Назва")]
        public string Title { get; set; }
        [DisplayName("Опис файлу")]
        [Description("Що у файлі?")]
        public string Description { get; set; }
        [DisplayName("Формат")]
        [Description("Тип MIME, напр. pdf, xls, xlsx, doc, docs, rtf, тощо")]
        public string ContentType { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
