﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.CouncilBodyInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class CouncilBodyInfo
    {
        public CouncilBodyInfo()
        {
            Members = new List<CouncilMemberInfo>();
        }

        public GenericPersonID GovernedEntityID { get; set; }
        [Description("Члени органу управління")]
        public List<CouncilMemberInfo> Members { get; set; }
        [Description("№ п/п голови")]
        public GenericPersonID HeadMember { get; set; }
        public string CouncilBodyName { get; set; }
        public override string ToString()
        {
            int membersCnt = Members != null ? Members.Count : 0;
            return string.Format("{0} members", membersCnt);
        }
    }
}
