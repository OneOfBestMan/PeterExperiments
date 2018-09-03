using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Peter.ExcelOperation
{
	public static class WorksheetExtention
	{
		public static DataTable ToDataTable(this ExcelWorksheet ws, bool hasHeaderRow = true)
		{
			var tbl = new DataTable();
			foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
			{
				var columnName = hasHeaderRow ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column);
				if (!tbl.Columns.Contains(columnName))
				{
					tbl.Columns.Add(columnName);
				}
			}

			var startRow = hasHeaderRow ? 2 : 1;
			for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
			{
				var wsRow = ws.Cells[rowNum, 1, rowNum, tbl.Columns.Count];
				var row = tbl.NewRow();
				foreach (var cell in wsRow) row[cell.Start.Column - 1] = cell.Text;
				tbl.Rows.Add(row);
			}
			return tbl;
		}

		public static List<T> ToDataTable<T>(this ExcelWorksheet ws, bool hasHeaderRow = true) where T : class
		{
			List<T> retList = new List<T>();
			using (var pck = new ExcelPackage())
			{
				var rowCount = ws.Dimension?.Rows;
				var toColumn = ws.Dimension?.Columns;

				Dictionary<string, int> columnNames = new Dictionary<string, int>();
				// wsRow = ws.Row(0);
				var colPosition = 0;

				foreach (var cell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
				{
					var key = cell.Value.ToString();
					if (!columnNames.ContainsKey(key))
					{
						columnNames.Add(key, colPosition);
					}
					colPosition++;
				}
				//create a instance of T
				T objT = Activator.CreateInstance<T>();
				//Retrieve the type of T
				Type myType = typeof(T);
				//Get all the properties associated with T
				PropertyInfo[] myProp = myType.GetProperties();

				//Loop through the rows of the excel sheet
				var fromRow = hasHeaderRow ? 2 : 1;
				for (var rowNum = fromRow; rowNum <= ws.Dimension.End.Row; rowNum++)
				{
					objT = Activator.CreateInstance<T>();
					var wsRow = ws.Cells[rowNum, 1, rowNum, columnNames.Keys.Count];
					foreach (var propertyInfo in myProp)
					{
						if (columnNames.ContainsKey(propertyInfo.Name))
						{
							int position = 0;
							columnNames.TryGetValue(propertyInfo.Name, out position);
							//int position = columnNames.IndexOf(propertyInfo.Name);
							//To prevent an exception cast the value to the type of the property.
							propertyInfo.SetValue(objT, Convert.ChangeType(wsRow[rowNum, position + 1].Value, propertyInfo.PropertyType));
						}
					}
					retList.Add(objT);
				}
			}
			return retList;
		}

	}
}
