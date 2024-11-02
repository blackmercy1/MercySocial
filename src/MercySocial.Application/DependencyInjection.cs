using MediatR;
using MercySocial.Application.Common.Authentication.PasswordHasher;
using MercySocial.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MercySocial.Application.Common.Authentication;
using System.Reflection;

namespace MercySocial.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<IPasswordHasherService, PasswordHasherService>()
            .AddScoped<IAuthenticationService, AuthenticationService>();

        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        services
            .AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
        
        services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}