using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRLTest.VirtualMethod
{
    class Program
    {
        static void Main2(string[] args)
        {

            Person p = new Person("test1");
            p = Person.Find("Aseven");
            int Age = p.GetAge();
            Console.WriteLine("age:"+Age);
            p.Say();
            Console.ReadKey();

        }
    }
    public class Person
    {
        private string _name;
        private int _age;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public virtual void Say()
        {
            Console.WriteLine("******");
        }
        public static Person Find(string name)
        {
            return new Chinese(name);//模拟数据库查找
        }
        public int GetAge()
        {
            return _age;
        }
        public Person() { }
        public Person(string name)
        {
            this._name = name;
        }
    }

    public class Chinese : Person
    {
        public Chinese(string name)
        {
            this.Name = name;
        }
        public override void Say()
        {
            Console.WriteLine("你好！");
        }
    }
    public class American : Person
    {
        public American(string name)
        {
            this.Name = name;
        }
        public override void Say()
        {
            Console.WriteLine("Hello！");
        }
    }
}
