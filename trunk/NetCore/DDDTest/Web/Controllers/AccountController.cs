using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Users;
using Model.Users;
using Manage.Models;
using Web.Framework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Manage.Controllers
{
    //[Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        IUserService _userService;
        IApplicationContext _applicationContext;

        [TempData]
        public string ErrorMessage { get; set; }

        public AccountController(ILogger<BaseController> logger,
            IApplicationContext applicationContext,
            IUserService userService) : base(logger)
        {
            _userService = userService;
            _applicationContext = applicationContext;
        }

        [AllowAnonymous]
        [HttpGet()]
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
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    _logger.LogInformation($"登录：UserName={model.Username}");
                    var userInfo = _userService.GetUser(model.Username, model.Password);
                    if (userInfo != null)
                    {
                        SignIn(userInfo);
                        return new RedirectResult(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
                    }
                    else
                    {
                        ViewBag.error = "用户名或者密码错误";
                        return new ViewResult();
                    }
                }
            }
            catch (Exception exc)
            {
                ViewBag.error = exc.Message;
                _logger.LogCritical(exc, $"登录异常：{ exc.Message}");
                return new ViewResult();
            }
            return new ViewResult();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"{User.Identity.Name}登出");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Password = model.Password };
                user.OrgId = _applicationContext.CurrentAdminOrg.Id;
                user = _userService.AddUser(user, _applicationContext.CurrentUrl);

                if (user != null)
                {
                    _logger.LogInformation("User created a new account with password.");
                    SignIn(user);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return new RedirectResult(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region tools

        /// <summary>
        /// TODO 去掉Role的硬编码
        /// </summary>
        /// <param name="userInfo"></param>
        private void SignIn(User userInfo)
        {
            if (userInfo != null)
            {
                var claims = new Claim[]
               {
                            new Claim(ClaimTypes.Role,"Manager"),
                            new Claim(ClaimTypes.Name,userInfo.UserName),
                            new Claim(ClaimTypes.Sid,userInfo.Id.ToString()),
                            new Claim(ClaimTypes.GroupSid,userInfo.Id.ToString()),
               };
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims)));
            }
        }
        #endregion
    }
}
