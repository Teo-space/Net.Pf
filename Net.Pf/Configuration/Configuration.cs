namespace Net.Pf.Configuration;


public static class Configuration
{
    public static void ConfigureAll(this WebApplicationBuilder builder)
    {
        ConfigurationIdentity.Configure(builder);

    }
}
