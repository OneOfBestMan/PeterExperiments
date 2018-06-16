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
    public class BlogReposityTest
    {

        BlogDbContext _dbContext;
        IBlogReposity _blogReposity;

        public BlogReposityTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=Pass@word;Host=localhost;Port=5432;Database=TChaoCode;Pooling=true;");
            _dbContext = new BlogDbContext(optionsBuilder.Options);
            _blogReposity = new BlogReposity(_dbContext);
        }

        [TestMethod]
        public void QueryTest()
        {
            var list = _blogReposity.Query(a=>a.Content.Contains("Blog"),new Pager(1));
            Assert.IsNotNull(list);
        }

         
    }
}
