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
        [Description("МФО")]
        [DisplayName("МФО")]
        public string HeadMFO { get; set; }
        /// <summary>
        /// REGN
        /// </summary>
        [Description("№ у реєстрі банків (лише для головних контор)")]
        [DisplayName("№ у реєстрі банків")]
        public string RegistryNr { get; set; }
        /// <summary>
        /// GLB
        /// </summary>
        [Description("Код банку (лише для головних контор)")]
        [DisplayName("Код банку")]
        public string Code { get; set; }
        [Description("Найменування банку (в оригіналі)")]
        [DisplayName("Найменування банку")]
        public string Name { get; set; }
        [Description("Найменування банку(українською)")]
        [DisplayName("Найменування банку(українською)")]
        public string NameUkr { get; set; }
        [Description("Відомості про юрособу-банк")]
        [DisplayName("Відомості про юрособу-банк")]
        public LegalPersonInfo LegalPerson { get; set; }

        /// <summary>
        /// http://www.swift.com/bsl/
        /// </summary>
        [DisplayName("SWIFT адреса")]
        [Description("Див. http://www.swift.com/bsl/")]
        public string SWIFTBIC { get; set; }

        [DisplayName("Країна діяльності")]
        public CountryInfo OperationCountry { get; set; }

        public BankInfo() 
        {
            OperationCountry = CountryInfo.UKRAINE;
        }
        public BankInfo(LegalPersonInfo le) : this()
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
