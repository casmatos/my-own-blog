namespace CMS.Blog.Repository.PostData
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context) { }
    }
}