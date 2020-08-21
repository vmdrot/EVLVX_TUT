using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsages.Utilities
{
    public static class Tools
    {
        public static bool DataTableToCSV(DataTable dtSource, string saveAsPath, bool includeHeader)
        {

            using (StreamWriter sw = new StreamWriter(saveAsPath, false, Encoding.Unicode))
            {
                return DataTableToCSV(dtSource, sw, includeHeader);
            }
        }
        public static bool DataTableToCSV(DataTable dtSource, StreamWriter writer, bool includeHeader)
        {
            if (dtSource == null || writer == null) return false;

            if (includeHeader)
            {
                string[] columnNames = dtSource.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray<string>();
                writer.WriteLine(String.Join("\t", columnNames));
                writer.Flush();
            }

            foreach (DataRow row in dtSource.Rows)
            {
                string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray<string>();
                for (int i = 0; i < fields.Length; i++)
                {
                    if (fields[i].IndexOf('\r') == -1 && fields[i].IndexOf('\n') == -1)
                        continue;
                    fields[i] = fields[i].Replace('\r', ' ').Replace('\n', ' ');
                }
                writer.WriteLine(String.Join("\t", fields));
                writer.Flush();
            }

            return true;
        }

    }
}
