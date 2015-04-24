using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using Evolvex.Utility.Core.ComponentModelEx;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    /// <summary>
    /// ПОВІДОМЛЕННЯ про наміри набуття/збільшення істотної участі в банку
    /// Додаток 7 до Положення про порядок реєстрації та ліцензування банків, відкриття відокремлених підрозділів
    /// file                                  : f364524n1027.doc
    /// Рівень складності                     : 5
    /// (оціночний, шкала від 1 до 10)
    /// Пріоритетність                        : Lo  (Легенда: Hi - Висока, Lo - Низька, Mid - Середня, Hold - Поки що притримати)
    /// Подавач анкети                        : LP;PP (Легенда: LP - юр.особа, PP - фіз.особа, BK - банк)
    /// Чи заповнюватиметься он-лайн          : Так
    /// Первинну розробку структури завершено : Ні
    /// Структуру фіналізовано                : Ні
    /// (=остаточно узгоджено 
    /// з бізнес-користувачами)
    /// </summary>
    [System.ComponentModel.Editor(typeof(BGU.DRPL.SignificantOwnership.Core.TypeEditors.RegLicAppx7ShareAcqIntent_Editor), typeof(System.Drawing.Design.UITypeEditor))]
    public class RegLicAppx7ShareAcqIntent : IQuestionnaire
    {
        public RegLicAppx7ShareAcqIntent()
        {
            ExistingOwnership = new List<OwnershipStructure>();
            MentionedIdentities = new List<GenericPersonInfo>();
            PersonsLinks = new List<PersonsAssociation>();
        }

        [Required]
        public BankInfo BankRef { get; set; }
        [Required]
        public GenericPersonInfo Acquiree { get; set; }
        [Required]
        public TotalOwnershipDetailsInfo ExistingOwnershipSummary { get; set; }
        [Required]
        public List<OwnershipStructure> ExistingOwnership { get; set; }
        [Required]
        public List<GenericPersonInfo> MentionedIdentities { get; set; }
        public List<PersonsAssociation> PersonsLinks { get; set; }
        [Required]
        public TotalOwnershipDetailsInfo TargetedOwnershipSummary { get; set; }

        //public decimal TargetedOwnershipSharePct { get; set; }
        //public decimal TargetedOwnershipShareAmt { get; set; }
        [Required]
        public SignatoryInfo Signee { get; set; }


        public string SuggestSaveAsFileName()
        {
            throw new NotImplementedException();
        }
    }
}
