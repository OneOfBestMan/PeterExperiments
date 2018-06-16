using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreTest.Models;
using NetCoreTest.Models.DataModel;
using NetCoreTest.Models.Reposity;

namespace PostSqlMvcDemoUnitTest
{
    [TestClass]
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

        [TestMethod]
        public void LoginTest()
        {
            var user = _userReposity.GetUser("peter", "peter");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void AddUserTest()
        {
            var result = _userReposity.AddUser(new User() { Id = 2, UserName = "liu", Password = "liu", NickName = "oldliu", CreationTime = DateTime.Now, RoleId = 1 });
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddNullUserTest()
        {
            var result = _userReposity.AddUser(null);

        }
    }
}
