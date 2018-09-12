using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Asp.NetCoreWebApi.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Asp.Net_Core_WebApi.JWT.Controllers
{
	[Route("api/[controller]")]
	public class OAuthController : Controller
	{
		public IConfiguration Configuration;
		public OAuthController(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		/// <summary>
		/// <![CDATA[获取访问令牌]]>
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<OutputResult<AccessToken>> GetToken([FromBody] UserInfo user)
		{
			var result = new OutputResult<AccessToken>();
			try
			{
				if (string.IsNullOrEmpty(user.UserName)) throw new ArgumentNullException("UserName", "用户名不能为空！");
				if (string.IsNullOrEmpty(user.Password)) throw new ArgumentNullException("password", "密码不能为空！");

				//验证用户名和密码
				//var userInfo = await _UserService.CheckUserAndPassword(mobile: user, password: password);
				UserService userService = new UserService();
				var userInfo = userService.GetUser(user.UserName, user.Password);
				var claims = new Claim[]
				{
					new Claim(ClaimTypes.Name,user.UserName),
					new Claim(ClaimTypes.NameIdentifier,userInfo.Id.ToString()),
				};

				var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]));
				var expires = DateTime.Now.AddMinutes(1);//
				var token = new JwtSecurityToken(
							issuer: Configuration["issuer"],
							audience: Configuration["audience"],
							claims: claims,
							notBefore: DateTime.Now,
							expires: expires,
							signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

				//生成Token
				string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
				result.Code = "1";
				result.Data =new AccessToken() { Token=jwtToken} ;
				result.Message = "授权成功！";
				return result;
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
				result.Code = "0";
				return result;
			}
		}
	}
}