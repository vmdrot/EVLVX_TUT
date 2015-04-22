﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Структура для зберігання відомостей про надану гарантію, поручительство, тощо
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.FinancialGuaranteeInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class FinancialGuaranteeInfo
    {
        /// <summary>
        /// Ідентифікатор особи;
        /// обирається зі списку, джерело - MentionedEntities, або ж уводиться (тут же) й "кладеться" у MentionedEntities.
        /// </summary>
        [DisplayName("Особа, щодо якої гарантую/ручаюся,тощо")]
        public GenericPersonID Person { get; set; }
        /// <summary>
        /// Обов'зкове
        /// </summary>
        [DisplayName("Роль")]
        [Description("Роль (гарантор, довірена особа, тощо)")]
        public FinancialGuarantorRoleType Role { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Сума гарантії/поруки/тощо")]
        public CurrencyAmount PledgeAmt { get; set; }
        /// <summary>
        /// обов'язкове (але вільного формату)
        /// </summary>
        [DisplayName("Деталі")]
        [Description("з яких питань")]
        public string GuaranteeDetails { get; set; }
    }
}
