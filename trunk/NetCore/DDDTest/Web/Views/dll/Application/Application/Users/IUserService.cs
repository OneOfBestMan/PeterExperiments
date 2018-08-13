using System.Collections.Generic;
using Model.Users;

namespace Application.Users
{
    public interface IUserService
    {
        Role AddRole(Role role);
        User AddUser(User user, string url);
        void DeleteRole(string roleId);
        Role EditRole(Role role);
        User GetById(string Id);
        Role GetRole(string id);
        User GetUser(string userName, string password);
        IList<Role> QueryRoles(string orgId);
        User GetUserByCode(string id, string code);
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        SearchUserOutput SearchUser(SearchUserInput input);

        bool IsExistUser(string orgId, string userName);

        bool IsExistUser(string orgId, string id, string userName);

        User EditUser(User user);

        void DeleteUser(string userId);

        /// <summary>
        ///  登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns>是否登录成功</returns>
        UserLoginOutput UserLogin(string userName, string passWord);

        UserLoginOutput UserRegister(UserRegisterInput input);
    }
}