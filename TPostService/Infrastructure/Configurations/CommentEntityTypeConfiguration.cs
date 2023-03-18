using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPostService.Entities;
using TPostService.Infrastructure.Configurations;

namespace T_PostService.Infrastructure.Configurations;

public class CommentEntityTypeConfiguration : BaseEntityTypeConfiguration<CommentEntity>
{
    public override void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable("comment");
        
        builder.Property(e => e.Content)
            .HasColumnName("content")
            .IsRequired();

        builder.Property(e => e.PostId)
            .HasColumnName("post_id");

        builder.HasOne<PostEntity>(c => c.PostEntity)
            .WithMany(p => p.CommentsEntities)
            .HasForeignKey(c => c.PostId);
    }

}