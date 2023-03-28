using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_PostService.Infrastructure.Configurations;
using TPostService.Entities;

namespace TPostService.Infrastructure.Configurations;

public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityConfiguration<TEntity> where TEntity : class, IBaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id).HasName("id");
        builder.Property(e => e.CreatedDate).HasColumnName("created_date").HasColumnType("timestamp with time zone");
        builder.Property(e => e.UpdatedDate).HasColumnName("updated_date").HasColumnType("timestamp with time zone");
        builder.Property(e => e.CreatedBy).HasColumnName("created_by");
        builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
    }

}