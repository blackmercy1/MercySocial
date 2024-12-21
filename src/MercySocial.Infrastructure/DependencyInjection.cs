using MercySocial.Application.Messages.Repository;
using MercySocial.Application.Users.Repository;
using MercySocial.Infrastructure.Data;
using MercySocial.Infrastructure.Messages.Repository;
using MercySocial.Infrastructure.Users.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MercySocial.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options 
            => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        
        return services;
    }
}