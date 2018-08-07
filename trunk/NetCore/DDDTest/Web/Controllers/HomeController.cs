using System.Collections.Generic;
using System.Linq;
using Application.System;
using Application.Web;
using Model.System;
using Web.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Manage.Controllers
{
    /// <summary>
    /// 后台首页
    /// </summary>
    public class HomeController : AdminBaseController
    {
        private readonly IHostingEnvironment _env;
        ISystemService _systemService;
        IApplicationContext _applicationContext;
        IWebService _webService;
        public HomeController(IHostingEnvironment env, IApplicationContext applicationContext, ISystemService systemService, IWebService webService)
        {
            _env = env;
            _applicationContext = applicationContext;
            _systemService = systemService;
            _webService = webService;

        }
        #region 后台首页
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (_applicationContext.CurrentAdmin == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Admin = _applicationContext.CurrentAdmin;
            ViewBag.Role = _applicationContext.CurrentAdminRole;
            ViewBag.Org = _applicationContext.CurrentAdminOrg;
            //获取菜单
            List<AdminMenu> list = new List<AdminMenu>();
            var input = new QueryAdminMenuInput();
            input.PId = 0;
            input.MaxLevel = 2;
            input.IsIndentation = false;
            input.ShowHide = false;
            input.IsSystem = _applicationContext.CurrentAdminRole != null && _applicationContext.CurrentAdminRole.Key == "systemmanager";
            list = _systemService.QueryAdminMenu(input).ToList();
            return View(list);
        }
        #endregion

        #region 后台主界面
        public IActionResult Main()
        {
            string remoteip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            string host = Request.HttpContext.Request.Host.Host;
            string port = Request.HttpContext.Connection.RemotePort.ToString();

            ViewBag.remoteip = remoteip;
            ViewBag.port = port;
            ViewBag.host = host;
            ViewBag.contentPath = _env.ContentRootPath;
            ViewBag.rootPath = _env.WebRootPath;
            var firstPage = _webService.GetConsoleFirstPage(_applicationContext.CurrentAdminOrg.Id);
            return View(firstPage);
        }
        #endregion

        #region 显示没权限
        [AllowAnonymous]
        //显示没有权限
        public IActionResult NotAuthorize()
        {
            return View();
        }
        #endregion
    }
}