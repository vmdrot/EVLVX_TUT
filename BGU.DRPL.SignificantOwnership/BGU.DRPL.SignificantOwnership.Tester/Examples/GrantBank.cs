using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;

namespace BGU.DRPL.SignificantOwnership.Tester.Examples
{
    public class GrantBank
    {
        private List<PhysicalPersonInfo> _INDs;
        private List<LegalPersonInfo> _LEs;
        private Appx2OwnershipStructLP _appx2Questionnaire;

        public Appx2OwnershipStructLP Appx2Questionnaire
        {
            get
            {
                if (_appx2Questionnaire == null)
                    _appx2Questionnaire = GenerateAppx2Questionnaire();
                return _appx2Questionnaire;
             }
        }

        private void PopulateDicts()
        {
            #region countries
            
            #endregion

            #region addresses
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaGagarina41a25 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Гагаріна", HouseNr = "41а", ApptOfficeNr ="25" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };
            LocationInfo addrKhaMosk248 = new LocationInfo() { Country = CountryInfo.UKRAINE, City = "Харків", ZipCode = "61000", Street = "пр.Московський", HouseNr = "248", ApptOfficeNr ="31" };

61166,м.Харків,вул.Леніна,5,кв.53
61000,м.Харків,вул.Артема,17,кв.3
62459,Харківська обл.,Харківський р-н.,пос.Високий,вул.Руднєва,34/2
61064,м.Харків,пров.Титаренківський, б.12, кв.40
61010,м.Харків,вул.Миргородська,буд.3
61000,м.Харків,вул.Танкопія,буд.11/3,кв.64

61010,м.Харків,вул.Миргородська,буд.3,кв.1
61166,м.Харків,вул.Мінська,буд.109
61058,м.Харків,вул.Ромена Роллана,12
61002,м.Харків,вул.Артема,46
61002,м.Харків, вул.Чубаря,1
62370,Харківська обл., Дергачівський р-н, смт.Солоницівка, вул. Пушкіна, буд.15/1
61022,м.Харків,вул.Сумська,буд.53,кв.4
            #endregion

            #region registrar(s)
            #endregion

            #region Phys
            #endregion

            #region Legals
            #endregion

        }


        private Appx2OwnershipStructLP GenerateAppx2Questionnaire()
        {
            throw new NotImplementedException();
        }
    }
}
