using Net.Pf.Application.Filters.PageFilters;
using Net.Pf.Application.Filters;
using Net.Pf.Identity;

namespace Net.Pf.Configuration;


public static class ConfigurationPages
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(typeof(CounterAttribute));
            options.Filters.Add(typeof(CustomExceptionFilterAttribute));
        });


        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.ConfigureFilter(new ValidatorPageFilter());
            options.Conventions.ConfigureFilter(new CounterPageFilter());

            /*
            options.Conventions.AuthorizeFolder("/AdminPanel", UserClaims.Administrator.ToString());


            options.Conventions.AuthorizeFolder("/Infrastructure", UserClaims.Administrator.ToString());

            options.Conventions
            .AuthorizeFolder("/Bootstrap", UserClaims.Administrator.ToString())
            .AuthorizeFolder("/Bootstrap", UserClaims.Moderator.ToString())
            .AuthorizeFolder("/Bootstrap", UserClaims.SoftBootstrap.ToString())
            ;
            */



        })
        .AddMvcOptions(options =>
        {
            options.Filters.Add(typeof(CounterAttribute));
            options.Filters.Add(typeof(CustomExceptionFilterAttribute));
        });


    }




}
