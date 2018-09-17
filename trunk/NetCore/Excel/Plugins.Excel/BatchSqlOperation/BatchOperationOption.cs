using System.Collections.Generic;

namespace LC.SDK.Plugins.Excel
{
    public class BatchOperationOption
	{

        public string ConnectionString { get; set; }

        public int ChiefId { get; set; }

        #region Insert
        /// <summary>
        /// Insert必填
        /// </summary>
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

        #endregion

        #region update
        /// <summary>
        /// update必填，更新操作用数据库存储过程
        /// </summary>
        public string SPName { get; set; }

        public ImportConfig[] AllowedAllColumns { get; set; }

        #endregion

        #region select

        /// <summary>
        /// select必填
        /// </summary>
        public string Where { get; set; }
        public string SelectColumns { get; set; }
        #endregion
    }
}
