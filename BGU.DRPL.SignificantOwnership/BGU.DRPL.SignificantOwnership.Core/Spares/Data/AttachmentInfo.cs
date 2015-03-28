using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.AttachmentInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class AttachmentInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
