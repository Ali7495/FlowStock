using AutoMapper;
using BuildingBlocks.Application;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Usermanagement.Application;

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

        services.AddValidatorsFromAssembly(typeof(ApplicationAssembly).Assembly);

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

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviors<,>));

        return services;

    }
}
