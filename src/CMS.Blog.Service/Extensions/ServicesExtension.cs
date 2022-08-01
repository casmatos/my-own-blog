namespace CMS.Blog.Service.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServicesApplication(this IServiceCollection services)
        {
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IPostService, PostService>();

            return services;
        }
    }
}
