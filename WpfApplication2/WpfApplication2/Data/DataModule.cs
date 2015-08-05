using BGU.DRPL.SignificantOwnership.Core.EKDRBU.Legacy;
using BGU.DRPL.SignificantOwnership.Core.Questionnaires;
using BGU.DRPL.SignificantOwnership.Core.Spares;
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
    public class DataModule : NotifyPropertyChangedBase
    {
        private static readonly ILog log = Logging.GetLogger(typeof(DataModule));

        //private static DataModule _instance;

        //private DataModule() { }

        //public static DataModule Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new DataModule();
        //        return _instance;
        //    }
        //}


        private static IQuestionnaire _CurrentQuestionnare;
        public static IQuestionnaire CurrentQuestionnare 
        {
            get { return _CurrentQuestionnare; }
            set { _CurrentQuestionnare = value; /* OnPropertyChanged("CurrentQuestionnare"); OnPropertyChanged("CurrentMentionedIdentities"); */ } 
        }


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

        private static void PopulatedHierachedBankDepts()
        { 
            _HierarchedBankDepts = new List<DeptListEntry>();


            string bkCode = SelectedBank != null ? SelectedBank.Code : string.Empty;
            if(!string.IsNullOrEmpty(bkCode) && bkCode.Length <3 )
            {
                if (bkCode.Length == 1)
                    bkCode = "00" + bkCode;
                else if(bkCode.Length == 2)
                    bkCode = "0" + bkCode;
            }
            
            DataTable dt = RcuKruReader.Read(@"DPTLIST.DBF");
            foreach (DataRow dr in dt.Rows)
            {
                DeptListEntry dle = DeptListEntry.Parse(dr);
                if (SelectedBank == null  || (SelectedBank != null && dle.NKB == bkCode))
                    _HierarchedBankDepts.Add(dle);
            }
            BGU.DRPL.SignificantOwnership.Facade.EKDRBU.DeptListUtil.BuildHierarchy(_HierarchedBankDepts);
        }

        private static List<DeptListEntry> _HierarchedBankDepts;
        public static IEnumerable HierarchedBankDepts
        {
            get
            { 
                if(_HierarchedBankDepts == null)
                {
                    PopulatedHierachedBankDepts();
                }
                var rslt = _HierarchedBankDepts.Where(dle => (dle.ParentCode == string.Empty));
                return (IEnumerable)rslt;
            }
        }

        private static BankInfo _SelectedBank;
        public static BankInfo SelectedBank
        {
            get 
            {
                return _SelectedBank;
            }
            set 
            {
                if (_SelectedBank != value)
                {
                    _SelectedBank = value;
                    PopulatedHierachedBankDepts();
                    //OnPropertyChanged("SelectedBank");
                    //OnPropertyChanged("HierarchedBankDepts");
                }
            }
        }

    }
}
