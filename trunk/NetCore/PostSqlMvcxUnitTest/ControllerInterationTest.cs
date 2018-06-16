using System;
using System.Linq;
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
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using NetCoreTest;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;

namespace PostSqlMvcxUnitTest
{
    [Trait("主控制器继承测试", "HomeControllerTest")]
    public class ControllerInterationTest
    {
        private TestServer _testServer;
        private HttpClient _httpClient;

        public ControllerInterationTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            _testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>().UseConfiguration(configuration));
            _httpClient = _testServer.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        [Fact]
        public void BlogTest()
        {
            var requestUrl = "/blogs";
            // var requestUrl = "http://localhost:39314/blogs";
            var response = _httpClient.GetAsync(requestUrl).Result;
            // response.EnsureSuccessStatusCode();
            // var content = response.Content.ReadAsAsync<ViewResult>().Result;
            //var model = content.Model;
            //Assert.IsType<IEnumerable<Blog>>(model);
            //Assert.Equal(3,((IEnumerable<Blog>)model).ToList().Count());
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.NotNull(result);



        }
        [Fact]
        public void LoginTest()
        {
            var requestUrl = "/login";
            // var requestUrl = "http://localhost:39314/login";
            var data = new Dictionary<string, string>();
            data.Add("userName", "peter");
            data.Add("password", "peter");
            data.Add("returnUrl", "");
            var content = new FormUrlEncodedContent(data);
            var response = _httpClient.PostAsync(requestUrl, content).Result;

            //response.EnsureSuccessStatusCode();

            //使用真实的地址测试可以返回html
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.NotNull(result);


        }


    }
}
