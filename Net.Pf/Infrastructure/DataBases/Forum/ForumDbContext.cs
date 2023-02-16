using Net.Pf.Identity;

namespace Net.Pf.Infrastructure.DataBases.Forum;

public class ForumDbContext : DbContext
{
    //public DbSet<AppIdentityUser> Users { get; set; }

    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }


    /*
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    */


    //ForumSections
}
