using Microsoft.EntityFrameworkCore;
using T_PostService.Infrastructure.Configurations;
using TPostService.Entities;


namespace TPostService.Infrastructure;

public class PostContext : DbContext
{
    private readonly string _username;
    public PostContext(
        DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User;
        // Get the username claim from the claims principal - if the user is not authenticated the claim will be null
        _username = claimsPrincipal?.Claims?.SingleOrDefault(c => c.Type == "username")?.Value ?? "Unauthenticated user";
    }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("post_service_db");
        modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
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
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.UpdatedDate = now;
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