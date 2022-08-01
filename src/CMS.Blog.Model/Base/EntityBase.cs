namespace CMS.Blog.Model.Base
{
    public class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
