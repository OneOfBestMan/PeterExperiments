using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;


namespace DataTableTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // List<DataTable> result = GetTable().AsEnumerable()
            //.GroupBy(row => row.Field<int>("MIVID"))
            //.Select(g => g.CopyToDataTable())
            //.ToList();

            DataTable tb = GetTable();
            var insertTable = tb.Where<string>("Id", a => string.IsNullOrEmpty(a));
            var updateTable= tb.Where<string>("Id", a => !string.IsNullOrEmpty(a));
            foreach (DataRow row in insertTable.Rows)
            {
                Console.WriteLine(row["Id"]+":"+row["Name"]+":"+row["Name1"]);
            }
            foreach (DataRow row in updateTable.Rows)
            {
                Console.WriteLine(row["Id"] + ":" + row["Name"] + ":" + row["Name1"]);
            }
            Console.Read();
        }



        public static DataTable GetTable()
        {
            DataTable tb = CreateTable();
            DataRow row = tb.NewRow();
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    row = tb.NewRow();
                    row["Id"] = "";
                    row["Name"] = "hahah" + i.ToString();
                    row["Name1"] = "akjaks" + i.ToString();
                    tb.Rows.Add(row);
                }
                else
                {
                    row = tb.NewRow();
                    row["Id"] = i.ToString();
                    row["Name"] = "hahah" + i.ToString();
                    row["Name1"] = "akjaks" + i.ToString();
                    tb.Rows.Add(row);
                }
            }
            return tb;
        }

        private static DataTable CreateTable()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("Id");
            tb.Columns.Add("Name");
            tb.Columns.Add("Name1");
            return tb;
        }
    }
}
