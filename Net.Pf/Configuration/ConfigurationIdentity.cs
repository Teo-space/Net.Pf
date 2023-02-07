﻿using Microsoft.AspNetCore.Identity;
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
        });

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
        //.AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders()
        ;


        builder.Services.AddAuthentication(options =>
        {
            options.RequireAuthenticatedSignIn = true;
            //options.AddPolicy("Founders", policy => policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5"));
        });

        builder.Services.AddAuthorization(options =>
        {
            //options.AddPolicy("Founders", policy => policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5"));
            //options.AddPolicy("Testers", policy => policy.RequireClaim("Test", "Value"));
            //options.AddPolicy("Testers", policy => policy.RequireClaim("Test", "Value"));
            //options.AddPolicy("Access", policy => policy.RequireClaim("AccessRights", UserRoles.Administrator.ToString()));

            //options.AddPolicy("Access2", policy => policy.RequireClaim("Access2"));
            options.AddPolicy("AccessRights", policy => policy.RequireClaim("AccessRights"));

        });


    }



}


class Policies
{
    public const string TestPolicy = "TestPolicy";


}


