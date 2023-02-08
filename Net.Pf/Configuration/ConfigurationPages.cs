using Net.Pf.Application.Filters.PageFilters;
using Net.Pf.Application.Filters;


namespace Net.Pf.Configuration;


public static class ConfigurationPages
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(typeof(CounterAttribute));
            //options.Filters.Add(typeof(CustomExceptionFilterAttribute));
        });

        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.ConfigureFilter(new ValidatorPageFilter());
            options.Conventions.ConfigureFilter(new CounterPageFilter());

            //options.Conventions.AuthorizeFolder("/");
            //options.Conventions.AuthorizeFolder("/SupportRequest");

            //AllowAnonymousToFolder
            //AllowAnonymousToAreaFolder
            //options.Conventions.AuthorizePage("/Contact");
            //options.Conventions.AuthorizeFolder("/Private");

            //options.AddPolicy("AccessRights", policy => policy.RequireClaim("AccessRights"));
            //options.Conventions.AuthorizeFolder("/SupportRequest");

            options.Conventions.AuthorizeFolder("/AdminPanel", "AccessRights");
        })
        .AddMvcOptions(options =>
        {
            options.Filters.Add(typeof(CounterAttribute));
            //options.Filters.Add(typeof(CustomExceptionFilterAttribute));
        });

    }

}
