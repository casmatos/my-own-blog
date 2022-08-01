using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Blog.Repository.EntityMapper
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(key => key.Id);

            builder.Property(prop => prop.PostId)
                    .IsRequired();

            builder.Property(prop => prop.Content)
                .HasMaxLength(120);

            builder.Property(prop => prop.Author)
                .HasMaxLength(30);

            builder.HasOne(nav => nav.Post)
                    .WithMany();
        }
    }
}
