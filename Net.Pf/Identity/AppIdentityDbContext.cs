using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace Net.Pf.Identity;


public partial class AppIdentityDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, Guid>
{
    public DbSet<AppIdentityUser> Users { get; set; }

    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
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

}
