using Microsoft.EntityFrameworkCore;
using T_PostService.Infrastructure.Configurations;
using TPostService.Entities;
using TPostService.Infrastructure.Configurations;
using TPostService.Services;


namespace TPostService.Infrastructure;

public class PostDbContext : DbContext
{
    private readonly IUserService _userService;
    public PostDbContext(
        DbContextOptions options, IUserService userService) : base(options)
    {
        _userService = userService;
    }
    
    public  DbSet<PostEntity> PostEntities { get; set; }
    public  DbSet<CommentEntity> CommentEntities { get; set; }
    public  DbSet<ReactionEntity> ReactionEntities { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("post_service_db");
        modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReactionEntityTypeConfiguration());

    }

    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
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
                        entity.CreatedBy = _userService.GetUserId();
                        entity.UpdatedBy = _userService.GetUserId();
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.UpdatedDate = now;
                        entity.UpdatedBy = _userService.GetUserId();
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
        return base.SaveChangesAsync(cancellationToken);
    }
}