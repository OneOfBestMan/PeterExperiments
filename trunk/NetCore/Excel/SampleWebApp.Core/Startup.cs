using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Peter.ExcelOperation;

namespace SampleWebApp.Core
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddScoped<IImportExcel, ImportExcelDefault>();

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