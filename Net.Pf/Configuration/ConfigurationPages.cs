using Net.Pf.Filters.PageFilters;
using Net.Pf.Identity;
using Net.Pf.Filters;

namespace Net.Pf.Configuration;


public static class ConfigurationPages
{
    public static void AddPagesAndControllers(this IServiceCollection services)
    {


        services.AddControllersWithViews(options =>
        {
            options.Filters.Add(typeof(CounterAttribute));
            options.Filters.Add(typeof(CustomExceptionFilterAttribute));
        });


        services.AddRazorPages(options =>
        {
            //options.Conventions.ConfigureFilter(new ValidatorPageFilter());
            options.Conventions.ConfigureFilter(new CounterPageFilter());


            options.Conventions.AuthorizeFolder("/AdminPanel", UserClaims.Administrator.ToString());
            options.Conventions.AuthorizeFolder("/Infrastructure", UserClaims.Administrator.ToString());

            options.Conventions
            .AuthorizeFolder("/Bootstrap", UserClaims.Administrator.ToString())
            .AuthorizeFolder("/Bootstrap", UserClaims.Moderator.ToString())
            .AuthorizeFolder("/Bootstrap", UserClaims.SoftBootstrap.ToString())
            ;




        })
        .AddMvcOptions(options =>
        {
            options.Filters.Add(typeof(CounterAttribute));
            options.Filters.Add(typeof(CustomExceptionFilterAttribute));
        });


    }




}
