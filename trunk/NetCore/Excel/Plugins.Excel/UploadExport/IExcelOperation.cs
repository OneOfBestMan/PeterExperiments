using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LC.SDK.Plugins.Excel
{
	public interface IExcelOperation
	{
        ExcelOperationResult AfterUploadValid(ExcelValidContext context);
        ExcelOperationResult BeforeUploadValid(ExcelValidContext context);
        Task<ExcelOperationResult> Upload(IFormFile file, ExcelOperationOption option);
        //ExcelOperationResult ValidDefault(ExcelWorksheet ws, ExcelOperationOption option);
    }

	public class ExcelOperationResult
	{
        public List<ErrorMessage> Messages { get; set; } = new List<ErrorMessage>();
	}

	public class ErrorMessage
	{
		public string Name { get; set; }
		public string Nums { get; set; }
		public string Positions { get; set; }
	}
}
