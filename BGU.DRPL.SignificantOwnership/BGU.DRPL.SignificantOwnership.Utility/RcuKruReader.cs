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
    }
}
