using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 

namespace Manage.Controllers
{
    public class BaseController: Controller
    {
        /// <summary>
        /// 日记类
        /// </summary>

        protected readonly ILogger<BaseController> _logger;
        //protected IUnitOfWork UOW;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
            //UOW = unitOfWork;
        }
    }
}