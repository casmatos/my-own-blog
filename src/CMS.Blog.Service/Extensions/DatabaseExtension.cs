using CMS.Blog.Service.Data.Seed;
using Microsoft.Extensions.Logging;

namespace CMS.Blog.Service.Extensions
{
    public static class DatabaseExtension
    {
        const string SQL_CONNECTION = "Server={0};Database=MyOwnBlog;Trusted_Connection=True;MultipleActiveResultSets=true";

        public static IServiceCollection AddDatabaseApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BlogContext>((services, options) =>
            {
                var logger = services.GetRequiredService<ILogger<BlogContext>>();

                try
                {

                    if (config.GetSection("UseMemoryDatabase").Exists() &&
                        bool.Parse(config.GetSection("UseMemoryDatabase").Value))
                    {
                        options.UseInMemoryDatabase("InMemoryDb");
                    }
                    else
                    {
                        var sqlServerConnection = config.GetSection("SQL_SERVER_CONNECTION_STRING").Value;

                        string connectionString = default;

                        if (!string.IsNullOrEmpty(sqlServerConnection))
                        {
                            connectionString = sqlServerConnection;
                        }
                        else
                        {
                            var isContainer = config.GetSection("DOTNET_RUNNING_IN_CONTAINER").Value;

                            if (isContainer is null)
                            {
                                connectionString = string.Format(SQL_CONNECTION, ".");
                            }
                            else
                            {
                                logger.LogWarning("Container : true");
                                
                                throw new ArgumentOutOfRangeException("SQL Server - Connection String", "In container environment, you need define Connection String.");
                            }
                        }

                        options.UseSqlServer(connectionString);
                    }

                }
                catch (ArgumentOutOfRangeException exOut)
                {
                    logger.LogError(exOut.Message, exOut);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.LogError("============================== : " + ex.Message, ex);
                }
            });

            services.AddMemoryCache();

            return services;
        }

        public static async Task InitializeSeedDatabase(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();
            using var contextDatabase = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

            var _logger = logger.CreateLogger("InitializeSeedDatabase");

            InitalSeedData initalData = new InitalSeedData(contextDatabase);

            try
            {

                if (contextDatabase.Database.CanConnect())
                    await initalData.SeedData();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        public static async Task InitializeMigrations(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();
            using var contextDatabase = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

            var _logger = logger.CreateLogger("InitializeMigrations");


            if (config.GetSection("UseMemoryDatabase").Exists() &&
                !bool.Parse(config.GetSection("UseMemoryDatabase").Value))
            {
                try
                {
                    await contextDatabase.Database.EnsureCreatedAsync();
                    await contextDatabase.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }
            }
        }

    }
}
