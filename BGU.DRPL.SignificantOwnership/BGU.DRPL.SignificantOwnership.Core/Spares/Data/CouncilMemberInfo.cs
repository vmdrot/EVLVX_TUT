using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.CouncilMemberInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class CouncilMemberInfo
    {
        
        public GenericPersonID Member { get; set; }
        public string PositionName { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", PositionName, Member);
        }
    }
}
