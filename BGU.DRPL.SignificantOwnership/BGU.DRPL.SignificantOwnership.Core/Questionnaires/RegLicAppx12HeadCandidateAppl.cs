using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using System.ComponentModel;
using BGU.DRPL.SignificantOwnership.Core.Spares;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;

namespace BGU.DRPL.SignificantOwnership.Core.Questionnaires
{
    /// <summary>
    /// АНКЕТА кандидатів на посади голови правління і головного бухгалтера банку, заступників голови та членів правління, заступників головного бухгалтера, голови, його заступників та членів спостережної (наглядової) ради банку, представників юридичної особи - члена наглядової (спостережної) ради, керівника підрозділу внутрішнього аудиту
    /// Додаток 12 до Положення про порядок реєстрації та ліцензування банків, відкриття відокремлених підрозділів
    /// file                                  : f364524n1221.doc
    /// Рівень складності                     : 9
    /// (оціночний, шкала від 1 до 10)
    /// Пріоритетність                        : Hi  (Легенда: Hi - Висока, Lo - Низька, Mid - Середня, Hold - Поки що притримати)
    /// Подавач анкети                        : PP (Легенда: LP - юр.особа, PP - фіз.особа, BK - банк)
    /// Чи заповнюватиметься он-лайн          : Так
    /// Первинну розробку структури завершено : Ні
    /// Структуру фіналізовано                : Ні
    /// (=остаточно узгоджено 
    /// з бізнес-користувачами)
    /// </summary>
    public class RegLicAppx12HeadCandidateAppl : IQuestionnaire
    {
        [DisplayName("у банку")]
        [Description("(повне офіційне найменування банку, у який призначається/рекомендується керівник)")]
        public BankInfo BankRef { get; set; }

        [DisplayName("Посада")]
        [Description("1. Посада, на яку призначається/рекомендується керівник")]
        public ManagementPosition PositionAppliedFor { get; set; }

        public GenericPersonID Candidate { get; set; }

        public List<EducationRecordInfo> Education { get; set; }

        public bool HasScientificTitle { get; set; }
        public string ScientificTitle { get; set; }


        public List<GenericPersonInfo> MentionedIdentities { get; set; }

        public string SuggestSaveAsFileName()
        {
            return "regLicDod12Kand";
        }
    }
}
