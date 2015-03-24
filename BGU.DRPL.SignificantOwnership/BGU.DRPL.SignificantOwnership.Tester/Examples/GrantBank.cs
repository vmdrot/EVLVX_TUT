using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

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
            LocationInfo addrKhaMost248kv31 = LocationInfo.Parse("61000,м.Харків, пр.Московський, буд.248, кв.31"); addrKhaMost248kv31.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaGag41a25 = LocationInfo.Parse("61000,м.Харків,пр.Гагаріна,41а,кв.25"); addrKhaGag41a25.Country = CountryInfo.UKRAINE;
            LocationInfo addreKhaLen5kv53 = LocationInfo.Parse("61166,м.Харків,вул.Леніна,5,кв.53"); addreKhaLen5kv53.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaArt17kv3 = LocationInfo.Parse("61000,м.Харків,вул.Артема,17,кв.3"); addrKhaArt17kv3.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaVysRudn342 = LocationInfo.Parse("62459,Харківська обл.,Харківський р-н.,пос.Високий,вул.Руднєва,34/2"); addrKhaVysRudn342.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaTytar12kv40 = LocationInfo.Parse("61064,м.Харків,пров.Титаренківський, б.12, кв.40"); addrKhaTytar12kv40.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaMyrh3 = LocationInfo.Parse("61010,м.Харків,вул.Миргородська,буд.3"); addrKhaMyrh3.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaTankop113kv64 = LocationInfo.Parse("61000,м.Харків,вул.Танкопія,буд.11/3,кв.64"); addrKhaTankop113kv64.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaMyrh3kv1 = LocationInfo.Parse("61010,м.Харків,вул.Миргородська,буд.3,кв.1"); addrKhaMyrh3kv1.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaMin109 = LocationInfo.Parse("61166,м.Харків,вул.Мінська,буд.109"); addrKhaMin109.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaRomRol12 = LocationInfo.Parse("61166,м.Харків,вул.Ромен Роллана,12"); addrKhaRomRol12.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaArt46 = LocationInfo.Parse("61002,м.Харків,вул.Артема,46"); addrKhaArt46.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaCubar1 = LocationInfo.Parse("61002,м.Харків, вул.Чубаря,1"); addrKhaCubar1.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaDergSolonytsPushk151 = LocationInfo.Parse("62370,Харківська обл., Дергачівський р-н, смт.Солоницівка, вул. Пушкіна, буд.15/1"); addrKhaDergSolonytsPushk151.Country = CountryInfo.UKRAINE;
            LocationInfo addrKhaSums53kv4 = LocationInfo.Parse("61022,м.Харків,вул.Сумська,буд.53,кв.4"); addrKhaSums53kv4.Country = CountryInfo.UKRAINE;
            #endregion

            #region registrar(s)

            #endregion

            #region Phys
            GenericPersonInfo physShkarupinaTA = new GenericPersonInfo() { PersonType = Core.Spares.EntityType.Physical, PhysicalPerson = new PhysicalPersonInfo() { FullName = "Шкарупіна Тамара Іванівна", TaxOrSocSecID = "2062001867", PassportID = "МК 694952", PassIssueAuthority = new RegistrarAuthority() { JurisdictionCountry = CountryInfo.UKRAINE, EntitiesHandled = Core.Spares.EntityType.Physical, RegistrarName = "Фрунзенським РВ ХМУ УМВСУ в Харківській обл." }, PassIssuedDate = DateTime.Parse("15.10.1996р."), Address = addrKhaMost248kv31 } };
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
