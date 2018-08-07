using System;
using System.Collections.Generic;
using Application.Users;
using Model;
using Model.Users;
using Utiles;
using Web.Framework;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Manage.Controllers
{
    /// <summary>
    /// 后台用户管理控制器
    /// </summary>
    public class UserController : AdminBaseController
    {
        IUserService _userService;
        IApplicationContext _applicationContext;
        public UserController(IApplicationContext applicationContext, IUserService userService)
        {
            _applicationContext = applicationContext;
            _userService = userService;
        }

        #region 机构角色管理
        public IActionResult UserRole()
        {
            IList<Role> list = _userService.QueryRoles(_applicationContext.CurrentAdminOrg.Id);
            return View(list);
        }

        //添加管理组
        public IActionResult AddUserRole()
        {
            return View();
        }
        //执行添加用户组
        [HttpPost]
        public IActionResult AddUserRole(Role model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                tip.Message = "角色名称不能为空！";
                return Json(tip);
            }
            model.OrgId = _applicationContext.CurrentAdminOrg.Id;
            model.CreationTime = DateTime.Now;
            _userService.AddRole(model);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加角色成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }
        //编辑用户组
        public IActionResult EditUserRole(string id)
        {
            Role entity = _userService.GetRole(id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！", 0, true, "");
            }
            return View(entity);
        }
        [HttpPost]
        public IActionResult EditUserRole(Role model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                tip.Message = "错误参数传递！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                tip.Message = "角色名称不能为空！";
                return Json(tip);
            }
            Role entity = _userService.GetRole(model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录！";
                return Json(tip);
            }
            entity.Name = model.Name;
            entity.Key = model.Key;
            entity.Description = model.Description;
            entity.NotAllowDel = model.NotAllowDel;
            entity.Sort = model.Sort;
            entity.LastModifiedTime = DateTime.Now;
            _userService.EditRole(entity);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑角色成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        //删除用户组
        [HttpPost]
        public IActionResult DelUserRole(string id)
        {
            var entity = _userService.GetRole(id);
            if (entity == null)
            {
                tip.Message = "系统找不到本角色详情！";
                return Json(tip);
            }
            if (entity.NotAllowDel)
            {
                tip.Message = "此角色设定不允许删除，如果需要删除，请先解除限制！";
                return Json(tip);
            }

            //删除角色，并删除旗下所有管理员
            _userService.DeleteRole(id);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除角色成功";
            return Json(tip);
        }

        #endregion

        #region 用户管理
        public IActionResult Users()
        {
            //加载用户组
            IList<Role> list = _userService.QueryRoles(_applicationContext.CurrentAdminOrg.Id);
            ViewBag.RoleList = list;
            return View();
        }
        public IActionResult GetUsers(string keyword, int page = 1, int limit = 20, string roleid = "")
        {
            var input = new SearchUserInput()
            {
                Keywords = keyword,
                RoleId = roleid,
                PageIndex = page,
                PageSize = limit,
                OrgId = _applicationContext.CurrentAdminOrg.Id
            };
            var output = _userService.SearchUser(input);
            return Content(JsonConvert.SerializeObject(new { total = output.Total, rows = output.Users }), "text/plain");
        }
        //查看，编辑用户信息
        public IActionResult AddUser()
        {
            IList<Role> list = _userService.QueryRoles(_applicationContext.CurrentAdminOrg.Id);
            ViewBag.RoleList = list;
            //ViewBag.allmember = Member.FindAll(Member._.Id > 0 & Member._.IsLock != 1, null, null, 0, 0);
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public IActionResult AddUser(User model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                tip.Message = "用户名为空！";
                return Json(tip);
            }
            //判断用户是否存在
            if (_userService.IsExistUser(_applicationContext.CurrentAdmin.OrgId, model.UserName))
            {
                tip.Message = "用户名已经存在！";
                return Json(tip);
            }

            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 6)
            {
                tip.Message = "密码必须不小于6个字符";
                return Json(tip);
            }

            if (!string.IsNullOrEmpty(model.Email) && !Utils.IsValidEmail(model.Email))
            {
                tip.Message = "邮箱格式错误！";
                return Json(tip);
            }
            if (string.IsNullOrEmpty(model.RoleId))
            {
                tip.Message = "请选择一个橘色！";
                return Json(tip);
            }
            //执行添加

            model.Salt = Utils.GetRandomChar(20);
            model.Password = Utils.MD5(model.Salt + model.Password);
            model.OrgId = _applicationContext.CurrentAdminOrg.Id;
            model.CreationTime = DateTime.Now;
            _userService.AddUser(model, _applicationContext.CurrentUrl);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "添加用户成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }


        //查看，编辑用户信息
        public IActionResult EditUser(string id)
        {
            IList<Role> list = _userService.QueryRoles(_applicationContext.CurrentAdminOrg.Id);
            ViewBag.RoleList = list;

            var entity = _userService.GetById(id);
            if (entity == null)
            {
                return EchoTipPage("系统找不到本记录！");
            }
            // ViewBag.allmember = Member.FindAll(Member._.Id > 0 & Member._.IsLock != 1 & Member._.Id != entity.Id, null, null, 0, 0);
            return View(entity);
        }
        [HttpPost]
        public IActionResult EditUser(User model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                tip.Message = "错误请求";
                return Json(tip);
            }
            User entity = _userService.GetById(model.Id);
            if (entity == null)
            {
                tip.Message = "系统找不到本记录";
                return Json(tip);
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 6)
                {
                    tip.Message = "密码必须不小于6个字符";
                    return Json(tip);
                }
                string PassWord2 = Request.Form["PassWord2"];
                if (model.Password != PassWord2)
                {
                    tip.Message = "两次输入密码不一致，请重新输入！";
                    return Json(tip);
                }
                model.Password = Utils.MD5(entity.Salt + model.Password);
            }
            if (string.IsNullOrEmpty(model.UserName))
            {
                tip.Message = "用户名为空！";
                return Json(tip);
            }
            //判断用户是否存在
            if (_userService.IsExistUser(model.Id, model.UserName))
            {
                tip.Message = "用户名已经存在，请重新填写一个！";
                return Json(tip);
            }
            _userService.EditUser(model);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "编辑用户成功";
            tip.ReturnUrl = "close";
            return Json(tip);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DelUser(string id)
        {
            var he = _userService.GetById(id);
            if (he == null)
            {
                tip.Message = "系统找不到本用户！";
                return Json(tip);
            }
            _userService.DeleteUser(id);
            tip.Status = JsonTip.SUCCESS;
            tip.Message = "删除用户成功！";
            return Json(tip);
        }
        #endregion




    }
}