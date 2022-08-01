using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace CMS.Blog.WebApi.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddDependencyInjectionApplication(this IServiceCollection services, IConfiguration config)
        {
            return services.AddDependencyInjectionWebApi(config)
                        .AddDependencyInjectionServices(config);
        }

        public static async Task<IApplicationBuilder> UseDependencyInjectionApplication(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDependencyInjectionWebApi(env);

            using var scope = app.ApplicationServices.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

            var _logger = logger.CreateLogger("InitializeSeedDatabase");

            _logger.LogInformation("== Start Migrations... ==");

            await app.ApplicationServices.InitializeMigrations();
            await app.ApplicationServices.InitializeSeedDatabase();

            _logger.LogInformation("== End Migrations... ==");

            return app;
        }
    }
}
