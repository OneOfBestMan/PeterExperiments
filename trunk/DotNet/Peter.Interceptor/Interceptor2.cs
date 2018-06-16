using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace Peter.Interceptor
{
    public class Interceptor2 : IInterceptor
    {
        #region Interceptor2成员

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine(string.Format("[Interceptor2]Intercept调用方法：{0} ", invocation.Method.Name));
            invocation.Proceed();
            Console.WriteLine(string.Format("[Interceptor2]Intercept调用结果：{0} ", invocation.ReturnValue));
        }

        #endregion
    }

}