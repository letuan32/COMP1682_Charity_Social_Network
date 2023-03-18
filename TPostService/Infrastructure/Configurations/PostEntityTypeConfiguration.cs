using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPostService.Entities;
using TPostService.Infrastructure.Configurations;

namespace T_PostService.Infrastructure.Configurations;

public class PostEntityTypeConfiguration : BaseEntityTypeConfiguration<PostEntity>
{
    public override void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.ToTable("post");
        
        builder.Property(e => e.Content)
            .HasColumnName("content")
            .IsRequired();

        builder.Property(e => e.MediaUrls)
            .HasColumnName("media_urls")
            .IsRequired(false);

        
        builder.HasMany<CommentEntity>(c => c.CommentsEntities)
            .WithOne(p => p.PostEntity)
            .HasForeignKey(c => c.PostId);
    }

}