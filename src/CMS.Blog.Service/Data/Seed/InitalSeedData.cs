namespace CMS.Blog.Service.Data.Seed
{
    public class InitalSeedData
    {
        private readonly BlogContext _context;

        public InitalSeedData(BlogContext context)
        {
            this._context = context;
        }

        public async Task SeedData()
        {
            var listPosts = await SeedPosts();

            if (listPosts.Count > 0)
                await SeedComments(listPosts);
        }

        private async Task<List<Post>> SeedPosts()
        {
            List<Post> listPosts = new List<Post>();

            if (_context.Posts.Count() == 0)
            {
                listPosts = new List<Post>()
                {
                    new Post
                    {
                        Id = Guid.NewGuid(),
                        Content = "New Code",
                        Title = "First Code"
                    },
                    new Post
                    {
                        Id = Guid.NewGuid(),
                        Content = "New Code to another company",
                        Title = "Second Code"
                    }
                };

                _context.Posts.AddRange(listPosts);

                await _context.SaveChangesAsync();
            }

            return listPosts;
        }

        private async Task SeedComments(List<Post> listPosts)
        {
            if (_context.Comments.Count() == 0)
            {
                _context.Comments.AddRange(
                    new Comment
                    {
                        Id = Guid.NewGuid(),
                        Author = "Camilo Matos",
                        Content = "Content from first Comment",
                        PostId = listPosts.FirstOrDefault().Id
                    },
                    new Comment
                    {
                        Id = Guid.NewGuid(),
                        Author = "Camilo The Big",
                        Content = "Content from Second Comment",
                        PostId = listPosts.LastOrDefault().Id
                    }
                );

                await _context.SaveChangesAsync();
            }
        }
    }
}
