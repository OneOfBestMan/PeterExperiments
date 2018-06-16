using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //string RegexStr = string.Empty;


            //RegexStr = "^[0-9]+$"; //匹配字符串的开始和结束是否为0-9的数字[定位字符]
            //Console.WriteLine("判断'R1123'是否为数字:{0}", Regex.IsMatch("R1123", RegexStr));
            //Console.WriteLine("判断'1123'是否为数字:{0}", Regex.IsMatch("1123", RegexStr));

            //RegexStr = @"\d+"; //匹配字符串中间是否包含数字(这里没有从开始进行匹配噢,任意位子只要有一个数字即可)
            //Console.WriteLine("'R1123'是否包含数字:{0}", Regex.IsMatch("R1123", RegexStr));
            //Console.WriteLine("'博客园'是否包含数字:{0}", Regex.IsMatch("博客园", RegexStr));

            ////感谢@zhoumy的提醒..已修改错误代码
            //RegexStr = @"^Hello World[\w\W]*"; //已Hello World开头的任意字符(\w\W：组合可匹配任意字符)
            //Console.WriteLine("'HeLLO WORLD xx hh xx'是否已Hello World开头:{0}", Regex.IsMatch("HeLLO WORLD xx hh xx", RegexStr, RegexOptions.IgnoreCase));
            //Console.WriteLine("'LLO WORLD xx hh xx'是否已Hello World开头:{0}", Regex.IsMatch("LLO WORLD xx hh xx", RegexStr, RegexOptions.IgnoreCase));
            ////RegexOptions.IgnoreCase：指定不区分大小写的匹配。


            //string RegexStr = string.Empty;


            //string LinkA = "<a href=\"http://www.baidu.com\" target=\"_blank\">百度</a>";

            //RegexStr = @"href=""[\S]+"""; // ""匹配"
            //Match mt = Regex.Match(LinkA, RegexStr);

            //Console.WriteLine("{0}。", LinkA);
            //Console.WriteLine("获得href中的值：{0}。", mt.Value);

            //RegexStr = @"<h[^23456]>[\S]+<h[1]>"; //<h[^23456]>:匹配h除了2,3,4,5,6之中的值,<h[1]>:h匹配包含括号内元素的字符
            //Console.WriteLine("{0}。GetH1值：{1}", "<H1>标题<H1>", Regex.Match("<H1>标题<H1>", RegexStr, RegexOptions.IgnoreCase).Value);
            //Console.WriteLine("{0}。GetH1值：{1}", "<h2>小标<h2>", Regex.Match("<h2>小标<h2>", RegexStr, RegexOptions.IgnoreCase).Value);
            ////RegexOptions.IgnoreCase:指定不区分大小写的匹配。

            //RegexStr = @"ab\w+|ij\w{1,}"; //匹配ab和字母 或 ij和字母
            //Console.WriteLine("{0}。多选结构：{1}", "abcd", Regex.Match("abcd", RegexStr).Value);
            //Console.WriteLine("{0}。多选结构：{1}", "efgh", Regex.Match("efgh", RegexStr).Value);
            //Console.WriteLine("{0}。多选结构：{1}", "ijk", Regex.Match("ijk", RegexStr).Value);

            //RegexStr = @"张三?丰"; //?匹配前面的子表达式零次或一次。
            //Console.WriteLine("{0}。可选项元素：{1}", "张三丰", Regex.Match("张三丰", RegexStr).Value);
            //Console.WriteLine("{0}。可选项元素：{1}", "张丰", Regex.Match("张丰", RegexStr).Value);
            //Console.WriteLine("{0}。可选项元素：{1}", "张飞", Regex.Match("张飞", RegexStr).Value);

            //string RegexStr = string.Empty;
            //string f = "fooot";
            ////贪婪匹配
            //RegexStr = @"f[o]+";
            //Match m1 = Regex.Match(f, RegexStr);
            //Console.WriteLine("{0}贪婪匹配(匹配尽可能多的字符)：{1}", f, m1.ToString());

            ////懒惰匹配
            //RegexStr = @"f[o]+?";
            //Match m2 = Regex.Match(f, RegexStr);
            //Console.WriteLine("{0}懒惰匹配(匹配尽可能少重复)：{1}", f, m2.ToString());

            //string RegexStr = string.Empty;
            //string TaobaoLink = "<a href=\"http://www.taobao.com\" title=\"淘宝网 - 淘！我喜欢\" target=\"_blank\">淘宝</a>";
            ////RegexStr = @"<a[^>]+href=""(\S+)""[^>]+title=""([\s\S]+?)""[^>]+>(\S+)</a>";
            //RegexStr = @"<a[^>]+href=""(\S+)""[^>]+title=""([\s\S]+?)""[^>]+>(\S+)</a>";
            //Match mat = Regex.Match(TaobaoLink, RegexStr);
            //for (int i = 0; i < mat.Groups.Count; i++)
            //{
            //    Console.WriteLine("第" + i + "组：" + mat.Groups[i].Value);
            //}




            //string RegexStr = string.Empty;

            //string Resume = "基本信息姓名:CK|求职意向:.NET软件工程师|性别:男|学历:本专|出生日期:1988-08-08|户籍:湖北.孝感|E - Mail:9245162@qq.com|手机:15000000000";
            //RegexStr = @"姓名:(?<name>[\S]+)\|\S+性别:(?<sex>[\S]{1})\|学历:(?<xueli>[\S]{1,10})\|出生日期:(?<Birth>[\S]{10})\|[\s\S]+手机:(?<phone>[\d]{11})";
            //Match matc = Regex.Match(Resume, RegexStr);
            //Console.WriteLine("姓名：{0},手机号：{1}", matc.Groups["name"].ToString(), matc.Groups["phone"].ToString());


            //       string RegexStr = string.Empty;
            //       string PageInfo = @"<hteml>
            // <div id=""div1"">
            //  <a href=""http://www.baidu.con"" target=""_blank"">百度</a>
            //  <a href=""http://www.taobao.con"" target=""_blank"">淘宝</a>
            //  <a href=""http://www.cnblogs.com"" target=""_blank"">博客园</a>
            //  <a href=""http://www.google.con"" target=""_blank"">google</a>
            // </div>
            // <div id=""div2"">
            //  <a href=""/zufang/"">整租</a>
            //  <a href=""/hezu/"">合租</a>
            //  <a href=""/qiuzu/"">求租</a>
            //  <a href=""/ershoufang/"">二手房</a>
            //  <a href=""/shangpucz/"">商铺出租</a>
            // </div>
            //</hteml>";
            //       RegexStr = @"<a[^>]+href=""(?<href>[\S]+?)""[^>]*>(?<text>[\S]+?)</a>";
            //       MatchCollection mc = Regex.Matches(PageInfo, RegexStr);
            //       foreach (Match item in mc)
            //       {
            //           Console.WriteLine("href:{0}--->text:{1}", item.Groups["href"].ToString(), item.Groups["text"].ToString());
            //       }


            // string RegexStr = string.Empty;
            //string PageInputStr = "靠.TMMD,今天真不爽....";
            //RegexStr = @"靠|TMMD|妈的";
            //Regex rep_regex = new Regex(RegexStr);
            //Console.WriteLine("用户输入信息：{0}", PageInputStr);
            //Console.WriteLine("页面显示信息：{0}", rep_regex.Replace(PageInputStr, "***"));

            //string SplitInputStr = "1xxxxx.2ooooo.3eeee.4kkkkkk.";
            //RegexStr = @"\d";
            //Regex spl_regex = new Regex(RegexStr);
            //string[] str = spl_regex.Split(SplitInputStr);
            //foreach (string item in str)
            //{
            //    Console.WriteLine(item);
            //}

            //RegexStr = @"(?i)<a .*?href=\""([^\""]+)\""[^>]*>(.*?)</a>";
            //string PageInfo = @"<hteml>
            // <div id=""div1"">
            //  <a href=""http://www.baidu.con"" target=""_blank"">百度</a>
            //  <a href=""http://www.taobao.con"" target=""_blank"">淘宝</a>
            //  <a href=""http://www.cnblogs.com"" target=""_blank"">博客园</a>
            //  <a href=""http://www.google.con"" target=""_blank"">google</a>
            // </div>
            // <div id=""div2"">
            //  <a href=""/zufang/"">整租</a>
            //  <a href=""/hezu/"">合租</a>
            //  <a href=""/qiuzu/"">求租</a>
            //  <a href=""/ershoufang/"">二手房</a>
            //  <a href=""/shangpucz/"">商铺出租</a>
            // </div>
            //</hteml>";
            //MatchCollection mc = Regex.Matches(PageInfo, RegexStr);
            //foreach (Match item in mc)
            //{
            //    Console.WriteLine("href:{0}", item.Value);
            //}

            string RegexStr = @"\p{Lu}";
            string toMatch = "Long Long Ago";
            //Display(RegexStr, toMatch);



           // RegexStr = @"\w\s";
           // toMatch = "ID A1.3";
           //// Display( RegexStr, toMatch);

           // RegexStr = @"\s\S";
           // toMatch = "int __ctr";
           // Display(RegexStr, toMatch);

            RegexStr = @"\G\(\d\)";
            toMatch = "(1)(3)(5)[7](9)";
           // Display(RegexStr, toMatch);

            RegexStr = @"\A\w{4}";
            toMatch = "Code-007-";
            //Display(RegexStr, toMatch);



            RegexStr = @"-\d{3}\Z";
            toMatch = "Bond-901-007";
            //Display(RegexStr, toMatch);


            RegexStr = @"-\d{3}\z";
            toMatch = "-901-333";
           // Display(RegexStr, toMatch);


            RegexStr = @"(\w)\1";
            toMatch = "deep";
           // Display(RegexStr, toMatch);


            RegexStr = @"(?<double>\w)\k<double>";
            toMatch = "deep";
           // Display(RegexStr, toMatch);


            RegexStr = @"(((?'Open'\()[^\(\)]*)+((?'Close-Open'\))[^\(\)]*)+)*(?(Open)(?!))$";
            toMatch = "3+2^((1-3)*(3-1))";
            Display(RegexStr, toMatch);

            Console.Read();
        }

        //  \k 

        private static void Display( string RegexStr,string toMatch)
        {
            
           var matches = Regex.Matches(toMatch, RegexStr);
            foreach (Match item in matches)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}
