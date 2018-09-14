namespace LC.SDK.Plugins.Excel
{
    public class ExcelOperationOption
    {
        /// <summary>
        /// 字段Map表的名称
        /// </summary>
        public string TableName { get; set; }

        public ImportType ImportType { get; set; }

        /// <summary>
        /// 批量操作的配置
        /// </summary>
        public BatchOperationOption BatchOption { get; set; }


    }
}
