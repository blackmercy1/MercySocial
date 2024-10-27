using System.Text.Json.Serialization;
using MercySocial.Presentation.Middlewares;
using MercySocial.Presentation.Users.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MercySocial.Presentation;

public static class DependencyInjection
{
    public static void AddPresentation(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        
        services
            .AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; });

        ConfigureServices(services);
        ConfigureAutoMapper(services);

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddProblemDetails();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
    }

    private static void ConfigureServices(IServiceCollection services) => services.AddScoped<ErrorHandlerMiddleware>();

    private static void ConfigureAutoMapper(IServiceCollection services) => services.AddAutoMapper(
        typeof(UserMappingProfile));
}