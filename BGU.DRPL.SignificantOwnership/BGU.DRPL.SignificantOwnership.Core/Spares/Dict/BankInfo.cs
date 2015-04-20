using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;

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

        public static BankInfo ParseFromRcuKruRow(DataRow dr)
        {
            string prb = dr["PRB"] as string;

            string glmfo = ((int)dr["GLMFO"]).ToString();
            string mfo = ((int)dr["MFO"]).ToString();
            string glb = ((int)dr["GLB"]).ToString();

            string prkb = ((int)dr["PRKB"]).ToString();

            string zipCode = dr["PI"] as string;
            string city = dr["NP"] as string;
            string address = dr["ADRESS"] as string;
            string yedrpou = dr["IKOD"] as string;

            BankInfo bi = new BankInfo();
            bi.OperationCountry = CountryInfo.UKRAINE;
            bi.Name = dr["NB"] as string;
            bi.HeadMFO = mfo;
            bi.Code = glb;
            bi.RegistryNr = prkb;
            bi.LegalPerson = new LegalPersonInfo() { TaxCodeOrHandelsRegNr = yedrpou, Name = dr["FULLNAME"] as string, Address = LocationInfo.Parse(address), ResidenceCountry = CountryInfo.UKRAINE };
            bi.LegalPerson.Address.City = city;
            bi.LegalPerson.Address.ZipCode = zipCode;
            return bi;
        }
    }
}
