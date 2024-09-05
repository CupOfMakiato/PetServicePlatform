using Server.API.Services;
using Server.Application;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application.Services;
using Server.Infrastructure.Repositories;
using Server.Infrastructure;

namespace Server.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<IClaimsService, ClaimsService>();
        services.AddHttpContextAccessor();

        return services;
    }
}
