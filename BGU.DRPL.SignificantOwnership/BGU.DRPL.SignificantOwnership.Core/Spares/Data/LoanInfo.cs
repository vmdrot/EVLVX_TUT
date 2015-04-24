﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Evolvex.Utility.Core.ComponentModelEx;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Інформація про позику
    /// </summary>
    /// <seealso cref="IndebtnessInfoBase"/>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.LoanInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class LoanInfo : IndebtnessInfoBase
    {
        /// <summary>
        /// для банківських позик - обов'язковий
        /// </summary>
        [DisplayName("№ договору")]
        public string AgreementNr { get; set; }
        /// <summary>
        /// обов'язкове
        /// </summary>
        [DisplayName("Дата договору")]
        [Required]
        public DateTime AgreementDt { get; set; }
        /// <summary>
        /// якщо є прострочка
        /// </summary>
        [DisplayName("Прострочено з ... (дата)")]
        public DateTime? OverdueSince { get; set; }
    }
}
