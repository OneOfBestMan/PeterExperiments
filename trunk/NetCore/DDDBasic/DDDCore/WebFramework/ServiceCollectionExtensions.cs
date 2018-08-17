
using LC.SDK.Core.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace LC.SDK.Core.Framework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InjectBaseServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
          
            return services;
        }

        public static IServiceCollection RegisterGzip(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
                options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    // Custom
                    "image/svg+xml",
                    "font/woff2",
                    "application/font-woff",
                    "application/font-ttf",
                    "application/font-eot",
                    "image/jpeg",
                    "image/png"
                };
            });

            return services;
        }
    }
}