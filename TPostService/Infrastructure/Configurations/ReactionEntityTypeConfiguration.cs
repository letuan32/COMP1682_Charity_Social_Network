using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPostService.Entities;

namespace TPostService.Infrastructure.Configurations;

public class ReactionEntityTypeConfiguration : BaseEntityTypeConfiguration<ReactionEntity>
{
    public override void Configure(EntityTypeBuilder<ReactionEntity> builder)
    {
        builder.ToTable("reaction");
        builder.HasKey(e => new {e.PostId, e.UserId});

        builder.Property(e => e.PostId)
            .HasColumnName("post_id");

        builder.Property(e => e.UserId)
            .HasColumnName("user_id");

        builder.HasOne<PostEntity>(c => c.PostEntity)
            .WithMany(p => p.ReactionEntities)
            .HasForeignKey(c => c.PostId);
    }

}