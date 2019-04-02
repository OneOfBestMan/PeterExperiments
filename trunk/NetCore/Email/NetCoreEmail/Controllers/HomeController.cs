using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using NetCoreEmail.Models;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dm.Model.V20151123;

namespace NetCoreEmail.Controllers
{
    public class HomeController : Controller
    {

        ///// <summary>
        ///// QQ邮件服务器
        ///// </summary>
        ///// <param name="email"></param>
        ///// <returns></returns>
        //public async Task<IActionResult> Index([FromServices]IFluentEmail email)
        //{
        //    var model = new
        //    {
        //        Name = "peter"
        //    };

        //    await email
        //        .SetFrom("1065114664@qq.com")
        //        .To("2389849664@qq.com")
        //        .Subject("test email subject")
        //        .UsingTemplate(@"hi @Model.Name this is a razor template @(5 + 5)!", model)
        //        .SendAsync();

        //    return View();
        //}



        /// <summary>
        ///Aliyun
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index([FromServices]IFluentEmail email)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "LTAIp6xGrmeOdHig", "dkVvKPhMk0ZQOuS86IaMbR4jUuqvCC");
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendMailRequest request = new SingleSendMailRequest();
            try
            {
                request.AccountName = "控制台创建的发信地址";
                request.FromAlias = "peter";
                request.AddressType = 1;
                request.TagName = "控制台创建的标签";
                request.ReplyToAddress = true;
                request.ToAddress = "目标地址";
                request.Subject = "邮件主题";
                request.HtmlBody = "邮件正文";
                SingleSendMailResponse httpResponse = client.GetAcsResponse(request);
            }
            catch (ServerException e)
            {
                View(e.StackTrace);
                
            }
            catch (ClientException e)
            {
                View(e.StackTrace);
            }

            return View();
        }

      

}
}
