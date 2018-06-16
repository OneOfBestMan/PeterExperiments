using System;
using Microsoft.EntityFrameworkCore;
using NetCoreTest.Models;
using NetCoreTest.Models.DataModel;
using NetCoreTest.Models.Reposity;
using Xunit;

namespace PostSqlMvcxUnitTest
{
    public class UserReposityTest
    {
        BlogDbContext _dbContext;
        IUserReposity _userReposity;

        public UserReposityTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=Pass@word;Host=localhost;Port=5432;Database=TChaoCode;Pooling=true;");
            _dbContext = new BlogDbContext(optionsBuilder.Options);
            _userReposity = new UserReposity(_dbContext);
        }

        [Fact]
        public void LoginTest()
        {
            var user = _userReposity.GetUser("peter", "peter");
            Assert.NotNull(user);
        }

        [Fact]
        public void AddUserTest()
        {
            var result = _userReposity.AddUser(new User() { Id = 3, UserName = "pan", Password = "pan", NickName = "feng", CreationTime = DateTime.Now, RoleId = 1 });
            Assert.True(result);
        }

        [Fact]
        public void AddNullUserTest()
        {
            var result = Assert.Throws<Exception>(() => _userReposity.AddUser(null));
            Assert.Contains("user²ÎÊýÎª¿Õ", result.Message);
        }
    }
}
