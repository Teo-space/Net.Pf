using Net.Pf.DataBases.Forum.Models;
using Net.Pf.Identity;

namespace Net.Pf.DataBases.Forum;

public class ForumDbContext : DbContext
{
    public DbSet<ForumFork> Forks { get; set; }

    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }



}


