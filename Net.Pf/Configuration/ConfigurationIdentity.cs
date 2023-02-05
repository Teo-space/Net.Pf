using Microsoft.AspNetCore.Identity;
using Net.Pf.Identity;

namespace Net.Pf.Configuration;

public static class ConfigurationIdentity
{
    //sad23rtfS!As23
    public static void Configure(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection:IdentityDb")
        ///                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
        {
            //options.UseSqlServer(connectionString)
            //options.UseInMemoryDatabase("IdentityDb");
            options.UseSqlite($"Data Source=Identity/IdentityDb.db");
        })
            ;//IdentityDb.db

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services
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
        .AddDefaultIdentity<AppIdentityUser>(options =>
        {

        })
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        ;

        /*
        builder.Services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        })
         ;
        */

        builder.Services.AddAuthentication(options =>
        {
            options.RequireAuthenticatedSignIn = true;
        })
        ;
    }

}
