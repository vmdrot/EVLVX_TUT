using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.LiquidatedEntityOwnershipInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class LiquidatedEntityOwnershipInfo
    {
        [DisplayName("Ліквідовувана юрособа")]
        public GenericPersonID Asset { get; set; }
        [DisplayName("Частки власності")]
        public TotalOwnershipDetailsInfo Stake { get; set; }
        [DisplayName("Дата ліквідації")]
        public DateTime LiquidationDate { get; set; }
        [DisplayName("Причина ліквідації")]
        public string LiquidationReason { get; set; }
        [DisplayName("Підстава ліквідації")]
        public string LiquidationPretext { get; set; }
    }
}
