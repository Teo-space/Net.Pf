namespace Infrastructure.Forums.DbContexts;

internal class ForumDbContext : DbContext
{
    public DbSet<Infrastructure.Forums.Models.Forum> Forums { get; set; }
    public DbSet<Infrastructure.Forums.Models.Topic> Topics { get; set; }
    public DbSet<Infrastructure.Forums.Models.Post> Posts { get; set; }


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


