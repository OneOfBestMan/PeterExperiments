using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NetCoreTest.Controllers;
using NetCoreTest.Models;
using NetCoreTest.Models.DataModel;
using NetCoreTest.Models.Reposity;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PostSqlMvcxUnitTest
{
    [Trait("主控制器方法测试", "HomeControllerTest")]
    public class ControllerTest
    {
        Mock<IUserReposity> _userReposity;
        Mock<IBlogReposity> _blogReposity;
        Mock<ILogger<BaseController>> _logger;
        HomeController _homeController;

        public ControllerTest()
        {
            _userReposity = new Mock<IUserReposity>();
            _blogReposity = new Mock<IBlogReposity>();
            _logger = new Mock<ILogger<BaseController>>();
            _homeController = new HomeController(_logger.Object, _userReposity.Object, _blogReposity.Object)
            {
                ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext()
            };
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));
            //在控制器中调用 HttpContext.SignInAsync方法时，需要调用IAuthenticationService
            //否则会出现错误：No service for type 'Microsoft.AspNetCore.Authentication.IAuthenticationService' has been registered.
            var serviceProviderMock = new Mock<IServiceProvider>();
            //serviceProviderMock.Setup(_ => _.GetService(typeof(IAuthenticationService)))
            //    .Returns(authServiceMock.Object);

            //在仓储中返回View(blogs)时，会调用Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionaryFactory
            //所以把这个放到serviceProviderMock中
            var tempDataDictionaryFactoryMock = new Mock<ITempDataDictionaryFactory>();
            serviceProviderMock.Setup(_=>_.GetService(typeof(ITempDataDictionaryFactory)))
                .Returns(tempDataDictionaryFactoryMock.Object);

            //设置HttpContext，否则在控制器中调用 HttpContext.SignInAsync方法时，HttpContext为null
            _homeController.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                RequestServices = serviceProviderMock.Object
            };
        }

        /// <summary>
        /// 测试登陆
        /// </summary>
        [Fact]
        public void LoginTest()
        {
            _userReposity.Setup(a => a.GetUser("a", "a")).Returns(new User() { Id = 1, UserName = "peter", Password = "peter", NickName = "刘", CreationTime = DateTime.Now, RoleId = 1 });
            var result = _homeController.Login("a", "a", null);
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
        }

        /// <summary>
        /// 测试用户名密码错误
        /// </summary>
        [Fact]
        public void LoginNullUser()
        {
            _userReposity.Setup(a => a.GetUser("a", "a")).Returns(value: null);
            var result = _homeController.Login("a", "a", null);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        /// <summary>
        /// 测试获取博客列表
        /// </summary>
        [Fact]
        public void BlogsTest()
        {
            var blogList = new List<Blog>();
            blogList.Add(new Blog() { Id = 1, Title = "美丽", Content = "美丽农村", CreationTime = DateTime.Now });
            blogList.Add(new Blog() { Id = 2, Title = "人体", Content = "美丽人体", CreationTime = DateTime.Now });
            _blogReposity.Setup(_ => _.Query(It.IsAny<Expression<Func<Blog, bool>>>(), It.IsAny<Pager>())).Returns(blogList);
            var viewResult = _homeController.Blogs(1);
            Assert.IsType<ViewResult>(viewResult);
            Assert.NotNull(viewResult);
        }
    }
}
