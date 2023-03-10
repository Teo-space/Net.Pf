using Microsoft.AspNetCore.Identity;
using Net.Pf.Identity;

namespace Net.Pf.Configuration;

public static class ConfigurationIdentity
{
    //sad23rtfS!As23
    public static void AddIdentity(this IServiceCollection services)
    {
        // Add services to the container.
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection:IdentityDb")
        ///                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            //options.UseSqlServer(connectionString)
            //options.UseInMemoryDatabase("IdentityDb");
            options.UseSqlite($"Data Source=Identity/Data/IdentityDb.db");
        });

        services.AddDatabaseDeveloperPageExceptionFilter();

        services
        .Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        })
        //Тут должен соблюдатся Constraint
        //IdentityUser<Guid>, IdentityRole<Guid>
        //IdentityUser<Guid>
        .AddDefaultIdentity<AppIdentityUser>(options =>
        {

        })
        //IdentityRole<Guid>
        .AddRoles<AppIdentityRole>()
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders()
        ;


        services.AddAuthentication(options =>
        {
            options.RequireAuthenticatedSignIn = true;
        });

        services.AddAuthorization(options =>
        {
            foreach(var userClaim in UserClaimList.Get())
            {
                options.AddPolicy(userClaim, policy => policy.RequireClaim(userClaim, userClaim));
            }

        });


    }



}


