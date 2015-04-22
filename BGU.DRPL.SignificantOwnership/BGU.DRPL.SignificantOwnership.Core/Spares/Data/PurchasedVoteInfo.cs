﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Інформація про частку/голоси в управління юр.особою (ут.ч. банку), що 
    /// подавач хоче набути/придбати
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PurchasedVoteInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PurchasedVoteInfo
    {
        /// <summary>
        /// Від кого передаються голоси;
        /// варто подумати, щоб замінити на GenericPersonID
        /// </summary>
        [DisplayName("Передавач власності")]
        [Description("Особа, що передає/передає право голосу/частку власності")]
        public GenericPersonInfo Transferror { get; set; }
        [DisplayName("Набувані голоси/частка")]
        [Description("Голоси/частка у власності, що купується/отримується")]
        public OwnershipVotesInfo PurchaseVotes { get; set; }
        /// <summary>
        /// напр. "довіреність ... від ..., угода про купівлю/продаж ...", тощо
        /// </summary>
        [DisplayName("Шлях передачі/купівлі голосів/власності")]
        [Description("Шлях передачі/купівлі голосів/власності")]
        public string VotesTransferPath { get; set; }

        public override string ToString()
        {
            return string.Format("from {0}: {1} , {2}", Transferror, PurchaseVotes, VotesTransferPath);
        }
    }
}
