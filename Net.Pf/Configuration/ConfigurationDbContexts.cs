using Net.Pf.Identity;
using Net.Pf.Infrastructure.DataBases.Forum;

namespace Net.Pf.Configuration;


public static class ConfigurationDbContexts
{
    public static void AddDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<ForumDbContext>(options =>
        {
            //options.UseSqlServer(connectionString)
            //options.UseInMemoryDatabase("IdentityDb");
            options.UseSqlite($"Data Source=Infrastructure/DataBases/Forum/ForumDbContext.db");
        });









    }


}

