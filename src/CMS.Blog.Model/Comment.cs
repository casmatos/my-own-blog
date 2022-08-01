namespace CMS.Blog.Model.Database
{
    public class Comment : EntityBase
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}