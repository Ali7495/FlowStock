using AutoMapper;
using BuildingBlocks.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Stock.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
                      {
                          cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly);
                          cfg.AddOpenBehavior(typeof(LoggingBehaviors<,>));
                          cfg.AddOpenBehavior(typeof(ValidationBehaviors<,>));
                      });

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
