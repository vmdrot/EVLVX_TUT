﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using Evolvex.Utility.Core.ComponentModelEx;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.ComponentModel;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    /// <summary>
    /// АНКЕТА юридичної особи (у тому числі банку)
    /// Додаток 2 до Положення про порядок реєстрації та ліцензування банків, відкриття відокремлених підрозділів
    /// file                                  : f364524n1227.doc
    /// Рівень складності                     : 10
    /// (оціночний, шкала від 1 до 10)
    /// Пріоритетність                        : Hi  (Легенда: Hi - Висока, Lo - Низька, Mid - Середня, Hold - Поки що притримати)
    /// Подавач анкети                        : LP (Легенда: LP - юр.особа, PP - фіз.особа, BK - банк)
    /// Чи заповнюватиметься он-лайн          : Так
    /// Первинну розробку структури завершено : Ні
    /// Структуру фіналізовано                : Ні
    /// (=остаточно узгоджено 
    /// з бізнес-користувачами)
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.RegLicAppx2OwnershipAcqRequestLP_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class RegLicAppx2OwnershipAcqRequestLP : IQuestionnaire
    {

        #region cctor(s)
        public RegLicAppx2OwnershipAcqRequestLP()
        { 
            this.AccountsWithBanks = new List<BankInfo>();
            this.AccountsWithBanksToBePaidFrom = new List<BankInfo>();
            this.ExistingOwnershipWithBankDetails = new List<OwnershipStructure>();
            this.AppliedForOwnershipDetails = new List<OwnershipStructure>();
            this.TargetedOwnershipDetails = new List<OwnershipStructure>();
            this.FundsOrigin = new List<IncomeOriginInfo>();
            this.PaymentDeadLines = new List<PaymentDeadlineInfo>();
            this.LoansWithIssuingBank = new List<LoanInfo>();
            this.LoansWithOtherBank = new List<LoanInfo>();
            this.AcquireeOwnershipOtherThanBankRef = new List<OwnershipStructure>();
            this.AssociatedPersonsInOtherCouncils = new List<CouncilBodyInfo>();
            this.AssociatedOrOwnerWithBanks = new List<BankInfo>();
            this.AcquireeOwners = new List<OwnershipStructure>();
            this.BankruptcyInvestigations = new List<BankruptcyInvestigationInfo>();
            this.MiscNonRepaidDebts = new List<IndebtnessInfo>();
            this.BreachesOfLaw = new List<BreachOfLawRecordInfo>();
            this.LiquidatedSignOwnershipLastYear = new List<LiquidatedEntityOwnershipInfo>();
            this.MentionedIdentities = new List<GenericPersonInfo>();
            this.PersonsLinks = new List<PersonsAssociation>();
        }
        #endregion

        /// <summary>
        /// стосовно участі в ___________________________________
        /// (повне офіційне найменування банку)
        /// </summary>
        [DisplayName("Повне офіційне найменування банку")]
        [Description("стосовно участі в ...")]
        [Required]
        public BankInfo BankRef { get; set; }

        /// <summary>
        /// 1. Інформація про юридичну особу
        /// 
        /// 1.1. ____________________________________________________________________________.
        ///                                                                                                          (найменування юридичної особи)
        /// 
        /// 1.2. ____________________________________________________________________________.
        ///                                                                                                       (місцезнаходження юридичної особи)
        /// 
        /// 1.3. Код за ЄДРПОУ _____________________________________________________________.
        /// 1.4. Код платника податків ________________________________________________________.
        /// ---- 
        /// Самі реквізити мають потрапити до MentionedIdentities
        /// </summary>
        [DisplayName("Юр.особа-заявник")]
        [Description("1. Інформація про юридичну особу")]
        [Required]
        public GenericPersonID Acquiree { get; set; }

        /// <summary>
        /// 1.5. Рейтингова оцінка ____________________________________________________________.
        /// (за рейтингом, який підтверджений у бюлетені, однієї  з провідних рейтингових компаній IBCA,
        ///  Standart & Poor's, Moody's)
        /// </summary>
        [DisplayName("1.5. Рейтингова оцінка")]
        [Description("(за рейтингом, який підтверджений у бюлетені, однієї  з провідних рейтингових компаній IBCA,Standart & Poor's, Moody's)")]
        [Required]
        public CreditRatingInfo CreditRatingGrade { get; set; }

        /// <summary>
        /// 1.6. Основний вид діяльності юридичної особи _______________________________________.
        /// </summary>
        [DisplayName("1.6. Основний вид діяльності юридичної особи")]
        [Description("1.6. Основний вид діяльності юридичної особи")]
        [Required]
        public EconomicActivityType AcquireeCoreActivity { get; set; }

        /// <summary>
        /// 1.7. Повні  офіційні  найменування банків,  у яких юридична особа має рахунки (не заповнюється банками), 
        /// __________________________________________________________.
        /// ------
        /// Поле обов'язкове, лише якщо заявник не є банком.
        /// </summary>
        [DisplayName("1.7. Повні  офіційні  найменування банків,  у яких юридична особа має рахунки")]
        [Description("(не заповнюється банками)")]
        public List<BankInfo> AccountsWithBanks { get; set; }

        /// <summary>
        /// 1.8. Повні  офіційні найменування банків,  які надають інформацію про стан рахунків юридичної особи, з яких має здійснюватися  оплата за акції/паї (не заповнюється банками), ________________________________________________________________________________
        /// _______________________________________________________________________________.
        /// ------
        /// Поле обов'язкове, лише якщо заявник не є банком.
        /// </summary>
        [DisplayName("1.8 Банки, з яких має здійснюватися  оплата за акції/паї")]
        [Description("1.8. Повні  офіційні найменування банків,  які надають інформацію про стан рахунків юридичної особи, з яких має здійснюватися  оплата за акції/паї (не заповнюється банками)")]
        public List<BankInfo> AccountsWithBanksToBePaidFrom { get; set; }
        /// <summary>
        /// Чекбокс, щоб вказати, що її не існує (не передбачено), цього органу
        /// </summary>
        [Browsable(true)]
        [DisplayName("Наглядова (спостережна) рада юридичної особи?")]
        [Description("1.9. Голова та  члени наглядової (спостережної) ради юридичної  особи - Чи існує наглядова (спостережна) рада юридичної особи?")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Required]
        public bool IsSupervisoryCouncilPresent { get; set; }
        /// <summary>
        /// 1.9. Голова та  члени наглядової (спостережної) ради юридичної  особи _______________________________________________________________________________.
        ///                                                                                           (прізвища, імена, по батькові)
        /// ------
        /// Поле обов'язкове, тільки якщо IsSupervisoryCouncilPresent == true
        /// </summary>
        [Browsable(true)]
        [DisplayName("1.9. Особовий склад наглядової ради")]
        [Description("1.9. Голова та  члени наглядової (спостережної) ради юридичної  особи")]
        public CouncilBodyInfo SupervisoryCouncil { get; set; }

        /// <summary>
        /// 1.10. Голова та члени виконавчого органу юридичної особи
        /// Чекбокс, щоб вказати, що її не існує (не передбачено), цього органу
        /// </summary>
        [Browsable(true)]
        [DisplayName("1.4. Виконавчий орган існує?")]
        [Description("1.4. Чи існує виконавчий орган юридичної особи")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Required]
        public bool IsExecutivesPresent { get; set; }
        /// <summary>
        /// 
        /// 1.10. Голова та члени виконавчого органу юридичної особи ____________________________
        /// _______________________________________________________________________________.
        ///            (прізвища, імена, по батькові)
        /// </summary>
        [Browsable(true)]
        [DisplayName("1.10. Особовий склад виконавчого органу")]
        [Description("1.10. Голова та члени виконавчого органу юридичної особи")]
        [Required("IsExecutivesPresent == true")]
        public CouncilBodyInfo Executives { get; set; }
        
        /// <summary>
        /// 1.11. Державний орган, що здійснює контроль (нагляд) за діяльністю юридичної особи,
        /// _______________________________________________________________________________.
        ///            (найменування органу)
        /// </summary>
        [Browsable(true)]
        [DisplayName("1.11. Державний наглядовий орган")]
        [Description("1.11. Державний орган, що здійснює контроль (нагляд) за діяльністю юридичної особи ???!")]
        public FinancialOversightAuthorityInfo StateRegulatorAuthority { get; set; } //todo

        /// <summary>
        /// 1.12. Державний  орган,  який здійснив реєстрацію юридичної особи  (для іноземців),
        /// _______________________________________________________________________________.
        ///  (найменування органу, країна місцезнаходження)
        /// </summary>
        [DisplayName("1.12. Державний  орган,  який здійснив реєстрацію юридичної особи  (для іноземців)")]
        [Description("1.12. Державний  орган,  який здійснив реєстрацію юридичної особи  (для іноземців) (найменування органу, країна місцезнаходження)")]
        [Required("Acquiree.ResidenceCountry != UKRAINE")]
        public RegistrarAuthority AcquireeRegistrar { get; set; }



        /// <summary>
        /// 2. Відносини з банком-емітентом
        /// </summary>
        [DisplayName("2.1. Це первинне розміщення?")]
        [Description("2.1. Подається заявка на придбання частки істотної участі в рамках первинного розміщення акцій банку?")]
        [Required]
        public bool IsIPO { get; set; }
      
        /// <summary>
        /// 2.1. Найменування, номер і дата офіційного видання, у якому опубліковано оголошення про емісію (у разі первинного розміщення), _____________________________________________.
        /// </summary>
        [DisplayName("2.1. Публікація про первинне розміщення")]
        [Description("2.1. Найменування, номер і дата офіційного видання, у якому опубліковано оголошення про емісію (у разі первинного розміщення)")]
        [Required("IsIPO == true")]
        public PublicationInfo IPOPublication { get; set; }

        /// <summary>
        /// 2.2. Андеррайтер (за наявності)
        /// ____________________________________________________.
        ///           (найменування, код за ЄДРПОУ)
        /// </summary>
        [DisplayName("2.2. Андеррайтер (за наявності)")]
        [Description("Найменування, код за ЄДРПОУ андеррайтера")]
        public GenericPersonID Underwriter { get; set; }
        
        /// <summary>
        /// 2.3. Наявна участь у банку:
        /// а) пряме володіння - _______ відсотків статутного капіталу банку  в розмірі _______ гривень, що становить __________ акцій (паїв), загальною номінальною вартістю _________ гривень;
        ///                                            (кількість)
        /// </summary>
        [DisplayName("2.3. а) Наявна участь у банку - пряме володіння")]
        [Description("а) пряме володіння - _______ відсотків статутного капіталу банку  в розмірі _______ гривень, що становить __________ акцій (паїв), загальною номінальною вартістю _________ гривень")]
        [Required]
        public SharesAcquisitionInfo ExistingDirectOwnershipWithBankSummary { get; set; } //todo - check type
        
        /// <summary>
        /// 2.3. Наявна участь у банку:
        /// б) опосередковане володіння - _____ відсотків 
        /// </summary>
        [DisplayName("2.3. а) Наявна участь у банку - опосередковане володіння")]
        [Description("б) опосередковане володіння - _____ відсотків")]
        [Required]
        public SharesAcquisitionInfo ExistingImplicitOwnershipWithBankSummary { get; set; } //todo - check type
        
        /// <summary>
        /// б) опосередковане володіння - _____ відсотків у розподілі голосів
        /// через _______________________________________________________________________________.
        ///       (найменування учасника (учасників) банку та розмір його (їх) володіння в банку)
        /// --------------
        /// Розшифровка ExistingDirectOwnershipWithBankSummary і ExistingImplicitOwnershipWithBankSummary
        /// </summary>
        [DisplayName("2.3. Наявна участь у банку - розшифровка")]
        [Description("б) опосередковане володіння - _____ відсотків у розподілі голосів через __(найменування учасника (учасників) банку та розмір його (їх) володіння в банку)")]
        [Required("ExistingImplicitOwnershipWithBankSummary > 0 OR ExistingDirectOwnershipWithBankSummary > 0")]
        public List<OwnershipStructure> ExistingOwnershipWithBankDetails { get; set; }

        /// <summary>
        /// 2.4. Наміри щодо набуття/збільшення істотної участі в банку:
        /// а) пряме володіння - _______ відсотків статутного капіталу банку в розмірі _______ гривень, що становить ________ акцій (паїв), загальною номінальною вартістю ___________ гривень;
        ///                                       (кількість)
        /// </summary>
        [DisplayName("2.4. а) Наміри щодо набуття/збільшення істотної участі в банку - пряме володіння")]
        [Description("а) пряме володіння - ____ відсотків статутного капіталу банку в розмірі _____ гривень, що становить ____ акцій (паїв), загальною номінальною вартістю ___ гривень")]
        [Required]
        public SharesAcquisitionInfo AppliedForDirectOwnershipSummary { get; set; } //todo - check type

        /// <summary>
        /// 2.4. Наміри щодо набуття/збільшення істотної участі в банку:
        /// б) опосередковане володіння - _____ відсотків у розподілі голосів  через* _______________________________________________________________________________.
        ///                                (найменування учасника (учасників) банку та розмір його (їх) володіння в банку)
        /// </summary>
        [DisplayName("2.4. б) Наміри щодо набуття/збільшення істотної участі в банку - опосередковане володіння")]
        [Description("б) опосередковане володіння - _____ відсотків у розподілі голосів")]
        [Required]
        public SharesAcquisitionInfo AppliedForImplicitOwnershipSummary { get; set; } //todo - check type

        /// <summary>
        /// 2.4. Наміри щодо набуття/збільшення істотної участі в банку:
        ///  - у розподілі голосів  через* _______________________________________________________________________________.
        ///                                (найменування учасника (учасників) банку та розмір його (їх) володіння в банку)
        /// ---------------------
        /// Розшифровка AppliedForImplicitOwnershipSummary
        /// </summary>
        [DisplayName("2.4. Наміри щодо набуття/збільшення істотної участі в банку - розшифровка")]
        [Description("Наміри щодо набуття/збільшення істотної участі в банку у розподілі голосів  через* __ (найменування учасника (учасників) банку та розмір його (їх) володіння в банку)")]
        [Required("AppliedForDirectOwnershipSummary > 0 OR AppliedForImplicitOwnershipSummary > 0")]
        public List<OwnershipStructure> AppliedForOwnershipDetails { get; set; }

        /// <summary>
        /// 2.5. Майбутня   істотна  участь  з  урахуванням  кількості  акцій  (паїв), 
        /// які  юридична  особа  має  намір  придбати,   становитиме  _______________ відсотків  
        /// статутного  капіталу  банку  в розмірі  __________________________ гривень і складатиметься із:
        /// а) прямого володіння в розмірі ___ відсотків статутного  капіталу  банку 
        /// в сумі ____ гривень, що становитиме ________ акцій (паїв), загальною номінальною вартістю _________ гривень;
        ///                                   (кількість)
        /// </summary>
        [DisplayName("2.5. а) Майбутня   істотна  участь - пряме володіння")]
        [Description("Майбутня   істотна  участь  з  урахуванням  кількості  акцій  (паїв) ..., складатиметься із:а) прямого володіння в розмірі ___ відсотків статутного  капіталу  банку в сумі ____ гривень, що становитиме ________ акцій (паїв), загальною номінальною вартістю _________ гривень")]
        [Required]
        public SharesAcquisitionInfo TargetedDirectOwnershipSummary { get; set; } //todo - check type

        /// <summary>
        /// 2.5. Майбутня   істотна  участь  з  урахуванням  кількості  акцій  (паїв),складатиметься із
        /// б) опосередкованого володіння - ___ відсотків у розподілі голосів  
        /// </summary>
        [DisplayName("2.5. б) Майбутня   істотна  участь - опосередковане володіння")]
        [Description("2.5. Майбутня   істотна  участь  з  урахуванням  кількості  акцій  (паїв),складатиметься із ... б) опосередкованого володіння - ___ відсотків у розподілі голосів")]
        [Required]
        public SharesAcquisitionInfo TargetedImplicitOwnershipSummary { get; set; } //todo - check type

        /// <summary>
        /// 2.5. Майбутня   істотна  участь  з  урахуванням  кількості  акцій  (паїв),складатиметься із 
        /// володіння ... через
        /// _______________________________________________________________________________.
        ///  (найменування учасника (учасників) банку та розмір його (їх) володіння в банку)
        /// ------------------
        /// Розшифровка TargetedImplicitOwnershipSummary 
        /// </summary>
        [DisplayName("2.5. Майбутня   істотна  участь - розшифровка")]
        [Description("володіння ... через _____ (найменування учасника (учасників) банку та розмір його (їх) володіння в банку)")]
        [Required("TargetedDirectOwnershipSummary > 0 OR TargetedImplicitOwnershipSummary > 0")]
        public List<OwnershipStructure> TargetedOwnershipDetails { get; set; }

        /// <summary>
        /// 3. Інформація про оплату
        /// 
        /// 3.1. Кількість  акцій,  які  юридична  особа  має намір придбати,  ___ одиниць, номінальна вартість однієї акції (паю) ____ гривень,  загальна сума ________ гривень, фактична вартість придбаних акцій  (паїв) ___________ гривень, у тому числі кількість акцій з правом  голосу _______________________________________________________________________________.
        /// </summary>
        [DisplayName("3.1. Кількість  акцій,  які  юридична  особа  має намір придбати...")]
        [Description("3.1. Кількість  акцій,  які  юридична  особа  має намір придбати,  ___ одиниць, номінальна вартість однієї акції (паю) ____ гривень,  загальна сума ________ гривень, фактична вартість придбаних акцій  (паїв) ___________ гривень, у тому числі кількість акцій з правом  голосу ______.")]
        [Required]
        public SharesAcquisitionInfo SharesToBeBought { get; set; }

        /// <summary>
        /// 3.2. Джерела  походження коштів юридичної особи,  за рахунок 
        /// яких  здійснюється сплата за акції (паї), ____________________________.
        ///                                     (прибуток, резервний капітал тощо)
        /// </summary>
        [DisplayName("3.2. Джерела  походження коштів юридичної особи")]
        [Description("3.2. Джерела  походження коштів юридичної особи,  за рахунок яких  здійснюється сплата за акції (паї), ______.(прибуток, резервний капітал тощо)")]
        [Required]
        public List<IncomeOriginInfo> FundsOrigin { get; set; }

        /// <summary>
        /// 3.3. Терміни сплати ______________________________________________________________.
        ///                      (одноразово, частинами, із зазначенням терміну, протягом якого здійснюватиметься сплата)
        /// </summary>
        [DisplayName("3.3. Терміни сплати")]
        [Description("(одноразово, частинами, із зазначенням терміну, протягом якого здійснюватиметься сплата)")]
        [Required]
        public List<PaymentDeadlineInfo> PaymentDeadLines { get; set; }

        /// <summary>
        /// 3.4. Інформація   про   кредити,   одержані  юридичною  особою  в  банку-емітенті,
        /// </summary>
        [DisplayName("3.4. Чи є кредити, одержані  юридичною  особою  в  банку-емітенті")]
        [Description("(Станом на дату подання анкети)")]
        [Required]
        public bool HasLoansWithIssuingBank { get; set; }

        /// <summary>
        /// 3.4. Інформація   про   кредити,   одержані  юридичною  особою  в  банку-емітенті,
        /// ________________________________________________________________________________
        ///  (номер і дата договору про надання кредиту, сума кредиту, термін
        /// _______________________________________________________________________________.
        ///  погашення кредиту, сума заборгованості за договором на дату подання анкети)
        /// </summary>
        [DisplayName("3.4. Інформація   про   кредити,   одержані  юридичною  особою  в  банку-емітенті")]
        [Description("(номер і дата договору про надання кредиту, сума кредиту, термін погашення кредиту, сума заборгованості за договором на дату подання анкети)")]
        [Required("HasLoansWithIssuingBank == true")]
        public List<LoanInfo> LoansWithIssuingBank { get; set; }

        [DisplayName("3.5. Чи є кредити, одержані в інших  банках")]
        [Description("(Станом на дату подання анкети)")]
        [Required]
        public bool HasLoansWithOtherBank { get; set; }

        /// <summary>
        /// 3.5. Інформація про кредити, що одержані юридичною особою в інших  банках (не заповнюється банками),
        ///  ___________________________________________________________
        ///     (номер і дата договору про надання кредиту, сума
        /// 
        /// _______________________________________________________________________________.
        ///     кредиту, строк погашення кредиту, сума заборгованості  за договором на дату подання анкети)
        /// 3.6. Інформація  про  стан  виконання зобов'язань щодо повернення  кредитів, одержаних у банках, 
        ///  _________________________________________________________________________
        ///   (згідно з умовами договору або з порушенням умов договору.
        /// 
        /// ________________________________________________________________________________
        ///   Якщо порушено умови договору, то зазначити номер договору, суму, строки, які умови порушені, 
        ///     причини, залишок заборгованості на день заповнення анкети)
        /// 
        /// </summary>
        [DisplayName("3.5., 3.6.  Інформація про кредити в інших  банках та стан їх виконання")]
        [Description("3.5. Інформація про кредити, що одержані юридичною особою в інших  банках (не заповнюється банками)\n3.6. Інформація  про  стан  виконання зобов'язань щодо повернення  кредитів, одержаних у банках")]
        [Required("HasLoansWithOtherBank == true")]
        public List<LoanInfo> LoansWithOtherBank { get; set; }


        /// <summary>
        /// 4. Відносини власності юридичної особи із суб'єктами господарювання, 
        /// у тому числі іншими банками (крім банку, у який планується
        /// здійснення інвестиції)
        /// </summary>
        [DisplayName("4. Чи перебуває заявник у відносинах власності з іншими суб'єктами господарювання")]
        [Description("4. Чи існують відносини власності юридичної особи із суб'єктами господарювання, у тому числі іншими банками (крім банку, у який планується здійснення інвестиції)")]
        [Required]
        public bool HasAcquireeOwnershipOtherThanBankRef { get; set; }

        /// <summary>
        /// 4. Відносини власності юридичної особи із суб'єктами господарювання, 
        /// у тому числі іншими банками (крім банку, у який планується
        /// здійснення інвестиції)
        /// 4.1. Найменування юридичної особи, код за ЄДРПОУ _________________________________
        /// _______________________________________________________________________________.
        /// 4.2. Місцезнаходження ___________________________________________________________.
        /// 4.3. Основний вид діяльності юридичної особи _______________________________________.
        /// 4.4. Відсоток власності в юридичній особі ___________________________________________.
        /// </summary>
        [DisplayName("4. Відносини власності заявника з іншими суб'єктами господарювання")]
        [Description("4. Відносини власності юридичної особи із суб'єктами господарювання, у тому числі іншими банками (крім банку, у який планується здійснення інвестиції)")]
        [Required("HasAcquireeOwnershipOtherThanBankRef == true")]
        public List<OwnershipStructure> AcquireeOwnershipOtherThanBankRef { get; set; }

        /// <summary>
        /// 4.5. Чи  є  працівники  або  довірена  особа  юридичної  особи  в наглядовому органі цієї юридичної особи?
        /// _______________________________________________________________.
        /// </summary>
        [DisplayName("4.5. Працівники  або  довірена  особа  юридичної  особи  в наглядовому органі юридичної особи")]
        [Description("4.5. Чи  є  працівники  або  довірена  особа  юридичної  особи  в наглядовому органі цієї юридичної особи?")]
        [Required("HasAcquireeOwnershipOtherThanBankRef == true")]
        public List<CouncilBodyInfo> AssociatedPersonsInOtherCouncils { get; set; } //todo - ask Bevz



        /// <summary>
        /// 4.7. Перелік  банків,  у яких юридична особа є власником істотної  участі або 
        /// афілійованою особою
        /// </summary>
        [DisplayName("4.7. Власник істотної  участі або афілійована особа у банках?")]
        [Description("Чи існують банки,  у яких юридична особа є власником істотної  участі або афілійованою особою?")]
        [Required]
        public bool IsAssociatedOrOwnerWithBanks { get; set; }
        /// <summary>
        /// 4.6. Чи представляє юридична особа інтереси інших суб'єктів господарювання? 
        ///  _______________________________________________________________________________.
        ///         (якщо так, то зазначити, якого саме, з яких питань)
        // див. PersonsLinks
        /// 4.7. Перелік  банків,  у яких юридична особа є власником істотної  участі або 
        /// афілійованою особою, ________________________________________________________________________.
        ///                          (зазначити конкретно, якою особою, у якому банку)
        /// </summary>
        [DisplayName("4.7. Перелік  банків істотної участі або афілійованих")]
        [Description("Перелік  банків,  у яких юридична особа є власником істотної  участі або афілійованою особою")]
        [Required("IsAssociatedOrOwnerWithBanks == true")]
        public List<BankInfo> AssociatedOrOwnerWithBanks { get; set; }

        /// <summary>
        /// 5. Загальна інформація
        /// 
        /// 5.1. Власники істотної участі в юридичній особі:
        /// юридична особа _________________________________________________________________
        ///                  (найменування, код за ЄДРПОУ,
        /// _______________________________________________________________________________;
        ///                  місцезнаходження, відсоток участі)
        /// 
        /// фізична особа ___________________________________________________________________
        ///                  [прізвище, ім'я, по батькові, паспортні дані,
        /// 
        /// _______________________________________________________________________________.
        ///  реєстраційний номер облікової картки платника податків або серія та номер паспорта
        ///  (для фізичних осіб, які через свої релігійні переконання відмовляються  від прийняття реєстраційного
        ///  номера облікової картки платника  податків та повідомили про це відповідний контролюючий орган 
        ///  і мають відмітку у паспорті),  відсоток участі]
        /// </summary>
        [DisplayName("5.1. Власники істотної участі в юридичній особі")]
        [Description("Ланцюжок власників істотної участі в юридичній особі")]
        [Required]
        public List<OwnershipStructure> AcquireeOwners { get; set; }

        /// <summary>
        /// 6. Інші питання
        /// (крім банків)
        /// 
        /// 6.1. Чи порушувалася справа про банкрутство юридичної особи? _______________________.
        /// </summary>
        [DisplayName("6.1. Чи порушувалася справа про банкрутство юридичної особи?")]
        [Description("6.1. Чи порушувалася справа про банкрутство юридичної особи?")]
        [Required]
        public bool HadBankruptcyInvestigation { get; set; } //todo - question - resolved (thanks Yaretskyi)

        /// <summary>
        /// 6.1. Деталі порушуваної(-их) справи(справ) про банкрутство юридичної особи
        /// </summary>
        [DisplayName("6.1. Деталі порушуваної(-их) справи(справ) про банкрутство юридичної особи")]
        [Description("")]
        [Required("HadBankruptcyInvestigation == true")]
        public List<BankruptcyInvestigationInfo> BankruptcyInvestigations { get; set; }

        /// <summary>
        /// 6.2. Чи  має   юридична  особа  невиконані  майнові  (фінансові)  зобов'язання перед іншими особами?
        /// _______________________________________________________________________
        /// </summary>
        [DisplayName("6.2. Є невиконані  зобов'язання перед іншими особами?")]
        [Description("6.2. Чи  має   юридична  особа  невиконані  майнові  (фінансові)  зобов'язання перед іншими особами?")]
        [Required]
        public bool HasMiscNonRepaidDebts { get; set; }

        /// <summary>
        /// 6.2. Чи  має   юридична  особа  невиконані  майнові  (фінансові)  зобов'язання перед іншими особами?
        /// ________________________________________________________________________________
        ///  (якщо так, то зазначити, які саме зобов'язання, у якому розмірі,
        /// 
        /// _______________________________________________________________________________.
        ///  перед якою особою та з яких причин не були виконані, а також подальші плани
        ///   щодо виконання/невиконання цих зобов'язань)
        /// 
        /// </summary>
        [DisplayName("6.2. Невиконані  зобов'язання перед іншими особами - розшифровка")]
        [Description("(зазначити, які саме зобов'язання, у якому розмірі, перед якою особою та з яких причин не були виконані, а також подальші плани щодо виконання/невиконання цих зобов'язань)")]
        [Required("HasMiscNonRepaidDebts == true")]
        public List<IndebtnessInfo> MiscNonRepaidDebts { get; set; }

        /// <summary>
        /// 6.3. Чи  притягувалася  юридична  особа  до  відповідальності  за  порушення антимонопольного,  податкового, банківського, валютного  законодавства, правил діяльності на ринку цінних паперів тощо?
        /// </summary>
        [DisplayName("6.3. Були порушення галузевого законодавства?")]
        [Description("6.3. Чи  притягувалася  юридична  особа  до  відповідальності  за  порушення антимонопольного,  податкового, банківського, валютного  законодавства, правил діяльності на ринку цінних паперів тощо?")]
        [Required]
        public bool HadIndustrySpecificBreaches { get; set; }

        /// <summary>
        /// 6.3. ________________________________________________________________________________
        ///       (зазначити, коли вчинено порушення, зміст порушення, накладені санкції)
        /// </summary>
        [DisplayName("6.3. Порушення галузевого законодавства - деталі")]
        [Description("(зазначити, коли вчинено порушення антимонопольного,  податкового, банківського, валютного  законодавства, правил діяльності на ринку цінних паперів тощо, зміст порушення, накладені санкції)")]
        [Required("HadIndustrySpecificBreaches == true")]
        public List<BreachOfLawRecordInfo> BreachesOfLaw { get; set; }

        /// <summary>
        /// 6.4. Чи була юридична особа протягом останнього року, що передував прийняттю рішення про ліквідацію юридичної особи,  
        /// власником істотної участі (10 і більше відсотків) у цій особі? 
        /// </summary>
        [DisplayName("6.4. Були власником ліквідованих юросіб протягом останнього року?")]
        [Description("6.4. Чи була юридична особа протягом останнього року, що передував прийняттю рішення про ліквідацію юридичної особи, власником істотної участі (10 і більше відсотків) у цій особі?")]
        [Required]
        public bool HasLiquidatedSignOwnershipLastYear { get; set; }

        /// <summary>
        /// 6.4. __________________________________________________________________________
        ///  (найменування юридичної особи,
        /// 
        /// _______________________________________________________________________________.
        ///  код за ЄДРПОУ, відсоток володіння в ній, докладна інформація про причини та підстави ліквідації)
        /// </summary>
        [DisplayName("6.4. Ліквідовані протягом останнього року юрособи у власності")]
        [Description("6.4. Протягом останнього року, що передував прийняттю рішення про ліквідацію юридичної особи, власник істотної участі (10 і більше відсотків) у ... (найменування юридичної особи, код за ЄДРПОУ, відсоток володіння в ній, докладна інформація про причини та підстави ліквідації)")]
        [Required("HasLiquidatedSignOwnershipLastYear == true")]
        public List<LiquidatedEntityOwnershipInfo> LiquidatedSignOwnershipLastYear { get; set; }

        /// <summary>
        /// 6.5. Стверджую, що юридична особа належним чином виконує вимоги законодавства України з питань запобігання та протидії легалізації (відмиванню) доходів, одержаних злочинним шляхом, або фінансуванню тероризму та до юридичної особи (або власників істотної участі в юридичній особі) не застосовувалися міжнародні санкції.
        /// </summary>
        [DisplayName("6.5. Дотримується законів щодо відмивання грошей та фінансування тероризму...?")]
        [Description("6.5. Стверджую, що юридична особа належним чином виконує вимоги законодавства України з питань запобігання та протидії легалізації (відмиванню) доходів, одержаних злочинним шляхом, або фінансуванню тероризму та до юридичної особи (або власників істотної участі в юридичній особі) не застосовувалися міжнародні санкції.")]
        [Required]
        public bool IsMoneyLaunderingEtcLawsKept { get; set; }


        /// <summary>
        /// Стверджую, що інформація, надана в анкеті, є правдивою і повною, та не заперечую проти перевірки Національним банком України достовірності поданих документів і персональних даних, що в них містяться.
        /// </summary>
        [DisplayName("Підтверджую правдивість інформації?")]
        [Description("Я, (прізвище, ім'я, по батькові) стверджую, що інформація,  надана в анкеті,\n є правдивою і повною, та не заперечую проти перевірки Національним банком України достовірності поданих документів і персональних даних, що в них містяться.\n")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Required]
        public bool IsApplicationInfoAccurateAndTrue { get; set; }

        /// <summary>
        /// У разі будь-яких змін в інформації, що зазначена в цій анкеті,  зобов'язуюся повідомити про них Національний банк України  протягом 10-ти днів з дня їх виникнення.
        /// </summary>
        [DisplayName("Зобов'язуюсь повідомляти про зміни?")]
        [Description("У разі будь-яких змін в інформації, що зазначена в цій анкеті, зобов'язуюся повідомити про них Національний банк України протягом 10-ти днів з дня їх виникнення.")]
        [Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.BooleanEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Required]
        public bool AmObligingToKeepUp2DateWithin10Days { get; set; }

        /// <summary>
        /// _________________________
        /// (посада керівника юридичної
        /// особи - заявника, який підписав анкету
        /// і відповідальний за інформацію,
        /// що наведена в ній)
        /// 
        /// (підпис керівника)
        /// 
        /// 
        /// М.П.
        /// 
        /// (ініціали та прізвище
        /// керівника
        /// друкованими літерами)
        /// 
        /// 
        /// Підписано ______________________
        ///            (число, місяць, рік)
        /// </summary>
        [DisplayName("Підписант")]
        [Description("Відомості по особу, що підписала анкету")]
        [Required]
        public SignatoryInfo Signatory { get; set; }
        
        /// <summary>
        /// Прізвище, ім'я, по батькові контактної особи ___________________.
        /// 
        /// Номер телефону та факсу ________________________________________.
        /// </summary>
        [DisplayName("Контакти")]
        [Description("Контактні дані відправника анкети")]
        [Required]
        public ContactInfo ContactPerson { get; set; }

        /// <summary>
        /// Реквізити осіб-фігурантів анкети
        /// </summary>
        [DisplayName("")]
        [Description("")]
        [Required("")]
        public List<GenericPersonInfo> MentionedIdentities { get; set; }

        /// <summary>
        /// Зв'язки між фігурантами анкети
        /// </summary>
        [DisplayName("")]
        [Description("")]
        [Required("")]
        public List<PersonsAssociation> PersonsLinks { get; set; }

        public string SuggestSaveAsFileName()
        {
            return "regLicDod2IstUchYO";
        }
    }
}
