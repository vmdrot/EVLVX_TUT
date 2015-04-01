using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.SharesAcquisitionInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class SharesAcquisitionInfo
    {
        public int SharesCount { get; set; }
        public int InclVotingSharesCount { get; set; }
        public decimal SharePrice { get; set; }
        public decimal TotalCosts { get; set; }
    }
}
