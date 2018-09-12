using System.Collections.Generic;
using System.Data;

namespace Peter.ExcelOperation
{
    public interface ICRUDObject
    {
        event CRUDObjectDefault.SqlRowsCopied RowsCopied;

        void Insert<T>(DataTable table, CRUDOption option) where T : class;

        void Update<T>(DataTable table, CRUDOption option) where T : class;

        List<T> Select<T>(CRUDOption option) where T : class;

    }
}
