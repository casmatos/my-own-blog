using System.Reflection;

namespace CMS.Blog.Service
{
    public static class StartupExtension
    {
        public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDatabaseApplication(config)
                    .AddRepositoriesApplication()
                    .AddServicesApplication();
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
