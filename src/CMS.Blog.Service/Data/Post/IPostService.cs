namespace CMS.Blog.Service.PostData
{
    public interface IPostService
    {
        Task<PostDTO> GetPost(Guid id);
        Task<IReadOnlyCollection<PostDTO>> GetPosts();
        Task<PostDTO> CreatePost(PostDTO record);
        Task<PostDTO> UpdatePost(Guid? id, PostDTO record);
        Task<bool> DeletePost(Guid id);
    }
}