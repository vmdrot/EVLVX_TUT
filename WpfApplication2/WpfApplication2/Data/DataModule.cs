using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using Evolvex.Utility.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Data
{
    public class DataModule
    {
        private static readonly ILog log = Logging.GetLogger(typeof(DataModule));

        public static IQuestionnaire CurrentQuestionnare { get; set; }

        public static List<GenericPersonInfo> CurrentMentionedIdentities 
        {
            get
            {
                log.Debug("in get_CurrentMentionedIdentities ...");
                if (CurrentQuestionnare == null || !(CurrentQuestionnare is IGenericPersonsService))
                {
                    log.Debug("get_CurrentMentionedIdentities: CurrentQuestionnare == null || !(CurrentQuestionnare is IGenericPersonsService)");
                    return new List<GenericPersonInfo>();
                }
                return (List<GenericPersonInfo>)((IGenericPersonsService)CurrentQuestionnare).MentionedGenericPersons;
            }

        }

    }
}
