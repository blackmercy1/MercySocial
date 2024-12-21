using MercySocial.Presentation.Middlewares;

namespace MercySocial.Presentation.Configuration;

public static class MiddlewareConfiguration
{
    public static WebApplication Configure(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            ConfigureSwaggerGen(app);
        }
        else
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }
        
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}"
        );
        app.MapRazorPages();
        app.UseCors("AllowAll");

        return app;
    }

    private static void ConfigureSwaggerGen(
        WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            var url = "/swagger/v1/swagger.json";
            var appName = $"{app.Environment.ApplicationName} v1";

            c.SwaggerEndpoint(url, appName);
        });
    }
}