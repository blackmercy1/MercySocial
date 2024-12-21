using MercySocial.Presentation.Middlewares;
using MercySocial.Presentation.Users.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
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
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            });

        AddCors(services);
        AddServices(services);
        AddAutoMapper(services);
        AddSwaggerGen(services);

        services
            .AddEndpointsApiExplorer()
            .AddProblemDetails()
            .AddRazorPages();

        AddAuthentication(services, jwtSettings);
    }

    private static void AddCors(
        IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }

    private static void AddAuthentication(
        IServiceCollection services,
        IConfigurationSection jwtSettings)
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

                options.Events = new()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("jwt_token"))
                            context.Token = context.Request.Cookies["jwt_token"];
                        return Task.CompletedTask;
                    }
                };
            });
    }

    private static void AddSwaggerGen(
        IServiceCollection services)
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

    private static void AddServices(
        IServiceCollection services)
        => services.AddScoped<ErrorHandlerMiddleware>();

    private static void AddAutoMapper(
        IServiceCollection services)
        => services.AddAutoMapper(typeof(UserMappingProfile));
}