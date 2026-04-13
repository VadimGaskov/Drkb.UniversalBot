using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Drkb.UniversalBot.Infrastructure.DI;

public static class SwaggerServiceCollectionExtention
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {

            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Drkb.UniversalBot", Version = "v1" });

            OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            };

            options.AddSecurityDefinition("jwt_auth", securityDefinition);

            OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            };

            OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
            {
                {securityScheme, new string[] { }},
            };

            options.AddSecurityRequirement(securityRequirements);
        });

        return services;
    }
}