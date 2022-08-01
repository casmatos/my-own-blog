namespace CMS.Blog.Model.Base
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
        DateTime CreationDate { get; set; }
    }
}