using CMS.Blog.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CMS.Blog.WebApi.Extensions
{
    public static class DependencyInjectionWebApiExtension
    {
        public static IServiceCollection AddDependencyInjectionWebApi(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", 
                    new OpenApiInfo
                    {
                        Title = "CMS My Own Blog",
                        Version = "v1",
                        Description = "Samples using WebAPI"
                    });
            });

            services.AddScoped<ExeptionMiddleware>();

            return services;
        }

        public static IApplicationBuilder UseDependencyInjectionWebApi(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS My Own Blog API");
                });
            }

            app.UseMiddleware<ExeptionMiddleware>();

            return app;
        }

    }
}
