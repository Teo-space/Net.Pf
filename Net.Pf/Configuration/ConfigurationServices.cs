using FluentValidation.AspNetCore;

namespace Net.Pf.Configuration;

public static class ConfigurationServices
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging();
        builder.Services.AddCors();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(6);
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.HttpOnly = true;
            //options.Cookie.IsEssential = true;
            //options.Cookie.Name = ".HelpDesk.Session";
        });



        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


        builder.Services.AddSwaggerGen();


    }


}
