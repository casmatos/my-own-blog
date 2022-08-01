namespace CMS.Blog.Repository.CommentData
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        Task<ICollection<Comment>> GetCommentsByPostId(Guid postId);
    }
}