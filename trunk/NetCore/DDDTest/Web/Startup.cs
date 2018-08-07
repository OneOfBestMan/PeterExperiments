using System.Data;
using Data;
using Model.Config;
using Web.Framework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //部分系统配置
            services.Configure<SystemSetting>(Configuration.GetSection("SystemSetting"));

            //验证注放
            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.LoginPath = new PathString("/login");
                opt.Cookie.Path = "/";
                opt.AccessDeniedPath = "/NoPermission";
            });

            // 设置使用的数据库
            System.Action<DbContextOptionsBuilder> databaseOptions = options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            services.AddDbContextPool<CCDbContext>(databaseOptions);
            services.AddScoped<IDbConnection, Npgsql.NpgsqlConnection>();
            
            services.InjectApplicationServices();

            services.AddMvc(
               options =>
               {
                   options.Filters.Add<HttpGlobalExceptionFilter>();
                   //options.Filters.Add<SystemLogFilter>();
               }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.RegisterGzip();
            //转接头
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            //转接头
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseMvc(routes =>
            {


                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Index}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
