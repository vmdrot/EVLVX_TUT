using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BankInfo_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class BankInfo
    {
        /// <summary>
        /// GLMFO
        /// </summary>
        [Description("МФО головного банку")]
        public string HeadMFO { get; set; }
        /// <summary>
        /// REGN
        /// </summary>
        [Description("№ у реєстрі банків")]
        public string RegistryNr { get; set; }
        /// <summary>
        /// GLB
        /// </summary>
        [Description("Код банку")]
        public string Code { get; set; }
        [Description("Найменування банку")]
        public string Name { get; set; }
        [Description("Відомості про юрособу-банк")]
        public LegalPersonInfo LegalPerson { get; set; }

        public BankInfo() { }
        public BankInfo(LegalPersonInfo le)
        {
            LegalPerson = le;
            Name = le.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
