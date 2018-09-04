﻿using System.Data;

namespace Peter.ExcelOperation
{
	public interface IInsertObject
	{
		event InsertObjectDefault.SqlRowsCopied RowsCopied;

		void Insert<T>(DataTable table, CRUDOption option) where T : class;
	}
}
