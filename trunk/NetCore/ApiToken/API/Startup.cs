﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace API
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
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAnyOrigin", policy =>
				{
					// App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
					policy
					.WithOrigins(Configuration["App:CorsOrigins"])
					.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials();
				});
			});


			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = "localhost",
						ValidAudience = "localhost",
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
					};
				});
			#region CORS
			//services.AddCors(c =>
			//{
			//    c.AddPolicy("AllowAnyOrigin", policy =>
			//    {
			//        policy.AllowAnyOrigin()//允许任何源
			//        .AllowAnyMethod()//允许任何方式
			//        .AllowAnyHeader() //允许任何头
			//        .AllowCredentials();//允许cookie
			//    });
			//});
			#endregion

			// Configure CORS for angular2 UI



			services.AddMvc();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = Configuration["APITitle"], Version = "v1" });
				c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
				c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
				{ "Bearer", Enumerable.Empty<string>() },
				});
			});
		}
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors("AllowAnyOrigin");
            app.UseMvc();
            app.UseStaticFiles();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });


        }

        
    }
}
