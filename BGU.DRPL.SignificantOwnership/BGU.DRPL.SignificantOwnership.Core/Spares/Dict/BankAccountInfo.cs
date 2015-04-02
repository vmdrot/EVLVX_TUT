using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BankAccountInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BankAccountInfo
    {
        [DisplayName("Власник рахунку")]
        public GenericPersonID AccountOwner { get; set; }
        [DisplayName("У банку...")]
        public BankInfo Bank { get; set; }
        [DisplayName("№ рахунку")]
        public string AccountNr { get; set; }
        [DisplayName("Валюта рахунку")]
        public string AccountCCY { get; set; }
        [DisplayName("Опис/примітки/призначення рахунку")]
        public string AccountDescription { get; set; }
    }
}
