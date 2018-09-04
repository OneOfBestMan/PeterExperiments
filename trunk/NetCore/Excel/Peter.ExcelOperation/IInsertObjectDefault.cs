using System.Data;
using System.Data.SqlClient;

namespace Peter.ExcelOperation
{
	public interface IInsertObjectDefault
	{
		event InsertObjectDefault.SqlRowsCopied RowsCopied;

		void Insert<T>(DataTable table, CRUDOption option) where T : class;
		void OnSqlRowsCopied(object sender, SqlRowsCopiedEventArgs e);
	}
}