using System;
using System.Collections.Generic;
using System.Linq;
using Application.Web;
using Core;
using Core.Logs;
using Core.Users;
using Model;
using Model.Logs;
using Model.Users;
using Utiles;

namespace Application.Users
{
    public class UserService : ApplicationBaseService, IUserService
    {
        IUserRepository _userRepository;
        IUserLogRepository _userLogRepository;
        IRepository<Role> _roleRepository;
        IEmailService _emailService;


        public UserService(IUserRepository userRepository,
            IUserLogRepository userLogRepository,
            IRepository<Role> repository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _userLogRepository = userLogRepository;
            _roleRepository = repository;
            _emailService = emailService;
        }
        #region 登录

        /// <summary>
        ///  登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns>是否登录成功</returns>
        public UserLoginOutput UserLogin(string userName, string passWord)
        {
            UserLoginOutput output = new UserLoginOutput();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                output.Message = "参数为空";
                return output;
            }

            var entity = _userRepository.Single(a => a.UserName == userName);
            if (entity == null)
            {
                output.Message = "登录失败：用户名错误。";
                return output;
            }
            if (entity.Password == Utils.MD5(entity.Salt + passWord.Trim()))
            {
                output.LoginAccount = entity;
                output.LoginRole = _roleRepository.GetById(entity.RoleId);
            }
            else
            {
                output.Message = "登录失败：密码错误。";
                return output;
            }
            return output;
        }

        public UserLoginOutput UserRegister(UserRegisterInput input)
        {
            UserLoginOutput output = new UserLoginOutput();
            //if (string.IsNullOrEmpty(model.RoleId))
            //{
            //    output.Message = "请选择一个角色！";
            //    return output;
            //}
            if (string.IsNullOrEmpty(input.UserName))
            {
                output.Message = "登录用户名不能为空！";
                return output;
            }
            if (Utils.GetStringLength(input.UserName.Trim()) < 5)
            {
                output.Message = "登录用户名不能小于5个字节！";
                return output;
            }
            if (string.IsNullOrEmpty(input.Password))
            {
                output.Message = "密码不能为空！";
                return output;
            }
            if (input.Password.Length < 5)
            {
                output.Message = "密码不能小于5个字符！";
                return output;
            }
            switch (input.Type)
            {
                case RegisterType.Email:
                    //valid Email
                    if (!Utils.IsValidEmail(input.UserName))
                    {
                        output.Message = "邮件格式不正确！";
                        return output;
                    }
                    break;
                case RegisterType.Mobile:
                    if (!Utils.IsMobile(input.UserName))
                    {
                        output.Message = "手机号码不正确！";
                        return output;
                    }
                    break;
                case RegisterType.QQ:
                    break;
                case RegisterType.WeiXin:
                    break;
                default:
                    break;
            }

            //验证用户名
            if (IsExistUser(input.OrgId, input.UserName))
            {
                output.Message = "该用户名已经存在，请选择其他用户名！";
                return output;
            }
            User user = new User();
            user.UserName = input.UserName;
            user.Salt = Utils.GetRandomChar(10);
            user.Password = Utils.MD5(user.Salt + input.Password);
            user.OrgId = input.OrgId;
            user.RType = input.Type;
            if (!string.IsNullOrEmpty(input.RoleId))
            {
                user.RoleId = input.RoleId;
            }
            else
            {
                //注册用户
                var role = _roleRepository.Single(a => a.OrgId == input.OrgId && a.Name == "注册用户");
                if (role != null)
                {
                    user.RoleId = role.Id;
                }
            }
            user.ActiveCode = Guid.NewGuid().ToString("N");
            var result = _userRepository.AddUser(user);


            AddUserLog(user.OrgId, PublicModuleNames.UserModule, OperationType.Add, input.Url);


            output.LoginAccount = result;
            if (!string.IsNullOrEmpty(user.RoleId))
            {
                output.LoginRole = _roleRepository.GetById(user.RoleId);
            }
            try
            {
                switch (input.Type)
                {
                    case RegisterType.Email:
                        user.Email = input.UserName;
                        _userRepository.Update(user);
                        _emailService.SendActiveEmail(input.OrgId, result.Id);
                        break;
                    case RegisterType.Mobile:
                        
                        break;
                }
            }
            catch (Exception ex)
            {
                output.Message = ex.Message;
                return output;
            }

            return output;
        }

        #endregion
        #region 用户登录，注册
        public User GetUser(string userName, string password)
        {
            return _userRepository.GetUser(userName, password);
        }

        public User GetUserByCode(string id, string code)
        {
            return _userRepository.Single(a => a.Id == id && a.ActiveCode.ToLower() == code.ToLower());
        }

        public User GetById(string Id)
        {
            return _userRepository.GetById(Id);
        }
        /// <summary>
        /// 创建用户
        /// 创建用户实体，记录用户日志
        /// url TODO，需要移除url,改为用事件机制，
        /// 在注册了一个用户后需要做的事情，再这个里面做
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AddUser(User user, string url)
        {
            var result = _userRepository.AddUser(user);
            AddUserLog(user.OrgId, PublicModuleNames.UserModule, OperationType.Add, url);
            return result;
        }

        private void AddUserLog(string orgId, string moduleName, OperationType type, string url)
        {
            var userLog = new UserLog();
            userLog.OrgId = orgId;
            userLog.ModuleName = moduleName;
            userLog.OpeType = type;
            userLog.Location = url;
            userLog.SpendTime = 0;
            userLog.Decription = "";
            _userLogRepository.Add(userLog);
        }

        #endregion

        #region 用户管理

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SearchUserOutput SearchUser(SearchUserInput input)
        {
            SearchUserOutput output = new SearchUserOutput();
            var pager = new Pager(input.PageIndex, input.PageSize);
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            IEnumerable<User> items = null;
            var total = 0;
            if (!string.IsNullOrEmpty(input.Keywords))
            {
                if (!string.IsNullOrEmpty(input.RoleId))
                {
                    items = _userRepository.Filter(out total, a => a.OrgId == input.OrgId && a.UserName.Contains(input.Keywords) && a.RoleId == input.RoleId, a => a.OrderByDescending(x => x.CreationTime), "", pager.CurrentPage, pager.ItemsPerPage);
                }
                else
                {
                    items = _userRepository.Filter(out total, a => a.OrgId == input.OrgId && a.UserName.Contains(input.Keywords), a => a.OrderByDescending(x => x.CreationTime), "", pager.CurrentPage, pager.ItemsPerPage);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(input.RoleId))
                {
                    items = _userRepository.Filter(out total, a => a.OrgId == input.OrgId && a.RoleId == input.RoleId, a => a.OrderByDescending(x => x.CreationTime), "", pager.CurrentPage, pager.ItemsPerPage);
                }
                else
                {
                    items = _userRepository.Filter(out total, a => a.OrgId == input.OrgId, a => a.OrderByDescending(x => x.CreationTime), "", pager.CurrentPage, pager.ItemsPerPage);
                }
            }
            output.Users = items.ToList();
            output.Total = total;
            return output;
        }

        public bool IsExistUser(string orgId, string userName)
        {
            return _userRepository.Count(a => a.OrgId == orgId && a.UserName == userName) > 0;
        }

        public bool IsExistUser(string orgId, string id, string userName)
        {
            return _userRepository.Count(a => a.OrgId == orgId && a.Id != id && a.UserName == userName) > 0;
        }


        public User EditUser(User user)
        {
            if (user != null)
            {
                _userRepository.Update(user);
                return user;
            }
            return null;
        }
        public void DeleteUser(string userId)
        {
            var user = _userRepository.Single(a => a.Id == userId);
            _userRepository.Delete(user);
        }

        #endregion

        #region 机构角色
        public Role GetRole(string id)
        {
            return _roleRepository.GetById(id);
        }

        public IList<Role> QueryRoles(string orgId)
        {
            return _roleRepository.Query(a => a.OrgId == orgId && a.IsSystem == false).ToList();
        }

        public Role AddRole(Role role)
        {
            role.IsSystem = false;
            _roleRepository.Add(role);
            return role;
        }

        public Role EditRole(Role role)
        {
            if (role != null)
            {
                role.IsSystem = false;
                _roleRepository.Update(role);
                return role;
            }
            return null;
        }
        public void DeleteRole(string roleId)
        {
            var role = _roleRepository.Single(a => a.Id == roleId);
            _roleRepository.Delete(role);
        }

        #endregion

    }
}
