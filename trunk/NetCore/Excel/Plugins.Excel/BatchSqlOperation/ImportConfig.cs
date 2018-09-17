using System.ComponentModel.DataAnnotations;

namespace LC.SDK.Plugins.Excel
{
    /// <summary>
    /// 数据导入类型
    /// </summary>
    public enum ImportType
    {
        [Display(Name = "小区信息")]
        ResidentialArea = 1,
        [Display(Name = "楼栋信息")]
        Unit = 2,
        [Display(Name = "单套信息")]
        SingleSet = 3,
        [Display(Name = "小区基价信息")]
        ResidentialAreaBasePrice = 4,
        [Display(Name = "楼栋基价信息")]
        UnitBasePrice = 5,
        [Display(Name = "楼栋修正系数表")]
        CoefficientTable = 6
    }

    public class ImportConfig
    {
        public ImportType ImportType { get; set; }
        public string ExcelName { get; set; }
        public string ColumnName { get; set; }
    }
}
