namespace Net.Pf.Configuration;


public static class Configuration
{
    public static WebApplicationBuilder ConfigureAll(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity();
        builder.Services.AddDbContexts();


        builder.Services.AddServices();
        builder.Services.AddPagesAndControllers();






        ConfigurationRateLimiter.Configure(builder);








        return builder;
    }
}
