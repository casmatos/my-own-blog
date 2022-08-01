using CMS.Blog.Service.Data.Seed;

namespace CMS.Blog.Service.Extensions
{
    public static class DatabaseExtension
    {
        const string SQL_CONNECTION = "Server=.;Database=MyOwnBlog;Trusted_Connection=True;MultipleActiveResultSets=true";

        public static IServiceCollection AddDatabaseApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BlogContext>(x =>
            {
                if (config.GetSection("UseMemoryDatabase").Exists() && bool.Parse(config.GetSection("UseMemoryDatabase").Value))
                    x.UseInMemoryDatabase("InMemoryDb");
                else
                    x.UseSqlServer(SQL_CONNECTION);
                
            });

            services.AddMemoryCache();

            return services;
        }

        public static async Task InitializeSeedDatabase(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();
            var contextDatabase = scope.ServiceProvider.GetRequiredService<BlogContext>();

            InitalSeedData initalData = new InitalSeedData(contextDatabase);

            if (contextDatabase.Database.CanConnect())
                await initalData.SeedData();
        }

        public static async Task InitializeMigrations(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();
            var contextDatabase = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            if (config.GetSection("UseMemoryDatabase").Exists() && 
                !bool.Parse(config.GetSection("UseMemoryDatabase").Value))
            {
                await contextDatabase.Database.EnsureCreatedAsync();
                await contextDatabase.Database.MigrateAsync();
            }
        }

    }
}
