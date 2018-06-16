using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreTest.Models;
using NetCoreTest.Models.Reposity;

namespace NetCoreTest.Controllers
{
    [Authorize(Roles = "User,Manager")]
    public class HomeController : BaseController
    {
        private IUserReposity _userReposity;
        private IBlogReposity _blogRepository;
         
        public HomeController(ILogger<BaseController> logger,IUserReposity userReposity, IBlogReposity blogRepository) : base(logger)
        {
            _userReposity = userReposity;
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            _logger.LogInformation("登录");
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.returnUrl = returnUrl;
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string userName, string password, string returnUrl)
        {
            try
            {
                _logger.LogInformation($"登录：UserName={userName}");
                var userRole = _userReposity.GetUser(userName, password);
                if (userRole != null)
                {
                    var claims = new Claim[]
                   {
                            new Claim(ClaimTypes.Role,userRole.RoleId.ToString()=="1"?"Manager":"User"),
                            new Claim(ClaimTypes.Name,"刘"),
                            new Claim(ClaimTypes.Sid,userRole.Id.ToString()),
                            new Claim(ClaimTypes.GroupSid,userRole.Id.ToString()),
                   };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims)));
                    return new RedirectResult(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
                }
                else
                {
                    ViewBag.error = "用户名或者密码错误";
                    return new ViewResult();
                }
            }
            catch (Exception exc)
            {
                ViewBag.error = exc.Message;
                _logger.LogCritical(exc, $"登录异常：{ exc.Message}");
                return new ViewResult();
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"{User.Identity.Name}登出");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet("blogs")]
        public IActionResult Blogs(int page = 1)
        {
            var pager = new Pager(page);

            var blogs = _blogRepository.Query(a=>a.Content.Contains("Blog"), pager);
            return View(blogs);
        }
    }
}
