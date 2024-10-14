using System.Text.Json.Serialization;
using AutoMapper;
using MercySocial.Presentation.Middlewares;
using MercySocial.Presentation.Users.Mapping;

namespace MercySocial.Presentation;

public static class DependencyInjection
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; });
        
        ConfigureServices(services);
        ConfigureAutoMapper(services);
        
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddProblemDetails();
    }
    
    private static void ConfigureServices(IServiceCollection services) => services.AddScoped<ErrorHandlerMiddleware>();

    private static void ConfigureAutoMapper(IServiceCollection services) => services.AddAutoMapper(
        typeof(UserMappingProfile));
}