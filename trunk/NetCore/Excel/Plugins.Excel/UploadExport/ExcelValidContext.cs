using OfficeOpenXml;
using System.Data;

namespace LC.SDK.Plugins.Excel
{
    public class ExcelValidContext
    {
        public ExcelWorksheet Worksheet { get; set; }
        public ExcelOperationOption OperationOption { get; set; }
        public DataTable Table { get; set; }

    }
}
