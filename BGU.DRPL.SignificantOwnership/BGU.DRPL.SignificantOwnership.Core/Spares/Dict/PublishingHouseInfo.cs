using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.PublishingHouseInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class PublishingHouseInfo
    {
        [DisplayName("Назва видавництва")]
        public string Name { get; set; }
        [DisplayName("Реквізити юрособи видавництва")]
        public LegalPersonInfo LegalPerson { get; set; }
    }
}
