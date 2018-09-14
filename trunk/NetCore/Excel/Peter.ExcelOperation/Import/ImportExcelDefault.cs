using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Peter.ExcelOperation
{
	public class ImportExcelDefault : IImportExcel
	{
		public ValidOption Options { get; set; }
		public ImportExcelDefault()
		{
			Options = new ValidOption();
		}

		public virtual ImportResult<T> Valid<T>(ExcelWorksheet ws) where T : class
		{
			ImportResult<T> result = new ImportResult<T>();
			result.Messages = new List<ErrorMessage>();
			var clumns = new List<string>();
			foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
			{
				var columnName = firstRowCell.Text;
				if (!clumns.Contains(columnName))
				{
					clumns.Add(columnName);
				}
			}
			if (clumns.Count != ws.Dimension.End.Column)
			{
				result.Messages.Add(new ErrorMessage() { Name = "有名称重复的列", Nums = (ws.Dimension.End.Column - clumns.Count).ToString() });
			}
			if (Options.ColumnsShouldBe > 0)
			{
				if (Options.ColumnsShouldBe != clumns.Count)
				{
					result.Messages.Add(new ErrorMessage() { Name = "列数和指定的不同", Nums = "" });
				}
			}
			return result;
		}
		public bool Execute(string sql)
		{
			throw new NotImplementedException();
		}

		public string MakeSql(ExcelWorksheet worksheet)
		{
			throw new NotImplementedException();
		}

		public async Task<DataTable> Upload(IFormFile file)
		{
			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream).ConfigureAwait(false);

				using (var package = new ExcelPackage(memoryStream))
				{
					var worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0

					return worksheet.ToDataTable();
					//return worksheet;
				}
			}
		}

		public async Task<ImportResult<T>> Upload<T>(IFormFile file) where T : class
		{
			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream).ConfigureAwait(false);

				using (var package = new ExcelPackage(memoryStream))
				{
					var worksheet = package.Workbook.Worksheets[0]; // Tip: To access the first worksheet, try index 1, not 0
					var list = worksheet.ToList<T>();
					var result = Valid<T>(worksheet);
					if (result != null && result.Messages.Count > 0)
					{
						return result;
					}
					else
					{
						result.Data = list;
						return result;
					}
					//return worksheet;
				}
			}
		}

		private string DisplaySheetString(ExcelWorksheet worksheet)
		{
			var rowCount = worksheet.Dimension?.Rows;
			var colCount = worksheet.Dimension?.Columns;

			if (!rowCount.HasValue || !colCount.HasValue)
			{
				return string.Empty;
			}

			var sb = new StringBuilder();
			for (int row = 1; row <= rowCount.Value; row++)
			{
				for (int col = 1; col <= colCount.Value; col++)
				{
					sb.AppendFormat("{0}\t", worksheet.Cells[row, col].Value);
				}
				sb.Append(Environment.NewLine);
			}
			return sb.ToString();
		}

		private void DisplaySheetConsole(ExcelWorksheet worksheet)
		{
			var rowCount = worksheet.Dimension?.Rows;
			var colCount = worksheet.Dimension?.Columns;

			if (!rowCount.HasValue || !colCount.HasValue)
			{
				return;
			}

			var sb = new StringBuilder();
			for (int row = 1; row <= rowCount.Value; row++)
			{
				for (int col = 1; col <= colCount.Value; col++)
				{
					sb.AppendFormat("{0}\t", worksheet.Cells[row, col].Value);
				}
				sb.Append(Environment.NewLine);
			}
			Console.WriteLine(sb.ToString());
		}




	}
}
