﻿using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SampleWebApp.Core
{
	public class Program
	{


		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				//.UseKestrel(op => op.Limits.MaxRequestBodySize = null)
				//.UseHttpSys(op=>op.MaxRequestBodySize=1_000_000_000)
				.Build();
	}
}