using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace CompressSample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGZipCompressFile();

            //string str = "abssssdddssssssssss11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";
            //string orgStr = str;
            //string result = "";

            ////将传入的字符串直接进行压缩，解压缩，会出现乱码。
            ////先转码，然后再压缩。解压缩：先解压缩，再反转码
            //Console.WriteLine("源字符串为：{0}", str);            
            //result = GZipCompress.Compress(str);
            //Console.WriteLine("压缩后为：{0}", result);

            //Console.Write("压缩前：{0},压缩后：{1}", str.Length, result.Length);

            //Console.WriteLine("开始解压...");
            //Console.WriteLine("解压后：{0}", result);
            //result = GZipCompress.Decompress(result);
            //Console.WriteLine("解压后与源字符串对比，是否相等：{0}", result == orgStr);


            //Console.WriteLine("源字符串为：{0}", str);
            //result = ZipComporessor.Compress(str);
            //Console.WriteLine("压缩后为：{0}", result);

            //Console.Write("压缩前：{0},压缩后：{1}", str.Length, result.Length);

            //Console.WriteLine("开始解压...");
            //Console.WriteLine("解压后：{0}", result);
            //result = ZipComporessor.Decompress(result);
            //Console.WriteLine("解压后与源字符串对比，是否相等：{0}", result == orgStr);

            //Console.WriteLine("输入任意键，退出！");
            Console.ReadKey();
        }
        /// <summary>
        /// 测试GZipCompress压缩文件
        /// </summary>
        public static void TestGZipCompressFile()
        {
            string filePath = @"D:\项目\4.自己项目\PeterExperiment\trunk\Peter.Experiment\CompressSample\CompressSample\Temp";
            var fiels = Directory.GetFiles(filePath);
           // FileInfo fileToCompress = new FileInfo(filePath);
            ZipComporessor comporessor = new ZipComporessor();
            var path = @"D:\项目\4.自己项目\PeterExperiment\trunk\Peter.Experiment\CompressSample\CompressSample\Temp\test.zip";
            string outError = "";
            comporessor.ZipFiles(fiels, path,out outError);
            Console.WriteLine(outError);



            //GZipCompress.Compress(fileToCompress);

            //FileInfo fileToDecompress = new FileInfo(@"D:\项目\4.自己项目\PeterExperiment\trunk\Peter.Experiment\CompressSample\CompressSample\Temp\test.txt.gz");
            //GZipCompress.Decompress(fileToDecompress);
        }
    }
}
