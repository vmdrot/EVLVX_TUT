using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.SharesAcquisitionInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class SharesAcquisitionInfo
    {
        [DisplayName("Кількість акцій (одиниць)")]
        public int SharesCount { get; set; }
        [DisplayName("У тому числі кількість акцій з правом голосу")]
        public int InclVotingSharesCount { get; set; }
        [DisplayName("номінальна вартість однієї акції (паю)")]
        public decimal SharePrice { get; set; }
        [DisplayName("загальна сума ... гривень")]
        public decimal TotalCosts { get; set; }
    }
}
