using MercySocial.Presentation.Middlewares;

namespace MercySocial.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        ConfigureServices(services);
        
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        
        return services;
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<GlobalExceptionHandlerMiddleware>();
    }
}