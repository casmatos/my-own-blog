using AutoMapper;

namespace CMS.Blog.Service.Mapper
{
    public class ProfileComment : Profile
    {
        public ProfileComment()
        {
            CreateMap<Comment, CommentDTO>()
                .ReverseMap();
        }
    }
}
