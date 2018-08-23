
using Microsoft.AspNetCore.Http;

namespace LC.SDK.Core.Framework
{
    /// <summary>
    /// Web上下文
    /// </summary>
    public class ApplicationContext : IApplicationContext
    {
        IHttpContextAccessor _contextAccessor;

        public ApplicationContext(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;

        }
        #region Common Property
        public IHttpContextAccessor ContextAccessor { get => _contextAccessor; set => _contextAccessor = value; }
        public HttpContext HttpContext { get { return ContextAccessor.HttpContext; } }

        public string CurrentUrl
        {
            get
            {
               // PathString requestPath = context.Request.PathBase + context.Request.Path;
                return string.Format("{0}://{1}{2}{3}", HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, HttpContext.Request.QueryString);
            }
        }
        #endregion

        #region web
        public string UserName
        {
            get
            {
                var userName = "";
                // Figure out the user's identity
                if (HttpContext != null)
                {
                    if (HttpContext.User != null)
                    {
                        var identity = HttpContext.User.Identity;

                        if (identity != null && identity.IsAuthenticated)
                        {
                            userName = identity.Name;
                        }
                    }
                }
                return userName;
            }
        }

        #endregion


    }
}
