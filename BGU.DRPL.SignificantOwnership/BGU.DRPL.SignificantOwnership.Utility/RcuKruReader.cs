using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BGU.DRPL.SignificantOwnership.Utility
{
    public class RcuKruReader
    {

        public static DataTable Read(string rcuKruPath)
        {
            ParseDBF.CharsEncoding = Encoding.GetEncoding("CP866");
            ParseDBF.DosCharsReplacer = RcuKruDosCharsReplacer;
            return ParseDBF.ReadDBF(rcuKruPath);
        }

        private static string RcuKruDosCharsReplacer(string strIn)
        {
            string rslt = strIn.Trim();
            char[] origChars = new char[] { 'ї', 'Ї', 'Ў', 'ў', '°', '∙'};
            char[] replaceChars = new char[] { 'є', 'Є', 'І', 'і', 'Ї', 'ї' };
            for (int i = 0; i < origChars.Length; i++)
                rslt = rslt.Replace(origChars[i], replaceChars[i]);
            return rslt;
        }

        public static DataTable Search(DataTable dt, string anyText)
        {
            string whereClause = ComposeWhereClause(anyText);
            if(string.IsNullOrEmpty(whereClause))
                return dt;
            DataTable rslt = dt.Copy();
            rslt.Clear();
            DataRow[] rows = dt.Select(whereClause);
            foreach (DataRow dr in rows)
                rslt.Rows.Add(dr.ItemArray);
            return rslt;
        }

        public static string ComposeWhereClause(string anyText)
        {
            if (string.IsNullOrEmpty(anyText))
                return string.Empty;

            string trimmed = anyText.Trim();
            if (trimmed.Length == 0)
                return string.Empty;
            if (trimmed.Length == 6)
            {
                int tmp;
                if(int.TryParse(trimmed,out tmp))
                    return string.Format("MFO={0}", trimmed);
            }

            return string.Format("NB LIKE '%{0}%' OR KNB LIKE = '%{0}%' OR NLF LIKE '%{0}%'", anyText);
        }
    }
}
