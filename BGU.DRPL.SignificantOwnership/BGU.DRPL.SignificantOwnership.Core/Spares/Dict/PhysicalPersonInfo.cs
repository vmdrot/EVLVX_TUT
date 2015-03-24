using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGU.DRPL.SignificantOwnership.Core.Spares.Data;
using System.Text.RegularExpressions;

namespace BGU.DRPL.SignificantOwnership.Core.Spares.Dict
{
    public class PhysicalPersonInfo
    {
        public string SurnameUkr { get; set; }
        public string NameUkr { get; set; }
        public string MiddleNameUkr { get; set; }
        public string FullNameUkr { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public SexType Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string PassportID { get; set; }
        public DateTime PassIssuedDate { get; set; }
        public RegistrarAuthority PassIssueAuthority { get; set; }
        public string TaxOrSocSecID { get; set; }
        public LocationInfo Address { get; set; }
        public CountryInfo CitizenshipCountry { get; set; }

        public GenericPersonID GenericID { get { return new GenericPersonID() { CountryISO3Code = CitizenshipCountry != null ? CitizenshipCountry.CountryISONr : string.Empty, PersonCode = TaxOrSocSecID ?? PassportID, PersonType = EntityType.Physical }; } }


        #region inner type(s)
        public enum PhysPersonInfoPart
        {
            None = 0,
            SurnameUkr,
            NameUkr,
            MiddleNameUkr,
            FullNameUkr,
            Surname,
            Name,
            MiddleName,
            FullName,
            Sex,
            BirthDate,
            PassportID,
            PassIssuedDate,
            PassIssueAuthority,
            TaxOrSocSecID,
            Address,
            CitizenshipCountry
        }

        public class ParseMatchInfo
        {
            public ParseMatchInfo()
            {
                RecognizedParts = new Dictionary<int, PhysPersonInfoPart>();
                NonRecognizedParts = new Dictionary<int, string>();
            }
            public bool IsSurnameUkrRecognized = false;
            public bool IsNameUkrRecognized = false;
            public bool IsMiddleNameUkrRecognized = false;
            public bool IsFullNameUkrRecognized = false;
            public bool IsSurnameRecognized = false;
            public bool IsNameRecognized = false;
            public bool IsMiddleNameRecognized = false;
            public bool IsFullNameRecognized = false;
            public bool IsSexRecognized = false;
            public bool IsBirthDateRecognized = false;
            public bool IsPassportIDRecognized = false;
            public bool IsPassIssuedDateRecognized = false;
            public bool IsPassIssueAuthorityRecognized = false;
            public bool IsTaxOrSocSecIDRecognized = false;
            public bool IsAddressRecognized = false;
            public bool IsCitizenshipCountryRecognized = false;

            public Dictionary<int, PhysPersonInfoPart> RecognizedParts
            {
                get;
                private set;
            }
            public Dictionary<int, string> NonRecognizedParts { get; private set; }

        }
        #endregion

        #region parse method(s)
        public static void TryParseFillPassIssueData(string rawData, PhysicalPersonInfo target)
        {
            ParseMatchInfo pmi;
            TryParseFillPassIssueData(rawData, target, out pmi);
        }

        public static void TryParseFillPassIssueData(string rawData, PhysicalPersonInfo target, out ParseMatchInfo pmi)
        {
            string remString = rawData;
            pmi = new ParseMatchInfo();
            int i = 0;
            {
                bool matched = false;
                if (!pmi.IsPassportIDRecognized)
                {
                    string currMatchedString;
                    matched = MatchPassID(remString, 0, 1, target, out currMatchedString);
                    if (matched) { pmi.IsPassportIDRecognized = true; pmi.RecognizedParts.Add(i++, PhysPersonInfoPart.PassportID); remString = remString.Replace(currMatchedString, string.Empty); }
                }
                if (!pmi.IsPassIssuedDateRecognized)
                {
                    string currMatchedString;
                    matched = MatchPassIssueDt(remString, 0, 1, target, out currMatchedString);
                    if (matched) { pmi.IsPassIssuedDateRecognized = true; pmi.RecognizedParts.Add(i++, PhysPersonInfoPart.PassIssuedDate); ; remString = remString.Replace(currMatchedString, string.Empty); }
                }
                if (!pmi.IsPassIssueAuthorityRecognized)
                {
                    string currMatchedString;
                    matched = MatchPassIssueAuthority(remString, 0, 1, target, out currMatchedString);
                    if (matched) { pmi.IsPassIssueAuthorityRecognized = true; pmi.RecognizedParts.Add(i++, PhysPersonInfoPart.PassIssueAuthority); ; remString = remString.Replace(currMatchedString, string.Empty); }
                }
                pmi.NonRecognizedParts.Add(0, remString);
            }

        }

        private static readonly Regex _rgxPassIDUA = new Regex("[А-Я]{2}([ ]*№[ ]*|[ ]*ї[ ]*|[ ]*)[0-9]{6}");
        private static readonly Regex _rgxPassIssueDtUA = new Regex("[0-9]{1,2}(\\.|\\/|\\-)[0-9]{1,2}(\\.|\\/|\\-)([0-9]{4}|[0-9]{2})[ ]*(р.|г.|року|рік|)");
        
        #region match helper methods
        public static bool MatchPassID(string src, int wordIdx, int wordsCount, PhysicalPersonInfo target, out string matchedString)
        {
            matchedString = string.Empty;
            if (target.CitizenshipCountry == null || target.CitizenshipCountry.CountryISONr == CountryInfo.UKRAINE.CountryISONr)
            {
                Match m = _rgxPassIDUA.Match(src);
                if (m.Length == 0)
                    return false;
                matchedString = m.Groups[0].Captures[0].Value;
                string res = matchedString;
                if (m.Groups.Count > 1)
                {
                    string splitter = m.Groups[1].ToString();
                    res = res.Replace(splitter, " ");
                    
                }
                target.PassportID = res;
                return true;
            }
            return false;
        }

        public static bool MatchPassIssueDt(string src, int wordIdx, int wordsCount, PhysicalPersonInfo target, out string matchedString)
        {
            matchedString = string.Empty;
            
            Match m = _rgxPassIssueDtUA.Match(src);
            if (m.Length == 0)
                return false;

            DateTime dt;
            matchedString = m.Groups[0].Captures[0].Value;
            if(DateTime.TryParse(matchedString, out dt))
            {
                target.PassIssuedDate = dt;
                return true;
            }
            return false;
        }

        public static bool MatchPassIssueAuthority(string src, int wordIdx, int wordsCount, PhysicalPersonInfo target, out string matchedString)
        {
            matchedString = string.Empty;
            string[] tokens = new string[] { "вид.", "вид", "виданий", "видане", "видана"};

            int matchedTokenIdx = -1;

            List<string> words = LocationInfo.NormalizeAbbrs(Regex.Split(src, @"(\W|')+").ToList());
            for (int tokenIdx = 0; tokenIdx < tokens.Length; tokenIdx++)
            {
                bool bFound = false;
                foreach (string currWord in words)
                {
                    if (currWord.Trim().ToLower() == tokens[tokenIdx].Trim().ToLower())
                    {
                        bFound = true;
                        matchedTokenIdx = tokenIdx;
                        break;
                    }
                }
                if (!bFound)
                    continue;
                matchedTokenIdx = tokenIdx;
                break;
            }
            bool rslt = (matchedTokenIdx != -1);
            if (rslt)
            {
                int pos0 = src.ToLower().IndexOf(tokens[matchedTokenIdx]);
                
                string rawRes = src.Substring(pos0).Trim();
                char lastChar = rawRes[rawRes.Length-1];
                if(lastChar == ',' || lastChar == '.')
                    rawRes = rawRes.Substring(0,rawRes.Length -1).Trim();
                matchedString = rawRes;
                string res = rawRes.Substring(tokens[matchedTokenIdx].Length).Trim();
                target.PassIssueAuthority = new RegistrarAuthority();
                if (target.CitizenshipCountry != null) target.PassIssueAuthority.JurisdictionCountry = target.CitizenshipCountry;
                target.PassIssueAuthority.EntitiesHandled = EntityType.Physical;
                target.PassIssueAuthority.RegistrarName = res;
            }
            return rslt;
        }
        #endregion

        #endregion
    }
}
