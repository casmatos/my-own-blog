using System.Reflection;

namespace CMS.Blog.Service.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
