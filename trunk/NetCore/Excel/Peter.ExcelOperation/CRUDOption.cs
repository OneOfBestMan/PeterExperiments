using System;
using System.Collections.Generic;
using System.Text;

namespace Peter.ExcelOperation
{
	public class CRUDOption
	{

		public string TableName { get; set; }

		public int BatchSize { get; set; }

		/// <summary>
		/// 执行时间，秒
		/// </summary>
		public int BulkCopyTimeout { get; set; }

		/// <summary>
		/// 执行多少条后通知
		/// </summary>
		public int NotifyAfter { get; set; }

		public Dictionary<string, string> ColumnMapping { get; set; }
	}
}
