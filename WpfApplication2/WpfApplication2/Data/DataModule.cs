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
                    _currentBanks.Sort((bk1, bk2) => bk1.Name.CompareTo(bk2.Name));
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


        public static List<DeptListEntry> ReadAllDeptList()
        {
            List<DeptListEntry> rslt = new List<DeptListEntry>();
            DataTable dt = RcuKruReader.Read(@"DPTLIST.DBF");
            foreach (DataRow dr in dt.Rows)
            {
                DeptListEntry dle = DeptListEntry.Parse(dr);
                rslt.Add(dle);
            }
            return rslt;
        }

        public static List<DeptListEntry> FilterDeptsByBank(BankInfo bk, List<DeptListEntry> src)
        {
            string bkCode = SelectedBank != null ? bk.Code : string.Empty;
            if (!string.IsNullOrEmpty(bkCode) && bkCode.Length < 3)
            {
                if (bkCode.Length == 1)
                    bkCode = "00" + bkCode;
                else if (bkCode.Length == 2)
                    bkCode = "0" + bkCode;
            }
            List<DeptListEntry> rslt = new List<DeptListEntry>();
            foreach (DeptListEntry dle in src)
            {
                if (bk == null || (bk != null && dle.NKB == bkCode))
                    rslt.Add(dle);
            }
            return rslt;
        }
        private static void PopulatedHierachedBankDepts()
        {
            _HierarchedBankDepts = FilterDeptsByBank(SelectedBank, ReadAllDeptList());
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

        public class OblastInfo
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        private static List<OblastInfo> _oblasts;
        public static List<OblastInfo> Oblasts
        {
            get
            {
                if (_oblasts == null)
                {
                    _oblasts = new List<OblastInfo>();
                    _oblasts.AddRange(
                        new OblastInfo[] { 
                            new OblastInfo(){Code= "", Name = ""},
new OblastInfo(){Code= "01", Name = "Вінницька"},
new OblastInfo(){Code= "02", Name = "Волинська"},
new OblastInfo(){Code= "03", Name = "Дніпропетровська"},
new OblastInfo(){Code= "04", Name = "Донецька"},
new OblastInfo(){Code= "05", Name = "Житомирська"},
new OblastInfo(){Code= "06", Name = "Закарпатська"},
new OblastInfo(){Code= "07", Name = "Запорізька"},
new OblastInfo(){Code= "08", Name = "Івано-Франківська"},
new OblastInfo(){Code= "09", Name = "Київська"},
new OblastInfo(){Code= "10", Name = "Кіровоградська"},
new OblastInfo(){Code= "11", Name = "АР Крим"},
new OblastInfo(){Code= "12", Name = "Луганська"},
new OblastInfo(){Code= "13", Name = "Львівська"},
new OblastInfo(){Code= "14", Name = "Миколаївська"},
new OblastInfo(){Code= "15", Name = "Одеська"},
new OblastInfo(){Code= "16", Name = "Полтавська"},
new OblastInfo(){Code= "17", Name = "Рівненська"},
new OblastInfo(){Code= "18", Name = "Сумська"},
new OblastInfo(){Code= "19", Name = "Тернопільська"},
new OblastInfo(){Code= "20", Name = "Харківська"},
new OblastInfo(){Code= "21", Name = "Херсонська"},
new OblastInfo(){Code= "22", Name = "Хмельницька"},
new OblastInfo(){Code= "23", Name = "Черкаська"},
new OblastInfo(){Code= "24", Name = "Чернігівська"},
new OblastInfo(){Code= "25", Name = "Чернівецька"}
                        }
                        ); // todo
                }
                return _oblasts;
            }
        }

    }
}
