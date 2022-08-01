namespace CMS.Blog.Service.Mapper
{
    public class ProfilePost : Profile
    {
        public ProfilePost()
        {
            CreateMap<Post, PostDTO>()
                .ReverseMap();
        }
    }
}
