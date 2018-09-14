using System.Collections.Generic;
using System.Data.SqlClient;
namespace LC.SDK.Plugins.Excel
{
    public static class SqlBulkCopyExtention
    {
        public static void AddColumnMappings(this SqlBulkCopy sqlBulkCopy, Dictionary<string, string> mapping)
        {
            if (mapping != null && mapping.Count > 0)
            {
                foreach (var item in mapping)
                {
                    sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(item.Key, item.Value));
                }
            }
        }

        public static void AddValueToValueMappings(this SqlBulkCopy sqlBulkCopy, Dictionary<string, string> mapping)
        {
            if (mapping != null && mapping.Count > 0)
            {
                foreach (var item in mapping)
                {
                    if (item.Key == "Id")
                    {
                        continue;
                    }
                    sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(item.Value, item.Value));
                }
            }
        }
    }
}
