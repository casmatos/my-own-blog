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

        public static IApplicationBuilder UseDependencyInjectionApplication(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDependencyInjectionWebApi(env);

            Task taskMigrate = new Task(async() =>
            {
                await app.ApplicationServices.InitializeMigrations();
                await app.ApplicationServices.InitializeSeedDatabase();
            });

            taskMigrate.Start();

            return app;
        }
    }
}
