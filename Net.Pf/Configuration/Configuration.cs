namespace Net.Pf.Configuration;


public static class Configuration
{
    public static WebApplicationBuilder ConfigureAll(this WebApplicationBuilder builder)
    {
        ConfigurationIdentity.Configure(builder);



        ConfigurationServices.Configure(builder);


        ConfigurationPages.Configure(builder);

        ConfigurationRateLimiter.Configure(builder);


        return builder;
    }
}
