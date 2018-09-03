using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Peter.ExcelOperation
{
	public interface IImportExcel
	{

		Task<DataTable> Upload(IFormFile file);
		Task<ImportResult<T>> Upload<T>(IFormFile file) where T : class;
		ImportResult<T> Valid<T>(ExcelWorksheet ws) where T : class;
		string MakeSql(ExcelWorksheet worksheet);
		bool Execute(string sql);
		ValidOption Options { get; set; }
	}

	public class ImportResult<T> where T : class
	{
		public List<ErrorMessage> Messages { get; set; }
		public List<T> Data { get; set; }
	}

	public class ErrorMessage
	{
		public string Name { get; set; }
		public string Nums { get; set; }
		public string Positions { get; set; }
	}
}
