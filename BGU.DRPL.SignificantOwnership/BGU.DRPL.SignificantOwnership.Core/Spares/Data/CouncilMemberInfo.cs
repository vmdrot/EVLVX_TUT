using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.CouncilMemberInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class CouncilMemberInfo
    {

        [Description("Член органу управління")]
        [DisplayName("Член органу управління")]
        public GenericPersonID Member { get; set; }
        [Description("Посада")]
        [DisplayName("Посада")]
        public string PositionName { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", PositionName, Member);
        }
    }
}
