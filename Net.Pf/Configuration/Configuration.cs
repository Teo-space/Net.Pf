namespace Net.Pf.Configuration;


public static class Configuration
{
    public static WebApplicationBuilder ConfigureAll(this WebApplicationBuilder builder)
    {
        ConfigurationIdentity.Configure(builder);


        ConfigurationPages.Configure(builder);

        return builder;
    }
}
