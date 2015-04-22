﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Data
{
    /// <summary>
    /// Інформація про банківську ліцензію (ідентифікація)
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BankingLicenseInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BankingLicenseInfo
    {
        /// <summary>
        /// Обов'язкове
        /// </summary>
        [DisplayName("№ ліцензії")]
        public string LicenseNr { get; set; }
        /// <summary>
        /// Обов'язкове
        /// </summary>
        [DisplayName("Дата видачі ліцензії")]
        public DateTime IssueDate { get; set; }

        public override string ToString()
        {
            return string.Format("{0} dd {1}", LicenseNr, IssueDate);
        }
    }
}
