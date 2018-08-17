using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog.Web;

namespace LC.SDK.Core.Framework
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        readonly IHostingEnvironment _env;
        NLog.Logger logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            logger.Error(context.Exception, "action error");
        }
    }
}
