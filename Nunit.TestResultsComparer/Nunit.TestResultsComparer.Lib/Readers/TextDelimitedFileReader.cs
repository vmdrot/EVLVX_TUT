using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit.TestResultsComparer.Lib.Readers
{
    public static class TextDelimitedFileReader
    {
        public static DataTable Read(string path, char delimiter, int expectedColumnCount)
        {
            DataTable dt = null;
            using (GenericParsing.GenericParserAdapter p = new GenericParsing.GenericParserAdapter(path))
            {
                p.ColumnDelimiter = delimiter;
                p.FirstRowHasHeader = true;
                p.ExpectedColumnCount = expectedColumnCount;
                dt = p.GetDataTable();
            }

            return dt;
        }

    }
}
