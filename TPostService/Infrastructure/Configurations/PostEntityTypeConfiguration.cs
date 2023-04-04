using APIGateway.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedModels.Enums;
using TPostService.Entities;

namespace TPostService.Infrastructure.Configurations;

public class PostEntityTypeConfiguration : BaseEntityTypeConfiguration<PostEntity>
{
    public override void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.ToTable("post");
        
        builder.Property(e => e.Content)
            .HasColumnName("content")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(e => e.MediaUrls)
            .HasColumnName("media_urls")
            .HasColumnType("text[]")
            .IsRequired(false);
        
        builder.Property(e => e.DocumentUrls)
            .HasColumnName("document_urls")
            .HasColumnType("text[]")
            .IsRequired(false);

        builder.Property(e => e.Views)
            .HasColumnName("views");

        builder.Property(e => e.Donations)
            .HasColumnName("donation");

        builder.Property(e => e.ExpectedAmount)
            .HasColumnName("expected_amount");
        
        builder.Property(e => e.ExpectedReceivedDate)
            .HasColumnName("expected_received_date")
            .HasColumnType("timestamp with time zone");
        
        builder.Property(e => e.Location)
            .HasColumnName("location")
            .HasMaxLength(255);

        builder.Property(e => e.PostCategoryEnum)
            .HasConversion(
                v => v.GetDescription(),
                v => EnumHelper.ParseEnumValue<PostCategoryEnum>(v)
            )
            .HasColumnName("category")
            .IsRequired();

        builder.Property(e => e.CurrencyEnum)
            .HasConversion(
                v => v.GetDescription(),
                v => EnumHelper.ParseEnumValue<CurrencyEnum>(v)
            )
            .HasColumnName("currency")
            .IsRequired();
        
        builder.Property(e => e.ApproveStatusEnum)
            .HasConversion(
                v => v.GetDescription(),
                v => EnumHelper.ParseEnumValue<PostApproveStatusEnum>(v)
            )
            .HasColumnName("approve_status")
            .IsRequired()
            .HasDefaultValue(PostApproveStatusEnum.Pending);
        
        builder.HasMany<CommentEntity>(c => c.CommentsEntities)
            .WithOne(p => p.PostEntity)
            .HasForeignKey(c => c.PostId);
    }

}