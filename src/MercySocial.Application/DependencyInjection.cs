using MercySocial.Application.Common.Users.Service;
using Microsoft.Extensions.DependencyInjection;

namespace MercySocial.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}