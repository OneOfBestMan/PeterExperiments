using System;

namespace Peter.ExcelOperation
{
    public class ExcelImport
	{
        public int Id { get; set; }

		public string Name { get; set; }

		public int Age { get; set; }
		/// <summary>
		/// 小区名称
		/// </summary>
		public string RAName { get; set; }
		/// <summary>
		/// 小区地址
		/// </summary>
		public string RAAdress { get; set; }
		/// <summary>
		/// 竣工年份
		/// </summary>
		public int CompletionYear { get; set; }
		/// <summary>
		/// 主力面积
		/// </summary>
		public double MainArea { get; set; }
		/// <summary>
		/// 有无电梯
		/// </summary>
		public string IsHaveElevator { get; set; }
		/// <summary>
		/// 地上层数
		/// </summary>
		public int Floors { get; set; }
		/// <summary>
		/// 物业档次
		/// </summary>
		public string PropertyLevel { get; set; }
		/// <summary>
		/// 主力面积基价
		/// </summary>
		public double MainAreaBasePrice { get; set; }
		/// <summary>
		/// 小区价格说明
		/// </summary>
		public string RAPriceMemo { get; set; }
		/// <summary>
		/// 价值时点
		/// </summary>
		public DateTime ValuePoint { get; set; }












	}
}
