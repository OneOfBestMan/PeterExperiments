using System.Data;

namespace Peter.ExcelOperation
{
	public interface IInsertObject
	{
		void Insert<T>(DataTable table, string tableName) where T : class;

	}
}
