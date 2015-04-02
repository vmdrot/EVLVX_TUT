using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.GenericPersonIDShare_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class GenericPersonIDShare
    {
        [DisplayName("Власник")]
        [Description("Власник")]
        public GenericPersonID Person { get; set; }
        [DisplayName("Частка")]
        [Description("Частка власності (%)")]
        public decimal Share { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", Person, Share );
        }
    }
}
