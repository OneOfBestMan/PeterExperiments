using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Peter.ExcelOperation;
using System.Linq;

namespace SampleWebApp.Core.Controllers
{
    public class HomeController : Controller
    {
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IImportExcel _importer;
        private readonly ICRUDObject _crudObject;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IImportExcel importer, ICRUDObject insertObject)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _importer = importer;
            _importer.Options = new ValidOption() { ColumnsShouldBe = 2 };
            _crudObject = insertObject;
        }

        /// <summary>
        /// /Home/FileReport
        /// </summary>
        public IActionResult FileReport()
        {
            var fileDownloadName = "report.xlsx";
            var reportsFolder = "reports";

            using (var package = createExcelPackage())
            {
                package.SaveAs(new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, reportsFolder, fileDownloadName)));
            }
            return File($"~/{reportsFolder}/{fileDownloadName}", XlsxContentType, fileDownloadName);
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// An in-memory report
        /// </summary>
        public IActionResult InMemoryReport()
        {
            byte[] reportBytes;
            using (var package = createExcelPackage())
            {
                reportBytes = package.GetAsByteArray();
            }

            return File(reportBytes, XlsxContentType, "report.xlsx");
        }

        /// <summary>
        /// 下载excel
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Download()
        {
            byte[] reportBytes;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("excelexport");
                SetHeaders<ExcelImport>(package, worksheet);
                SetData(worksheet);
                reportBytes = package.GetAsByteArray();
            }

            return File(reportBytes, XlsxContentType, "excelTest.xlsx");

        }
        private void SetHeaders<T>(ExcelPackage package, ExcelWorksheet worksheet) where T : class
        {
            
            Type myType = typeof(T);
            //Get all the properties associated with T
            PropertyInfo[] myProp = myType.GetProperties();
            int columnNums = 1;
            foreach (var propertyInfo in myProp)
            {
                var mappedColumnName = TableMapDict.Mapping.FirstOrDefault(a => a.Value == propertyInfo.Name).Key;
                worksheet.Cells[1, columnNums].Value = mappedColumnName;
                columnNums++;
            }
            var numberformat = "#,##0";
            var dataCellStyleName = "TableNumber";
            var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
            numStyle.Style.Numberformat.Format = numberformat;
        }
        private void SetData(ExcelWorksheet worksheet)
        {
            CRUDOption option = new CRUDOption()
            {
                TableName = "ExcelImport",
                Where= " Id<1040000 "
            };
            var list = _crudObject.Select<ExcelImport>( option);
            worksheet.Cells["A2"].LoadFromCollection(list, PrintHeaders: false);
        }



        //[DisableRequestSizeLimit]
        public async Task<IActionResult> FileInsert(IFormFile file)
        {
            #region testw
            if (file == null || file.Length == 0)
            {
                return RedirectToAction("Index");
            }
            //var list = await _importer.Upload<ExcelImport>(file);
            var table = await _importer.Upload(file);
            StringBuilder sb = new StringBuilder();
            var name = file.Name;
            CRUDOption option = new CRUDOption()
            {
                TableName = "ExcelImport",
                BatchSize = 10000,
                BulkCopyTimeout = 600,
                NotifyAfter = 10000,
                ColumnMapping = TableMapDict.Mapping
            };

            //_insertObject.RowsCopied += OnSqlRowsCopied;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            _crudObject.Insert<ExcelImport>(table, option);
            watch.Stop();
            string executedTime = watch.ElapsedMilliseconds.ToString();
            return Content("执行共:" + executedTime + "毫秒");
            #endregion

        }

        public async Task<IActionResult> FileUpdate(IFormFile file)
        {
            #region testw
            if (file == null || file.Length == 0)
            {
                return RedirectToAction("Index");
            }
            string returnStr = "";
            //var list = await _importer.Upload<ExcelImport>(file);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var table = await _importer.Upload(file);
            watch.Stop();
            returnStr = returnStr + "导入一共:" + watch.ElapsedMilliseconds + "毫秒";
            StringBuilder sb = new StringBuilder();
            var name = file.Name;
            CRUDOption option = new CRUDOption()
            {
                TableName = "ExcelImport",
                BatchSize = 10000,
                BulkCopyTimeout = 600,
                NotifyAfter = 10000,
                ColumnMapping = TableMapDict.Mapping
            };

            //_insertObject.RowsCopied += OnSqlRowsCopied;

            watch.Start();
            _crudObject.Update<ExcelImport>(table, option);
            watch.Stop();
            string executedTime = watch.ElapsedMilliseconds.ToString();
            return Content(returnStr + ",更新执行共:" + executedTime + "毫秒");
            #endregion

        }

        public virtual void OnSqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Response.WriteAsync(e.RowsCopied.ToString());
        }

        /// <summary>
        /// /Home/DataTableReport
        /// </summary>
        public IActionResult DataTableReport()
        {
            var dataTable = new DataTable("Users");
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
            var rnd = new Random();
            for (var i = 0; i < 100; i++)
            {
                var row = dataTable.NewRow();
                row["Name"] = $"User {i}";
                row["Age"] = rnd.Next(20, 100);
                dataTable.Rows.Add(row);
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Excel Test");
                worksheet.Cells["A1"].LoadFromDataTable(dataTable, PrintHeaders: true);
                for (var col = 1; col < dataTable.Columns.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }
                return File(package.GetAsByteArray(), XlsxContentType, "report.xlsx");
            }
        }

        private string readExcelPackage(FileInfo fileInfo, string worksheetName)
        {
            using (var package = new ExcelPackage(fileInfo))
            {
                return readExcelPackageToString(package.Workbook.Worksheets[worksheetName]);
            }
        }

        private string readExcelPackageToString(ExcelWorksheet worksheet)
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

        private ExcelPackage createExcelPackage()
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Salary Report";
            package.Workbook.Properties.Author = "Vahid N.";
            package.Workbook.Properties.Subject = "Salary Report";
            package.Workbook.Properties.Keywords = "Salary";


            var worksheet = package.Workbook.Worksheets.Add("Employee");

            //First add the headers
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Gender";
            worksheet.Cells[1, 4].Value = "Salary (in $)";

            //Add values

            var numberformat = "#,##0";
            var dataCellStyleName = "TableNumber";
            var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
            numStyle.Style.Numberformat.Format = numberformat;

            worksheet.Cells[2, 1].Value = 1000;
            worksheet.Cells[2, 2].Value = "Jon";
            worksheet.Cells[2, 3].Value = "M";
            worksheet.Cells[2, 4].Value = 5000;
            worksheet.Cells[2, 4].Style.Numberformat.Format = numberformat;

            worksheet.Cells[3, 1].Value = 1001;
            worksheet.Cells[3, 2].Value = "Graham";
            worksheet.Cells[3, 3].Value = "M";
            worksheet.Cells[3, 4].Value = 10000;
            worksheet.Cells[3, 4].Style.Numberformat.Format = numberformat;

            worksheet.Cells[4, 1].Value = 1002;
            worksheet.Cells[4, 2].Value = "Jenny";
            worksheet.Cells[4, 3].Value = "F";
            worksheet.Cells[4, 4].Value = 5000;
            worksheet.Cells[4, 4].Style.Numberformat.Format = numberformat;

            // Add to table / Add summary row
            var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: 4, toColumn: 4), "Data");
            tbl.ShowHeader = true;
            tbl.TableStyle = TableStyles.Dark9;
            tbl.ShowTotal = true;
            tbl.Columns[3].DataCellStyleName = dataCellStyleName;
            tbl.Columns[3].TotalsRowFunction = RowFunctions.Sum;
            worksheet.Cells[5, 4].Style.Numberformat.Format = numberformat;

            // AutoFitColumns
            worksheet.Cells[1, 1, 4, 4].AutoFitColumns();

            worksheet.HeaderFooter.OddFooter.InsertPicture(
                new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, "images", "captcha.jpg")),
                PictureAlignment.Right);

            return package;
        }
    }
}