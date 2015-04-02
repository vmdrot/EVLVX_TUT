﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PaymentDeadlineInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PaymentDeadlineInfo
    {
        [DisplayName("Сума")]
        public CurrencyAmount Amount { get; set; }
        [DisplayName("Дата")]
        public DateTime Deadline { get; set; }
    }
}
