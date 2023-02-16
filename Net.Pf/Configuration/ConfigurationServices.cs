using FluentValidation.AspNetCore;

namespace Net.Pf.Configuration;

public static class ConfigurationServices
{
    public static void AddServices(this IServiceCollection services)
    {


        services.AddLogging();
        services.AddCors();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(6);
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.HttpOnly = true;
            //options.Cookie.IsEssential = true;
            //options.Cookie.Name = ".HelpDesk.Session";
        });



        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        services.AddMediatR(Assembly.GetExecutingAssembly());


        services.AddSwaggerGen();










    }


}
