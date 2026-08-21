using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Stock.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(provider =>
        { 
            ILoggerFactory loggerFactory =
                provider.GetRequiredService<ILoggerFactory>();

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.AddMaps(typeof(ApplicationAssembly).Assembly);
            }, loggerFactory);

            return configuration.CreateMapper();
        });

        return services;
    }
}
