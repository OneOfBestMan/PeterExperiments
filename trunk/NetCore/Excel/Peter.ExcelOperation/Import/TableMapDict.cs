using System;
using System.Collections.Generic;
using System.Text;

namespace Peter.ExcelOperation
{
    public static class TableMapDict
    {
        public static Dictionary<string, string> Mapping
        {
            get
            {
                var map = new Dictionary<string, string>();
                map.Add("Id", "Id");
                map.Add("名称", "Name");
                map.Add("年龄", "Age");
                map.Add("小区名称", "RAName");
                map.Add("小区地址", "RAAdress");
                map.Add("竣工年份", "CompletionYear");
                map.Add("主力面积", "MainArea");
                map.Add("有无电梯", "IsHaveElevator");
                map.Add("地上层数", "Floors");
                map.Add("物业档次", "PropertyLevel");
                map.Add("主力面积基价", "MainAreaBasePrice");
                map.Add("小区价格说明", "RAPriceMemo");
                map.Add("价值时点", "ValuePoint");
                return map;
            }
        }
    }
}
