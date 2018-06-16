using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace Peter.Interceptor
{ 
    public class Interceptor1: StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine(string.Format("[Interceptor1]PreProceed调用开始方法：{0} ", invocation.Method.Name.ToString()));
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            invocation.Proceed();

           // Console.WriteLine(string.Format("[Interceptor1]PerformProceed调用结果：{0} ", invocation.ReturnValue));

        }

        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine(string.Format("[Interceptor1]PostProceed调用结果方法：{0} ", invocation.Method.Name.ToString()));
        }
    }
}