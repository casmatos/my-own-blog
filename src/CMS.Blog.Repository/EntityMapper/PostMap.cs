using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Blog.Repository.EntityMapper
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(key => key.Id);

            builder.Property(prop => prop.Title)
                .HasMaxLength(30);

            builder.Property(prop => prop.Content)
                .HasMaxLength(1200);

        }
    }
}
