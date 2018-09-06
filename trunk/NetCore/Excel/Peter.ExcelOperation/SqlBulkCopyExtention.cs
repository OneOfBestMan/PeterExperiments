using System.Collections.Generic;
using System.Data.SqlClient;
namespace Peter.ExcelOperation
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
    }
}
