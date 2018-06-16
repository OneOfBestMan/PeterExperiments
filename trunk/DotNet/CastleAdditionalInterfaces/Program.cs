
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Reflection;

namespace CastleAdditionalInterfaces
{
    /// <summary>
    /// 首先见下图（图一），其中FooController是一个没有实现任何Interface的空类。需要实现的效果是：通过FooController对象调用FooService的Do方法。
    /// 设置这一不常见的场景主要是为了说明Castle中AdditionalInterfaces的用法。
    /// 这个场景诡异的地方在于FooController是一个空类，其类和对象都没什么可供调用的？
    /// 假如FooController也有一个Do方法，那么通过Castle给FooController对象添加拦截器，就可以轻松实现上述的效果。
    /// 基于上述分析，方案分两步走：
    /// 第一步， 安装常规的方法创建拦截器，并通过拦截器调用FooService的Do方法。拦截器代码见图二。
    /// 第二步， 动态的给FooController添加Do方法。在Castle中有两种做法，一种是使用castle的mixin方式，网上已有相关用法的介绍。 
    /// 本文着重介绍另一种使用方式：AdditionalInterfaces。
    /// 使用其实很简单（如图三）：Component.For(typeof(FooController)).Proxy.AdditionalInterfaces(typeof(IFooService))。 
    /// 意思就是给FooController类型的代理类添加一个接口IFooService。
    /// 最后调用（图三）， 通过  var obj = container.Resolve<FooController>(); 
    /// 获取FooController的FooController的代理类对象，然后通过反射调用其Do方法（因为代理类实现了接口IFooService）。
    /// 最后拦截器拦截代理类对象的Do方法，完成真正调用FooService的Do方法实名。
    /// 代理类是如何实现接口的？ ToA
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<FooService, IFooService>(),
                Component.For(typeof(FooInterceptor)).LifestyleTransient(),
                Component.For(typeof(FooController)).Proxy.AdditionalInterfaces(typeof(IFooService)).Interceptors(typeof(FooInterceptor)).LifestyleTransient()
                );
            var obj = container.Resolve<FooController>();
            MethodInfo method = obj.GetType().GetMethod("Do");
            method.Invoke(obj, null);
            System.Console.ReadKey();
        }
    }
}
