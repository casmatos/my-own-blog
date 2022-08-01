namespace CMS.Blog.Service.CommentData
{
    public interface ICommentService
    {
        Task<IReadOnlyCollection<CommentDTO>> GetComments();
        Task<CommentDTO> GetComment(Guid id);
        Task<CommentDTO> CreateComment(CommentDTO record);
        Task<CommentDTO> UpdateComment(Guid? id, CommentDTO record);
        Task<bool> DeleteComment(Guid id);
        Task<ICollection<Comment>> GetCommentsByPostId(Guid postId);
    }
}