namespace CMS.Blog.Repository.CommentData
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context) : base(context) { }

        public async Task<ICollection<Comment>> GetCommentsByPostId(Guid postId) =>
            await _context.Comments
                        .Where(c => c.PostId == postId)
                        .ToListAsync();
    }
}