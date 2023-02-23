using Api.Authorization;

WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder().Build()
    .ConfigureApp().Run()
    ;

public static class WebApplicationBuilderConfiguration
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ApiKeyAuthMiddlewareIMiddleware>();
        builder.Services.AddControllers();
        //builder.Services.AddControllers(options => options.Filters.Add<ApiKeyAuthFilter>());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGenConfigureApiKeyAuth();

        return builder;
    }
}

public static class WebApplicationConfiguration
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseMiddleware<ApiKeyAuthMiddlewareIMiddleware>();
        //app.UseMiddleware<ApiKeyMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }
}



