using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;
using Evolvex.Utility.Core.ComponentModelEx;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Інформація про ліквідовану юр.особу
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.LiquidatedEntityOwnershipInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class LiquidatedEntityOwnershipInfo
    {
        /// <summary>
        /// Ідентифікатор, обов'язкове
        /// </summary>
        [DisplayName("Ліквідована юрособа")]
        [Required]
        public GenericPersonID Asset { get; set; }
        /// <summary>
        /// обов'язкове; заповнення секцій - за фактом власності подавача (чи кого там в анкеті вимагають, за контекстом)
        /// </summary>
        [DisplayName("Частки власності")]
        [Required]
        public TotalOwnershipDetailsInfo Stake { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Дата ліквідації")]
        [Required]
        public DateTime LiquidationDate { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Причина ліквідації")]
        [Required]
        public string LiquidationReason { get; set; }
        /// <summary>
        /// обов'язкове; мається на увазі, документ, рішення, розпорядження, закон, тощо (конкретний...)
        /// </summary>
        [DisplayName("Підстава ліквідації")]
        [Required]
        public string LiquidationPretext { get; set; }
    }
}
