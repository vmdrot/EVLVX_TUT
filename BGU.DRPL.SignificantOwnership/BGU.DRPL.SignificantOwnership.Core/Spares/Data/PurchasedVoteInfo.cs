using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PurchasedVoteInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PurchasedVoteInfo
    {
        [DisplayName("Передавач власності")]
        [Description("Особа, що передає/передає право голосу/частку власності")]
        public GenericPersonInfo Transferror { get; set; }
        [DisplayName("Набувані голоси/частка")]
        [Description("Голоси/частка у власності, що купується/отримується")]
        public OwnershipVotesInfo PurchaseVotes { get; set; }
        [DisplayName("Шлях передачі/купівлі голосів/власності")]
        [Description("Шлях передачі/купівлі голосів/власності")]
        public string VotesTransferPath { get; set; }

        public override string ToString()
        {
            return string.Format("from {0}: {1} , {2}", Transferror, PurchaseVotes, VotesTransferPath);
        }
    }
}
