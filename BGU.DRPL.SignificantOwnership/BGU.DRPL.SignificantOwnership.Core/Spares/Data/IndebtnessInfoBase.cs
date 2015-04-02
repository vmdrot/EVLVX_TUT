using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    public class IndebtnessInfoBase
    {
        [DisplayName("Кредитор")]
        public GenericPersonID Lender { get; set; }
        [DisplayName("Позичальник")]
        public GenericPersonID Borrower { get; set; }
        [DisplayName("Суть заборгованості")]
        public string DebtSubject { get; set; }
        [DisplayName("Основна сума боргу")]
        public CurrencyAmount Principal { get; set; }
        [DisplayName("Дата погашення")]
        public DateTime RepaymentDueDate { get; set; }
        [DisplayName("Залишок заборгованості")]
        public CurrencyAmount OutstandingPricipal { get; set; }
        [DisplayName("Прострочена заборгованість?")]
        public bool IsOverdue { get; set; }
        [DisplayName("Сума простроченої заборгованості")]
        public CurrencyAmount PrincipalOverdue { get; set; }
        [DisplayName("Деталі прострочки")]
        public string OverdueDetails { get; set; }
        [DisplayName("Прични прострочки")]
        public string OverdueReasons { get; set; }

    }
}
