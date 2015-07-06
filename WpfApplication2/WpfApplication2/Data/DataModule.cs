using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Utility;
using Evolvex.Utility.Core.Common;
using System;
using System.Collections.Generic;
using System.Data;
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

        private static List<BankInfo> _currentBanks;
        public static List<BankInfo> СurrentBanks
        {
            get
            {
                if (_currentBanks == null)
                {
                    _currentBanks = new List<BankInfo>();
                    DataTable dt = RcuKruReader.Read("RCUKRU.DBF");
                    foreach (DataRow dr in dt.Rows)
                    {
                        BankInfo bi = BankInfo.ParseFromRcuKruRow(dr);
                        if (bi == null)
                            continue;
                        _currentBanks.Add(bi);
                    }
                }
                return _currentBanks;
            }
        }

    }
}
