 
using Microsoft.AspNetCore.Http;

namespace LC.SDK.Core.Framework
{
    public interface IApplicationContext
    {
        #region Common Property
        IHttpContextAccessor ContextAccessor { get; set; }
        HttpContext HttpContext { get; }

        string CurrentUrl { get; }
        #endregion

    }
}