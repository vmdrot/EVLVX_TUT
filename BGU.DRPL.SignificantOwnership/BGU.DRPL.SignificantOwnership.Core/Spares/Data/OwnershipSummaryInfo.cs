using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Структура для групування загальної інформації про власність
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.OwnershipSummaryInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class OwnershipSummaryInfo
    {
        [DisplayName("%")]
        [Description("%")]
        public decimal Pct { get; set; }
        /// <summary>
        /// Частина статутного капіталу/фонду, що перебуває у володінні
        /// </summary>
        [DisplayName("Cума")]
        [Description("Cума")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Кількість голосів, що припадає на частку володіння (якщо припадають якісь)
        /// </summary>
        [DisplayName("Кількість голосів")]
        [Description("Кількість голосів")]
        public int Votes { get; set; }

        public override string ToString()
        {
            return string.Format("{0}% {1:N0} {2:N0}", Pct, Amount, Votes);
        }
    }
}
