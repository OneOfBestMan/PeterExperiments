using System.Collections.Generic;
using System.Data;

namespace LC.SDK.Plugins.Excel
{
    public interface IBatchSqlOperation
    {
        event BatchSqlOperationDefault.SqlRowsCopied RowsCopied;
        /// <summary>
        /// 对导入数据进行拆分，分为插入和更新
        /// </summary>
        /// <param name="table"></param>
        /// <param name="option"></param>
        void InsertOrUpdate(DataTable table, BatchOperationOption option);
        void Insert(DataTable table, BatchOperationOption option);

        void Update(DataTable table, BatchOperationOption option);

        List<T> Select<T>(BatchOperationOption option) where T : class;

    }
}
