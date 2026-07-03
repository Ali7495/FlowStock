using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Usermanagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(cfg =>
              {
                  cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly);
              });

        services.AddValidatorsFromAssembly(typeof(ApplicationAssembly).Assembly);
        services.AddAutoMapper(cfg=> cfg.AddProfile<UserMappingProfile>());

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviors<,>));

        return services;

    }
}
