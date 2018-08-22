using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreWebApi.JWT
{
	public class AccessToken
	{
		public string Token { get; set; }
	}

	public class UserInfo
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}

	public class UserService
	{

		public UserInfo GetUser(string userName,string password)
		{
			var list = QueryUsers();
			var user = list.FirstOrDefault(a=>a.UserName==userName && a.Password==password);
			return user;
		}

		public List<UserInfo> QueryUsers()
		{
			var list = new List<UserInfo>();
			list.Add(new UserInfo() { UserName="liu",Password="123456",Id=1});
			list.Add(new UserInfo() { UserName = "peter", Password = "123456", Id = 2 });
			list.Add(new UserInfo() { UserName = "cao", Password = "123456", Id = 3 });
			list.Add(new UserInfo() { UserName = "zhang", Password = "123456", Id = 4 });
			return list;
		}
	}

	public class OutputResult<T> where T:class
	{
		public string Code { get; set; }
		public string Message { get; set; }
		public T Data { get; set; } 
		 
	}
}
