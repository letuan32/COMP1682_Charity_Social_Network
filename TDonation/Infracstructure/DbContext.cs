using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using TDonation.Entities;
using TDonation.Infracstructure.Configurations;
using TDonation.Services.Interfaces;

namespace TDonation.Infracstructure;

public class DonationDbContext : DbContext
{
    private readonly IUserService _userService;
    private readonly string _userId;

    public DonationDbContext(
        DbContextOptions options, IUserService userService) : base(options)
    {
        _userService = userService;
        _userId = _userService.GetUserId();;
    }
    
    public DbSet<DonationTransactionEntity> DonationTransactionEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DonationTransactionEntityConfiguration());
        modelBuilder.HasDefaultSchema("donation_service_db");
    }

    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var now = DateTime.UtcNow;

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is IBaseEntity entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = now;
                        entity.UpdatedDate = now;
                        entity.CreatedBy ??= _userId;
                        entity.UpdatedBy ??= _userId;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.UpdatedDate = now;
                        entity.UpdatedBy = _userId;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}