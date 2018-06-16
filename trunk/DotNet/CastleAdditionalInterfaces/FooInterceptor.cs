using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CastleAdditionalInterfaces
{
   public class FooInterceptor:IInterceptor
    {
       private readonly IFooService _proxyObject;

       public FooInterceptor()
       { }
       public FooInterceptor(IFooService service)
       {
           _proxyObject = service;
       }


        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("calling service now");
            invocation.Method.Invoke(_proxyObject,invocation.Arguments);
        }
    }
}
