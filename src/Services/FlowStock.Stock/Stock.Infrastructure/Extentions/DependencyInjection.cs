using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application;
using Stock.Domain;

namespace Stock.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StockDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("StockDb"));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddJwtAuthentication(configuration);
        services.AddApplicationAuthorization();

        return services;
    }
}
