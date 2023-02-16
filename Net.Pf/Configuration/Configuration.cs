namespace Net.Pf.Configuration;


public static class Configuration
{
    public static WebApplicationBuilder ConfigureAll(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity();
        builder.Services.AddDbContexts();




        ConfigurationServices.Configure(builder);


        ConfigurationPages.Configure(builder);

        ConfigurationRateLimiter.Configure(builder);




        return builder;
    }
}
