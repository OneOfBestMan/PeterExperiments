using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using CalculatorLib;
using System.Reflection;

namespace TestDynamicMethod
{
    /// <summary>
    /// http://www.cnblogs.com/chenzhao/articles/2081784.html
    /// Castle AOP 多个拦截器的动态代理（四)
    /// 1：获取拦截器生成器，ProxyGenerator
    /// 2：为对象获取动态代理，通过拦截器生成器的CreateClassProxy方法创建动态代理（本质是把拦截器注入到Ihandle对象）
    /// 3：调用对象的方法
    /// </summary>
    class Program
    {
        static void Main2(string[] args)
        {
            var pg = GetProxySimple();
            //ICalculator cal = pg.CreateClassProxy(typeof(Calculator), new PermissionInterceptor(), new ModifyInterceptor()) as ICalculator;
            ICalculator cal = pg.CreateClassProxy<Calculator>(new PermissionInterceptor(), new ModifyInterceptor()) ;
            Console.WriteLine("当前类型:{0},父类型:{1}", cal.GetType(), cal.GetType().BaseType);  
            Console.WriteLine("待计算表达式为：1 + 3 = ?,方法最初传入参数为:(1,1)\n");
            Console.WriteLine("表达式:1 + 3 = {0}\n", cal.AddOperation(1, 1));

           // scope.SaveAssembly(false);
           // scope.SaveAssembly(false);
            Console.ReadKey();
        }

        #region get proxy generator
        public static ProxyGenerator GetProxySimple()
        {
            return new ProxyGenerator();
        }
        public static ProxyGenerator GetProxyFull()
        {
            String path = AppDomain.CurrentDomain.BaseDirectory;
            ModuleScope scope = new ModuleScope(true, true, "Invocation", path + "\\Invocation.dll", "Proxy", path + "\\Proxy.dll");
            DefaultProxyBuilder builder = new DefaultProxyBuilder(scope);
            return new ProxyGenerator(builder);
        }
        #endregion
    }

    # region Interceptor
    public class PermissionInterceptor : StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine("申请更改参数和返回值的权限....\n");
        }
        //protected override void PerformProceed(IInvocation invocation)
        //{
        //    Console.WriteLine("正在计算....\n");
        //    base.Intercept(invocation);
        //}
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("收回更改参数和返回值的权限....\n");
        }
    }

    public class ModifyInterceptor : StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine("----------------------Begin----------------------------\n");
            Console.WriteLine("对参数值进行拦截\n");
            invocation.SetArgumentValue(0, 2);
            invocation.SetArgumentValue(1, 1);
            Console.WriteLine("方法参数为:(2,1)\n");
            Console.WriteLine("----------------------End----------------------------\n");
        }

        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("----------------------Begin----------------------------\n");
            Console.WriteLine("对返回值进行拦截并加1\n");
            invocation.ReturnValue = Int32.Parse(invocation.ReturnValue.ToString()) + 1;
            Console.WriteLine("----------------------End----------------------------\n");
        }
    }

    #endregion
}

namespace CalculatorLib
{
    public interface ICalculator
    {
        Int32 AddOperation(Int32 p1, Int32 p2);
    }

    public class Calculator : ICalculator
    {
        public virtual Int32 AddOperation(Int32 p1, Int32 p2)
        {
            return p1 + p2;
        }
    }
}