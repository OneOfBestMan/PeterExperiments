using Dapper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC.SDK.Plugins.Excel
{
    public abstract class ExcelOperationDefault : IExcelOperation
    {
        IBatchSqlOperation _batchSqlOperation;

        public ExcelOperationDefault(IBatchSqlOperation batchSqlOperation)
        {
            _batchSqlOperation = batchSqlOperation;
        }

        public abstract ExcelOperationResult BeforeUploadValid(ExcelValidContext context);
        public abstract ExcelOperationResult AfterUploadValid(ExcelValidContext context);

        private ExcelOperationResult ValidDefault(ExcelWorksheet ws, ExcelOperationOption option, List<ImportConfig> configList)
        {
            ExcelOperationResult result = new ExcelOperationResult();
            result.Messages = new List<ErrorMessage>();
            var clumns = GetHeaderFromSheet(ws);
            //验证是否有名称不在允许的列中

            var notExistList = new List<string>();
            if (configList == null || configList.Count == 0)
            {
                throw new System.Exception("excel到表字段的配置表不存在");
            }
            var notExistImportClumns = clumns.FindAll(a => !configList.Exists(b => b.ExcelName == a));
            if (notExistImportClumns.Count > 0)
            {
                var notExistsColumnStr = string.Join(",", notExistImportClumns.ToArray());
                result.Messages.Add(new ErrorMessage() { Name = "列不存在:" + notExistsColumnStr, Nums = notExistList.Count.ToString() });
            }
            //验证有重复列
            if (clumns.Count != ws.Dimension.End.Column)
            {
                result.Messages.Add(new ErrorMessage() { Name = "有名称重复的列", Nums = (ws.Dimension.End.Column - clumns.Count).ToString() });
            }

            return result;
        }

        public async Task<ExcelOperationResult> Upload(IFormFile file, ExcelOperationOption option)
        {
            ExcelOperationResult validResult = new ExcelOperationResult();

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                    using (var package = new ExcelPackage(memoryStream))
                    {
                        var worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                        var configList = _batchSqlOperation.Select<ImportConfig>(new BatchOperationOption()
                        {
                            ConnectionString = option.BatchOption.ConnectionString,
                            TableName = string.IsNullOrEmpty(option.TableName) ? "LC_Meta_ImportConfigs" : option.TableName,
                            Where = string.Format(" ImportType={0} and IsUpdate=1", (int)option.ImportType)
                        });
                        //var list = configList.ToList();
                        //configList.CopyTo(list);
                        option.BatchOption.AllowedAllColumns = configList.ToArray();
                        validResult = ValidDefault(worksheet, option, configList);
                        if (validResult != null && validResult.Messages != null && validResult.Messages.Count > 0)
                        {
                            return validResult;
                        }
                        ExcelValidContext context = new ExcelValidContext() { Worksheet = worksheet, OperationOption = option };
                        validResult = BeforeUploadValid(context);
                        if (validResult != null && validResult.Messages != null && validResult.Messages.Count > 0)
                        {
                            return validResult;
                        }
                        var chiefId = CreateImportChief(option, file.Name);
                        var table = worksheet.ToMappedDataTable(configList);

                        context = new ExcelValidContext() { Worksheet = worksheet, OperationOption = option, Table = table };
                        validResult = AfterUploadValid(context);
                        if (validResult != null && validResult.Messages != null && validResult.Messages.Count > 0)
                        {
                            return validResult;
                        }
                        var importColumns = GetHeaderFromSheet(worksheet);
                        configList = configList.FindAll(a => importColumns.Exists(b => b == a.ExcelName));
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        foreach (var item in configList)
                        {
                            dict.Add(item.ExcelName, item.ColumnName);
                        }
                        option.BatchOption.ColumnMapping = dict;
                       
                        if (chiefId>0)
                        {
                            option.BatchOption.ChiefId = chiefId;
                            _batchSqlOperation.InsertOrUpdate(table, option.BatchOption);
                        }
                        
                    }
                }
            }
            catch (System.Exception ex)
            {
                validResult = new ExcelOperationResult();
                validResult.Messages.Add(new ErrorMessage() { Name = "上传错误：" + ex.Message, Nums = "1" });
            }

            return validResult;
        }

        public List<string> GetHeaderFromSheet(ExcelWorksheet worksheet)
        {
            var clumns = new List<string>();
            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                var columnName = firstRowCell.Text.Replace("（", "(").Replace("）", ")");
                if (!clumns.Contains(columnName))
                {
                    clumns.Add(columnName);
                }
            }
            return clumns;
        }

        private int CreateImportChief(ExcelOperationOption option, string name)
        {
            var sql = string.Format("insert into LC_Meta_ImportChiefs(ExcelName,OperationResult,InsertNum,UpdateNum,Operator,OperationTime,IsUpdated) " +
                "values('{0}','','0','0','',Getdate(),0)" +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)", name);
            using (var con = new SqlConnection(option.BatchOption.ConnectionString))
            {
                var chiefId = con.ExecuteScalar(sql);
                return (int)chiefId;
            }
        }
    }
}
