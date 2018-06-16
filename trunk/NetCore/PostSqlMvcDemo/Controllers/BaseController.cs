using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreTest.Models;

namespace NetCoreTest.Controllers
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