using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application;

namespace Stock.Infrastructure;

public static class AuthorizationExtentions
{
    public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                Policies.ProductCategoryCreate,
                policy => policy.AddRequirements(new PermissionRequirement(Permissions.ProductCategoryCreate))
            );
        });

        return services;
    }
}
