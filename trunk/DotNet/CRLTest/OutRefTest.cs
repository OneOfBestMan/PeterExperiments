using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRLTest
{


    /// <summary>
    /// 普通方法的参数，引用类型传递的是指针，指针指向的是对象的内存地址
    /// 1:传递的参数的字段被修改的话，调用的方法内的属性也是被修改的
    /// 2：传的参数的被赋予一个新的对象（新的地址），则新对象的修改，不会对调用方法的对象的属性
    /// 产生影响。（传递的只是一个对象的指针地址的复制品）
    /// 3：按照引用传递一个对象，传递的是指针的内存，被调用方法将操纵这个指针。（包括把这个指针指向另外一个新的对象）
    /// </summary>
    public class OutRefTest
    {


        public static void baseMethod(TestClass pa)
        {
            pa.PrivateValue = "lllll";
        }

        public static void SetType(TestClass pa)
        {
            pa = new TestClass();
            pa.PrivateValue = "lllll";
        }
        public static void SetType(out TestClass pa)
        {
            pa = new TestClass();
            pa.PrivateValue = "sdfsdf";
        }


        static void Main(string[] args)
        {
            TestClass c2 = new TestClass();
            baseMethod(c2);
            TestClass c1 = new TestClass();
            c1.PrivateValue = "2222";
            SetType(c1);

            TestClass c=new TestClass();  
            SetType(out c);
            System.Console.WriteLine(c2.PrivateValue);
            System.Console.WriteLine(c1.PrivateValue);
            
            System.Console.WriteLine(c.PrivateValue);
         
            Console.ReadKey();
        }
    }

    public class TestClass
    {
        public string PrivateValue = "I have a dream.";
    }
}
