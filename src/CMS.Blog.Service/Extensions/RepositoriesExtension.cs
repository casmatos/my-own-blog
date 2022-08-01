namespace CMS.Blog.Service.Extensions
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositoriesApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
