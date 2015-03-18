using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.Spares;
using System.Xml.Serialization;
using System.IO;

namespace BGU.DRPL.SignificantOwnership.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSampleAppx2OwnershipStructLP();
        }

        private static void CreateSampleAppx2OwnershipStructLP()
        {
            Appx2OwnershipStructLP questio = new Appx2OwnershipStructLP();
            #region filling sample with data

            #region re-usable constants
            #region countries
            CountryInfo countryUkraine = new CountryInfo() { CountryISO2Code = "UA", CountryISO3Code = "UKR", CountryISONr = "804", CountryNameEng = "Ukraine", CountryNameUkr = "Україна" };
            CountryInfo countryAustria = new CountryInfo() { CountryISO2Code = "AT", CountryISO3Code = "AUT", CountryISONr = "040", CountryNameEng = "Austria", CountryNameUkr = "Австрія" };
            CountryInfo countryCyprus = new CountryInfo() { CountryISO2Code = "CY", CountryISO3Code = "CYP", CountryISONr = "196", CountryNameEng = "Cyprus", CountryNameUkr = "Кіпр" };
            #endregion

            #region addresses
            LocationInfo mArnautska102 = new LocationInfo() { Country = countryUkraine, City = "Одеса", Region = "Одеська область", ZipCode = "65007", Street = "вул. Мала Арнаутська", HouseNr = "102", ApptOfficeNr = "5" };
            LocationInfo addrPerDiv83a = new LocationInfo() { Country = countryUkraine, City = "Одеса", Region = "Одеська область", ZipCode = "65016", Street = "вул. Фонтанська Дорога", HouseNr = "83" };
            LocationInfo addrKordProv13a = new LocationInfo() { Country = countryUkraine, City = "Одеса", Region = "Одеська область", ZipCode = "65074", Street = "пров. Кордонний", HouseNr = "13А" };
            LocationInfo addrTirasp22 = new LocationInfo() { Country = countryUkraine, City = "Одеса", Region = "Одеська область", ZipCode = "65020", Street = "вул. Тираспольська", HouseNr = "22", ApptOfficeNr = "3" };
            //https://www.post.at/suche/standortsuche.php/index/selectedsearch/plz?_cc_=1 , http://www.stadtplandienst.at/
            LocationInfo addrStPoeltenErtl3 = new LocationInfo() { Country = countryAustria, City = "St.Pölten", Region = "Niederösterreich", ZipCode = "AT-3100", Street = "Ertlstr.", HouseNr = "3", ApptOfficeNr = "5" };
            #endregion

            #region Registrars
            RegistrarAuthority kyivOdRVUMVS = new RegistrarAuthority()
                                                {
                                                    //src: http://mvs.gov.ua/mvs/control/odesa/uk/publish/article/144997
                                                    Address = new LocationInfo()
                                                                    {
                                                                        Country = countryUkraine,
                                                                        City = "Одеса",
                                                                        ZipCode = "65080",
                                                                        Street = "вул. Академіка Філатова",
                                                                        Region = "Одеська область",
                                                                        HouseNr = "15-А"
                                                                    },
                                                    EntitiesHandled = EntityType.Physical,
                                                    RegistrarCode = "",
                                                    RegistrarName = "Київський РВ ОМУ ГУМВС України в Одеській області"
                                                };
            RegistrarAuthority magistratStPoelten = new RegistrarAuthority()
                                                {
                                                    //src: https://www.help.gv.at/linkaufloesung/applikation-flow;jsessionid=557D7A72865E9928014A07F8671AB9E9.tomcat_help1?execution=e1s1
                                                    Address = new LocationInfo()
                                                                    {
                                                                        Country = countryAustria,
                                                                        City = "St. Pölten",
                                                                        ZipCode = "3100",
                                                                        Street = "Rathausplatz",
                                                                        Region = "Niederösterreich",
                                                                        HouseNr = "1"
                                                                    },
                                                    EntitiesHandled = EntityType.Physical,
                                                    RegistrarCode = "",
                                                    RegistrarName = "Magistrat St. Pölten"
                                                };
            RegistrarAuthority cyprusRegister = new RegistrarAuthority()
                                                    {
                                                        JurisdictionCountry = countryCyprus,
                                                        EntitiesHandled = EntityType.Legal,
                                                        RegistrarName = "DEPARTMENT OF REGISTRAR OF COMPANIES AND OFFICIAL RECEIVER",
                                                        //RegistrarCode = "",
                                                        Address = new LocationInfo()
                                                                    {
                                                                        Country = countryCyprus,
                                                                        City = "Nicosia",
                                                                        Street = "Corner Makarios Avenue & Karpenisiou",
                                                                        HouseNr = "XENIOS Building"
                                                                    }
                                                    };
            RegistrarAuthority reestrOdesa = new RegistrarAuthority()
            {
                JurisdictionCountry = countryUkraine,
                RegistrarName = "Реєстраційна служба Одеського міського управління юстиції",
                Address = new LocationInfo() { Country = countryUkraine, City = "Одеса", Street = "вул. Велика Арнаутська", HouseNr = "15", ZipCode = "65012" },
                EntitiesHandled = EntityType.Legal,
                RegistrarCode = ""
            };
            #endregion

            #region physical persons
            PhysicalPersonInfo kacmanAM = new PhysicalPersonInfo()
                                                           {
                                                               CitizenshipCountry = countryUkraine,
                                                               FullName = "Кацман Абрам Мойсейович",
                                                               Name = "Абрам",
                                                               MiddleName = "Мойсейович",
                                                               Surname = "Кацман",
                                                               PassportID = "ЕА 293857",
                                                               PassIssueAuthority = kyivOdRVUMVS,
                                                               PassIssuedDate = DateTime.Parse("1995-03-12T00:00:00"),
                                                               TaxOrSocSecID = "0540117865",
                                                               Address = addrPerDiv83a

                                                           };

            PhysicalPersonInfo handrabura = new PhysicalPersonInfo()
                                                           {
                                                               CitizenshipCountry = countryUkraine,
                                                               FullName = "Гандрабура Циля Авазівна",
                                                               Name = "Циля",
                                                               MiddleName = "Авазівна",
                                                               Surname = "Гандрабура",
                                                               PassportID = "КЕ 554635",
                                                               PassIssueAuthority = kyivOdRVUMVS,
                                                               PassIssuedDate = DateTime.Parse("1997-12-12T00:00:00"),
                                                               TaxOrSocSecID = "5543216455",
                                                               Address = addrKordProv13a
                                                           };
            PhysicalPersonInfo holovatyi = new PhysicalPersonInfo()
            {
                CitizenshipCountry = countryUkraine,
                FullName = "Головатий Марко Пафнутійович",
                Name = "Марко",
                MiddleName = "Пафнутійович",
                Surname = "Головатий",
                PassportID = "АВ 135986",
                PassIssueAuthority = kyivOdRVUMVS,
                PassIssuedDate = DateTime.Parse("1993-10-05T00:00:00"),
                TaxOrSocSecID = "0514235444",
                Address = addrTirasp22

            };

            // sample - 
            PhysicalPersonInfo siehnert = new PhysicalPersonInfo()
            {
                CitizenshipCountry = countryAustria,
                FullName = "Siehnert Andreas Johann",
                Name = "Andreas",
                MiddleName = "Johann",
                Surname = "Siehnert",
                FullNameUkr = "Зінерт Андреас Йоганн",
                NameUkr = "Андреас",
                MiddleNameUkr = "Йоганн",
                SurnameUkr = "Зінерт",
                BirthDate = DateTime.Parse("1982-05-10T00:00:00"),
                Sex = SexType.Male,
                PassportID = "10008456", //http://de.wikipedia.org/wiki/Personalausweis#/media/File:Austrian_ID_card.jpg
                PassIssueAuthority = magistratStPoelten,
                PassIssuedDate = DateTime.Parse("2002-12-18T00:00:00"),
                TaxOrSocSecID = "1237 010180", //http://de.wikipedia.org/wiki/Sozialversicherungsnummer#.C3.96sterreich
                Address = addrStPoeltenErtl3

            };
            #endregion

            #region legal persons
            LegalPersonInfo markManLtd = new LegalPersonInfo() // https://efiling.drcor.mcit.gov.cy/DrcorPublic/SearchResults.aspx?name=Mark&number=%25&searchtype=optStartMatch&index=4&lang=EN&tname=%25&sc=1
                                                                      {
                                                                          Address = new LocationInfo()
                                                                          {
                                                                              Country = countryCyprus,
                                                                              City = "Μέσα Γειτονιά",
                                                                              Street = "Αρχ. Μακαρίου ΙΙΙ",
                                                                              HouseNr = "1",
                                                                              ApptOfficeNr = "ATLANTIS BUILDING, Floor 1",
                                                                              ZipCode = "4000",
                                                                              Region = "Λεμεσός"
                                                                          },
                                                                          ResidenceCountry = countryCyprus,
                                                                          Registrar = cyprusRegister,
                                                                          Name = "MARK MAN LIMITED",
                                                                          TaxCodeOrHandelsRegNr = "ΗΕ 68757"
                                                                      };

            LegalPersonInfo sigmaHldngsLtd = new LegalPersonInfo() // https://efiling.drcor.mcit.gov.cy/DrcorPublic/SearchResults.aspx?name=Sigma+holdings&number=%25&searchtype=optStartMatch&index=1&lang=EN&tname=%25&sc=1
            {
                Address = new LocationInfo()
                {
                    Country = countryCyprus,
                    City = "Λεμεσός",
                    Street = "Γρ. Ξενοπούλου",
                    HouseNr = "17",
                    ZipCode = "3106"

                },
                ResidenceCountry = countryCyprus,
                Registrar = cyprusRegister,
                Name = "SIGMA EXPLORATIONS HOLDINGS LIMITED",
                TaxCodeOrHandelsRegNr = "ΗΕ 285734"
            };

            LegalPersonInfo ebensyCustdyLtd = new LegalPersonInfo() // https://efiling.drcor.mcit.gov.cy/DrcorPublic/SearchResults.aspx?name=custody&number=%25&searchtype=optStartMatch&index=1&lang=EN&tname=%25&sc=1
            {
                Address = new LocationInfo()
                {
                    Country = countryCyprus,
                    City = "Λεμεσός",
                    Street = "Ρήγαινας",
                    HouseNr = "13",
                    ZipCode = "4632"

                },
                ResidenceCountry = countryCyprus,
                Registrar = cyprusRegister,
                Name = "EBENSY CUSTODY LIMITED",
                TaxCodeOrHandelsRegNr = "ΗΕ 140707",
                RepresentedBy = siehnert
            };
            #endregion

            #endregion

            #region the Questionnaire itself
            questio.BankRef = new BankInfo() { HeadMFO = "300614", Name = "ПАТ \"Креді Агріколь Банк\"", Code = "171", RegistryNr = "171" };
            questio.Acquiree = new LegalPersonInfo()
                                  {
                                      Address = mArnautska102,
                                      TaxCodeOrHandelsRegNr = "21546214",
                                      Name = "МЧП Ветерок",
                                      Registrar = reestrOdesa,
                                      ResidenceCountry = countryUkraine,
                                      RepresentedBy = kacmanAM
                                  };
            questio.IsSupervisoryCouncilPresent = true;
            questio.SupervisoryCouncil = new CouncilBodyInfo()
                                            {
                                                Members = new List<CouncilMemberInfo>
                                                    (new CouncilMemberInfo[]
                                                       {
                                                          new CouncilMemberInfo(){ Member = new GenericPersonInfo(){ PersonType = EntityType.Physical, PhysicalPerson = kacmanAM }, PositionName = "Голова"},
                                                          new CouncilMemberInfo(){ Member = new GenericPersonInfo(){ PersonType = EntityType.Legal, LegalPerson = ebensyCustdyLtd }, PositionName = "Член"},
                                                          new CouncilMemberInfo(){ Member = new GenericPersonInfo(){ PersonType = EntityType.Physical, PhysicalPerson = siehnert }, PositionName = "Член"}

                                                       }
                                                    ),
                                                HeadMemberIndex = 0
                                            };
            questio.IsExecutivesPresent = true;
            questio.Executives = new CouncilBodyInfo()
                                            {
                                                Members = new List<CouncilMemberInfo>
                                                          (
                                                               new CouncilMemberInfo[] 
                                                               { 
                                                                   new CouncilMemberInfo() { Member = new GenericPersonInfo() { PersonType = EntityType.Physical, PhysicalPerson = handrabura}, PositionName = "Генеральний директор" }, 
                                                                   new CouncilMemberInfo() { Member = new GenericPersonInfo() { PersonType = EntityType.Physical, PhysicalPerson = holovatyi }, PositionName = "Головний бухгалтер" }
                                                               }
                                                          ),
                                                HeadMemberIndex = 0
                                            };
            questio.TotalOwneshipPct = 24.5M;
            questio.TotalOwneshipDetails = new TotalOwnershipDetailsInfo()
                                               {
                                                   DirectOwnership = new OwnershipSummaryInfo() { Pct = 12.3M, Amount = 33000000, Votes = 52 },
                                                   ImplicitOwnership = new OwnershipSummaryInfo() { Pct = 10.2M, Amount = 30000000, Votes = 49 },
                                                   AcquiredVotes = new OwnershipVotesInfo() { Pct = 2, Votes = 8 },
                                                   TotalCapitalSharePct = 24.5M,
                                                   TotalVotes = 52 + 49 + 8
                                               };
            //questio.CommonOwnership = new CommonOwnershipInfo();
            //questio.ImplicitOwnership = new ImplicitOwnershipInfo();
            questio.BankExistingCommonImplicitOwners = new List<CommonOwnershipInfo>
                                                           (
                                                            new CommonOwnershipInfo[]
                                                            {
                                                                new CommonOwnershipInfo() 
                                                                { 
                                                                    Partners = new List<GenericPersonInfo>
                                                                                    (
                                                                                    new GenericPersonInfo[] 
                                                                                    {
                                                                                        new GenericPersonInfo()
                                                                                        {
                                                                                            PersonType = EntityType.Legal,
                                                                                            LegalPerson = new LegalPersonInfo()
                                                                                                              {
                                                                                                                  Address = mArnautska102, Name = "ТОВ \"Наяда\"", Registrar = reestrOdesa, RepresentedBy = kacmanAM, ResidenceCountry = countryUkraine, TaxCodeOrHandelsRegNr = "50245463"
                                                                                                              }
                                                                                        }
                                                                                    }
                                                                                    ),
                                                                                    OwnershipTestimony = "довіреність від 23.06.2001р.",
                                                                                    OwnershipPct = 99.98M, OwnershipType = CommonOwnershipType.Implicit
                                                                },
                                                                new CommonOwnershipInfo() 
                                                                { 
                                                                    Partners = new List<GenericPersonInfo>
                                                                                    (
                                                                                    new GenericPersonInfo[] 
                                                                                    {
                                                                                        new GenericPersonInfo()
                                                                                        {
                                                                                        PersonType = EntityType.Physical,
                                                                                        PhysicalPerson = handrabura
                                                                                        }
                                                                                    }),
                                                                    OwnershipPct = 0.02M, OwnershipType = CommonOwnershipType.Direct, OwnershipTestimony = ""
                                                                }
                                                            }
                                                           );
            questio.SharesAppliedFor = new List<PurchasedVoteInfo>(
                                      new PurchasedVoteInfo[] 
                                       {
                                           new PurchasedVoteInfo() { Transferror = new GenericPersonInfo() { PersonType = EntityType.Physical, LegalPerson = null, PhysicalPerson = holovatyi}, PurchaseVotes = new OwnershipVotesInfo() { Pct = 6.02M, Votes = 12}, VotesTransferPath = "довіреність від 03.12.2012"},
                                           new PurchasedVoteInfo() { Transferror = new GenericPersonInfo() { PersonType = EntityType.Legal, 
                                                                     PhysicalPerson = null, 
                                                                     LegalPerson = markManLtd }, 
                                                                     PurchaseVotes = new OwnershipVotesInfo() { Pct = 5.11M, Votes = 51}, VotesTransferPath = "Power of attorney dd 22.10.2010"},
                                       }
                                     );
            questio.SignificantSharesPhysicalPersons = new List<PhysicalPersonShareInfo>
                                                            (
                                                              new PhysicalPersonShareInfo[] 
                                                              {
                                                                  new PhysicalPersonShareInfo() { Person = handrabura, SharePct = 20M },
                                                                  new PhysicalPersonShareInfo() { Person = holovatyi, SharePct = 15M },
                                                                  new PhysicalPersonShareInfo() { Person = siehnert, SharePct = 14.5M }
                                                              }
                                                            );
            questio.SignificantSharesLegalPersons = new List<LegalPersonShareInfo>
                                                            (
                                                              new LegalPersonShareInfo[] 
                                                              {
                                                                  new LegalPersonShareInfo() { Person = markManLtd, SharePct = 12M },
                                                                  new LegalPersonShareInfo() { Person = sigmaHldngsLtd, SharePct = 11M },
                                                                  new LegalPersonShareInfo() { Person = ebensyCustdyLtd, SharePct = 10.5M }
                                                              }
                                                            );
            questio.AcquireeCommonImplicitOwners = new List<CommonOwnershipInfo>
                (
                    new CommonOwnershipInfo[]
                    {
                        new CommonOwnershipInfo()
                        {
                            Partners = new List<GenericPersonInfo>
                                (
                                   new GenericPersonInfo[]
                                   {
                                       new GenericPersonInfo() { PersonType = EntityType.Legal, LegalPerson = markManLtd },
                                       new GenericPersonInfo() { PersonType = EntityType.Physical, PhysicalPerson = siehnert}
                                   }
                                ),
                            OwnershipPct = 49.9M, OwnershipTestimony = "Угода про спільне володіння № 23/44 від 04.08.2007р.", OwnershipType = CommonOwnershipType.Direct
                        },
                        new CommonOwnershipInfo()
                        {
                            Partners = new List<GenericPersonInfo>
                                (
                                   new GenericPersonInfo[]
                                   {
                                       new GenericPersonInfo() { PersonType = EntityType.Legal, LegalPerson = sigmaHldngsLtd },
                                       new GenericPersonInfo() { PersonType = EntityType.Physical, PhysicalPerson = handrabura},
                                       new GenericPersonInfo() { PersonType = EntityType.Physical, PhysicalPerson = holovatyi},
                                       new GenericPersonInfo() { PersonType = EntityType.Legal, LegalPerson = ebensyCustdyLtd}
                                   }
                                ),
                            OwnershipPct = 49.9M, OwnershipTestimony = "Довіренність про представництво спільних інтересів від 11.08.2007р.", OwnershipType = CommonOwnershipType.Implicit
                        }
                    }
                );

            questio.Signatory = new SignatoryInfo() { DateSigned = (DateTime.Now - new TimeSpan(360, 0, 0)).Date, SignatoryPosition = "Директор", SurnameInitials = "Голопупенко І.Й." };
            questio.ContactPerson = new PhysicalPersonInfo();
            questio.ContactPhone = "(050)254-51-35";
            #endregion

            #endregion
            XmlSerializer serializer = new XmlSerializer(typeof(Appx2OwnershipStructLP));
            serializer.Serialize(File.Create(@"D:\home\vmdrot\HaErez\BGU\Var\SignificantOwnership\XMLs\Appx2OwnershipStructLP.sample.xml"), questio);
        }
    }
}
