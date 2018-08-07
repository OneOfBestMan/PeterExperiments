using System.Security.Claims;
using System.Threading.Tasks;
using Application.System;
using Model;
using Model.System;
using Model.Users;
using Utiles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace Manage.Controllers
{
    public class LoginController : Controller
    {
        public JsonTip tip = new JsonTip();
        ISystemService _systemService;

        public LoginController(ISystemService userService)
        {
            _systemService = userService;
        }

        #region 登录页面
        public IActionResult Index()
        {
            string key = Utils.GetRandomChar(12);
            ViewBag.key = key;
            return View();
        }
        #endregion

        #region 低版本IE界面
        //判断低版本IE
        public IActionResult Ie()
        {
            return View();
        }
        #endregion

        #region 执行登录
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login()
        {
            string strUserName = Request.Form["username"];
            string strPassWord = Request.Form["password"];
            //验证用户
            if (string.IsNullOrEmpty(strUserName))
            {
                tip.Message = "请输入用户名！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(strPassWord))
            {
                tip.Message = "请输入密码！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(strPassWord) || Utils.GetStringLength(strPassWord) < 5)
            {
                tip.Message = "登录密码不能为空或者长度小于5！";
                return Json(tip);
            }
            //执行登录操作
            var loginResult = _systemService.AdminLogin(strUserName, strPassWord);
            if (string.IsNullOrEmpty(loginResult.Message))
            {
                SignIn(loginResult);
                tip.Status = JsonTip.SUCCESS;
                tip.Message = "登录成功";
                tip.ReturnUrl = "/";
                return Json(tip);
            }
            else
            {
                tip.Message = "用户名或者密码错误！请重新登录！";
                return Json(tip);
            }
        }
        #endregion


        #region 退出登录
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/Login");
        }
        #endregion

        #region tools

        /// <summary>
        /// TODO 去掉Role的硬编码
        /// </summary>
        /// <param name="userInfo"></param>
        private void SignIn(AdminLoginOutput loginResult)
        {
            if (loginResult != null && loginResult.LoginAccount != null && loginResult.LoginRole != null)
            {
                var claims = new Claim[]
               {
                            new Claim(ClaimTypes.Role,loginResult.LoginRole.Key),
                            //new Claim(ClaimTypes.Role,loginResult.LoginAccount.RoleId),
                            new Claim(ClaimTypes.Name,loginResult.LoginAccount.UserName),
                            new Claim(ClaimTypes.Sid,loginResult.LoginAccount.Id),
                            new Claim(ClaimTypes.GroupSid,loginResult.LoginAccount.RoleId),
               };
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims)));
            }
        }
        #endregion
    }
}