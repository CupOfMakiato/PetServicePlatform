using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Application;
using Server.Infrastructure.Repositories;
using Server.Infrastructure.Services;
using Server.Application.Services;
using Server.Infrastructure.Data;

namespace Server.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //UOW
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Service
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITemporaryStoreService, TemporaryStoreService>();
        services.AddScoped<IShopService, ShopService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<PasswordService>();
        services.AddScoped<RedisService>();
        services.AddScoped<OtpService>();
        services.AddScoped<EmailService>();
        services.AddScoped<ServiceService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddMemoryCache();

        // Repo
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();


        #region Configuration

        #endregion
        // Database Sql
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
