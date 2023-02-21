using Microsoft.EntityFrameworkCore;

namespace TCharity.Post.Infrastructure.Configurations;

public interface IEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    
}