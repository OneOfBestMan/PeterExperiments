using System.Data;

namespace Peter.ExcelOperation
{
	public interface ICRUDObject
	{
		event CRUDObjectDefault.SqlRowsCopied RowsCopied;

		void Insert<T>(DataTable table, CRUDOption option) where T : class;
	}
}
