using Business.Core;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peter.Interceptor
{
    /// <summary>
    /// http://www.cnblogs.com/chenzhao/articles/2083284.html
    /// Castle AOP配置(多个拦截器的配置）拦截
    /// 1： 拦截器是注入到容器中，所以调用的时候，要用容器Resove那个对象才能实现拦截
    /// 2： 需要被拦截的方法需要是虚方法
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 拦截器
        /// 1：获取一个容器
        /// 2：在容器中注册接口和类，并且把需要拦截的类，添加拦截器
        /// 3：容器的核心对象的组件注册委托中添加拦截器注入，
        ///    即IHandler对象的ComponentModel属性的拦截器列表中添加需要注入的拦截器
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //IWindsorContainer container1 = new WindsorContainer(new XmlInterpreter());
            //IBllDealOrder bllOrder = container1.Resolve<IBllDealOrder>();
            //Console.WriteLine(bllOrder.Deal("jackychen"));
            //Console.Read(); 

            //IWindsorContainer container1 = new WindsorContainer(new XmlInterpreter());
            IWindsorContainer container1 = new WindsorContainer();
            container1.Register(new RegistInterface());
            InterceptorRegistrar.Initialize(container1);
            IBllGetOrder bllOrder = container1.Resolve<IBllGetOrder>();
            Console.WriteLine(bllOrder.Get("jackychen"));
            Console.Read();
        }
    }

    public class RegistInterface : IRegistration
    {
        public void Register(Castle.MicroKernel.IKernelInternal kernel)
        {
            kernel.Register(
                Component.For<Interceptor1>(),
                 Component.For<Interceptor2>(),
                Component.For<IBllGetOrder, bllGetOrder2>().ImplementedBy<bllGetOrder2>().Interceptors<Interceptor2, Interceptor1>()
                );
        }
    }

    internal static class InterceptorRegistrar
    {
        public static void Initialize(IWindsorContainer IocContainer)
        {
            IocContainer.Kernel.ComponentRegistered += ComponentRegistered;
        }

        private static void ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(IBllGetOrder).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(Interceptor1)));
            }


        }
    }
}
