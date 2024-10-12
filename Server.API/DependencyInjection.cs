using Server.API.Services;
using Server.Application;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application.Services;
using Server.Contracts.Enum;
using Server.Infrastructure;
using Server.Infrastructure.Repositories;

namespace Server.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                options.JsonSerializerOptions.Converters.Add(
                    new System.Text.Json.Serialization.JsonStringEnumConverter()); // Add this line to handle enums as strings in JSON
            });
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<IClaimsService, ClaimsService>();
        services.AddHttpContextAccessor();
        services.AddControllersWithViews();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Pet Service Platform Web App API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Please enter into field the word 'Bearer' followed by space and JWT",
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });


            c.MapType<CardProviderEnum>(() => new Microsoft.OpenApi.Models.OpenApiSchema
            {
                Type = "string",
                Enum = new List<Microsoft.OpenApi.Any.IOpenApiAny>
                {
                    new Microsoft.OpenApi.Any.OpenApiString(CardProviderEnum.Visa.ToString()),
                    new Microsoft.OpenApi.Any.OpenApiString(CardProviderEnum.MasterCard.ToString()),
                    new Microsoft.OpenApi.Any.OpenApiString(CardProviderEnum.AmericanExpress.ToString()),
                    new Microsoft.OpenApi.Any.OpenApiString(CardProviderEnum.Discover.ToString())
                }
            });
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserPolicy", policy =>
                policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "User"));
            options.AddPolicy("AdminPolicy", policy =>
                policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "Admin"));
            options.AddPolicy("ShopPolicy", policy =>
                policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "Shop"));
        });

        return services;
    }
}
