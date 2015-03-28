using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PurchasedVoteInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PurchasedVoteInfo
    {
        public GenericPersonInfo Transferror { get; set; }
        public OwnershipVotesInfo PurchaseVotes { get; set; }
        public string VotesTransferPath { get; set; }
    }
}
