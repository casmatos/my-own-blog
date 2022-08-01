using System.ComponentModel.DataAnnotations;

namespace CMS.Blog.Service.DTOData
{
    public class CommentDTO : BaseDTO
    {
        [Required]
        public Guid PostId { get; set; }
        [MaxLength(120)]
        public string Content { get; set; }
        [MaxLength(30)]
        public string Author { get; set; }

    }
}
