using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LC.SDK.Plugins.Excel
{
    public static class DataTableExtentsion
    {
        /// <summary>
        /// 从DataTable中获取某个字段符合给定条件的记录组成的DataTable
        /// </summary>
        /// <typeparam name="T">字段的数据类型</typeparam>
        /// <param name="table">DataTable</param>
        /// <param name="name">字段名称</param>
        /// <param name="predicate">Func委托</param>
        /// <returns>DataTable</returns>
        public static DataTable Where(this DataTable table, string name, Func<string, bool> predicate)
        {
            if (table == null) throw new ArgumentNullException("table");
            if (predicate == null) throw new ArgumentNullException("predicate");
            DataColumnCollection columns = table.Columns;
            if (!columns.Contains(name)) throw new ArgumentException("invalid selector.");
            //if (table.Columns[name].DataType != typeof(T)) throw new ArgumentException("T");
            DataTable _table = new DataTable(table.TableName);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                _table.Columns.Add(table.Columns[i].ColumnName, table.Columns[i].DataType);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (predicate(table.Rows[i][name].ToString()))
                {
                    DataRow newRow = _table.NewRow();
                    for (int index = 0; index < table.Columns.Count; index++)
                    {
                        newRow[table.Columns[index].ColumnName] = table.Rows[i][table.Columns[index]];
                    }
                    _table.Rows.Add(newRow);
                }
            }
            return _table;
        }


        public static DataTable ToTable(this DataTable table, ImportConfig[] columnAll)
        {
            if (table == null) throw new ArgumentNullException("table");
            DataColumnCollection columns = table.Columns;
            //DataTable _table = new DataTable(table.TableName);
            foreach (var item in columnAll)
            {
                if (!table.Columns.Contains(item.ColumnName))
                {
                    table.Columns.Add(item.ColumnName);
                }
            }
            return table;
        }
    }
}
