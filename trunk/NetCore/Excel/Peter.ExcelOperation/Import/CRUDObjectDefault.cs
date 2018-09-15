using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Peter.ExcelOperation
{
    /// <summary>
    ///  增删改批量操作
    /// </summary>
	public class CRUDObjectDefault : ICRUDObject
	{

		IConfiguration _configuration;
		public CRUDObjectDefault(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Insert<T>(DataTable table, CRUDOption option) where T : class
		{
			using (var sqlCopy = new SqlBulkCopy(_configuration.GetConnectionString("DefaultConnection")))
			{
				sqlCopy.DestinationTableName = option.TableName;
				sqlCopy.BatchSize = option.BatchSize;
				sqlCopy.BulkCopyTimeout = option.BulkCopyTimeout;
				//sqlCopy.ColumnMappings = new SqlBulkCopyColumnMappingCollection();
				//sqlCopy.WriteToServer(table);

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
				}
				finally
				{
					table.Clear();
				}
			}
		}

        public void Update<T>(DataTable table, CRUDOption option) where T : class
        {
            string constr = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Update_ExcelImport"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@table", table);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public List<T> Select<T>(CRUDOption option) where T : class
        {
            string constr = _configuration.GetConnectionString("DefaultConnection");
            var sql = string.Format("select * from {0} where {1}", option.TableName, option.Where);
            using (var con = new SqlConnection(constr))
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
