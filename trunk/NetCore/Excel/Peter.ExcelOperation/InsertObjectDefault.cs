using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Peter.ExcelOperation
{
	public class InsertObjectDefault : IInsertObject
	{

		IConfiguration _configuration;
		public InsertObjectDefault(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Insert<T>(DataTable table, CRUDOption option) where T : class
		{
			Type myType = typeof(T);
			PropertyInfo[] myProp = myType.GetProperties();
			var copyParameters = myProp.ToArray();

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
				if (option.ColumnMapping != null && option.ColumnMapping.Count > 0)
				{
					foreach (var item in option.ColumnMapping)
					{
						sqlCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(item.Key, item.Value));
					}
				}
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
