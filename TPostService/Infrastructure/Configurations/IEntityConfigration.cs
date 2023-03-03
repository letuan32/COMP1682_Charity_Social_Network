using Microsoft.EntityFrameworkCore;

namespace T_PostService.Infrastructure.Configurations;

public interface IEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    
}