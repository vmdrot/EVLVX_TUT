using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.IncomeOriginInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class IncomeOriginInfo
    {
        [DisplayName("Тип походження")]
        public FundsOriginType Origin { get; set; }
        [DisplayName("Сума доходу")]
        public CurrencyAmount Income { get; set; }
        [DisplayName("Опис та деталі щодо джерела доходу")]
        public string IncomeOriginNotes { get; set; }
    }
}
