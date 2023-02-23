using Microsoft.OpenApi.Models;

namespace Api.Authorization;

public static class ApiKeyAuthSwaggerConfiguration
{
    public static IServiceCollection AddSwaggerGenConfigureApiKeyAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var scheme = new OpenApiSecurityScheme
            {
                Name = "X-API-KEY",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKeyScheme",
                In = ParameterLocation.Header,
                Description = "ApiKey must appear in header"
            };
            options.AddSecurityDefinition("X-API-KEY", scheme);

            var schemeRef = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-API-KEY"
                },
                In = ParameterLocation.Header
            };

            var requirement = new OpenApiSecurityRequirement
            {
                { schemeRef, new List<string>()}
            };
            options.AddSecurityRequirement(requirement);


        });

        return services;
    }



}
