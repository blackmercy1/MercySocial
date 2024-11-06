using System.Text.Json.Serialization;
using MercySocial.Presentation.Middlewares;
using MercySocial.Presentation.Users.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
        ConfigureSwaggerGen(services);

        services
            .AddEndpointsApiExplorer()
            .AddProblemDetails();

        AddAuthentication(services, jwtSettings);
    }

    private static void AddAuthentication(IServiceCollection services, IConfigurationSection jwtSettings)
    {
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

    private static void ConfigureSwaggerGen(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() {Title = "Your API", Version = "v1"});

            c.AddSecurityDefinition("Bearer", new()
            {
                In = ParameterLocation.Header,
                Description = "Please enter 'Bearer' [space] and then your token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
            });

            c.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
    }

    private static void ConfigureServices(IServiceCollection services)
        => services.AddScoped<ErrorHandlerMiddleware>();

    private static void ConfigureAutoMapper(IServiceCollection services)
        => services.AddAutoMapper(typeof(UserMappingProfile));
}