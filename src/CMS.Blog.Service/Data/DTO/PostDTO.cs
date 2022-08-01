using System.ComponentModel.DataAnnotations;

namespace CMS.Blog.Service.DTOData
{
    public class PostDTO : BaseDTO
    {
        [MaxLength(30)]
        public string Title { get; set; }
        [MaxLength(1200)]
        public string Content { get; set; }

    }
}
