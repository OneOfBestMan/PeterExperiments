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

		IConfigurationRoot _configuration;
		public InsertObjectDefault(IConfigurationRoot configuration)
		{
			_configuration = configuration;
		}

		public void Insert<T>(DataTable table, string tableName) where T : class
		{
			string connectionstring = _configuration["ConnectionString"];
			Type myType = typeof(T);
			PropertyInfo[] myProp = myType.GetProperties();
			var copyParameters = myProp.ToArray();

			using (var sqlCopy = new SqlBulkCopy(connectionstring))
			{
				sqlCopy.DestinationTableName = tableName;
				sqlCopy.BatchSize = 500;
				sqlCopy.WriteToServer(table);
			}
		}
	}
}
