using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application;
using Server.Infrastructure.Repositories;
using Server.Infrastructure.Services;
using Server.Application.Services;

namespace Server.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //Category
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #region Configuration

        #endregion
        // Database Sql
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
