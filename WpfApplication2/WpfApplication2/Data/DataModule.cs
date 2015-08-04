using BGU.DRPL.SignificantOwnership.Core.EKDRBU.Legacy;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using BGU.DRPL.SignificantOwnership.Core.Spares.Dict;
using BGU.DRPL.SignificantOwnership.Utility;
using Evolvex.Utility.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
                    DataTable dt = RcuKruReader.Filter(RcuKruReader.Read("RCUKRU.DBF"), true, new List<string>("1,2,5,6".Split(',')));
                    
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


        public static void RefreshMentionedIdentitiesDispNames(object newDS)
        {
            if (!(newDS is IGenericPersonsService))
                return;
            ((IGenericPersonsService)newDS).RefreshGenericPersonsDisplayNames();
        }

        public static List<DeptListEntry> _HierarchedBankDepts;
        public static IEnumerable HierarchedBankDepts
        {
            get
            { 
                if(_HierarchedBankDepts == null)
                {
                    _HierarchedBankDepts = new List<DeptListEntry>();

                    DataTable dt = RcuKruReader.Read(@"DPTLIST.DBF");
                    foreach (DataRow dr in dt.Rows)
                    {
                        _HierarchedBankDepts.Add(DeptListEntry.Parse(dr));
                    }

                    BGU.DRPL.SignificantOwnership.Facade.EKDRBU.DeptListUtil.BuildHierarchy(_HierarchedBankDepts);
                }
                var rslt = _HierarchedBankDepts.Where(dle => (dle.ParentCode == string.Empty));
                return (IEnumerable)rslt;
            }
        }   

    }
}
