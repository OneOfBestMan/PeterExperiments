using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LC.SDK.Plugins.Excel
{
    /// <summary>
    ///  增删改批量操作
    /// </summary>
	public class BatchSqlOperationDefault : IBatchSqlOperation
    {
        public BatchSqlOperationDefault()
        {
        }

        /// <summary>
        /// 对导入数据进行拆分，分为插入和更新
        /// </summary>
        /// <param name="table"></param>
        /// <param name="option"></param>
        public void InsertOrUpdate(DataTable table, BatchOperationOption option)
        {
            if (table == null || table.Rows.Count == 0)
            {
                throw new Exception("参数table为空");
            }
            var firstColumnName = table.Columns[0].ColumnName;
            var insertTable = table.Where(firstColumnName, a => string.IsNullOrEmpty(a));
            var updateTable = table.Where(firstColumnName, a => !string.IsNullOrEmpty(a));
            if (insertTable!=null && insertTable.Rows!=null && insertTable.Rows.Count>0)
            {
                Insert(insertTable, option);
            }
            if (updateTable != null && updateTable.Rows!=null && updateTable.Rows.Count > 0)
            {
                Update(updateTable, option);
            }
        }

        public void Insert(DataTable table, BatchOperationOption option)
        {
            if (option == null)
            {
                throw new Exception("参数option不能为空");
            }
            if (string.IsNullOrEmpty(option.ConnectionString))
            {
                throw new Exception("参数option的属性ConnectionString必须是一个数据库连接字符串");
            }
            if (string.IsNullOrEmpty(option.TableName))
            {
                throw new Exception("参数option的属性TableName必须是一个数据库表名称");
            }
            if (option.ColumnMapping == null && option.ColumnMapping.Count == 0)
            {
                throw new Exception("参数option的属性ColumnMapping必须是一个从excel到数据库字段名的字典");
            }
            using (var sqlCopy = new SqlBulkCopy(option.ConnectionString))
            {
                sqlCopy.DestinationTableName = option.TableName;
                if (option.BatchSize > 0)
                {
                    sqlCopy.BatchSize = option.BatchSize;
                }
                if (option.BulkCopyTimeout > 0)
                {
                    sqlCopy.BulkCopyTimeout = option.BulkCopyTimeout;
                }
                sqlCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);
                sqlCopy.NotifyAfter = option.NotifyAfter;

                // Set up the column mappings by name.
                sqlCopy.AddValueToValueMappings(option.ColumnMapping);
                try
                {
                    // Write from the source to the destination.
                    sqlCopy.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    table.Clear();
                }
            }
        }

        public void Update(DataTable table, BatchOperationOption option)
        {
            if (option == null)
            {
                throw new Exception("参数option不能为空");
            }
            if (string.IsNullOrEmpty(option.ConnectionString))
            {
                throw new Exception("参数option的属性ConnectionString必须是一个数据库连接字符串");
            }
            if (string.IsNullOrEmpty(option.SPName))
            {
                throw new Exception("参数option的属性SPName必须是一个数据库存储过程用来更新表");
            }
            var tempTable = table.ToTable(option.AllowedAllColumns);
            using (SqlConnection con = new SqlConnection(option.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(option.SPName))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@table", tempTable);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public List<T> Select<T>(BatchOperationOption option) where T : class
        {
            if (option == null)
            {
                throw new Exception("参数option不能为空");
            }
            if (string.IsNullOrEmpty(option.ConnectionString))
            {
                throw new Exception("参数option的属性ConnectionString必须是一个数据库连接字符串");
            }
            if (string.IsNullOrEmpty(option.TableName))
            {
                throw new Exception("参数option的属性TableName必须是一个数据库表名称");
            }
            if (string.IsNullOrEmpty(option.Where))
            {
                throw new Exception("参数option的属性Where必须是一个去掉where的sql查询条件");
            }
            var sql = "";
            if (!string.IsNullOrEmpty(option.SelectColumns))
            {
                sql = string.Format("select {0} from {1} where {2}", option.SelectColumns, option.TableName, option.Where);
            }
            else
            {
                sql = string.Format("select * from {0} where {1}", option.TableName, option.Where);
            }
            using (var con = new SqlConnection(option.ConnectionString))
            {
                var list = con.Query<T>(sql).ToList();
                return list;
            }
        }


        public delegate void SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e);
        public event SqlRowsCopied RowsCopied;

        public virtual void OnSqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            if (RowsCopied != null)
            {
                RowsCopied(sender, e);
            }
        }
    }
}
