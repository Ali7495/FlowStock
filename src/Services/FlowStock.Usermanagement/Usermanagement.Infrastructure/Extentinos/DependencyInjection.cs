using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Usermanagement.Application;
using Usermanagement.Domain;
using Usermanagement.Infrastructure.Services.Security;

namespace Usermanagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastruction(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsermanagementDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("UsermanagementDb"));
        });

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddScoped<IJWTService,JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
