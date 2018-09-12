using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peter.ExcelOperation;

namespace SampleWebApp.Core
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddSingleton(IConfigurationRoot, Configure);
			//services.AddSingleton<IConfiguration>(Configuration);
			services.AddScoped<IImportExcel, ImportExcelDefault>();
			services.AddScoped<ICRUDObject, CRUDObjectDefault>();
			services.AddMvc();
			
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseMvcWithDefaultRoute();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			
		}
	}
}