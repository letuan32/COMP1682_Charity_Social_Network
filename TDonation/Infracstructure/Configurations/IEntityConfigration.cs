using Microsoft.EntityFrameworkCore;

namespace TDonation.Infracstructure.Configurations;

public interface IEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    
}